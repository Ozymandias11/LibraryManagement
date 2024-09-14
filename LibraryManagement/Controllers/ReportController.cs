using Library.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;

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

        public async Task<IActionResult> PrintReport()
        {
            var renderFormat = "PDF";
            var extension = "pdf";
            var mimetype = "application/pdf";

            using var report = new LocalReport();

            await PopulateDataSets(report);

            var parameters = new[]
            {
                new ReportParameter("param1", "Customer Registration RDLC Report")
            };

            report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\Reports\\rptCustomerRegistration.rdlc";

            report.SetParameters(parameters);

            var pdf = report.Render(renderFormat);

            return File(pdf, mimetype, "report." + extension);

        }

        private async Task PopulateDataSets(LocalReport report)
        {

            var customerRegistrationDto = await _serviceManager.CustomerService.GetCustomerRegistrationsByYear(2024);
            var popularBooks = await _serviceManager.BookService.GetMonthlyReport(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31), "Books");

            report.DataSources.Add(new ReportDataSource("dsCustomerRegistartion", customerRegistrationDto));
            report.DataSources.Add(new ReportDataSource("dsPopularBooks", popularBooks));
        }
        
    }
}
