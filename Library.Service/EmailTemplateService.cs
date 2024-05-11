using AutoMapper;
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

        public async Task<IEnumerable<EmailtemplateDto>> GetAllTemplate(bool trackChanges)
        {
            var EmailTemplates = await _emailTemplateReposiotry.GetAllEmailTemplates(trackChanges);
            var EmailTemplatesDto = _mapper.Map<IEnumerable<EmailtemplateDto>>(EmailTemplates);

            return EmailTemplatesDto;
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
