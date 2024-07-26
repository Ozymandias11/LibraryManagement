using FluentResults;
using Library.Data.RequestFeatures;
using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
    public interface IPublisherService
    {
        Task<(IEnumerable<PublisherDto> publishers, MetaData metaData)> GetAllPublishers(PublisherParameters publisherParameters, bool trackChanges);
        Task<Result<PublisherDto>> GetPublisher(Guid id, bool trackChanges);
        Task<Result> CreatePublisher(CreatePublisherDto createPublisherDto, bool trackChanges);
        Task<Result> DeletePublisher(Guid guid, bool trackChanges);
        Task<Result> UpdatePublisher(PublisherDto publisherDto, bool trackChanges);
    }
}
