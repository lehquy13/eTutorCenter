﻿using ESCenter.Domain.Aggregates.Subjects;
using ESCenter.Domain.Aggregates.Tutors;
using ESCenter.Domain.Aggregates.Tutors.Entities;
using ESCenter.Domain.Aggregates.Tutors.ValueObjects;
using ESCenter.Domain.Aggregates.Users;
using ESCenter.Domain.Aggregates.Users.Identities;
using ESCenter.Domain.Aggregates.Users.ValueObjects;
using ESCenter.Domain.DomainServices.Errors;
using ESCenter.Domain.DomainServices.Interfaces;
using ESCenter.Domain.Shared.Courses;
using ESCenter.Domain.Specifications.Identities;
using ESCenter.Domain.Specifications.Subjects;
using ESCenter.Domain.Specifications.Users;
using MapsterMapper;
using Matt.ResultObject;
using Matt.SharedKernel.Domain;
using Matt.SharedKernel.Domain.Interfaces;
using Matt.SharedKernel.Domain.Interfaces.Repositories;

namespace ESCenter.Domain.DomainServices;

public class IdentityDomainServices(
    IAppLogger<IdentityDomainServices> logger,
    IIdentityRepository identityRepository,
    ISubjectRepository subjectRepository,
    ITutorRepository tutorRepository,
    IReadOnlyRepository<IdentityRole, IdentityRoleId> identityRoleRepository)
    : DomainServiceBase(logger), IIdentityDomainServices
{
    public async Task<IdentityUser?> SignInAsync(string email, string password)
    {
        await Task.CompletedTask;

        var identityUser = await identityRepository
            .FindByEmailAsync(email);

        if (identityUser is null || identityUser.ValidatePassword(password) is false) return null;

        return identityUser;
    }

    public async Task<Result<IdentityUser>> CreateAsync(
        string userName,
        string email,
        string password,
        string phoneNumber)
    {
        var roles = await identityRoleRepository
            .GetListAsync();

        if (roles.Count <= 0)
        {
            return Result.Fail(DomainServiceErrors.RoleNotFoundDomainError);
        }

        var identityUser = await identityRepository
            .CheckExistingAccount(email, userName);

        if (identityUser is not null)
        {
            return Result.Fail(DomainServiceErrors.UserAlreadyExistDomainError);
        }

        identityUser = IdentityUser.Create(
            userName,
            email,
            password,
            phoneNumber,
            roles.First().Id
        );

        await identityRepository.InsertAsync(identityUser);

        return identityUser;
    }

    public async Task<Result> ChangePasswordAsync(IdentityGuid identityId, string currentPassword, string newPassword)
    {
        var identityUser = await identityRepository.FindAsync(identityId);

        if (identityUser is null)
        {
            return Result.Fail(DomainServiceErrors.UserNotFound);
        }

        var verifyResult = identityUser.ValidatePassword(currentPassword);

        if (!verifyResult)
        {
            return Result.Fail(DomainServiceErrors.InvalidPassword);
        }

        identityUser.HandlePassword(newPassword);

        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(string email, string newPassword, string otpCode)
    {
        var identityUser = await identityRepository.FindByEmailAsync(email);

        if (identityUser is null)
        {
            return DomainServiceErrors.UserNotFound;
        }

        //Check does otp have same value and still valid
        if (identityUser.OtpCode.Value != otpCode)
        {
            return DomainServiceErrors.InvalidOtp;
        }

        if (identityUser.OtpCode.IsExpired())
        {
            return DomainServiceErrors.OtpExpired;
        }

        identityUser.HandlePassword(newPassword);
        identityUser.OtpCode.Reset();

        return Result.Success();
    }

    public async Task<Result> RegisterAsTutor(
        IdentityGuid userId,
        AcademicLevel academicLevel,
        string university,
        List<string> majors,
        List<string> tutorVerificationInfoDtos)
    {
        try
        {
            // Check if the user existed
            var user = await identityRepository
                .GetAsync(userId);

            if (user is null)
            {
                return Result.Fail(DomainServiceErrors.UserNotFound);
            }

            // Check if the user is already a tutor
            var alTutor = await tutorRepository.GetAsync(userId);

            if (alTutor is not null)
            {
                return Result.Fail(DomainServiceErrors.AlreadyTutorError);
            }

            Tutor tutor = Tutor.Create(
                user.Id,
                academicLevel,
                university,
                tutorVerificationInfoDtos,
                false,
                0
            );

            // TODO: Set user role to tutor

            // user.Role = UserRole.Tutor;
            await SetTutorRole(user);

            await tutorRepository.InsertAsync(tutor);

            // Handle major
            var subjects = await subjectRepository.GetListAsync(
                new SubjectListByNameSpec(majors)
            );

            var tutorMajors = subjects
                .Select(x => TutorMajor.Create(tutor.Id, x.Id, x.Name))
                .ToList();

            tutor.UpdateAllMajor(tutorMajors);

            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Fail(
                DomainServiceErrors.FailRegisteringAsTutorErrorWhileSavingChanges(
                    e.InnerException?.Message ?? e.Message));
        }
    }

    public async Task<Result> SetTutorRole(IdentityUser user)
    {
        try
        {
            var roles = await identityRoleRepository
                .GetAsync(new GetTutorRoleSpec());

            if (roles is null)
            {
                return Result.Fail(DomainServiceErrors.RoleNotFoundDomainError);
            }

            user.SetRole(roles);

            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Fail("Error while setting tutor role. Details error: " + e.Message);
        }
    }
}