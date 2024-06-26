using Library.Data.Library.Interfaces;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class BookShelfRepository : RepositoryBase<BookCopyShelf>, IBookShelfRepository
    {
        public BookShelfRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBookCopyShelf(BookCopy bookCopy, Shelf shelf)
        {
            var BookCopyShelf = new BookCopyShelf
            {
                BookCopyId = bookCopy.BookCopyId,
                ShelfId = shelf.ShelfId
            };

            Create(BookCopyShelf);
        }
    }
}
