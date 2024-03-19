﻿using ESCenter.Client.Application.Contracts.Users.Tutors;
using ESCenter.Domain.Aggregates.Tutors;
using ESCenter.Domain.Aggregates.Tutors.ValueObjects;
using MapsterMapper;
using Matt.ResultObject;
using Matt.SharedKernel.Application.Mediators.Queries;
using Matt.SharedKernel.Domain.Interfaces;

namespace ESCenter.Client.Application.ServiceImpls.TutorProfiles.Queries.GetTutorProfile;

public class GetTutorProfileQueryHandler(
    ITutorRepository tutorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IAppLogger<GetTutorProfileQueryHandler> logger)
    : QueryHandlerBase<GetTutorProfileQuery, TutorMinimalBasicDto>(unitOfWork, logger, mapper)
{
    public override async Task<Result<TutorMinimalBasicDto>> Handle(GetTutorProfileQuery request,
        CancellationToken cancellationToken)
    {
        var tutor = await tutorRepository.GetAsync(TutorId.Create(request.TutorId), cancellationToken);

        if (tutor is null)
        {
            return Result.Fail(TutorProfileAppServiceError.NonExistTutorError);
        }

        return Mapper.Map<TutorMinimalBasicDto>(tutor);
    }
}