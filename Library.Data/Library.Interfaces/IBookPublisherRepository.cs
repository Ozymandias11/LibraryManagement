using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface IBookPublisherRepository
    {
        void CreateBookPublisher(Book book, Publisher publisher);
        void DeleteBookPublisher(Guid bookId, Guid publisherId);
        void RemoveRange(IEnumerable<BookPublisher> bookPublishers);
    }
}
