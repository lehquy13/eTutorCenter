﻿using ESCenter.Application.Contracts.Authentications;
using ESCenter.Application.Contracts.Commons;
using ESCenter.Domain.Aggregates.Users;
using FluentValidation;
using Mapster;
using Matt.SharedKernel.Application.Mediators.Commands;

namespace ESCenter.Application.Accounts.Commands.CreateUpdateBasicProfile;

public record UpdateBasicProfileCommand(
    UserProfileUpdateDto UserProfileUpdateDto
) : ICommandRequest<AuthenticationResult>;

public class UpdateBasicProfileCommandValidator : AbstractValidator<UpdateBasicProfileCommand>
{
    public UpdateBasicProfileCommandValidator()
    {
        RuleFor(x => x.UserProfileUpdateDto).NotNull();
        RuleFor(x => x.UserProfileUpdateDto).SetValidator(new UserProfileCreateUpdateDtoValidator());
    }
}

public class UserProfileUpdateDto : IAuditDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = "Male";
    public int BirthYear { get; set; } = 1960;

    public string Avatar { get; set; } =
        "https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";

    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime? LastModificationTime { get; set; }
    public DateTime CreationTime { get; set; }
}

public class UserProfileCreateUpdateDtoValidator : AbstractValidator<UserProfileUpdateDto>
{
    public UserProfileCreateUpdateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50)
            .WithMessage("First name must be between 2 and 50 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50)
            .WithMessage("Last name must be between 2 and 50 characters long.");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Gender must be a valid option.");

        RuleFor(x => x.BirthYear)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithMessage("Birth year must be between 1900 and the current year.");

        // Optional validation for Avatar URL (you can adjust based on your needs)
        RuleFor(x => x.Avatar)
            .NotEmpty()
            //.Uri()
            .WithMessage("Please enter a valid image URL.");

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("City must be between 1 and 50 characters long.");

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Country must be between 1 and 50 characters long.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must be less than 500 characters long.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Please enter a valid email address.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d+$") // Matches a sequence of digits
            .WithMessage("Phone number must only contain digits.");
    }
}

public class LearnerForCreateUpdateDtoMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserProfileUpdateDto, Customer>();

        config.NewConfig<Customer, UserProfileUpdateDto>()
            .Map(des => des.Gender, src => src.Gender)
            .Map(des => des.FirstName, src => src.FirstName)
            .Map(des => des.LastName, src => src.LastName)
            .Map(des => des.BirthYear, src => src.BirthYear)
            .Map(des => des.City, src => src.Address.City)
            .Map(des => des.Country, src => src.Address.Country)
            .Map(des => des.Description, src => src.Description)
            .Map(des => des.Email, src => src.Email)
            .Map(des => des.PhoneNumber, src => src.PhoneNumber)
            .IgnoreNonMapped(true);
    }
}