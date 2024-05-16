using Library.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(ForgotPasswordDto forgotPasswordDto, string templateName);
        Task<bool> SendConfirmationEmail(RegisterViewModelDto registerViewModel, string templateName);
    }
}
