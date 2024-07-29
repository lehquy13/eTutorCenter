﻿using ESCenter.Application.Interfaces;
using ESCenter.Domain.Aggregates.Courses.DomainEvents;
using ESCenter.Domain.Aggregates.Notifications;
using ESCenter.Domain.Aggregates.Users;
using ESCenter.Domain.Shared.NotificationConsts;
using Matt.SharedKernel.Application.Contracts.Interfaces.Infrastructures;
using Matt.SharedKernel.Domain.Interfaces;
using Matt.SharedKernel.Domain.Interfaces.Emails;
using Matt.SharedKernel.Domain.Interfaces.Repositories;
using MediatR;

namespace ESCenter.Application.EventHandlers.CourseAssigned;

public class NotifyCourseAssignedDomainEventHandler(
    IEmailSender emailSender,
    ICustomerRepository customerRepository,
    IRepository<Notification, int> notificationRepository,
    IUnitOfWork unitOfWork,
    IFireBaseNotificationService fireBaseNotificationService,
    ICurrentUserService currentUserService,
    IAppLogger<NotifyCourseAssignedDomainEventHandler> logger
) : INotificationHandler<TutorAssignedDomainEvent>
{
    public async Task Handle(TutorAssignedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var message = $"Course {domainEvent.Course.Title} has been assigned to you. " +
                      $"Please check your tutor dashboard for more details.";

        var tutorEmail = await customerRepository.GetTutorByTutorId(domainEvent.TutorId, cancellationToken);

        List<Notification> notifications =
        [
            Notification.Create(
                "Course Assigned",
                domainEvent.Course.Id.Value.ToString(),
                NotificationEnum.Course),
            Notification.Create(
                "Course was assigned to you",
                domainEvent.Course.Id.Value.ToString(),
                NotificationEnum.Course,
                currentUserService.UserId,
                domainEvent.TutorId.Value)
        ];

        await notificationRepository.InsertManyAsync(notifications, cancellationToken);

        if (await unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
        {
            logger.LogError("Failed to save notification to database");

            return;
        }

        if (tutorEmail == null)
        {
            logger.LogWarning("Tutor email is null when assigning course: {CourseId}", domainEvent.Course.Id);

            return;
        }

        try
        {
            await emailSender.SendEmail("20521318@gm.uit.edu.vn", "Course Assigned", message);
            await fireBaseNotificationService.SendNotificationAsync("Course Assigned",
                $"Course {domainEvent.Course.Title} has been assigned to you. "
                , tutorEmail.FCMToken);
        }
        catch (Exception e)
        {
            logger.LogError("Failed to send email or notification to tutor {TutorId} for course {CourseId}",
                domainEvent.TutorId, domainEvent.Course.Id);
        }
    }
}