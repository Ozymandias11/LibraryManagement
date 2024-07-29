using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Library.Data.RequestFeatures;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using Library.Service.Library.Interfaces;
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
        private readonly IReportService _reportService;
        private readonly INotyfService _notyf;
        public ReservationController(IServiceManager serviceManager, IMapper mapper, IUserService userService, 
            IReportService reportService, INotyfService notyf)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _userService = userService;
            _reportService = reportService;
            _notyf = notyf;
            
        }

        public async Task<IActionResult> Reservations([FromQuery] ReservationParameters reservationParameters)
        {

            var (reservationsDto, metaData) = await _serviceManager.ReservationService.GetAllReservations(reservationParameters ,false);

            var reservationsViewModel = _mapper.Map<IEnumerable<ReservationViewModel>>(reservationsDto);

            var pagedViewModel = new PagedViewModel<ReservationViewModel>(reservationsViewModel, metaData);

            return View(pagedViewModel);

        }

        public IActionResult Createreservation()
        {
            var createReservationViewModel = new CreateReservationViewModel();  

            return View(createReservationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationViewModel createReservationViewModel)
        {
            var reservationDto = _mapper.Map<CreateReservationDto>(createReservationViewModel);

            var currentEmployee = await _userService.GetUserWithClaimsPrincipal(User);
            reservationDto.EmployeeId = currentEmployee.Id;

            var result = await _serviceManager.ReservationService.CreateReservation(reservationDto);

            if (result.IsFailed)
            {
                _notyf.Error("Something went wrong, please try again");
                return View(createReservationViewModel);

            }

            _notyf.Success("Reservation completed successfully");
            return RedirectToAction("Reservations");

        }

        public async Task<IActionResult> Details(Guid id, int page = 1)
        {
            ViewBag.CurrentPage = page;
            var reservation = await _serviceManager.ReservationService.GetReservation(id, false);
            var reservationViewModel = _mapper.Map<ReservationDetailsViewModel>(reservation.Value);

            return View(reservationViewModel);
        }


        public async Task<IActionResult> CheckBookCopyAvailability(Guid originalBookId, string edition, Guid PublisherId, int quantity)
        {
            var result = await _serviceManager.ReservationService.CheckBookCopyAvailability(originalBookId, edition, PublisherId, quantity);

            return Json(new { isAvailable = result.isAvailable, message = result.message });

        }


        [HttpPost]
        public async Task<IActionResult> ReturnBook( [FromBody]ReturnBookViewModel model)
        {
            var returnBookDto = _mapper.Map<ReturnBookDto>(model);
            await _serviceManager.ReservationService.ReturnBook(returnBookDto);
            return Json(new { success = true, message = "Books returned successfully" });

        }
        public async Task<IActionResult> GenerateReport(Guid id)
        {
            var reservationResult = await _serviceManager.ReservationService.GetReservation(id, false);
            var returnBookResult = await _serviceManager.ReservationService.GetReturnBookInfo(id, false);
            var reportContent = _reportService.GenerateReportContent(reservationResult.Value, returnBookResult.Value);   
            string fileName = $"Reservation_Report_{id}.pdf";
            return File(reportContent, "application/pdf", fileName);


        }
    }
}
