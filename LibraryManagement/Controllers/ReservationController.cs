using AutoMapper;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Globalization;

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

        public async Task<IActionResult> Reservations(
            string sortBy,
            string sortOrder,
            string searchString,
            int page = 1,
            int pageSize = 10)
        {

            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.isPaginated = true;
            ViewData["CurrentSearchString"] = searchString;
            ViewData["CurrentPage"] = page;

            var reservations = await _serviceManager.ReservationService.GetAllReservations(sortBy, sortOrder, searchString, page, pageSize,false);
            var reservationsViewModel = _mapper.Map<IEnumerable<ReservationViewModel>>(reservations);

            foreach(var reservation in reservationsViewModel)
            {
                reservation.CurrentPage = page;
                reservation.PageSize = pageSize;
                reservation.TotalCount = reservations.Count();
            }
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

        public async Task<IActionResult> Details(Guid id)
        {
            var reservation = await _serviceManager.ReservationService.GetReservation(id, false);
            var reservationViewModel = _mapper.Map<ReservationDetailsViewModel>(reservation.Value);

            return View(reservationViewModel);
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

        public async Task<IActionResult> CheckBookCopyAvailability(Guid originalBookId, string edition, Guid PublisherId, int quantity)
        {
            var result = await _serviceManager.ReservationService.CheckBookCopyAvailability(originalBookId, edition, PublisherId, quantity);

            return Json(new { isAvailable = result.isAvailable, message = result.message });

        }

        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _serviceManager.CustomerService.GetAllCustomersUnfiltered(false);
            var customerViewModel = _mapper.Map<IEnumerable<CustomerDropDownViewModel>>(customers);
            return Json(customerViewModel);
        }


    }
}
