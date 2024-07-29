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

        public async Task<IActionResult> GetRoomShelvesForSelect2(Guid id)
        {
            var shelvesDto = await _serviceManager.ShelfService.GetShelves(id, false);
            return Json(shelvesDto.Select(s => new { id = s.ShelfId, name = s.ShelfNumber}));
        }

    }
}
