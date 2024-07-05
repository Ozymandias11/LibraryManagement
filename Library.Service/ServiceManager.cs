using AutoMapper;
using Library.Data.Library.Interfaces;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Interfaces;
using Library.Service.Library.Implementations;
using Library.Service.Library.Interfaces;
using Library.Service.Logging;
using Microsoft.AspNetCore.Identity;


namespace Library.Service
{
    public sealed class ServiceManager : IServiceManager
    {
       
        

        private readonly Lazy<IAuthentificationService> _authentificationService;
        private readonly Lazy<IAuthorService> _authorService;
        private readonly Lazy<IPublisherService> _publisherService;
        private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IBookCopyService> _bookCopyService;
        private readonly Lazy<IRoomService> _roomService;
        private readonly Lazy<IShelfService> _shelfService;
        private readonly Lazy<ICustomerService> _customerService;



        public ServiceManager( IRepositoryManager reposiotryManager, UserManager<Employee> usermanager, 
            IMapper mapper, SignInManager<Employee> signInManager, ILoggerManager loggerManager)
        {
            _authentificationService = new Lazy<IAuthentificationService>(() => new AuthenticationService(usermanager, mapper, signInManager, loggerManager));
            _authorService = new Lazy<IAuthorService>(() => new AuthorService(reposiotryManager, mapper));
            _publisherService = new Lazy<IPublisherService>(() => new PublisherService(reposiotryManager, mapper));
            _bookService = new Lazy<IBookService>(() => new BookService(reposiotryManager,mapper));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(reposiotryManager, mapper));
            _bookCopyService = new Lazy<IBookCopyService>(() => new BookCopyService(reposiotryManager, mapper));
            _roomService = new Lazy<IRoomService>(() => new RoomService(reposiotryManager, mapper));
            _shelfService = new Lazy<IShelfService>(() => new ShelfService(reposiotryManager, mapper));
            _customerService = new Lazy<ICustomerService>(() => new CustomerService(reposiotryManager, mapper));


        }
        public IAuthentificationService AuthenticationService => _authentificationService.Value;

        public IAuthorService AuthorService => _authorService.Value;

        public IPublisherService PublisherService => _publisherService.Value;

        public IBookService BookService => _bookService.Value;

        public ICategoryService CategoryService => _categoryService.Value;

        public IBookCopyService BookCopyService => _bookCopyService.Value;

        public IRoomService RoomService => _roomService.Value;

        public IShelfService ShelfService => _shelfService.Value;

        public ICustomerService CustomerService => _customerService.Value;
    }
}
