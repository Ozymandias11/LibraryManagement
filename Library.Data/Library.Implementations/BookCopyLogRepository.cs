using Library.Data.Library.Interfaces;
using Library.Data.RequestFeatures;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class BookCopyLogRepository : RepositoryBase<BookCopyLog>, IBookCopyLogRepository
    {
        public BookCopyLogRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBookCopy(BookCopyLog bookCopyLog) => Create(bookCopyLog);


        public async Task<PagedList<BookCopyLog>> GetBookCopyLogs(BookCopyLogParameters parameters,Guid originalBookId, Guid publisherId, string edition)
        {
            var bookCopyLogs = await FindByCondition(bcl =>
                        (bcl.OriginalBookId == originalBookId &&
                         bcl.PublishersId == publisherId &&
                         bcl.Edition == edition) &&
                        (!parameters.StartDate.HasValue || bcl.TimeStamp >= parameters.StartDate.Value) &&
                        (!parameters.EndDate.HasValue || bcl.TimeStamp <= parameters.EndDate.Value),
                        false).ToListAsync();

            return PagedList<BookCopyLog>
                .ToPagedList(bookCopyLogs, parameters.PageNumber, parameters.PageSize); 
        }


    }
}
