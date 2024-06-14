using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface IBookAuthorRepository
    {
        void CreateBookAuthor(Book book ,Author author);
        void DeleteBookAuthor(Guid bookid, Guid authorId);
        void RemoveRange(IEnumerable<BookAuthor> bookauthors);

    }
}
