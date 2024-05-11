using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Interfaces
{
    public interface IEmailTemplateReposiotry
    {
        Task<IEnumerable<EmailTemplate>> GetAllEmailTemplates(bool trackChanges);
        Task<EmailTemplate?> GetEmailTemplateById(Guid id, bool trackChanges);    
    }
}
