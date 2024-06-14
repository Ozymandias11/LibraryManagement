
using Library.Data.Library.Interfaces;
using Library.Service.Library.Interfaces;

namespace Library.Service.Interfaces
{
    public interface IServiceManager
    {
       IAuthentificationService AuthenticationService { get; }
       IPublisherService PublisherService { get; }  
       IBookService BookService { get; }
       ICategoryService CategoryService { get; }
       IAuthorService AuthorService { get; }
       IBookCopyService BookCopyService { get; }
       IRoomService RoomService { get; }
       IShelfService ShelfService { get; }
    }
}
