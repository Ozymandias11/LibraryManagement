using AutoMapper;
using FluentResults;
using Library.Data.NewFolder;
using Library.Data.RequestFeatures;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Errors.NotFoundError;
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



        public async Task<(IEnumerable<PublisherDto> publishers, MetaData metaData)> GetAllPublishers(PublisherParameters publisherParameters, bool trackChanges)
        {
            var publishersWithMetaData = await _repositoryManager.PublisherRepository.GetAllPublisher(publisherParameters, trackChanges);

            var publishersDto = _mapper.Map<IEnumerable<PublisherDto>>(publishersWithMetaData);

            return (publishersDto, publishersWithMetaData.MetaData);

        }

        public async Task<Result<PublisherDto>> GetPublisher(Guid id, bool trackChanges)
        {
            var publisher = await _repositoryManager.PublisherRepository.GetPublisher(id, trackChanges);


            if (publisher == null)
            {
                return Result.Fail(new NotFoundError("Publisher", id));
            }

            var publisherDto = _mapper.Map<PublisherDto>(publisher);

            return publisherDto;

        }


        public async Task<Result> CreatePublisher(CreatePublisherDto createPublisherDto, bool trackChanges)
        {
            var existingPublisher = await _repositoryManager.PublisherRepository.GetPublisherWithTitle(createPublisherDto.PublisherName, trackChanges);

            if (existingPublisher != null)
            {
                return Result.Fail($"The publisher with name {existingPublisher.PublisherName} already exists");
            }

            var publisherEntity = _mapper.Map<Model.Models.Publisher>(createPublisherDto);

            _repositoryManager.PublisherRepository.CreatePublisher(publisherEntity);

            await _repositoryManager.SaveAsync();

            return Result.Ok();

        }

        public async Task<Result> DeletePublisher(Guid id, bool trackChanges)
        {
            var publisher = await _repositoryManager.PublisherRepository.GetPublisher(id, trackChanges);

            if(publisher == null)
            {
                return Result.Fail(new NotFoundError("Publisher", id));
            }

            _repositoryManager.PublisherRepository.DeletePublisher(publisher);

            await _repositoryManager.SaveAsync();

            return Result.Ok(); 

        }

        public async Task<Result> UpdatePublisher(PublisherDto publisherDto, bool trackChanges)
        {
            var publisherEntity = await _repositoryManager.PublisherRepository.GetPublisher(publisherDto.PublisherId, trackChanges);

            if(publisherEntity == null)
            {
                return Result.Fail(new NotFoundError("Publisher", publisherDto.PublisherId));
            }

            _mapper.Map(publisherDto, publisherEntity);

            await _repositoryManager.SaveAsync();

            return Result.Ok(); 
        }

      
    }
}
