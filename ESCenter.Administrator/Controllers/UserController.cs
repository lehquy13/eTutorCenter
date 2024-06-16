﻿using ESCenter.Admin.Application.Contracts.Users.Learners;
using ESCenter.Admin.Application.ServiceImpls.Customers.Commands.CreateUpdateUserProfile;
using ESCenter.Admin.Application.ServiceImpls.Customers.Commands.DeleteUser;
using ESCenter.Admin.Application.ServiceImpls.Customers.Queries.GetLearnerDetail;
using ESCenter.Admin.Application.ServiceImpls.Customers.Queries.GetLearners;
using ESCenter.Administrator.Utilities;
using ESCenter.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESCenter.Administrator.Controllers;

[Authorize(Policy = "RequireAdministratorRole")]
[Route("admin/[controller]")]
public class UserController(ILogger<UserController> logger, ISender sender) : Controller
{
    private void PackStaticListToView()
    {
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.Genders;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
    }

    #region basic user management

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var userDtos = await sender.Send(new GetLearnersQuery());
        if (userDtos.IsFailure)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(userDtos.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        PackStaticListToView();

        var result = await sender.Send(new GetLearnerDetail(id));
        if (result.IsFailure)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(result.Value);
    }

    [HttpPost("{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] Guid id, [FromForm] LearnerForCreateUpdateDto userDto)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit", userDto);
        }

        var result = await sender.Send(new CreateUpdateUserProfileCommand(userDto));

        if (!result.IsSuccess)
        {
            return Helper.FailResult();
        }

        PackStaticListToView();

        return Helper.UpdatedResult();
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        PackStaticListToView();
        return View(new LearnerForCreateUpdateDto());
    }


    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LearnerForCreateUpdateDto userForCreateDto)
    {
        var result = await sender.Send(new CreateUpdateUserProfileCommand(userForCreateDto));

        if (result.IsFailure)
        {
            return RedirectToAction("Error", "Home");
        }

        return RedirectToAction("Index");
    }

    [HttpGet("{id}/delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        PackStaticListToView();

        var result = await sender.Send(new GetLearnerDetail(id));
        
        if (result.IsFailure)
        {
            return RedirectToAction("Error", "Home");
        }

        return Helper.RenderRazorViewToString(this, "Delete", result.Value);
    }

    [HttpPost("{id:guid}/delete-confirm")]
    public async Task<IActionResult> DeleteConfirm(Guid id)
    {
        var result = await sender.Send(new DeleteUserCommand(id));

        return result.IsFailure ? RedirectToAction("Error", "Home") : RedirectToAction("Index");
    }

    [HttpGet("{id}/detail")]
    public async Task<IActionResult> Detail(Guid id)
    {
        var result = await sender.Send(new GetLearnerDetail(id));
        if (result.IsFailure)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(result.Value);
    }

    #endregion
}