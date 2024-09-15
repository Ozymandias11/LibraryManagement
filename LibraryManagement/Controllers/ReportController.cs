using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;

namespace LibraryManagement.Controllers
{
    public class ReportController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IServiceManager serviceManager, IWebHostEnvironment webHostEnvironment)
        {
            _serviceManager = serviceManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult RDLCReport()
        {
            return View();
        }
        public async Task<IActionResult> PrintReport(DateTime startDate, DateTime endDate)
        {
            var renderFormat = "PDF";
            var extension = "pdf";
            var mimetype = "application/pdf";

            using var report = new LocalReport();

            await PopulateDataSets(report, startDate, endDate);

            var parameters = new[]
            {
                new ReportParameter("param1", "Library Management RDLC Report")
            };

            report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptCustomerRegistration.rdlc";

            report.SetParameters(parameters);

            var pdf = report.Render(renderFormat);

            return File(pdf, mimetype, "report." + extension);

        }

        private async Task PopulateDataSets(LocalReport report, DateTime startDate, DateTime endDate)
        {

            var customerRegistrationDto = await _serviceManager.CustomerService.GetCustomerRegistrationsByYear(2024);
            var popularBooks = await _serviceManager.BookService.GetMonthlyReport(startDate, endDate, "Books");
            var lostBooks = await _serviceManager.BookService.GetMonhtlyLostBooksReport(startDate, endDate, "Books");

            report.DataSources.Add(new ReportDataSource("dsCustomerRegistartion", customerRegistrationDto));
            report.DataSources.Add(new ReportDataSource("dsPopularBooks", popularBooks));
            report.DataSources.Add(new ReportDataSource("dsLostBooks", lostBooks));
        }
        
    }
}
