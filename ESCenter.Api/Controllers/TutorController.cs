﻿using ESCenter.Host;
using ESCenter.Mobile.Application.Contracts.Users.Learners;
using ESCenter.Mobile.Application.Contracts.Users.Params;
using ESCenter.Mobile.Application.Contracts.Users.Tutors;
using ESCenter.Mobile.Application.ServiceImpls.Tutors.Commands.RequestTutor;
using ESCenter.Mobile.Application.ServiceImpls.Tutors.Queries.GetTutorDetail;
using ESCenter.Mobile.Application.ServiceImpls.Tutors.Queries.GetTutors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESCenter.Api.Controllers;

public class TutorController(
    ILogger<TutorController> logger,
    IMediator mediator)
    : ApiControllerBase(logger)
{
    // Query
    // GET: api/<Tutor>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllTutors([FromQuery] TutorParams tutorParams)
    {
        var tutorDtos = await mediator.Send(new GetTutorsQuery(tutorParams));
        return Ok(tutorDtos);
    }


    // GET api/<TutorController>/5
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetTutor(Guid id)
    {
        var tutorDto = await mediator.Send(new GetTutorDetailQuery(id));
        return Ok(tutorDto);
    }

    // TODO: test
    // POST api/<TutorController>/Register 
    [Authorize(Roles = "Learner")]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> TutorRegistration(
        [FromBody] TutorBasicForRegisterCommand tutorBasicForRegisterCommand)
    {
        var result = await mediator.Send(tutorBasicForRegisterCommand);
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    [Route("{tutorId}/request-tutor")]
    public async Task<IActionResult> RequestTutor(Guid tutorId,
        [FromBody] TutorRequestForCreateDto tutorRequestForCreateDto)
    {
        var result = await mediator.Send(new RequestTutorCommand(tutorRequestForCreateDto));
        return Ok(result);
    }
}