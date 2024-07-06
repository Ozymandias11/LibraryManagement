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

namespace Library.Service.Library.Implementations
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public BookCopyService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task CreateBookCopy(
            Guid originalBookId,
            Guid PublisherId, 
            Guid ShelfId,
            Guid RoomId,
            CreateBookCopyDto createBookCopyDto)
        {
            var BookCopies = Enumerable.Range(0, createBookCopyDto.Quantity)
                .Select(_ => new BookCopy
                {
                    NumberOfPages = createBookCopyDto.NumberOfPages,
                    Status = createBookCopyDto.Status,
                    Edition = createBookCopyDto.Edition,
                    Quantity = createBookCopyDto.Quantity,
                })
                .ToList();

            _repositoryManager.BookCopyRepository.AddBookCopies(originalBookId, PublisherId ,BookCopies);

            // temporary solution

            await _repositoryManager.SaveAsync();

            var shelf = await _repositoryManager.ShelfRepository.GetShelf(RoomId, ShelfId, false);

            foreach(var bookCopy in BookCopies)
            {
                _repositoryManager.BookShelfRepository.CreateBookCopyShelf(bookCopy, shelf);
            }

            

            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<BookCopyDto>> GetAllBookCopies(
            string sortBy, 
            string sortOrder,
            string searchString, 
            int page,
            int pageSize, 
            bool trackChanges)
        {
            var bookCopy = await _repositoryManager.BookCopyRepository.GetAllBookCopies(page, pageSize,trackChanges);

            if (!string.IsNullOrEmpty(searchString))
            {
                bookCopy = bookCopy.Where(bc => bc.OriginalBook.Title.Contains(searchString));
            }

            bookCopy = ApplySorting(bookCopy, sortBy, sortOrder);   

            var bookCopyDto = _mapper.Map<IEnumerable<BookCopyDto>>(bookCopy);

            foreach (var dto in bookCopyDto)
            {
                var bookCopyShelf = bookCopy.FirstOrDefault(bc => bc.BookCopyId == dto.BookCopyId)?.Shelves.FirstOrDefault();
                if (bookCopyShelf != null)
                {
                    dto.RoomId = bookCopyShelf.Shelf.Room.RoomId;
                    dto.RoomNumber = bookCopyShelf.Shelf.Room.RoomNumber;
                    dto.ShelfNumber  = bookCopyShelf.Shelf.ShelfNumber;
                    dto.ShelfId = bookCopyShelf.Shelf.ShelfId;
                }
            }


            return bookCopyDto;
        }

        public async Task<int> GetTotalBookCopiesCount() => await _repositoryManager.BookCopyRepository.GetTotalBookCopiesCount();

        private IEnumerable<BookCopy> ApplySorting(IEnumerable<BookCopy> bookCopies, string sortBy, string sortOrder)
        {
            return bookCopies = sortBy switch
            {
                "BookTitle" => sortOrder == "BookTitle_Asc" ? bookCopies.OrderBy(bc => bc.OriginalBook.Title) :
                                             bookCopies.OrderByDescending(bc => bc.OriginalBook.Title),
                "PublisherName" => sortOrder == "PublisherName_Asc" ? bookCopies.OrderBy(bc => bc.Publisher.PublisherName) :
                                             bookCopies.OrderByDescending(bc => bc.Publisher.PublisherName),
                "Edition" => sortOrder == "Edition_Asc" ? bookCopies.OrderBy(bc => bc.Edition) :
                                             bookCopies.OrderByDescending(bc => bc.Edition),
                _ => bookCopies.OrderBy(bc => bc.CreatedDate)
            };
        }
       
    }
}
