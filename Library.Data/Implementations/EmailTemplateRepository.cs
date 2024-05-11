using Library.Data.Interfaces;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Implementations
{
    public class EmailTemplateRepository : RepositoryBase<EmailTemplate>, IEmailTemplateReposiotry
    {
        public EmailTemplateRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<EmailTemplate>> GetAllEmailTemplates(bool trackChanges)
            => await FindAll(trackChanges).OrderBy(et => et.Subject).ToListAsync();

        public async Task<EmailTemplate?> GetEmailTemplateById(Guid id, bool trackChanges)
            => await FindByCondition(et => et.Id == id, trackChanges).FirstOrDefaultAsync();
       
        
    }
}
