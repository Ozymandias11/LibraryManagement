using AutoMapper;
using Library.Data.RequestFeatures;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookCopyController : Controller
    {

        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public BookCopyController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> BookCopies([FromQuery] BookCopyParameters bookCopyParameters)
        {
            var (bookCopiesDto, metaData) = await _serviceManager.BookCopyService.GetAllBookCopies(bookCopyParameters, false);

            var bookCopyViewModel = _mapper.Map<IEnumerable<BookCopyViewModel>>(bookCopiesDto);

            var pagedViewModel = new PagedViewModel<BookCopyViewModel>(bookCopyViewModel, metaData);

            return View(pagedViewModel);


        }

        public IActionResult CreateBookCopy()
        {
            var createBookCopyViewModel = new CreateBookCopyViewModel();

            return View(createBookCopyViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBookCopy(CreateBookCopyViewModel createBookCopyViewModel)

        {

            var CreateBookCopyDto = _mapper.Map<CreateBookCopyDto>(createBookCopyViewModel);

            await _serviceManager.BookCopyService.CreateBookCopy(
                createBookCopyViewModel.SelectedBookId,
                createBookCopyViewModel.SelectedPublisherId,
                createBookCopyViewModel.SelectedShelfId,
                createBookCopyViewModel.SelectedRoomId,
                CreateBookCopyDto);

            return RedirectToAction("BookCopies");

        }

    }
}
