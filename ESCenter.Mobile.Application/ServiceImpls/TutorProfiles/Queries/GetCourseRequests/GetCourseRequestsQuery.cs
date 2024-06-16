﻿using ESCenter.Mobile.Application.Contracts.Courses.Dtos;
using Matt.SharedKernel.Application.Contracts.Interfaces;
using Matt.SharedKernel.Application.Mediators.Queries;

namespace ESCenter.Mobile.Application.ServiceImpls.TutorProfiles.Queries.GetCourseRequests;

public record GetCourseRequestsQuery() : IQueryRequest<IEnumerable<BasicCourseRequestDto>>, IAuthorizationRequest;