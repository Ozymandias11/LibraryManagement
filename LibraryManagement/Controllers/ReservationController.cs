using AutoMapper;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public ReservationController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Reservations()
        {
            var reservations = await _serviceManager.ReservationService.GetAllReservations(false);
            var reservationsViewModel = _mapper.Map<IEnumerable<ReservationViewModel>>(reservations);
            return View(reservationsViewModel); 

        } 

        public async Task<IActionResult> CreateReservation()
        {
            var createReservationViewModel = new CreateReservationViewModel();

            var booksForDropDown = await _serviceManager.BookService.GetAllBooks("", "", "", false);
            var booksForDropDownViewModel = _mapper.Map<IEnumerable<BookDropdownViewModel>>(booksForDropDown);

            createReservationViewModel.AllBooks = booksForDropDownViewModel;
            
            return View(createReservationViewModel);
        }


        public async Task<IActionResult> GetPublishersForBook(Guid bookId)
        {
            var publishers = await _serviceManager.BookService.GetBookPublishers(bookId, false);
            return Json(publishers);
        }
      



    }
}
