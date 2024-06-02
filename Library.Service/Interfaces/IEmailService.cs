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
        Task<bool> SendEmail<T>(T Model, string templateName, string resetToken);
    }
}
