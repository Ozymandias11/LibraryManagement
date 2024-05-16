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

        public async Task<bool> SendConfirmationEmail(RegisterViewModelDto registerViewModel, string templateName)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };


            var template = await _emailTemplateReposiotry.GetTemplateByName(templateName, trackChanges: false);

            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);

            var resetToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var EncodedToken = HttpUtility.UrlEncode(resetToken);

            var resetLink = $"{_configuration["AppUrl"]}/Account/ConfirmEmail?token={EncodedToken}&userId={user.Id}";

            var (Body, To) = FormatEmailBody1(registerViewModel, template.Body, resetLink, template.To);


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

        public async Task<bool> SendEmail(ForgotPasswordDto forgotPasswordDto, string templateName)
        {

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };


            var template = await _emailTemplateReposiotry.GetTemplateByName(templateName, trackChanges:false);

            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var EncodedToken = HttpUtility.UrlEncode(resetToken);

            var resetLink = $"{_configuration["AppUrl"]}/Account/ResetPassword?token={EncodedToken}&userId={user.Id}";

            var (Body, To) = FormatEmailBody(forgotPasswordDto, template.Body, resetLink, template.To);


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

        private (string Body, string To) FormatEmailBody(ForgotPasswordDto forgotPasswordDto, 
            string body,
            string ResetLink,
            string to
            ) 
        {
            body = body.Replace("@@userName@@", forgotPasswordDto.Email);
            body = body.Replace("@@resetLink@@", ResetLink);
            to = to.Replace("@@userEmail@@", forgotPasswordDto.Email);




            return (Body:body, To:to);
        }

        private (string Body, string To) FormatEmailBody1(RegisterViewModelDto registerViewModel,
           string body,
           string ResetLink,
           string to
           )
        {
            body = body.Replace("@@userName@@", registerViewModel.Email);
            body = body.Replace("@@verificationUrl@@", ResetLink);
            to = to.Replace("@@userEmail@@", registerViewModel.Email);




            return (Body: body, To: to);
        }





    }
}
