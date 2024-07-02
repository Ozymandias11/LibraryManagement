using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
    public interface IBookCopyService
    {
        Task<IEnumerable<BookCopyDto>> GetAllBookCopies(
            string sortBy,
            string sortOrder,
            string searchString,
            int page,
            int pageSize,
            bool trackChanges);
        Task CreateBookCopy(
            Guid originalBookId,
            Guid PublisherId,
            CreateBookCopyDto createBookCopyDto);
        Task<int> GetTotalBookCopiesCount();

    }
}
