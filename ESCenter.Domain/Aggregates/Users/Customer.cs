﻿using ESCenter.Domain.Aggregates.Users.ValueObjects;
using ESCenter.Domain.Shared.Courses;
using Matt.SharedKernel.Domain.Interfaces;
using Matt.SharedKernel.Domain.Primitives.Auditing;

namespace ESCenter.Domain.Aggregates.Users;

public class Customer : FullAuditedAggregateRoot<CustomerId>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public Gender Gender { get; private set; } = Gender.Male;
    public int BirthYear { get; private set; } = 1990;
    public Address Address { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Avatar { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public UserRole Role { get; private set; } = UserRole.Learner;

    private const string DefaultAvatar =
        "https://res.cloudinary.com/dhehywasc/image/upload/v1686121404/default_avatar2_ws3vc5.png";

    private Customer()
    {
    }

    public static Customer Create(
        CustomerId identityUserId,
        string firstName,
        string lastName,
        Gender gender,
        int birthYear,
        Address address,
        string description,
        string? avatar,
        string email,
        string phoneNumber,
        UserRole role)
    {
        return new Customer
        {
            Id = identityUserId,
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            BirthYear = birthYear,
            Address = address,
            Description = description,
            Avatar = avatar ?? DefaultAvatar,
            Email = email,
            PhoneNumber = phoneNumber,
            Role = role
        };
    }

    public string GetFullName() => $"{FirstName} {LastName}";

    public void SetAvatar(string result)
    {
        Avatar = result;
    }

    public void RegisterAsTutor(List<int> majors, AcademicLevel academicLevel, string university,
        List<string> verificationInfoDtos)
    {
        Role = UserRole.Tutor;
        RaiseDomainEvent(new RegisteredAsTutorDomainEvent(Id, majors, academicLevel, university, verificationInfoDtos));
    }
}

public record RegisteredAsTutorDomainEvent(
    CustomerId CustomerId,
    List<int> Majors,
    AcademicLevel AcademicLevel,
    string University,
    List<string> VerificationInfoDtos) : IDomainEvent;