using AutoMapper;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
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

            var reservations = await _serviceManager.ReservationService.GetAllReservations(sortBy, sortOrder, searchString, page, pageSize, false);
            var reservationsViewModel = _mapper.Map<IEnumerable<ReservationViewModel>>(reservations);

            foreach (var reservation in reservationsViewModel)
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

            if (result.IsSuccess)
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

        [HttpPost]

        public async Task<IActionResult> ReturnBook( [FromBody]ReturnBookViewModel model)
        {
            var returnBookDto = _mapper.Map<ReturnBookDto>(model);
            var result = await _serviceManager.ReservationService.ReturnBook(returnBookDto);
            return Json(new { success = true, message = "Books returned successfully" });

        }
        public async Task<IActionResult> GenerateReport(Guid id)
        {
            var reservationResult = await _serviceManager.ReservationService.GetReservation(id, false);
            var returnBookResult = await _serviceManager.ReservationService.GetReturnBookInfo(id, false);
            var reportContent = GenerateReportContent(reservationResult.Value, returnBookResult.Value);
            string fileName = $"Reservation_Report_{id}.pdf";
            return File(reportContent, "application/pdf", fileName);


        }

        private byte[] GenerateReportContent(ReservationDetailsDto reservation, ReturnBookDto returnBookInfo)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(ms))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);

                        document.Add(new Paragraph($"Reservation Report for ID: {reservation.ReservationId}")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(20));

                        document.Add(new Paragraph($"Customer: {reservation.CustomerFullName}"));
                        document.Add(new Paragraph($"Employee: {reservation.EmployeeFullName}"));
                        document.Add(new Paragraph($"Checkout Time: {reservation.CheckoutTime}"));
                        document.Add(new Paragraph($"Supposed Return Date: {reservation.SupposedReturnDate}"));
                        document.Add(new Paragraph($"Actual Return Date: {reservation.ActualReturnDate ?? DateTime.Now}"));
                        document.Add(new Paragraph($"Is Late: {(reservation.IsLate ? "Yes" : "No")}"));

                        Table reservationTable = new Table(6).UseAllAvailableWidth();
                        reservationTable.AddHeaderCell("Book Title");
                        reservationTable.AddHeaderCell("Edition");
                        reservationTable.AddHeaderCell("Publisher");
                        reservationTable.AddHeaderCell("Quantity");
                        reservationTable.AddHeaderCell("Return Date");
                        reservationTable.AddHeaderCell("Returned By");

                        foreach (var item in reservation.ReservationItems)
                        {
                            reservationTable.AddCell(item.BookTitle);
                            reservationTable.AddCell(item.Edition);
                            reservationTable.AddCell(item.PublisherName);
                            reservationTable.AddCell(item.Quantity.ToString());
                            reservationTable.AddCell(item.ActualReturnDate?.ToString("d") ?? "Not returned");
                            reservationTable.AddCell(item.ReturnCustomerId ?? "N/A");
                        }

                        document.Add(reservationTable);

                   
                        if (returnBookInfo != null && returnBookInfo.returnItems != null && returnBookInfo.returnItems.Any())
                        {
                            document.Add(new Paragraph("Return Book Information")
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFontSize(16)
                                .SetBold()
                                .SetPaddingTop(20));

                            Table returnTable = new Table(2).UseAllAvailableWidth();
                            returnTable.AddHeaderCell("Return Status");
                            returnTable.AddHeaderCell("Quantity");

                            foreach (var item in returnBookInfo.returnItems)
                            {
                                returnTable.AddCell(item.ReturnStatus ?? "N/A");
                                returnTable.AddCell(item.Quantity.ToString());
                            }

                            document.Add(returnTable);
                        }
                    }
                }

                return ms.ToArray();
            }
        }


    }
}
