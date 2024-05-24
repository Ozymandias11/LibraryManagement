using Library.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Interfaces
{
    public interface IEmailTemplateService
    {
        Task<IEnumerable<EmailtemplateDto>> GetAllTemplate(string sortBy, string sortOrder,bool trackChanges);
        Task<EmailtemplateDto> GetTemplateById(Guid id, bool trackChanges);
        Task UpdateEmailTemplate(EmailtemplateDto emailtemplate, bool trackChanges);

    }
}
