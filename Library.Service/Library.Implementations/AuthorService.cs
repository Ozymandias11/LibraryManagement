using AutoMapper;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
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

        public async Task DeleteAuthor(Guid id, bool trackChanges)
        {
            var Author = await _repositoryManager.AuthorRepository.GetAuthor(id, false);


            _repositoryManager.AuthorRepository.DeleteAuthor(Author);

            await _repositoryManager.SaveAsync();



        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthors(bool trackChanges)
        {
            var authors = await _repositoryManager.AuthorRepository.GetAllAuthor(trackChanges);
            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);  
            return authorsDto;
        }

        public async Task<AuthorDto> GetAuthor(Guid id, bool trackChanges)
        {
            var author = await _repositoryManager.AuthorRepository.GetAuthor(id, trackChanges);

            var authorDto = _mapper.Map<AuthorDto>(author);

            return authorDto;


        }

        public async Task CreateAuthor (CreateAuthorDto author, bool trackChanges)
        {
            var authorEntity = _mapper.Map<Author>(author);

            _repositoryManager.AuthorRepository.CreateAuthor(authorEntity);

            await _repositoryManager.SaveAsync();

        }

        public async Task UpdateAuthor(AuthorDto author, bool trackChanges)
        {
            var authorEntity = await _repositoryManager.AuthorRepository.GetAuthor(author.AuthorId, trackChanges);

            _mapper.Map(author, authorEntity);

            await _repositoryManager.SaveAsync();


        }
    }
}
