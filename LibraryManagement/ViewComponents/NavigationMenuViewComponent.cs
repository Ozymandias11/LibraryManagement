

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

        // getting all of the users menu based on their role
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Console.WriteLine("ViewComponent");
            var items = await _dynamicMenuService.GetMenuItemsAsyncService(HttpContext.User);
            var itemsVM = _mapper.Map<List<NavigationMenuViewModel>>(items);
            var itemsViewModel = CreateViewModel(null, [.. itemsVM]);

            return View(itemsViewModel); 

        }

        private IEnumerable<NavigationMenuViewModel> CreateViewModel(Guid? parentId,IEnumerable<NavigationMenuViewModel> source)
        {
            return from item in source
                   where item.ParentMenuId == parentId
                   select new NavigationMenuViewModel
                   {
                       Id = item.Id,
                       Name = item.Name,
                       ControllerName = item.ControllerName,
                       ActionName = item.ActionName,
                       Children = CreateViewModel(item.Id, source)
                   };
        }

    }
}
