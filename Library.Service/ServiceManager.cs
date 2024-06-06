using AutoMapper;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Interfaces;
using Library.Service.Library.Implementations;
using Library.Service.Library.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace Library.Service
{
    public sealed class ServiceManager : IServiceManager
    {
       
        

        private readonly Lazy<IAuthentificationService> _authentificationService;
        private readonly Lazy<IAuthorService> _authorService;
        private readonly Lazy<IPublisherService> _publisherService;



        public ServiceManager( IRepositoryManager reposiotryManager, UserManager<Employee> usermanager, 
            IMapper mapper, SignInManager<Employee> signInManager)
        {
            _authentificationService = new Lazy<IAuthentificationService>(() => new AuthenticationService(usermanager, mapper, signInManager));
            _authorService = new Lazy<IAuthorService>(() => new AuthorService(reposiotryManager, mapper));
            _publisherService = new Lazy<IPublisherService>(() => new PublisherService(reposiotryManager, mapper));



        }
        public IAuthentificationService AuthenticationService => _authentificationService.Value;

        public IAuthorService AuthorService => _authorService.Value;

        public IPublisherService PublisherService => _publisherService.Value;
    }
}
