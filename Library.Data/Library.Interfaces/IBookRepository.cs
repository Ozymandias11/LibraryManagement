using Library.Data.RequestFeatures;
using Library.Model.Helpers;
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
        Task<PagedList<Book>> GetAllBooks(BookParameters bookParameters ,bool trackChanges);
        Task<IEnumerable<Book>> GetBooksForDropDown(bool trackChanges); 
        Task<Book?> GetBook(Guid id, bool trackChanges);
        Task<IEnumerable<PopularityReport>> GetPopularityReport(DateTime startDate, DateTime endDate, string reportType);
        Task<IEnumerable<MonthlyReport>> GetMonthlyReport(DateTime startDate, DateTime endDate, string reportType);
        void CreateBook(Book book);
        void DeleteBook(Book book);
    }
}
