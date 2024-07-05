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

        public async Task<IActionResult> BookCopies(
            string sortBy,
            string sortOrder,
            string searchString, 
            int page = 1,
            int pageSize = 2) 
        {

            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.isPaginated = true;
            ViewData["CurrentSearchString"] = searchString;
            ViewData["CurrentPage"] = page;

            var bookCopies = await _serviceManager.BookCopyService.GetAllBookCopies(
                sortBy,
                sortOrder,
                searchString
                ,page,
                pageSize
                ,false);

            var bookCopyViewModel =  _mapper.Map<IEnumerable<BookCopyViewModel>>(bookCopies);

            foreach(var bookCopyviewModel_1 in bookCopyViewModel)
            {
                bookCopyviewModel_1.CurrentPage = page;
                bookCopyviewModel_1.PageSize = pageSize;
                bookCopyviewModel_1.TotalCount = await _serviceManager.BookCopyService.GetTotalBookCopiesCount();
            }


         

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
                createBookCopyViewModel.SelectedShelfId,
                createBookCopyViewModel.SelectedRoomId,
                CreateBookCopyDto);

            return RedirectToAction("BookCopies");

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
