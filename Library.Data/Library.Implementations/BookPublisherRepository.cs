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
    public class BookPublisherRepository : RepositoryBase<BookPublisher>, IBookPublisherRepository
    {
        public BookPublisherRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBookPublisher(Book book, Publisher publisher)
        {
            var bookPublisher = new BookPublisher
            {
                BookId = book.BookId,
                PublisherId = publisher.PublisherId
            };

            Create(bookPublisher);
        }

        public async void DeleteBookPublisher(Guid bookId, Guid publisherId)
        {
            var bookPublisher = await FindByCondition(bp => bp.BookId == bookId  && bp.PublisherId == publisherId, false).FirstOrDefaultAsync();

            if (bookPublisher != null)
            {
                Delete(bookPublisher);  
            }

        }

        public void RemoveRange(IEnumerable<BookPublisher> bookPublishers)
        {
            foreach(var bookPublisher in bookPublishers) 
            {
                Delete(bookPublisher);
            }


        }
    }
}
