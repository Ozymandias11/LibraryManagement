using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface IBookCopyRepository
    {
        Task<IEnumerable<BookCopy>> GetAllBookCopies(bool trackChanges);
        Task<BookCopy?> GetBookCopy(Guid id, bool trackChanges);
        
        void AddBookCopies(
            Guid originalBookId,
            Guid PublisherId,
            IEnumerable<BookCopy> bookCopies);
        void DeleteBookCopy(BookCopy bookCopy);

    }
}
