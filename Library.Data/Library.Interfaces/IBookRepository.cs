using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks(bool trackChanges);
        Task<Book?> GetBook(Guid id, bool trackChanges);
        void CreateBook(Book book);
        void DeleteBook(Book book);
    }
}
