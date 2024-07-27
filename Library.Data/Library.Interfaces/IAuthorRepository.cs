using Library.Data.RequestFeatures;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface IAuthorRepository
    {
        Task<PagedList<Author>> GetAllAuthor(AuthorParameters authorParameters ,bool trackChanges);
        Task<IEnumerable<Author>> GetAllAuthorsForDropDown(bool trackChanges);  
        Task<Author?> GetAuthor(Guid id, bool trackChanges);
        Task<IEnumerable<Author>> GetAuthorsOfBook(Guid id, bool trackChanges);
        Task<IEnumerable<Author>> GetAuthorsById(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteAuthor(Author author);
        void CreateAuthor(Author author);
    }
}
    