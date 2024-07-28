using AutoMapper;
using Library.Data.NewFolder;
using Library.Data.RequestFeatures;
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

        public async Task<(IEnumerable<BookCopyDto> bookCopies, MetaData metaData)> GetAllBookCopies(BookCopyParameters bookCopyParameters, bool trackChanges)
        {
            var bookCopy = await _repositoryManager.BookCopyRepository.GetAllBookCopies(bookCopyParameters ,trackChanges);
 

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


            return (bookCopyDto, bookCopy.MetaData);
        }

        public async Task<int> GetTotalBookCopiesCount() => await _repositoryManager.BookCopyRepository.GetTotalBookCopiesCount();
    }
}
