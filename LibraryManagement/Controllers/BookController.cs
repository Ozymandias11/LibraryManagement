using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Library.Data.RequestFeatures;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ActionFilters;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

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



            var updateBookViewModel = new UpdateBookViewModel()
            {
                BookId = book.BookId,
                Title = book.Title,
                PublishedYear = book.PublishedYear,
            };


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

   

    }
}
