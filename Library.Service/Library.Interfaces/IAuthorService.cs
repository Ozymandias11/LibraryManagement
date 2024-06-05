using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthors(bool trackChanges);
        Task<AuthorDto> GetAuthor(Guid id, bool trackChanges);  
        Task DeleteAuthor(Guid id, bool trackChanges);
        Task CreateAuthor(CreateAuthorDto author, bool trackChanges);
    }
}
