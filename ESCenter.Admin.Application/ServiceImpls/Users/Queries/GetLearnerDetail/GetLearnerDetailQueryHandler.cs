﻿using ESCenter.Admin.Application.Contracts.Users.Learners;
using ESCenter.Domain.Aggregates.Users;
using ESCenter.Domain.Aggregates.Users.ValueObjects;
using MapsterMapper;
using Matt.ResultObject;
using Matt.SharedKernel.Application.Contracts.Interfaces;
using Matt.SharedKernel.Application.Mediators.Queries;
using Matt.SharedKernel.Domain.Interfaces;

namespace ESCenter.Admin.Application.ServiceImpls.Users.Queries.GetLearnerDetail;

public class GetLearnerDetailQueryHandler(
    IUnitOfWork unitOfWork,
    IAppLogger<GetLearnerDetailQueryHandler> logger,
    IMapper mapper,
    IUserRepository userRepository,
    IAsyncQueryableExecutor asyncQueryableExecutor)
    : QueryHandlerBase<GetLearnerDetail, LearnerForCreateUpdateDto>(unitOfWork, logger, mapper)
{
    private readonly IMapper _mapper = mapper;

    public override async Task<Result<LearnerForCreateUpdateDto>> Handle(GetLearnerDetail request,
        CancellationToken cancellationToken)
    {
        var identityId = IdentityGuid.Create(request.Id);
        var learnerFromDb =
            from user in userRepository.GetAll()
            where user.Id == identityId
            select user;

        var resultFromDb = await asyncQueryableExecutor.FirstOrDefaultAsync(learnerFromDb, false,
            cancellationToken: cancellationToken);

        if (resultFromDb is null)
        {
            return Result.Fail(UserAppServiceError.NonExistUserError);
        }

        // TODO: this mapper may cause some problems
        var userForDetailDto = _mapper.Map<LearnerForCreateUpdateDto>(resultFromDb);
        return userForDetailDto;
    }
}