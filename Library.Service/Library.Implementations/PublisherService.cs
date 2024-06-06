using AutoMapper;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PublisherService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager; 
            _mapper = mapper;
        }


        public async Task CreatePublisher(CreatePublisherDto createPublisherDto, bool trackChanges)
        {
            var publisherEntity = _mapper.Map<Model.Models.Publisher>(createPublisherDto);

            _repositoryManager.PublisherRepository.CreatePublisher(publisherEntity);

            await _repositoryManager.SaveAsync();

        }

        public async Task DeletePublisher(Guid id, bool trackChanges)
        {
            var publisher = await _repositoryManager.PublisherRepository.GetPublisher(id, trackChanges);

            _repositoryManager.PublisherRepository.DeletePublisher(publisher);

            await _repositoryManager.SaveAsync();

        }

        public async Task<IEnumerable<PublisherDto>> GetAllPublishers(bool trackChanges)
        {
            var publishers = await _repositoryManager.PublisherRepository.GetAllPublisher(trackChanges);

            var publishersDto = _mapper.Map<IEnumerable<PublisherDto>>(publishers); 

            return publishersDto;

        }

        public async Task<PublisherDto> GetPublisher(Guid id, bool trackChanges)
        {
            var publisher = await _repositoryManager.PublisherRepository.GetPublisher(id, trackChanges);

            var publisherDto = _mapper.Map<PublisherDto>(publisher);

            return publisherDto;

        }

        public async Task UpdatePublisher(PublisherDto publisherDto, bool trackChanges)
        {
            var publisherEntity = await _repositoryManager.PublisherRepository.GetPublisher(publisherDto.PublisherId, trackChanges);

            _mapper.Map(publisherDto, publisherEntity);

            await _repositoryManager.SaveAsync();

        }
    }
}
