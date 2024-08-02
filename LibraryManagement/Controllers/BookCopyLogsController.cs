using AutoMapper;
using Library.Data.RequestFeatures;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookCopyLogsController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public BookCopyLogsController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;   
        }


        public async Task<IActionResult> Index([FromQuery] BookCopyLogParameters parameters, Guid originalBookId, Guid publisherId, string edition)
        {
            var (bookCopyLogsDto, metaData) = await _serviceManager.BookCopyLogService.GetBookCopyLogs(parameters,originalBookId, publisherId, edition);
            
            var bookCopyLogsViewModel = _mapper.Map<IEnumerable<BookCopyLogsViewModel>>(bookCopyLogsDto);

            var pagedViewModel = new PagedViewModel<BookCopyLogsViewModel>(bookCopyLogsViewModel, metaData);

            return View(pagedViewModel);
        }

    }
}
