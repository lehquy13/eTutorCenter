﻿using ESCenter.Domain.Aggregates.Users;
using ESCenter.Domain.Aggregates.Users.ValueObjects;
using ESCenter.Domain.Shared.Courses;
using Matt.SharedKernel.Domain.Interfaces;

namespace ESCenter.Domain.DomainServices.Interfaces;

public interface IUserDomainService : IDomainService
{
    Task<User> CreateAsync(
        string firstName,
        string lastName,
        Gender gender,
        int birthYear,
        Address address,
        string description,
        string avatar,
        string email,
        string phoneNumber,
        UserRole role);
}