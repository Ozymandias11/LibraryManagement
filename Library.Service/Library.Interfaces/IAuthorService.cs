using FluentResults;
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
        Task<Result<IEnumerable<AuthorDto>>> GetAllAuthors(string sortBy, string sortOrder, string searchString,bool trackChanges);
        Task<Result<AuthorDto>> GetAuthor(Guid id, bool trackChanges);  
        Task<Result> DeleteAuthor(Guid id, bool trackChanges);
        Task<Result> CreateAuthor(CreateAuthorDto author, bool trackChanges);
        Task<Result> UpdateAuthor(AuthorDto author, bool trackChanges);
    }
}
