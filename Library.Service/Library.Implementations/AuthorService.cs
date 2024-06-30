using AutoMapper;
using FluentResults;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Errors.NotFoundError;
using Library.Service.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{

    public class AuthorService : IAuthorService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;


        public AuthorService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Result> DeleteAuthor(Guid id, bool trackChanges)
        {
            var Author = await _repositoryManager.AuthorRepository.GetAuthor(id, false);

            if(Author == null)
            {
                return Result.Fail(new NotFoundError("Author", id));
            }


            _repositoryManager.AuthorRepository.DeleteAuthor(Author);

            await _repositoryManager.SaveAsync();

            return Result.Ok();


        }

        public async Task<Result<IEnumerable<AuthorDto>>> GetAllAuthors(
            string sortBy,
            string sortOrder,
            string searchString,
            bool trackChanges)
        {
            var authors = await _repositoryManager.AuthorRepository.GetAllAuthor(trackChanges);


            if (!string.IsNullOrEmpty(searchString))
            {
                authors = authors.Where(a => a.FirstName.Contains(searchString) ||
                                         a.LastName.Contains(searchString));
            }

            authors = ApplySorting(authors, sortBy, sortOrder); 



            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);  
            return Result.Ok(authorsDto);
        }

        public async Task<Result<AuthorDto>> GetAuthor(Guid id, bool trackChanges)
        {
            var author = await _repositoryManager.AuthorRepository.GetAuthor(id, trackChanges);

            if(author == null)
            {
                return Result.Fail(new NotFoundError("Author", id));
            }


            var authorDto = _mapper.Map<AuthorDto>(author);

            return authorDto;


        }

        public async Task<Result> CreateAuthor (CreateAuthorDto author, bool trackChanges)
        {
            var authorEntity = _mapper.Map<Author>(author);

            _repositoryManager.AuthorRepository.CreateAuthor(authorEntity);

            await _repositoryManager.SaveAsync();

            return Result.Ok();

        }

        public async Task<Result> UpdateAuthor(AuthorDto author, bool trackChanges)
        {
            var authorEntity = await _repositoryManager.AuthorRepository.GetAuthor(author.AuthorId, trackChanges);

            if(authorEntity == null )
            {
                return Result.Fail(new NotFoundError("Author", author.AuthorId));
            }

            _mapper.Map(author, authorEntity);

            await _repositoryManager.SaveAsync();

            return Result.Ok(); 


        }

        private IEnumerable<Author> ApplySorting(IEnumerable<Author> authors, string sortBy, string sortOrder)
        {

            return authors = sortBy switch
            {
                "FirstName" => sortOrder == "FirstName_Asc" ? authors.OrderBy(a => a.FirstName) : authors.OrderByDescending(a => a.FirstName),
                "LastName" => sortOrder == "LastName_Asc" ? authors.OrderBy(a => a.LastName) : authors.OrderByDescending(a => a.LastName),
                "DateOfBirth" => sortOrder == "DateOfBirth_Asc" ? authors.OrderBy(a => a.DateOfBirth) : authors.OrderByDescending(a => a.DateOfBirth),
                _ => authors.OrderBy(a => a.CreatedDate),
            };
        }
    }
}
