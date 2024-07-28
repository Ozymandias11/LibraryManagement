using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ShelfController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public ShelfController(IServiceManager serviceManager, IMapper mapper, INotyfService notyf)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetShelvesForDropDown(Guid bookId)
        {
            var shelvesDto = await _serviceManager.ShelfService.GetShelves(bookId, false);

            return Json(shelvesDto);

        }

    }
}
