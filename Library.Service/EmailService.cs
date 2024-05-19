using Library.Data.Interfaces;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Interfaces;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Library.Service
{
    public class EmailService : IEmailService
    {

        private readonly IMailjetClient _mailjetClient;
        private readonly IEmailTemplateReposiotry _emailTemplateReposiotry;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Employee> _userManager;

        public EmailService(IOptions<MailjetSettings> mailjetSettings, 
            IEmailTemplateReposiotry emailTemplateReposiotry,
            IConfiguration configuration,
            UserManager<Employee> userManager)
        {
            _mailjetClient = new MailjetClient(mailjetSettings.Value.ApiKey, mailjetSettings.Value.SecretKey);
            _emailTemplateReposiotry = emailTemplateReposiotry;
            _configuration = configuration;
            _userManager = userManager;
        }






        public async Task<bool> SendEmail<T>(T model, string templateName)
        {

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };


            var template = await _emailTemplateReposiotry.GetTemplateByName(templateName, trackChanges: false);

            var userEmail = GetEmail(model);


            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
            {
                return false;
            }

            var resetToken = await GenerateToken(model, user);

            var EncodedToken = HttpUtility.UrlEncode(resetToken);

            var resetLink = GenerateLink(EncodedToken, user.Id, templateName);

            var (Body, To) = FormatEmailBody(userEmail, template.Body, resetLink, template.To);


            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(template.From))
                .WithSubject(template.Subject)
                .WithHtmlPart(Body)
                .WithTo(new SendContact(To))
                .Build();

            var response = await _mailjetClient.SendTransactionalEmailAsync(email);
            var message = response.Messages[0];

            bool result = message.Status.ToLower() == "success";

            return result;



        }

        //private string GetEmail<T>(T Model)
        //{
        //    return Model switch
        //    {
        //        RegisterViewModelDto registerViewModelDto => registerViewModelDto.Email,
        //        ForgotPasswordDto forgotPasswordDto => forgotPasswordDto.Email,
        //        _ => throw new ArgumentException("Invalid model type")
        //    };
        //}

        private string GetEmail<T>(T model)
        {
            var emailProperty = model.GetType().GetProperty("Email");
            if (emailProperty != null)
            {
                return emailProperty.GetValue(model)?.ToString();
            }
            throw new ArgumentException("The model does not have an 'Email' property.");
        }

        private async Task<string> GenerateToken<T>(T model, Employee employee)
        {
            var modelType = model.GetType();
            if (modelType == typeof(RegisterViewModelDto))
            {
                return await _userManager.GenerateEmailConfirmationTokenAsync(employee);
            }
            else if (modelType == typeof(ForgotPasswordDto))
            {
                return await _userManager.GeneratePasswordResetTokenAsync(employee);
            }
            throw new ArgumentException("Invalid model type");
        }

        //private async Task<string> GenerateToken<T>(T Model, Employee employee)
        //{
        //    return Model switch
        //    {
        //        RegisterViewModelDto => await _userManager.GenerateEmailConfirmationTokenAsync(employee),
        //        ForgotPasswordDto => await _userManager.GeneratePasswordResetTokenAsync(employee),
        //        _ => throw new ArgumentException("Invalid model type")
        //    };
        //}

        private string GenerateLink(string encodedToken, string userId, string templateName)
        {

            var baseUrl = _configuration["AppUrl"];

            return templateName switch
            {
                "Email Verification" => $"{baseUrl}/Account/ConfirmEmail?token={encodedToken}&userId={userId}",
                "Password Reset" => $"{baseUrl}/Account/ResetPassword?token={encodedToken}&userId={userId}",
                _ => throw new ArgumentException("Invalid template name")
            }; 
        }

        private (string Body, string To) FormatEmailBody(
           string Email,
           string body,
           string ResetLink,
           string to
           )
        {
            body = body.Replace("@@userName@@", Email);
            body = body.Replace("@@resetLink@@", ResetLink);
            to = to.Replace("@@userEmail@@", Email);




            return (Body: body, To: to);
        }


    }
}
