using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
   public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllPublisher(bool trackChanges);
        Task<Publisher?> GetPublisher(Guid id, bool trackChanges);  
        Task<IEnumerable<Publisher>> GetPublishersOfBook(Guid id, bool trackChanges);
        Task<IEnumerable<Publisher>> GetPublishersById(IEnumerable<Guid> ids, bool trackChanges);
        void DeletePublisher(Publisher publisher);
        void CreatePublisher(Publisher publisher);  

    }
}
