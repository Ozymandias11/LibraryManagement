using AutoMapper;
using FluentResults;
using Library.Data.Library.Interfaces;
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
    public class BookCopyLogService : IBookCopyLogService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public BookCopyLogService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Result> CreateBookCopyLog(CreateBookCopyLogDto createBookCopyLogDto)
        {
            var bookcopyLog = _mapper.Map<BookCopyLog>(createBookCopyLogDto);

            bookcopyLog.TimeStamp = DateTime.Now;   

            _repositoryManager.BookCopyLogRepository.CreateBookCopy(bookcopyLog);

            await _repositoryManager.SaveAsync();

            return Result.Ok();

        }

        public async Task<(IEnumerable<BookCopyLogDto> bookCopyLogs, MetaData metaData)> GetBookCopyLogs(BookCopyLogParameters parameters,Guid originalBookId, Guid publisherId, string edition)
        {
            var bookCopyLogsWithMetaData = await _repositoryManager.BookCopyLogRepository.GetBookCopyLogs(parameters,originalBookId, publisherId, edition);

            var bookCopyLogsDto = _mapper.Map<IEnumerable<BookCopyLogDto>>(bookCopyLogsWithMetaData);

            return (bookCopyLogsDto, bookCopyLogsWithMetaData.MetaData);

        }
    }
}
