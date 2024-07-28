using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Web.Helpers;

namespace LibraryManagement.Controllers
{
    public class RoomController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public RoomController(IServiceManager serviceManager, IMapper mapper, INotyfService notyf)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetRoomsForDropDown()
        {
            var roomsDto = await _serviceManager.RoomService.GetallRooms(false);
            return Json(roomsDto.Select(r => new { id = r.RoomId, name = r.RoomNumber }));
        }
    }
}
