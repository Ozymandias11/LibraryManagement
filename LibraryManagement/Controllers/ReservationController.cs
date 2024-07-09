using AutoMapper;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public ReservationController(IServiceManager serviceManager, IMapper mapper, IUserService userService)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _userService = userService;
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

            //get all books for dropdown
            var booksForDropDown = await _serviceManager.BookService.GetAllBooks("", "", "", false);
            //get all customers for dropdown
            var customersForDropDown = await _serviceManager.CustomerService.GetAllCustomersUnfiltered(false);



            var customersForDropDownViewModel = _mapper.Map<IEnumerable<CustomerDropDownViewModel>>(customersForDropDown);
            var booksForDropDownViewModel = _mapper.Map<IEnumerable<BookDropdownViewModel>>(booksForDropDown);

            createReservationViewModel.AllBooks = booksForDropDownViewModel;
            createReservationViewModel.AllCustomers = customersForDropDownViewModel;    
            
            return View(createReservationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationViewModel createReservationViewModel)
        {
            var reservationDto = _mapper.Map<CreateReservationDto>(createReservationViewModel);

            var currentEmployee = await _userService.GetUserWithClaimsPrincipal(User);
            reservationDto.EmployeeId = currentEmployee.Id;

            var result = await _serviceManager.ReservationService.CreateReservation(reservationDto);

            if(result.IsSuccess)
            {
                return RedirectToAction("Reservations");
            }

            return View(createReservationViewModel);

        }



        public async Task<IActionResult> GetPublishersForBook(string bookId)
        {
            if (Guid.TryParse(bookId, out Guid parsedBookId))
            {
                var publishers = await _serviceManager.BookService.GetBookPublishers(parsedBookId, false);
                return Json(publishers);
            }
            return BadRequest("Invalid book ID");
        }




    }
}
