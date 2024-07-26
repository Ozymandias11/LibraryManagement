using FluentResults;
using Library.Data.RequestFeatures;
using Library.Service.Dto.Library.Dto;

namespace Library.Service.Library.Interfaces
{
    public interface IAuthorService
    {
        Task<(IEnumerable<AuthorDto> authors, MetaData metaData)> GetAllAuthors( AuthorParameters authorParameters,bool trackChanges);
        Task<Result<AuthorDto>> GetAuthor(Guid id, bool trackChanges);  
        Task<Result> DeleteAuthor(Guid id, bool trackChanges);
        Task<Result> CreateAuthor(CreateAuthorDto author, bool trackChanges);
        Task<Result> UpdateAuthor(AuthorDto author, bool trackChanges);
    }
}
