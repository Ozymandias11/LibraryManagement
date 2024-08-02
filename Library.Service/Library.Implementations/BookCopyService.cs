using AutoMapper;
using FluentResults;
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


        public async Task<Result> CreateBookCopy(CreateBookCopyDto dto)
        {
            var bookCopies = CreateBookCopies(dto);
            await AddBookCopiesToRepository(dto.SelectedBookId, dto.SelectedPublisherId, bookCopies);
            await AddBookCopiesToShelf(dto.SelectedRoomId, dto.SelectedShelfId, bookCopies);

            return Result.Ok();
        }

        public async Task<(IEnumerable<BookCopyDto> bookCopies, MetaData metaData)> GetAllBookCopies(BookCopyParameters bookCopyParameters, bool trackChanges)
        {
            var bookCopiesWithMetaData = await _repositoryManager.BookCopyRepository.GetAllBookCopies(bookCopyParameters, trackChanges);
            var bookCopyDtos = _mapper.Map<IEnumerable<BookCopyDto>>(bookCopiesWithMetaData);

            foreach (var dto in bookCopyDtos)
            {
                var bookCopy = bookCopiesWithMetaData.FirstOrDefault(bc => bc.BookCopyId == dto.BookCopyId);
                var bookCopyShelf = bookCopy?.Shelves?.FirstOrDefault();
                var shelf = bookCopyShelf?.Shelf;
                var room = shelf?.Room;

                if (room != null && shelf != null)
                {
                    dto.RoomId = room.RoomId;
                    dto.RoomNumber = room.RoomNumber;
                    dto.ShelfNumber = shelf.ShelfNumber;
                    dto.ShelfId = shelf.ShelfId;
                }
            }

            return (bookCopyDtos, bookCopiesWithMetaData.MetaData);
        }


        public async Task<Result> ModifyBookCopies(ModifyBookCopiesDto modifyBookCopiesDto)
        {
            if (modifyBookCopiesDto.State == "Added")
            {
                await AddBookCopies(modifyBookCopiesDto);

            }
            else if (modifyBookCopiesDto.State == "Deleted")
            {
                await DeleteBookCopies(modifyBookCopiesDto);
            }

            _repositoryManager.BookCopyLogRepository.CreateBookCopy(new BookCopyLog
            {
                OriginalBookId = modifyBookCopiesDto.OriginalBookId,
                PublishersId = modifyBookCopiesDto.PublisherId,
                Edition = modifyBookCopiesDto.Edition,
                State = modifyBookCopiesDto.State,
                Message = modifyBookCopiesDto.Message,
                QuantityModified = modifyBookCopiesDto.QuantityModified,
                TimeStamp = DateTime.Now
            });

            await _repositoryManager.SaveAsync();

            return Result.Ok();

        }

        private static List<BookCopy> CreateBookCopies(CreateBookCopyDto dto)
        {
            return Enumerable.Range(0, dto.Quantity)
                .Select(_ => new BookCopy
                {
                    NumberOfPages = dto.NumberOfPages,
                    Status = dto.Status,
                    Edition = dto.Edition,
                    Quantity = 1
                })
                .ToList();
        }


        private async Task AddBookCopiesToRepository(Guid bookId, Guid publisherId, List<BookCopy> bookCopies)
        {
            _repositoryManager.BookCopyRepository.AddBookCopies(bookId, publisherId, bookCopies);
            await _repositoryManager.SaveAsync();
        }

        private async Task AddBookCopiesToShelf(Guid roomId, Guid shelfId, List<BookCopy> bookCopies)
        {
            var shelf = await _repositoryManager.ShelfRepository.GetShelf(roomId, shelfId, false);

            foreach (var bookCopy in bookCopies)
            {
                _repositoryManager.BookShelfRepository.CreateBookCopyShelf(bookCopy, shelf);
            }
            await _repositoryManager.SaveAsync();
        }

        public async Task<int> GetTotalBookCopiesCount() => await _repositoryManager.BookCopyRepository.GetTotalBookCopiesCount();


        private async Task AddBookCopies(ModifyBookCopiesDto dto)
        {
            var createBookCopyDto = _mapper.Map<CreateBookCopyDto>(dto);

            var newCopies = CreateBookCopies(createBookCopyDto);

            await AddBookCopiesToRepository(dto.OriginalBookId, dto.PublisherId, newCopies);

            await AddBookCopiesToShelf(dto.RoomId, dto.ShelfId, newCopies);

        }

        private async Task DeleteBookCopies(ModifyBookCopiesDto dto)
        {
            var bookCopies = await _repositoryManager.BookCopyRepository.
                GetCustomNumberOfCopies(dto.OriginalBookId, dto.Edition, dto.PublisherId, dto.QuantityModified);

            foreach(var bookCopy in bookCopies)
            {
                _repositoryManager.BookCopyRepository.DeleteBookCopy(bookCopy);
            }

            await _repositoryManager.SaveAsync();

        }
    }
}
