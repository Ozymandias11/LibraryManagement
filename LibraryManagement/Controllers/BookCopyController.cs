using AutoMapper;
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

        public async Task<IActionResult> BookCopies() 
        {
            var bookCopies = await _serviceManager.BookCopyService.GetAllBookCopies(false);
            var bookCopyViewModel =  _mapper.Map<IEnumerable<BookCopyViewModel>>(bookCopies);

            return View(bookCopyViewModel);


        }

        public async Task<IActionResult> CreateBookCopy()
        {
            var bookDtos = await _serviceManager.BookService.GetAllBooks("", "", "", false);
            var bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(bookDtos);

            var roomDtos = await _serviceManager.RoomService.GetallRooms(false);
            var roomViewModel = _mapper.Map<IEnumerable<RoomViewModel>>(roomDtos);


            var createBookCopyViewModel = new CreateBookCopyViewModel
            {

                Books = bookViewModels,
                Rooms = roomViewModel
            };

            return View(createBookCopyViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBookCopy(CreateBookCopyViewModel createBookCopyViewModel)
            
        {

            var CreateBookCopyDto = _mapper.Map<CreateBookCopyDto>(createBookCopyViewModel);

            await _serviceManager.BookCopyService.CreateBookCopy(
                createBookCopyViewModel.SelectedBookId,
                createBookCopyViewModel.SelectedPublisherId,
                CreateBookCopyDto);

            return View();

        }


        public async Task<IActionResult> GetPublishersForBook(Guid bookId)
        {
            var bookPublishers = await _serviceManager.BookService.GetBookPublishers(bookId, false);

            return Json(bookPublishers);

        }

        public async Task<IActionResult> GetShelvesForRoom(Guid roomId)
        {
            var roomShelves = await _serviceManager.ShelfService.GetShelves(roomId, false);
            return Json(roomShelves);
        }


        public IActionResult GetPublishersUrl()
        {
            return Json(Url.Action("GetPublishersForBook", "BookCopy"));
        }

        public IActionResult GetShelvesUrl()
        {
            return Json(Url.Action("GetShelvesForRoom", "BookCopy"));
        }

    }
}
