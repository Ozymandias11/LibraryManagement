using AutoMapper;
using FluentResults;
using Library.Data.NewFolder;
using Library.Data.RequestFeatures;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Library.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vonage.Voice.EventWebhooks;

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

            var allocateShelfResult = await AddBookCopiesToShelf(dto.SelectedRoomId, dto.SelectedShelfId, bookCopies);

            if (allocateShelfResult.IsFailed)
            {
                return Result.Fail(allocateShelfResult.Errors);
            }

            var addingBookCopiesResult = AddBookCopiesToRepository(allocateShelfResult.Value, dto.SelectedBookId, dto.SelectedPublisherId, bookCopies);

            if (addingBookCopiesResult.IsFailed)
            {
                return Result.Fail(addingBookCopiesResult.Errors);
            }

            await _repositoryManager.SaveAsync();

            return Result.Ok();
        }

        public async Task<(IEnumerable<BookCopyDto> bookCopies, MetaData metaData)> GetAllBookCopies(BookCopyParameters bookCopyParameters, bool trackChanges)
        {
            var bookCopiesWithMetaData = await _repositoryManager.BookCopyRepository.GetAllBookCopies(bookCopyParameters, trackChanges);

            var bookCopyDtos = _mapper.Map<IEnumerable<BookCopyDto>>(bookCopiesWithMetaData);

            return (bookCopyDtos, bookCopiesWithMetaData.MetaData);
        }


        public async Task<Result> ModifyBookCopies(ModifyBookCopiesDto modifyBookCopiesDto)
        {
            if (modifyBookCopiesDto.State == "Added")
            {
                 AddBookCopies(modifyBookCopiesDto);

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


        private Result AddBookCopiesToRepository(Guid bookShelfId ,Guid bookId, Guid publisherId, List<BookCopy> bookCopies)
        {
            try
            {
                _repositoryManager.BookCopyRepository.AddBookCopies(bookShelfId,bookId, publisherId, bookCopies);
                return Result.Ok();
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private async Task<Result<Guid>> AddBookCopiesToShelf(Guid roomId, Guid shelfId, List<BookCopy> bookCopies)
        {
            try
            {
                var shelf = await _repositoryManager.ShelfRepository.GetShelf(roomId, shelfId, false);

                var bookShelfId = _repositoryManager.BookShelfRepository.CreateBookCopyShelf(bookCopies, shelf);

                return Result.Ok(bookShelfId);

            }catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        
           
        }

        private Result AddBookCopies(ModifyBookCopiesDto dto)
        {
            var createBookCopyDto = _mapper.Map<CreateBookCopyDto>(dto);

            var newCopies = CreateBookCopies(createBookCopyDto);

            var createBookCopiesResult = AddBookCopiesToRepository(dto.BookCopyShelfId ,dto.OriginalBookId, dto.PublisherId, newCopies);

            if (createBookCopiesResult.IsFailed)
            {
                return Result.Fail(createBookCopiesResult.Errors);
            }

            return Result.Ok();

        }

        private async Task DeleteBookCopies(ModifyBookCopiesDto dto)
        {
            var bookCopies = await _repositoryManager.BookCopyRepository.
                GetCustomNumberOfCopies(dto.OriginalBookId, dto.Edition, dto.PublisherId, dto.QuantityModified);

            foreach(var bookCopy in bookCopies)
            {
                _repositoryManager.BookCopyRepository.DeleteBookCopy(bookCopy);
            }
        }

        public async Task<int> GetTotalBookCopiesCount() => await _repositoryManager.BookCopyRepository.GetTotalBookCopiesCount();
    }
}
