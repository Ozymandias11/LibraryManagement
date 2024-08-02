using FluentResults;
using Library.Data.RequestFeatures;
using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
   public interface IBookCopyLogService
    {
        Task<(IEnumerable<BookCopyLogDto> bookCopyLogs, MetaData metaData)> GetBookCopyLogs(BookCopyLogParameters parameters,Guid originalBookId, Guid publisherId, string edition);
        Task<Result> CreateBookCopyLog(CreateBookCopyLogDto createBookCopyLogDto);
    }
}
