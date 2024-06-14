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
        Task<IEnumerable<BookDto>> GetAllBooks(string sortBy, string sortOrder, string searchString,bool trackChanges);
        Task<BookDto> GetBook(Guid id,  bool trackChanges); 
        Task<IEnumerable<AuthorDto>> GetBookAuthors(Guid id, bool trackChanges); 
        Task<IEnumerable<PublisherDto>> GetBookPublishers(Guid id, bool trackChanges);
        Task<IEnumerable<CategoryDto>> GetBookCategories(Guid id, bool trackChanges);
        Task CreateBook(
            CreateBookDto book,
            IEnumerable<Guid> authroIds,
            IEnumerable<Guid> publishersIds,
            IEnumerable<Guid> categoryIds
            , bool trackChanges);
        Task UpdateBook(
            BookDto book,
            IEnumerable<Guid> authordIds,
            IEnumerable<Guid> publisherIds,
            IEnumerable<Guid> categoryIds,
            bool trackChanges);
        Task DeleteBook(Guid id, bool trackChanges);

    }
}
