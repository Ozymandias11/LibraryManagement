using Library.Data.Library.Implementations;
using Library.Data.Library.Interfaces;
using Library.Data.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{

 
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IAuthorRepository> _authroRepository;
        private readonly Lazy<IPublisherRepository> _publisherRepository;
        private readonly Lazy<IBookRepository> _bookRepository;
        private readonly Lazy<IBookAuthorRepository> _bookAuthorRepository;
        private readonly Lazy<IBookPublisherRepository> _bookPublisherRepository;   
        private readonly Lazy<ICategoryRepository> _categoryRepository; 
        private readonly Lazy<IBookCategoryRepository> _bookCategoryRepository;
        private readonly Lazy<IBookCopyRepository> _bookCopyRepository;
        private readonly Lazy<IRoomRepository> _roomRepository;
        private readonly Lazy<IShelfRepository> _shelfRepository;
        private readonly Lazy<IBookShelfRepository> _bookCopyShelfRepository;
       

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _authroRepository = new Lazy<IAuthorRepository>(() => new AuthorRepository(repositoryContext));
            _publisherRepository = new Lazy<IPublisherRepository>(() => new PublisherRepository(repositoryContext));    
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(repositoryContext));
            _bookAuthorRepository = new Lazy<IBookAuthorRepository>(() => new BookAuthorRepository(repositoryContext));  
            _bookPublisherRepository = new Lazy<IBookPublisherRepository>(() => new BookPublisherRepository(repositoryContext)); 
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(repositoryContext));
            _bookAuthorRepository = new Lazy<IBookAuthorRepository>(() => new BookAuthorRepository(repositoryContext));
            _bookCategoryRepository = new Lazy<IBookCategoryRepository>(() => new BookCategoryRepository(repositoryContext));
            _bookCopyRepository = new Lazy<IBookCopyRepository>(() => new BookCopyRepository(repositoryContext));  
            _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(repositoryContext));
            _shelfRepository = new Lazy<IShelfRepository>(() => new ShelfRepository(repositoryContext));
            _bookCopyShelfRepository = new Lazy<IBookShelfRepository>(() => new BookShelfRepository(repositoryContext));   
        }

        public IAuthorRepository AuthorRepository => _authroRepository.Value;

        public IPublisherRepository PublisherRepository => _publisherRepository.Value;

        public IBookRepository BookRepository => _bookRepository.Value;

        public IBookAuthorRepository BookAuthorRepository => _bookAuthorRepository.Value;

        public IBookPublisherRepository BookPublisherRepository => _bookPublisherRepository.Value;

        public ICategoryRepository CategoryRepository => _categoryRepository.Value;

        public IBookCategoryRepository BookCategoryRepository => _bookCategoryRepository.Value;

        public IBookCopyRepository BookCopyRepository => _bookCopyRepository.Value;

        public IRoomRepository RoomRepository => _roomRepository.Value;

        public IShelfRepository ShelfRepository => _shelfRepository.Value;

        public IBookShelfRepository BookShelfRepository => _bookCopyShelfRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
       
    }
}
