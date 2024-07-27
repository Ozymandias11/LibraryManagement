using FluentResults;
using Library.Data.RequestFeatures;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
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
        Task<BookDto> GetBook(Guid id,  bool trackChanges); 
        Task<Result> CreateBook(CreateBookDto book, bool trackChanges);
        Task<Result> UpdateBook(BookDto book, IEnumerable<Guid> authordIds, IEnumerable<Guid> publisherIds, IEnumerable<Guid> categoryIds,
            bool trackChanges);   
        Task<Result> DeleteBook(Guid id, bool trackChanges);

    }
}
