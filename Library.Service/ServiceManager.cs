using AutoMapper;
using Library.Model.Models;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace Library.Service
{
    public sealed class ServiceManager : IServiceManager
    {
       
        

        private readonly Lazy<IAuthentificationService> _authentificationService;



        public ServiceManager(UserManager<Employee> usermanager, 
            IMapper mapper, SignInManager<Employee> signInManager)
        {
            _authentificationService = new Lazy<IAuthentificationService>(() => new AuthenticationService(usermanager, mapper, signInManager));
            
        }
        public IAuthentificationService AuthenticationService => _authentificationService.Value;
    }
}
