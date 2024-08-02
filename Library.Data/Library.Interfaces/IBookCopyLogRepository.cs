using Library.Data.RequestFeatures;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface IBookCopyLogRepository
    {
        Task<PagedList<BookCopyLog>> GetBookCopyLogs(BookCopyLogParameters bookCopyLogParameters,Guid originalBookId, Guid PublisherId, string edition);
        void CreateBookCopy(BookCopyLog bookCopyLog);   
    }
}
