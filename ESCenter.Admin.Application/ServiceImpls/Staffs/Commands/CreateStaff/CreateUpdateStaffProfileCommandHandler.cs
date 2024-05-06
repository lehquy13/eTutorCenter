﻿using ESCenter.Domain.Aggregates.Users;
using ESCenter.Domain.Aggregates.Users.ValueObjects;
using ESCenter.Domain.DomainServices;
using ESCenter.Domain.DomainServices.Interfaces;
using ESCenter.Domain.Shared;
using ESCenter.Domain.Shared.Courses;
using MapsterMapper;
using Matt.ResultObject;
using Matt.SharedKernel.Application.Mediators;
using Matt.SharedKernel.Application.Mediators.Commands;
using Matt.SharedKernel.Domain.Interfaces;

namespace ESCenter.Admin.Application.ServiceImpls.Staffs.Commands.CreateStaff;

public class CreateUpdateStaffProfileCommandHandler(
    ICustomerRepository customerRepository,
    IStaffDomainService staffDomainService,
    IUserDomainService userDomainService,
    IUnitOfWork unitOfWork,
    IAppLogger<CreateUpdateStaffProfileCommandHandler> logger,
    IMapper mapper)
    : CommandHandlerBase<CreateUpdateStaffProfileCommand>(unitOfWork, logger)
{
    private const string DefaultAvatar =
        "https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";

    public override async Task<Result> Handle(CreateUpdateStaffProfileCommand command,
        CancellationToken cancellationToken)
    {
        var user = await customerRepository.GetAsync(
            CustomerId.Create(command.LearnerForCreateUpdateDto.Id), cancellationToken);

        // Check if the user existed
        if (user is not null)
        {
            // Update user
            mapper.Map(command.LearnerForCreateUpdateDto, user);

            if (await UnitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return Result.Fail(StaffAppServiceError.FailToUpdateStaffErrorWhileSavingChanges);
            }

            return Result.Success();
        }

        // Create new user
        var staff = await staffDomainService.CreateStaff(
            string.Empty,
            command.LearnerForCreateUpdateDto.FirstName,
            command.LearnerForCreateUpdateDto.LastName,
            command.LearnerForCreateUpdateDto.Gender.ToEnum<Gender>(),
            command.LearnerForCreateUpdateDto.BirthYear,
            Address.Create(
                command.LearnerForCreateUpdateDto.City,
                command.LearnerForCreateUpdateDto.Country),
            command.LearnerForCreateUpdateDto.Description,
            DefaultAvatar,
            command.LearnerForCreateUpdateDto.Email,
            command.LearnerForCreateUpdateDto.PhoneNumber);

        if (staff.IsFailure || await UnitOfWork.SaveChangesAsync(cancellationToken) <= 0)
        {
            return staff.IsFailure
                ? staff.Error
                : Result.Fail(StaffAppServiceError.FailToCreateStaffErrorWhileSavingChanges);
        }

        return Result.Success();
    }
}