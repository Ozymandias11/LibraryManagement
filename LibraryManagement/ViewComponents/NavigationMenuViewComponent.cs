

using AutoMapper;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.ViewComponents
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IDynamicMenuService _dynamicMenuService;
        private readonly IMapper _mapper;
        public NavigationMenuViewComponent(IDynamicMenuService dynamicMenuService, IMapper mapper)
        {
            _dynamicMenuService = dynamicMenuService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Console.WriteLine("ViewComponent");
            var items = await _dynamicMenuService.GetMenuItemsAsyncService(HttpContext.User);
            var itemsViewModel = _mapper.Map<List<NavigationMenuViewModel>>(items);

            return View(itemsViewModel); 

        }

    }
}
