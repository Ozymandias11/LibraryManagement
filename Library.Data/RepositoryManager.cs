using Library.Data.Library.Implementations;
using Library.Data.Library.Interfaces;
using Library.Data.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{

    // Repository Manager is usefull for grouping multiple operations into a signe unit of work
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IAuthorRepository> _authroRepository;
        private readonly Lazy<IPublisherRepository> _publisherRepository;
       

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _authroRepository = new Lazy<IAuthorRepository>(() => new AuthorRepository(repositoryContext));
            _publisherRepository = new Lazy<IPublisherRepository>(() => new PublisherRepository(repositoryContext));    

        }

        public IAuthorRepository AuthorRepository => _authroRepository.Value;

        public IPublisherRepository PublisherRepository => _publisherRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
       
    }
}
