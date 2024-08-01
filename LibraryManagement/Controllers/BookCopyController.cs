using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notyf;
        public BookCopyController(IServiceManager serviceManager, IMapper mapper, INotyfService notyf)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _notyf = notyf;
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

            var result = await _serviceManager.BookCopyService.CreateBookCopy(CreateBookCopyDto);

            if (result.IsFailed)
            {
                _notyf.Error("Soemting went wrong please try again");
            }

            _notyf.Success("Copies created successfully");
            return RedirectToAction("BookCopies");

        }
        [HttpPost]
        public async Task<IActionResult> ModifyBookCopies(ModifyBookCopiesViewModel modifyBookCopiesViewModel)
        {
            var modifyBookCopiesDto = _mapper.Map<ModifyBookCopiesDto>(modifyBookCopiesViewModel);  

            var result = await _serviceManager.BookCopyService.ModifyBookCopies(modifyBookCopiesDto);   

            if (result.IsFailed)
            {
                _notyf.Error("Something went wrong please try again");
                return RedirectToAction("BookCopies");
            }

            _notyf.Success($"Book Copies {modifyBookCopiesViewModel.State.ToLower()} successfully");
            return RedirectToAction("BookCopies");

        }

    }
}
