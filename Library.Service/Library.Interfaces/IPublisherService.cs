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
        Task<IEnumerable<PublisherDto>> GetAllPublishers(bool trackChanges);
        Task<PublisherDto> GetPublisher(Guid id, bool trackChanges);
        Task CreatePublisher(CreatePublisherDto createPublisherDto, bool trackChanges);
        Task DeletePublisher(Guid guid, bool trackChanges);
        Task UpdatePublisher(PublisherDto publisherDto, bool trackChanges);
    }
}
