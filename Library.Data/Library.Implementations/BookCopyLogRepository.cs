using Library.Data.Library.Interfaces;
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


        public async Task<IEnumerable<BookCopyLog>> GetBookCopyLogs(Guid originalBookId, Guid PublisherId, string edition)
            => await FindByCondition(bcl => bcl.OriginalBookId == originalBookId ||
                                            bcl.PublishersId == PublisherId ||
                                            bcl.Edition == edition, false)
                                   .ToListAsync();


    }
}
