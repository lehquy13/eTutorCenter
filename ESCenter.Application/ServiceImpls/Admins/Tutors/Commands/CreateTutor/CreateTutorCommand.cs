﻿using ESCenter.Application.Contracts.Users.Tutors;
using ESCenter.Domain.Aggregates.Users.Errors;
using Matt.SharedKernel.Application.Mediators.Commands;

namespace ESCenter.Application.ServiceImpls.Admins.Tutors.Commands.CreateTutor;

public record CreateTutorCommand(TutorCreateDto TutorForCreateDto) : ICommandRequest;