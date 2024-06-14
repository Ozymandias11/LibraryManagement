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
    public class BookAuthorRepository : RepositoryBase<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBookAuthor(Book book, Author author)
        {
            var bookAuthor = new BookAuthor
            {
                BookId = book.BookId,
                AuthorID = author.AuthorId
            };

            Create(bookAuthor);
        }

        public async void DeleteBookAuthor(Guid bookid, Guid authorId)
        {
            var bookAuthor = await FindByCondition(ba => ba.BookId == bookid && ba.AuthorID == authorId, false).FirstOrDefaultAsync();

            if (bookAuthor != null)
            {
                Delete(bookAuthor);
            }

        }

        public void RemoveRange(IEnumerable<BookAuthor> bookauthors)
        {
            foreach(var bookAuthor in bookauthors) 
            {
                Delete(bookAuthor);
            }
          //  DetachEntities(bookauthors);

        }
    }
}
