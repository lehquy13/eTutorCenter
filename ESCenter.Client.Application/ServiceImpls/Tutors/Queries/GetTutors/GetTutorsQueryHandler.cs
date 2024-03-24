﻿using ESCenter.Client.Application.Contracts.Users.Tutors;
using ESCenter.Domain.Aggregates.Courses;
using ESCenter.Domain.Aggregates.Discoveries;
using ESCenter.Domain.Aggregates.DiscoveryUsers;
using ESCenter.Domain.Aggregates.DiscoveryUsers.ValueObjects;
using ESCenter.Domain.Aggregates.Tutors;
using ESCenter.Domain.Aggregates.Users;
using ESCenter.Domain.Aggregates.Users.ValueObjects;
using ESCenter.Domain.Shared;
using ESCenter.Domain.Shared.Courses;
using Mapster;
using MapsterMapper;
using Matt.Paginated;
using Matt.ResultObject;
using Matt.SharedKernel.Application.Contracts.Interfaces;
using Matt.SharedKernel.Application.Contracts.Interfaces.Infrastructures;
using Matt.SharedKernel.Application.Mediators;
using Matt.SharedKernel.Application.Mediators.Queries;
using Matt.SharedKernel.Domain.Interfaces;
using Matt.SharedKernel.Domain.Interfaces.Repositories;

namespace ESCenter.Client.Application.ServiceImpls.Tutors.Queries.GetTutors;

public class GetTutorsQueryHandler(
    ICurrentUserService currentUserService,
    ITutorRepository tutorRepository,
    ICustomerRepository customerRepository,
    IDiscoveryRepository discoveryRepository,
    ICourseRepository courseRepository,
    IAsyncQueryableExecutor asyncQueryableExecutor,
    IAppLogger<RequestHandlerBase> logger,
    IMapper mapper
)
    : QueryHandlerBase<GetTutorsQuery, PaginatedList<TutorListForClientPageDto>>(logger, mapper)
{
    public override async Task<Result<PaginatedList<TutorListForClientPageDto>>> Handle(GetTutorsQuery request,
        CancellationToken cancellationToken)
    {
        var tutors =
            from tutor in tutorRepository.GetAll()
            join user in customerRepository.GetAll() on tutor.CustomerId equals user.Id
            join course in courseRepository.GetAll() on tutor.Id equals course.TutorId into
                groupCourse
            where tutor.IsVerified == true
            select new
            {
                Tutor = tutor,
                tutor.TutorMajors,
                User = user,
                Courses = groupCourse
            };

        if (request.TutorParams.Academic?.ToEnum<AcademicLevel>()
                is { } ac && ac != AcademicLevel.Optional)
            tutors = tutors.Where(record => record.User != null && record.Tutor.AcademicLevel == ac);

        if (!string.IsNullOrEmpty(request.TutorParams.Address))
            tutors = tutors.Where(record => record.User.Address.Match(request.TutorParams.Address));

        if (request.TutorParams.Gender is { } g && g != GenderEnum.None)
            tutors = tutors.Where(record => record.User.Gender == g.ToEnum<Gender>());

        if (request.TutorParams.BirthYear != 0)
            tutors = tutors.Where(record => record.User.BirthYear == request.TutorParams.BirthYear);

        if (!string.IsNullOrEmpty(request.TutorParams.SubjectName))
            tutors = tutors.Where(record =>
                record.Tutor.TutorMajors.Any(sub =>
                    sub.SubjectName.ToLower().Contains(request.TutorParams.SubjectName.ToLower()))
            );

        tutors = tutors
            .OrderByDescending(record => record.Courses.Count())
            .ThenByDescending(record => record.Tutor.Rate);

        var totalCount = await asyncQueryableExecutor.LongCountAsync(tutors, cancellationToken);

        if (currentUserService.IsAuthenticated)
        {
            var userGuid = CustomerId.Create(currentUserService.UserId);
            var discoveryQueryable = await discoveryRepository.GetUserDiscoverySubjects(userGuid, cancellationToken);

            // Order by the number of subjects that the user has discovered
            tutors = tutors.OrderByDescending(
                record => record.TutorMajors.Join(
                    discoveryQueryable,
                    tutorMajor => tutorMajor.SubjectId,
                    discovery => discovery,
                    (tutor, discovery) => tutor).Count()
            );

            var learntSubjectQueryable =
                from userForSearch in customerRepository.GetAll()
                join courseForSearch in courseRepository.GetAll() on userForSearch.Id equals courseForSearch
                    .LearnerId
                where userForSearch.Id == userGuid
                select new
                {
                    LearntSubject = courseForSearch.SubjectId.Value
                };

            var learntSubjects = await asyncQueryableExecutor
                .ToListAsync(learntSubjectQueryable, false, cancellationToken);

            tutors = tutors.OrderByDescending(
                record => record.TutorMajors.Join(
                    learntSubjects,
                    subjectId => subjectId.SubjectId.Value,
                    discovery => discovery.LearntSubject,
                    (tutor, discovery) => tutor).Count()
            );
        }

        var tutorFromDb = await asyncQueryableExecutor
            .ToListAsync(tutors
                    .Skip((request.TutorParams.PageIndex - 1) * request.TutorParams.PageSize)
                    .Take(request.TutorParams.PageSize),
                false, cancellationToken);

        var mergeList = tutorFromDb.Select(
            x => (x.User, x.Tutor).Adapt<TutorListForClientPageDto>()
        );

        var result = PaginatedList<TutorListForClientPageDto>
            .Create(
                mergeList,
                (int)totalCount,
                request.TutorParams.PageIndex,
                request.TutorParams.PageSize);

        return result;
    }
}