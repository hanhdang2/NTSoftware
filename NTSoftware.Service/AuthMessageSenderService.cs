using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using NTSoftware.Core.Shared.Constants;
using NTSoftware.Service.Interface;
using NTSoftware.Service.Interface.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NTSoftware.Service
{
    public class AuthMessageSenderService : IEmailSender, ISmsSenderService
    {

        public AuthMessageSenderService(IOptions<EmailSettingViewModel> emailSettings)
        {
             _emailSettings = emailSettings.Value;
        }

        public EmailSettingViewModel _emailSettings { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {

            Execute(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string email, string subject, string message)
        {

            string toEmail = string.IsNullOrEmpty(email)
                             ? _emailSettings.ToEmail
                             : email;
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_emailSettings.UsernameEmail, EmailConfig.USERNAME_EMAIL_SENDER)
            };
            mail.To.Add(new MailAddress(toEmail));

            mail.Subject = EmailConfig.DEFAULT_SUBJECT ;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (SmtpClient smtp = new SmtpClient(_emailSettings.SecondayDomain, _emailSettings.SecondaryPort))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }

        }
    }
}
