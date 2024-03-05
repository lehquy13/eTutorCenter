﻿using ESCenter.Admin.Application.Contracts.Courses.Dtos;
using Matt.SharedKernel.Application.Mediators.Commands;

namespace ESCenter.Admin.Application.ServiceImpls.Admins.Courses.Commands.CancelCourseRequest;

public record CancelCourseRequestCommand(CourseRequestCancelDto CourseRequestCancelDto) : ICommandRequest;