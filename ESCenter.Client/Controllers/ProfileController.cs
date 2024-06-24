﻿using ESCenter.Application.Accounts.Commands.ChangeAvatar;
using ESCenter.Application.Accounts.Commands.ChangePassword;
using ESCenter.Application.Accounts.Commands.UpdateBasicProfile;
using ESCenter.Application.Accounts.Queries.GetUserProfile;
using ESCenter.Application.Interfaces.Cloudinarys;
using ESCenter.Client.Application.ServiceImpls.Courses.Commands.ReviewCourse;
using ESCenter.Client.Application.ServiceImpls.Notifications;
using ESCenter.Client.Application.ServiceImpls.Profiles.Queries.GetLearningCourse;
using ESCenter.Client.Application.ServiceImpls.Profiles.Queries.GetLearningCourses;
using ESCenter.Client.Application.ServiceImpls.Subjects.Queries.GetSubjects;
using ESCenter.Client.Application.ServiceImpls.TutorProfiles.Queries.GetCourseRequestDetail;
using ESCenter.Client.Models;
using ESCenter.Client.Utilities;
using ESCenter.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESCenter.Client.Controllers;

[Authorize]
[Route("client/[controller]")]
public class ProfileController(
    ISender sender,
    ILogger<ProfileController> logger,
    ICloudinaryServices cloudinaryServices,
    IWebHostEnvironment webHostEnvironment)
    : Controller
{
    private async Task PackStaticListToView()
    {
        var subjects = await sender.Send(new GetSubjectsQuery());
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.Genders;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
        ViewData["Subjects"] = subjects.Value;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        await PackStaticListToView();

        var learnerProfile = await sender.Send(new GetUserProfileQuery());
        var learningCourses = await sender.Send(new GetLearningCoursesQuery());
        var notifications = await sender.Send(new GetNotificationsQuery());

        if (learnerProfile is { IsSuccess: true, Value: not null }
            && learningCourses is { IsSuccess: true, Value: not null }
            && notifications is { IsSuccess: true, Value: not null })
        {
            return View(new ProfileViewModel
            {
                UserProfileDto = learnerProfile.Value,
                LearningCourseForListDtos = learningCourses.Value,
                NotificationDtos = notifications.Value
            });
        }

        return RedirectToAction("Error", "Home");
    }

    [HttpPost("change-avatar")]
    public async Task<IActionResult> ChangeAvatar(IFormFile? formFile)
    {
        if (formFile is null || formFile.Length <= 0)
        {
            return BadRequest();
        }

        var fileName = formFile.FileName;
        await using var fileStream = formFile.OpenReadStream();

        var result = cloudinaryServices.UploadImage(fileName, fileStream);

        var changePictureResult = await sender.Send(new ChangeAvatarCommand(result));

        if (!changePictureResult.IsSuccess)
        {
            return BadRequest();
        }

        HttpContext.Session.SetString("image", result);
        return Json(new { res = true, image = result });
    }

    [HttpPost("choose-picture")]
    public async Task<IActionResult> ChoosePicture(IFormFile? formFile)
    {
        if (formFile == null)
        {
            return Json(false);
        }

        var image = await Helper.SaveFiles(formFile, webHostEnvironment.WebRootPath);

        return Json(new { res = true, image = "/temp/" + Path.GetFileName(image) });
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserProfileUpdateDto userDto) //, IFormFile? formFile)
    {
        await PackStaticListToView();

        if (!ModelState.IsValid)
        {
            return Helper.FailResult();
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

        return Helper.FailResult();
    }

    [HttpPost("change-password")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        if (!ModelState.IsValid)
        {
            return Helper.FailResult();
        }

        var loginResult = await sender.Send(new ChangePasswordCommand(
            changePasswordRequest.CurrentPassword,
            changePasswordRequest.NewPassword,
            changePasswordRequest.ConfirmedPassword));

        return loginResult.IsSuccess ? Helper.UpdatedResult() : Helper.FailResult();
    }

    [HttpGet]
    [Route("learning-course/{courseId:guid}")]
    public async Task<IActionResult> GetLearningClass(Guid courseId)
    {
        var query = new GetLearningCourseDetailQuery(courseId);
        var course = await sender.Send(query);

        return course.IsSuccess
            ? Helper.RenderRazorViewToString(this, "_LearningCourseDetail", course.Value)
            : RedirectToAction("Error", "Home");
    }


    [Authorize]
    [HttpPost]
    [Route("learning-course/{courseId:guid}/review-tutor")]
    public async Task<IActionResult> ReviewTutor(Guid courseId, ReviewCourseViewModel reviewCourseView)
    {
        var command = new ReviewCourseCommand(courseId, reviewCourseView.Detail, reviewCourseView.Rate);

        var result = await sender.Send(command);

        if (result.IsSuccess)
        {
            return RedirectToAction("SuccessPage", "Home");
        }

        return RedirectToAction("FailPage", "Home");
    }
}