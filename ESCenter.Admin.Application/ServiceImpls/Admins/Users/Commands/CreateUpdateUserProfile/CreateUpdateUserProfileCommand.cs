﻿using ESCenter.Admin.Application.Contracts.Users.Learners;
using Matt.SharedKernel.Application.Mediators.Commands;

namespace ESCenter.Admin.Application.ServiceImpls.Admins.Users.Commands.CreateUpdateUserProfile;

public record CreateUpdateUserProfileCommand(
    LearnerForCreateUpdateDto LearnerForCreateUpdateDto
) : ICommandRequest;