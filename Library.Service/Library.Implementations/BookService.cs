using AutoMapper;
using FluentResults;
using Library.Data.NewFolder;
using Library.Data.RequestFeatures;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Dto.Reports.Dto;
using Library.Service.Errors.NotFoundError;
using Library.Service.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        public async Task<Result> CreateBook(CreateBookDto bookDto, bool trackChanges)
        {

            
            var bookEntity = _mapper.Map<Book>(bookDto);
           _repositoryManager.BookRepository.CreateBook(bookEntity);

            var result = await AddRelatedEntities(bookEntity, bookDto);
            if (!result.IsSuccess)
            {
                return result;
            }

            await _repositoryManager.SaveAsync();

            return Result.Ok();
          
            
        }


        public async Task<Result> DeleteBook(Guid id, bool trackChanges)
        {
            var book = await _repositoryManager.BookRepository.GetBook(id, trackChanges);

            if(book == null)
            {
                return Result.Fail(new NotFoundError("Book", id));
            }


            _repositoryManager.BookRepository.DeleteBook(book);

            await _repositoryManager.SaveAsync();

            return Result.Ok();

        }

        public async Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllBooks(BookParameters bookParameters ,bool trackChanges)
        {
            var booksWithMetaData = await _repositoryManager.BookRepository.GetAllBooks(bookParameters ,trackChanges);

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);   
            
            return (booksDto, booksWithMetaData.MetaData);
        }


        public async Task<IEnumerable<BookDto>> GetAllBooksForDropDown(bool trackChanges)
        {
            var books = await _repositoryManager.BookRepository.GetBooksForDropDown(trackChanges);

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);


            return booksDto;


        }

        public async Task<BookDto> GetBook(Guid id, bool trackChanges)
        {
           var book = await _repositoryManager.BookRepository.GetBook(id, trackChanges);


           var bookDto = _mapper.Map<BookDto>(book);        

            return bookDto; 
        }


        public async Task<Result> UpdateBook(BookDto bookDto, IEnumerable<Guid> authorIds, IEnumerable<Guid> publisherIds, IEnumerable<Guid> categoryIds,
            bool trackChanges)
        {

                var bookEntity = await _repositoryManager.BookRepository.GetBook(bookDto.BookId, trackChanges);
                if (bookEntity == null)
                {
                    return Result.Fail("book not found");
                }

                _mapper.Map(bookDto, bookEntity);

                await UpdateRelatedEntities(bookEntity, authorIds, publisherIds, categoryIds);

                await _repositoryManager.SaveAsync();

                return Result.Ok();
            }




        public async Task<IEnumerable<PopularityReportDto>> GetPopularityReport(DateTime startDate, DateTime endDate, string reportType)
        {
            var genrePopularity = await _repositoryManager.BookRepository.GetPopularityReport(startDate, endDate, reportType);

            var genrePopularityDto = _mapper.Map<IEnumerable<PopularityReportDto>> (genrePopularity);

            return genrePopularityDto;

        }


        private async Task UpdateRelatedEntities(Book bookEntity, IEnumerable<Guid> authorIds, IEnumerable<Guid> publisherIds, IEnumerable<Guid> categoryIds)
        {
            await UpdateAuthors(bookEntity, authorIds);
            await UpdatePublishers(bookEntity, publisherIds);
            await UpdateCategories(bookEntity, categoryIds);
        }
        private async Task<Result> AddRelatedEntities(Book bookEntity, CreateBookDto bookDto)
        {
            var authorResult = await AddAuthors(bookEntity, bookDto.SelectedAuthorIds);
            if (!authorResult.IsSuccess) return authorResult;

            var publisherResult = await AddPublishers(bookEntity, bookDto.SelectedPublisherIds);
            if (!publisherResult.IsSuccess) return publisherResult;

            var categoryResult = await AddCategories(bookEntity, bookDto.SelectedCategoryIds);
            if (!categoryResult.IsSuccess) return categoryResult;

            return Result.Ok();
        }


        private async Task UpdateAuthors(Book bookEntity, IEnumerable<Guid> authorIds)
        {
            var currentAuthorIds = bookEntity.Authors?.Select(a => a.AuthorID).ToHashSet() ?? new HashSet<Guid>();
            var newAuthorIds = authorIds.ToHashSet();

            var authorsToRemove = bookEntity.Authors?.Where(a => !newAuthorIds.Contains(a.AuthorID)).ToList() ?? new List<BookAuthor>();
            var authorIdsToAdd = newAuthorIds.Except(currentAuthorIds).ToList();

            _repositoryManager.BookAuthorRepository.RemoveRange(authorsToRemove);

            if (authorIdsToAdd.Any())
            {
                var authorsToAdd = await _repositoryManager.AuthorRepository.GetAuthorsById(authorIdsToAdd, false);
                foreach (var author in authorsToAdd)
                {
                    _repositoryManager.BookAuthorRepository.CreateBookAuthor(bookEntity, author);
                }
            }
        }

        private async Task UpdatePublishers(Book bookEntity, IEnumerable<Guid> publisherIds)
        {
            var currentPublisherIds = bookEntity.Publishers?.Select(p => p.PublisherId).ToHashSet() ?? new HashSet<Guid>();
            var newPublisherIds = publisherIds.ToHashSet();

            var publishersToRemove = bookEntity.Publishers?.Where(p => !newPublisherIds.Contains(p.PublisherId)).ToList() ?? new List<BookPublisher>();
            var publisherIdsToAdd = newPublisherIds.Except(currentPublisherIds).ToList();

            _repositoryManager.BookPublisherRepository.RemoveRange(publishersToRemove);

            if (publisherIdsToAdd.Any())
            {
                var publishersToAdd = await _repositoryManager.PublisherRepository.GetPublishersById(publisherIdsToAdd, false);
                foreach (var publisher in publishersToAdd)
                {
                    _repositoryManager.BookPublisherRepository.CreateBookPublisher(bookEntity, publisher);
                }
            }
        }

        private async Task UpdateCategories(Book bookEntity, IEnumerable<Guid> categoryIds)
        {
            var currentCategoryIds = bookEntity.Categories?.Select(c => c.CategoryId).ToHashSet() ?? new HashSet<Guid>();
            var newCategoryIds = categoryIds.ToHashSet();

            var categoriesToRemove = bookEntity.Categories?.Where(c => !newCategoryIds.Contains(c.CategoryId)).ToList() ?? new List<BookCategory>();
            var categoryIdsToAdd = newCategoryIds.Except(currentCategoryIds).ToList();

            _repositoryManager.BookCategoryRepository.RemoveRange(categoriesToRemove);

            if (categoryIdsToAdd.Any())
            {
                var categoriesToAdd = await _repositoryManager.CategoryRepository.GetCategoriesById(categoryIdsToAdd, false);
                foreach (var category in categoriesToAdd)
                {
                    _repositoryManager.BookCategoryRepository.CreateBookCategory(bookEntity, category);
                }
            }
        }


        private async Task<Result> AddAuthors(Book bookEntity, IEnumerable<Guid> authorIds)
        {
            foreach (var authorId in authorIds)
            {
                var author = await _repositoryManager.AuthorRepository.GetAuthor(authorId, false);
                if (author == null)
                {
                    return Result.Fail($"Author with ID {authorId} not found.");
                }
                _repositoryManager.BookAuthorRepository.CreateBookAuthor(bookEntity, author);
            }
            return Result.Ok();
        }

        private async Task<Result> AddPublishers(Book bookEntity, IEnumerable<Guid> publisherIds)
        {
            foreach (var publisherId in publisherIds)
            {
                var publisher = await _repositoryManager.PublisherRepository.GetPublisher(publisherId, false);
                if (publisher == null)
                {
                    return Result.Fail($"Publisher with ID {publisherId} not found.");
                }
                _repositoryManager.BookPublisherRepository.CreateBookPublisher(bookEntity, publisher);
            }
            return Result.Ok();
        }

        private async Task<Result> AddCategories(Book bookEntity, IEnumerable<Guid> categoryIds)
        {
            foreach (var categoryId in categoryIds)
            {
                var category = await _repositoryManager.CategoryRepository.GetCategory(categoryId, false);
                if (category == null)
                {
                    return Result.Fail($"Category with ID {categoryId} not found.");
                }
                _repositoryManager.BookCategoryRepository.CreateBookCategory(bookEntity, category);
            }
            return Result.Ok();
        }

      
    }
}

