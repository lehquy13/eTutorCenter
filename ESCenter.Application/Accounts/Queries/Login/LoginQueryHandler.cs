﻿using ESCenter.Application.Contracts.Authentications;
using ESCenter.Application.Interfaces.Authentications;
using ESCenter.Domain.Aggregates.Users;
using ESCenter.Domain.DomainServices.Interfaces;
using MapsterMapper;
using Matt.ResultObject;
using Matt.SharedKernel.Application.Mediators.Queries;
using Matt.SharedKernel.Domain.Interfaces;

namespace ESCenter.Application.Accounts.Queries.Login;

public class LoginQueryHandler(
    IUnitOfWork unitOfWork,
    IAppLogger<LoginQueryHandler> logger,
    IMapper mapper,
    IUserRepository userRepository,
    IIdentityDomainServices identityDomainServices,
    IJwtTokenGenerator jwtTokenGenerator
)
    : QueryHandlerBase<LoginQuery, AuthenticationResult>(unitOfWork, logger, mapper)
{
    public override async Task<Result<AuthenticationResult>> Handle(
        LoginQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var identityUserQ = await identityDomainServices.SignInAsync(request.Email, request.Password);

            if (identityUserQ is null)
            {
                return Result.Fail(AuthenticationErrorMessages.LoginFailError);
            }

            var user = await userRepository.GetAsync(identityUserQ.Id, cancellationToken);

            if (user is null)
            {
                return Result.Fail(AuthenticationErrorMessages.LoginFailError);
            }

            //3. Generate token
            var userLoginDto = new UserLoginDto()
            {
                Id = user.Id.Value,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                Role = user.Role.ToString(),
            };
            var loginToken = jwtTokenGenerator.GenerateToken(userLoginDto);

            return new AuthenticationResult()
            {
                User = userLoginDto,
                Token = loginToken,
            };
        }
        catch (Exception ex)
        {
            return Result.Fail($"AccountServiceError.FailToGetTutorProfileWithException {ex.Message}");
        }
    }
}