using FluentResults;
using Library.Data.RequestFeatures;
using Library.Model.Helpers;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Dto.Reports.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
    public interface IBookService
    {
        Task<(IEnumerable<BookDto> books, MetaData metaData)> GetAllBooks(BookParameters bookParameters ,bool trackChanges);
        Task<IEnumerable<BookDto>> GetAllBooksForDropDown(bool trackChanges);
        Task<BookDto> GetBook(Guid id,  bool trackChanges); 
        Task<Result> CreateBook(CreateBookDto book, bool trackChanges);
        Task<Result> UpdateBook(BookDto book, IEnumerable<Guid> authordIds, IEnumerable<Guid> publisherIds, IEnumerable<Guid> categoryIds,
            bool trackChanges);   
        Task<Result> DeleteBook(Guid id, bool trackChanges);
        Task<IEnumerable<PopularityReportDto>> GetPopularityReport(DateTime startDate, DateTime endDate, string reportType);

    }
}
