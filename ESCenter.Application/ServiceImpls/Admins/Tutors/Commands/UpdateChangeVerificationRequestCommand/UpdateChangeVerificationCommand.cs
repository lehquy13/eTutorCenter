﻿using Matt.SharedKernel.Application.Mediators.Commands;

namespace ESCenter.Application.ServiceImpls.Admins.Tutors.Commands.UpdateChangeVerificationRequestCommand;

public record UpdateChangeVerificationCommand(Guid TutorId, int RequestId, bool IsApproved) : ICommandRequest;