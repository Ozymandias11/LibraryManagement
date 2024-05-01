using AutoMapper;
using Library.Data.Interfaces;
using Library.Model.Models;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public sealed class ServiceManager : IServiceManager
    {
       
        

        private readonly Lazy<IAuthenticationService> _authentificationService;



        public ServiceManager(IRepositoryManager repositoryManager, UserManager<Employee> usermanager, 
            IMapper mapper, SignInManager<Employee> signInManager)
        {
            _authentificationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(usermanager, mapper, signInManager));
            
        }
        public IAuthenticationService AuthenticationService => _authentificationService.Value;
    }
}
