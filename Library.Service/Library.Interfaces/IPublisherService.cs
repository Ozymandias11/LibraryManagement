using FluentResults;
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
        Task<Result<IEnumerable<PublisherDto>>> GetAllPublishers(
            string sortBy,
            string sortOrder,
            string searchString, bool trackChanges);
        Task<Result<PublisherDto>> GetPublisher(Guid id, bool trackChanges);
        Task<Result> CreatePublisher(CreatePublisherDto createPublisherDto, bool trackChanges);
        Task<Result> DeletePublisher(Guid guid, bool trackChanges);
        Task<Result> UpdatePublisher(PublisherDto publisherDto, bool trackChanges);
    }
}
