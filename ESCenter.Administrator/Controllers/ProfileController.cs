﻿using ESCenter.Administrator.Models;
using ESCenter.Administrator.Utilities;
using ESCenter.Application.Accounts.Commands.ChangeAvatar;
using ESCenter.Application.Accounts.Commands.ChangePassword;
using ESCenter.Application.Accounts.Commands.UpdateBasicProfile;
using ESCenter.Application.Accounts.Queries.GetUserProfile;
using ESCenter.Application.Accounts.Queries.Login;
using ESCenter.Application.Interfaces.Cloudinarys;
using ESCenter.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESCenter.Administrator.Controllers;

[Route("admin/[controller]")]
[Authorize(Policy = "RequireAdministratorRole")]
public class ProfileController(
    ISender sender,
    ILogger<ProfileController> logger,
    ICloudinaryServices cloudinaryServices,
    IWebHostEnvironment webHostEnvironment)
    : Controller
{
    private void PackStaticListToView()
    {
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.GenderFilters;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        PackStaticListToView();

        var learnerProfile = await sender.Send(new GetUserProfileQuery());

        if (learnerProfile is { IsSuccess: true, Value: not null })
        {
            return View(new ProfileViewModel
            {
                UserProfileDto = learnerProfile.Value,
            });
        }

        return RedirectToAction("Index", "Authentication", new LoginQuery("", ""));
    }

    [HttpPost("change-avatar")]
    public async Task<IActionResult> ChangeAvatar(IFormFile? formFile)
    {
        if (formFile is null || formFile.Length <= 0)
        {
            return BadRequest();
        }

        var fileName = formFile.FileName;

        var result = cloudinaryServices.UploadImage(fileName, formFile.OpenReadStream());

        var changePictureResult = await sender.Send(new ChangeAvatarCommand(result));

        if (!changePictureResult.IsSuccess)
        {
            return BadRequest();
        }

        HttpContext.Session.SetString("image", result);
        return Json(new { res = true, image = result });
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserProfileUpdateDto userDto) //, IFormFile? formFile)
    {
        PackStaticListToView();

        if (!ModelState.IsValid)
        {
            return Helper.RenderRazorViewToString(this,
                "_ProfileEdit",
                userDto,
                true
            );
        }

        var result = await sender.Send(new UpdateBasicProfileCommand(userDto));

        try
        {
            Helper.ClearTempFile(webHostEnvironment.WebRootPath);
        }
        catch (Exception)
        {
            logger.LogError("Temp folder does not exist");
        }

        if (result is { IsSuccess: true, Value: not null })
        {
            HttpContext.Session.SetString("name", result.Value.User.FullName);
            HttpContext.Session.SetString("image", result.Value.User.Avatar);

            return Helper.UpdatedResult();
        }

        return Helper.RenderRazorViewToString(this,
            "_ProfileEdit",
            userDto,
            true
        );
    }

    [HttpPost("change-password")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand changePasswordCommand)
    {
        if (!ModelState.IsValid)
        {
            return Helper.RenderRazorViewToString(this, "_ChangePassword", changePasswordCommand, true);
        }

        try
        {
            var loginResult = await sender.Send(changePasswordCommand);

            if (loginResult.IsSuccess)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Authentication");
            }
        }
        catch (Exception ex)
        {
            //Log the error (uncomment ex variable name and write a log.)
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists, " + ex.Message +
                                         "see your system administrator.");
        }

        return Helper.RenderRazorViewToString(this, "_ChangePassword", changePasswordCommand, true);
    }
}