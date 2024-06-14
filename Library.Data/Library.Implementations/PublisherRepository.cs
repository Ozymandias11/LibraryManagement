using Library.Data.Library.Interfaces;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository
    {
        public PublisherRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void DeletePublisher(Publisher publisher) => Delete(publisher);
       

        public async Task<IEnumerable<Publisher>> GetAllPublisher(bool trackChanges) 
            => await FindAll(trackChanges).OrderBy(a => a.CreatedDate).ToListAsync();   
        

        public async Task<Publisher?> GetPublisher(Guid id, bool trackChanges)
            => await FindByCondition(a => a.PublisherId == id, trackChanges).FirstOrDefaultAsync();
       

        public void CreatePublisher(Publisher publisher) => Create(publisher);

        public async Task<IEnumerable<Publisher>> GetPublishersOfBook(Guid id, bool trackChanges) =>
            await FindByCondition(p => p.Books.Any(bp => bp.BookId == id), trackChanges).ToListAsync();

        public async Task<IEnumerable<Publisher>> GetPublishersById(IEnumerable<Guid> ids, bool trackChanges) => 
            await FindByCondition(p => ids.Contains(p.PublisherId), trackChanges).ToListAsync();
       
    }
}
