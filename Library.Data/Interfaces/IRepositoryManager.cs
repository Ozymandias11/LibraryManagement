using Library.Data.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.NewFolder
{
    public interface IRepositoryManager
    {
        IAuthorRepository AuthorRepository { get; }
        IPublisherRepository PublisherRepository { get; }
        IBookRepository BookRepository { get; }
        IBookAuthorRepository BookAuthorRepository { get; }
        IBookPublisherRepository BookPublisherRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBookCategoryRepository BookCategoryRepository { get; }
        IBookCopyRepository BookCopyRepository{ get; }
        IRoomRepository RoomRepository { get; }
        IShelfRepository ShelfRepository { get; }
        Task SaveAsync();
    }
}
