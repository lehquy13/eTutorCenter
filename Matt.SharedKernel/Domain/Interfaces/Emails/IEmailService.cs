namespace Matt.SharedKernel.Domain.Interfaces.Emails;

public interface IEmailSender
{
    Task SendEmail(string email, string subject, string message);
    Task SendHtmlEmail(string email, string subject, string template);
}