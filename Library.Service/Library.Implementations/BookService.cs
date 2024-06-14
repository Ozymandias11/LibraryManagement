using AutoMapper;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vonage.VerifyV2.StartVerification.WhatsApp;

namespace Library.Service.Library.Implementations
{
    public class BookService : IBookService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public BookService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager; 
            _mapper = mapper;
        }

        public async Task CreateBook(
            CreateBookDto bookDto, 
            IEnumerable<Guid> authroIds, 
            IEnumerable<Guid> publishersIds,
            IEnumerable<Guid> categoryIds,
            bool trackChanges)
        {
            var bookEntity =  _mapper.Map<Book>(bookDto);

            _repositoryManager.BookRepository.CreateBook(bookEntity);

            foreach (var authorId in  authroIds) 
            {
                var author = await _repositoryManager.AuthorRepository.GetAuthor(authorId, false);
                _repositoryManager.BookAuthorRepository.CreateBookAuthor(bookEntity, author);
            }

            foreach(var publishersId in publishersIds)
            {
                var publisher = await _repositoryManager.PublisherRepository.GetPublisher(publishersId, false); 
                _repositoryManager.BookPublisherRepository.CreateBookPublisher(bookEntity, publisher);
                
            }

            foreach(var categoryId in categoryIds)
            {
                var category = await _repositoryManager.CategoryRepository.GetCategory(categoryId, false);
                _repositoryManager.BookCategoryRepository.CreateBookCategory(bookEntity, category);
            }

           




            await _repositoryManager.SaveAsync();


        }

        public async Task DeleteBook(Guid id, bool trackChanges)
        {
            var bookEntity = await _repositoryManager.BookRepository.GetBook(id, trackChanges);

            _repositoryManager.BookRepository.DeleteBook(bookEntity);

            await _repositoryManager.SaveAsync();

        }

        public async Task<IEnumerable<BookDto>> GetAllBooks(string sortBy, string sortOrder, string searchString,bool trackChanges)
        {
            var books = await _repositoryManager.BookRepository.GetAllBooks(trackChanges);

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString));
            }


            switch (sortBy)
            {
                case "Title":
                    books = sortOrder == "Title_Asc" ? books.OrderBy(b => b.Title) : books.OrderByDescending(b => b.Title);
                    break;
                case "PublishedYear":
                    books = sortOrder == "PublishedYear_Asc" ? books.OrderBy(b => b.PublishedYear) : books.OrderByDescending(b => b.PublishedYear);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);    
            return booksDto;
        }

        public async Task<BookDto> GetBook(Guid id, bool trackChanges)
        {
           var book = await _repositoryManager.BookRepository.GetBook(id, trackChanges);


           var bookDto = _mapper.Map<BookDto>(book);        

            return bookDto; 
        }


        public async Task<IEnumerable<AuthorDto>> GetBookAuthors(Guid id, bool trackChanges)
        {
            var authors = await _repositoryManager.AuthorRepository.GetAuthorsOfBook(id, trackChanges);

            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);

            return authorsDto;


        }

        public async Task<IEnumerable<CategoryDto>> GetBookCategories(Guid id, bool trackChanges)
        {
            var categories = await _repositoryManager.CategoryRepository.GetCategoryOfBooks(id, trackChanges);

            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return categoryDto;


        }

        public async Task<IEnumerable<PublisherDto>> GetBookPublishers(Guid id, bool trackChanges)
        {
            var publishers = await _repositoryManager.PublisherRepository.GetPublishersOfBook(id, trackChanges);

            var publishersDto = _mapper.Map<IEnumerable<PublisherDto>>(publishers); 

            return publishersDto;   
        }


        public async Task UpdateBook(
            BookDto bookDto,
            IEnumerable<Guid> authorIds,
            IEnumerable<Guid> publisherIds,
            IEnumerable<Guid> categoryIds,
            bool trackChanges)
        {
            var bookentity = await _repositoryManager.BookRepository.GetBook(bookDto.BookId, trackChanges);

            _mapper.Map(bookDto, bookentity);

            // Manage authors
            var currentAuthorIds = bookentity.Authors.Select(a => a.AuthorID).ToList();
            var authorsToRemove = bookentity.Authors.Where(a => !authorIds.Contains(a.AuthorID)).ToList();
            var authorIdsToAdd = authorIds.Except(currentAuthorIds).ToList();

            if (authorsToRemove.Any())
            {
                _repositoryManager.BookAuthorRepository.RemoveRange(authorsToRemove);
            }

            if (authorIdsToAdd.Any())
            {
                var authorsToAdd = await _repositoryManager.AuthorRepository.GetAuthorsById(authorIdsToAdd, false);
                foreach (var author in authorsToAdd)
                {
                    _repositoryManager.BookAuthorRepository.CreateBookAuthor(bookentity, author);
                }
            }

            // Manage publishers
            var currentPublisherIds = bookentity.Publishers.Select(p => p.PublisherId).ToList();
            var publishersToRemove = bookentity.Publishers.Where(p => !publisherIds.Contains(p.PublisherId)).ToList();
            var publisherIdsToAdd = publisherIds.Except(currentPublisherIds).ToList();

            if (publishersToRemove.Any())
            {
                _repositoryManager.BookPublisherRepository.RemoveRange(publishersToRemove);
            }

            if (publisherIdsToAdd.Any())
            {
                var publishersToAdd = await _repositoryManager.PublisherRepository.GetPublishersById(publisherIdsToAdd, false);
                foreach (var publisher in publishersToAdd)
                {
                    _repositoryManager.BookPublisherRepository.CreateBookPublisher(bookentity, publisher);
                }
            }


            //Manage Categories
            var currentCategories = bookentity.Categories.Select(c => c.CategoryId).ToList();
            var categoriesToRemove = bookentity.Categories.Where(c => !categoryIds.Contains(c.CategoryId)).ToList();
            var categoriesToAdd = categoryIds.Except(currentCategories).ToList();   


            if(categoriesToRemove.Any()) 
            {
                _repositoryManager.BookCategoryRepository.RemoveRange(categoriesToRemove);
            }

            if (categoriesToAdd.Any())
            {
                var categoriesToBeAdded = await _repositoryManager.CategoryRepository.GetCategoriesById(categoriesToAdd, false);
                foreach (var category in categoriesToBeAdded)
                {
                    _repositoryManager.BookCategoryRepository.CreateBookCategory(bookentity, category);
                }

            }



            await _repositoryManager.SaveAsync();
        }

    }
}
