using AutoMapper;
using Library.Data.Implementations;
using Library.Data.Interfaces;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateReposiotry _emailTemplateReposiotry;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public EmailTemplateService(IEmailTemplateReposiotry emailTemplateReposiotry,
            IMapper mapper,
            IRepositoryManager repositoryManager)
        {
            _emailTemplateReposiotry = emailTemplateReposiotry;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

       

        public async Task<IEnumerable<EmailtemplateDto>> GetAllTemplate(string sortBy, string sortOrder, bool trackChanges)
        {
            var emailTemplates = await _emailTemplateReposiotry.GetAllEmailTemplates(trackChanges);

            // Sort the entities based on the provided sortBy and sortOrder parameters
            IOrderedEnumerable<EmailTemplate> sortedEmailTemplates = sortBy switch
            {
                "TemplateName" => sortOrder == "TemplateName_Asc"
                    ? emailTemplates.OrderBy(t => t.TemplateName)
                    : emailTemplates.OrderByDescending(t => t.TemplateName),
                _ => emailTemplates.OrderBy(t => t.Id)
            };

            // Map the sorted entities to DTOs
            var emailTemplatesDto = _mapper.Map<IEnumerable<EmailtemplateDto>>(sortedEmailTemplates);

            return emailTemplatesDto;
        }

        public async Task<EmailtemplateDto> GetTemplateById(Guid id, bool trackChanges)
        {
            var EmailTemplate = await _emailTemplateReposiotry.GetEmailTemplateById(id, trackChanges);
            var EmailTemplateDto = _mapper.Map<EmailtemplateDto>(EmailTemplate);
            return EmailTemplateDto;
        }

        public async Task UpdateEmailTemplate(EmailtemplateDto emailtemplateDto, bool trackChanges)
        {
            var emailTemplateEntity = await _emailTemplateReposiotry.GetEmailTemplateById(emailtemplateDto.Id, trackChanges);

            _mapper.Map(emailtemplateDto, emailTemplateEntity);

            await _repositoryManager.SaveAsync();
        }
    }
}
