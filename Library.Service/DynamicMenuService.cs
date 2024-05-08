using AutoMapper;
using Library.Data.Interfaces;
using Library.Service.Dto;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service
{
    public class DynamicMenuService : IDynamicMenuService
    {

        private readonly IDynamicMenuRepository _repository;
        private readonly IMapper _mapper;
        public DynamicMenuService(IDynamicMenuRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;   
        }

        public async Task<List<NavigationMenuDto>> GetMenuItemsAsyncService(ClaimsPrincipal claimsPrincipal)
        {
            Console.WriteLine("Service");
            var data = await _repository.GetMenuItemsAsync(claimsPrincipal);
            
            var dataDto = _mapper.Map<List<NavigationMenuDto>>(data);

            return dataDto;

        }
    }
}
