﻿using ESCenter.Admin.Application.Contracts.Courses.Dtos;
using Matt.SharedKernel.Application.Mediators.Commands;

namespace ESCenter.Admin.Application.ServiceImpls.Admins.Courses.Commands.UpdateCourse;

public record UpdateCourseCommand(CourseUpdateDto CourseUpdateDto) : ICommandRequest;