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

        public Guid CreateBookCopyShelf(List<BookCopy> bookCopies, Shelf shelf)
        {
            var bookCopyShelf = new BookCopyShelf
            {
                BookCopyShelfId = Guid.NewGuid(),
                RoomId = shelf.RoomId,
                ShelfId = shelf.ShelfId
            };

            Create(bookCopyShelf);

            return bookCopyShelf.BookCopyShelfId;
        }
    }
}
