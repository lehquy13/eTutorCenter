﻿using ESCenter.Domain.Aggregates.Subjects;
using ESCenter.Domain.Aggregates.Subjects.ValueObjects;
using ESCenter.Domain.Aggregates.TutorRequests;
using ESCenter.Domain.Aggregates.Tutors;
using ESCenter.Domain.Aggregates.Tutors.Entities;
using ESCenter.Domain.Aggregates.Tutors.Errors;
using ESCenter.Domain.Aggregates.Tutors.ValueObjects;
using ESCenter.Domain.Aggregates.Users.ValueObjects;
using ESCenter.Domain.DomainServices.Interfaces;
using ESCenter.Domain.Shared.Courses;
using Matt.ResultObject;
using Matt.SharedKernel.Domain.Interfaces;

namespace ESCenter.Domain.DomainServices;

public class TutorDomainService(
    IAppLogger<TutorDomainService> logger,
    ITutorRepository tutorRepository,
    ITutorRequestRepository tutorRequestRepository,
    ISubjectRepository subjectRepository
) : DomainServiceBase(logger), ITutorDomainService
{
    private static readonly List<string> EmptyVerification = ["document.png"];

    public async Task<Result> CreateTutorWithEmptyVerificationAsync(
        CustomerId userId,
        AcademicLevel academicLevel,
        string university,
        IEnumerable<int> majors,
        bool isVerified)
    {
        var tutor = Tutor.Create(
            userId,
            academicLevel,
            university,
            EmptyVerification,
            isVerified
        );
        var subjects = await subjectRepository.GetListByIdsAsync(majors.Select(SubjectId.Create));

        // add new majors to tutor
        var tutorMajors =
            subjects
                .Select(x => TutorMajor.Create(tutor.Id, x.Id, x.Name))
                .ToList();

        tutor.UpdateAllMajor(tutorMajors);

        await tutorRepository.InsertAsync(tutor);

        return Result.Success();
    }

    public async Task<Result> UpdateTutorMajorsAsync(TutorId tutorId, IEnumerable<int> majorIds)
    {
        var tutor = await tutorRepository.GetAsync(tutorId);

        if (tutor is null)
        {
            return Result.Fail(TutorDomainError.TutorNotFound);
        }

        var subjectIds = majorIds.Select(SubjectId.Create).ToList();
        var subjects = await subjectRepository.GetListByIdsAsync(subjectIds);

        var tutorMajors = subjects
            .Select(x => TutorMajor.Create(tutor.Id, x.Id, x.Name))
            .ToList();

        tutor.UpdateAllMajor(tutorMajors);

        return Result.Success();
    }

    public async Task<Result> RequestTutor(TutorId tutorId, CustomerId customerId, string message)
    {
        var tutor = await tutorRepository.GetAsync(tutorId);

        if (tutor is null)
        {
            return Result.Fail(TutorDomainError.TutorNotFound);
        }

        if (!tutor.IsVerified)
        {
            return Result.Fail(TutorDomainError.TutorUnavailable);
        }

        var tutorRequest = TutorRequest.Create(tutorId, customerId, message);
     
        await tutorRequestRepository.InsertAsync(tutorRequest);
        
        return Result.Success();
    }
}