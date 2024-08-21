using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Library.Data.RequestFeatures;
using Library.Service.Dto.Library.Dto;
using Library.Service.Dto.Reports.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ActionFilters;
using LibraryManagement.Extensions;
using LibraryManagement.ViewModels.Library.ViewModels;
using LibraryManagement.ViewModels.Reports;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        public BookController(IServiceManager serviceManager, IMapper mapper, INotyfService notyf)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Books([FromQuery] BookParameters bookParameters)
        {
            var (booksDto, metaData) = await _serviceManager.BookService.GetAllBooks(bookParameters, false);

            var booksViewModel = _mapper.Map<IEnumerable<BookViewModel>>(booksDto);

            var pagedViewModel = new PagedViewModel<BookViewModel>(booksViewModel, metaData);

            return View(pagedViewModel);
        }

        public IActionResult CreateBook()
        {
            var createBookViewModel = new CreateBookViewModel();
            return View(createBookViewModel);
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateBook(CreateBookViewModel createBookViewModel)
        {
            var bookDto = _mapper.Map<CreateBookDto>(createBookViewModel);

            var result = await _serviceManager.BookService.CreateBook(bookDto, false);

            if (result.IsFailed)
            {
                _notyf.Warning("Something went wrong please try again");
                return View(createBookViewModel);
            }

            _notyf.Success("Book Created successfully");
            return RedirectToAction("Books");

        }

        public async Task<IActionResult> EditBook(Guid id)
        {
            var book = await _serviceManager.BookService.GetBook(id, false);

            var updateBookViewModel = _mapper.Map<UpdateBookViewModel>(book);   
        
            return View(updateBookViewModel);

        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> EditBook(UpdateBookViewModel updateBookViewModel)
        { 
            var bookDto = _mapper.Map<BookDto>(updateBookViewModel);

            var result = await _serviceManager.BookService.UpdateBook(bookDto, updateBookViewModel.SelectedAuthorIds, updateBookViewModel.SelectedPublisherIds,
                                                         updateBookViewModel.SelectedCategoryIds, true);

            if (result.IsFailed)
            {
                _notyf.Error("something went wrong please try again");
            }


            _notyf.Success("Book edited succesfully");
            return RedirectToAction("Books");

        }

        public async Task<IActionResult> DeleteBook(Guid id)
        {

           var result = await _serviceManager.BookService.DeleteBook(id, false);

            if (result.IsFailed)
            {
                _notyf.Warning("Something Went wrong please try again");
            }

            _notyf.Success("Book deleted successfully");
            return RedirectToAction("Books");



        }

        public IActionResult PopularityReport()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetPopularityReport(DateTime startDate, DateTime endDate, string reportType)
        {
            var genrePopularityDto = await _serviceManager.BookService.GetPopularityReport(startDate, endDate, reportType);

            var genrePopularityViewModel = _mapper.Map<IEnumerable<GenericRangeReportViewModel>>(genrePopularityDto);

            return PartialView("_RangeReportTable", genrePopularityViewModel);


        }

        [HttpGet]
        public async Task<IActionResult> GetAnnualReport(DateTime startDate, DateTime endDate, string reportType)
        {
            var monthlyReport = await _serviceManager.BookService.GetMonthlyReport(startDate, endDate, reportType);

            var monthlyReportViewModel = _mapper.Map<IEnumerable<GenericMonthlyReportViewModel>>(monthlyReport);

            return PartialView("_MonthlyReportTable", monthlyReportViewModel);


        }


        [HttpPost]
        public async Task<FileResult> ExportRangeReport(DateTime startDate, DateTime endDate, string reportType)
        {
            var popularityReportDto = await _serviceManager.BookService.GetPopularityReport(startDate, endDate, reportType);

            var fileContents = ExcelExporter.ExportToExcel(popularityReportDto, $"Popularity by {reportType}");

            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"PopularityBy_{reportType}.xlsx");

        }

        [HttpPost]
        public async Task<FileResult> ExportAnnualReport(DateTime startDate, DateTime endDate, string reportType)
        {
            var monthlyReportDto = await _serviceManager.BookService.GetMonthlyReport(startDate, endDate, reportType);

            var fileContents = ExcelExporter.ExportToExcel(monthlyReportDto, $"Annual Report {startDate}, {endDate} - {reportType}");

            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"AnnualReport_{startDate}_{endDate}_{reportType}.xlsx");

        }

      

        public IActionResult LostBooksReport()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAnnualLostBooksReport(DateTime startDate, DateTime endDate, string reportType)
        {
            var lostBooksDto = await _serviceManager.BookService.GetMonhtlyLostBooksReport(startDate, endDate, reportType); 

            var lostBooksViewModel = _mapper.Map<IEnumerable<GenericMonthlyReportViewModel>>(lostBooksDto);

            return PartialView("_MonthlyReportTable", lostBooksViewModel);


        }

        [HttpGet]
        public async Task<IActionResult> GetLostBooksReport(DateTime startDate, DateTime endDate, string reportType)
        {
            var lostBooksDto = await _serviceManager.BookService.GetLostBooksReport(startDate, endDate, reportType);

            var lostBooksViewModel = _mapper.Map<IEnumerable<GenericRangeReportViewModel>>(lostBooksDto);

            return PartialView("_RangeReportTable", lostBooksViewModel);


        }

        [HttpPost]
        public async Task<FileResult> ExportAnnualLostBooksReport(DateTime startDate, DateTime endDate, string reportType)
        {
            var lostBooksDto = await _serviceManager.BookService.GetMonhtlyLostBooksReport(startDate, endDate, reportType);

            var fileContents = ExcelExporter.ExportToExcel(lostBooksDto, $"Annual Report {startDate}, {endDate} - {reportType}");

            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"AnnualReport_{startDate}_{endDate}_{reportType}.xlsx");

        }
        [HttpPost]
        public async Task<FileResult> ExportRangeLostBooksReport(DateTime startDate, DateTime endDate, string reportType)
        {
            var lostBooksDto = await _serviceManager.BookService.GetLostBooksReport(startDate, endDate, reportType);

            var fileContents = ExcelExporter.ExportToExcel(lostBooksDto, $"Annual Report {startDate}, {endDate} - {reportType}");

            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"AnnualReport_{startDate}_{endDate}_{reportType}.xlsx");

        }



        // below are methods used for populating dropdowns

        public async Task<IActionResult> GetBooksForDropDown()
        {
            var booksDto = await _serviceManager.BookService.GetAllBooksForDropDown(false);

            return Json(booksDto.Select(b => new { id = b.BookId, name = b.Title }));

            
        }




    }
}
