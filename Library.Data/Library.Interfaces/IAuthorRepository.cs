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
        Task<IEnumerable<Author>> GetAllAuthor(bool trackChanges);
        Task<Author?> GetAuthor(Guid id, bool trackChanges);  

        void DeleteAuthor(Author author);
        void CreateAuthor(Author author);
    }
}
    