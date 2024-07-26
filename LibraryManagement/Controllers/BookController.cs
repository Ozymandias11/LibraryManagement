//using AutoMapper;
//using Library.Service.Dto.Library.Dto;
//using Library.Service.Interfaces;
//using LibraryManagement.ViewModels.Library.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace LibraryManagement.Controllers
//{
//    public class BookController : Controller
//    {
//        private readonly IServiceManager _serviceManager;
//        private readonly IMapper _mapper;
//        public BookController(IServiceManager serviceManager, IMapper mapper)
//        {
//            _serviceManager = serviceManager;
//            _mapper = mapper;
//        }

//        public async Task<IActionResult> Books(string sortBy, string sortOrder, string searchString)
//        {

//            ViewBag.SortBy = sortBy;    
//            ViewBag.SortOrder = sortOrder;
//            ViewData["CurrentSearchString"] = searchString;

//            var booksDto = await _serviceManager.BookService.GetAllBooks(sortBy, sortOrder, searchString,false);
//            var booksViewModel = _mapper.Map<IEnumerable<BookViewModel>>(booksDto); 
//            return View(booksViewModel);    
//        }

//        public async Task<IActionResult> CreateBook()
//        {
//            return View(await PopulateCreateBookViewModel());
//        }


//        [HttpPost]
//        public async Task<IActionResult> CreateBook(CreateBookViewModel createBookViewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                createBookViewModel = await PopulateCreateBookViewModel();
//                return View(createBookViewModel);
//            }


//            var selectedAuthorIds = createBookViewModel.SelectedAuthorIds;
//            var SelectedPublisherIds = createBookViewModel.SelectedPublisherIds;
//            var SelctedCategoryIds = createBookViewModel.SelectedCategoryIds;

//            var bookDto = _mapper.Map<CreateBookDto>(createBookViewModel);

//            await _serviceManager.BookService.CreateBook(
//                bookDto,
//                selectedAuthorIds,
//                SelectedPublisherIds, 
//                SelctedCategoryIds,
//                false);

//            return RedirectToAction("Books");


//        }

//        public async Task<IActionResult> EditBook(Guid id)
//        {
//            var book = await _serviceManager.BookService.GetBook(id, false);


//            //fetching data to display all possible options

//            var AuthorDto = await _serviceManager.AuthorService.GetAllAuthors("","","",false);
//            var authorViewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(AuthorDto.Value);


//            var publisherDto = await _serviceManager.PublisherService.GetAllPublishers("", "", "", false);
//            var publisherViewModel = _mapper.Map<IEnumerable<PublisherViewModel>>(publisherDto.Value);


//            var categoryDto = await _serviceManager.CategoryService.GetAllCategories("", "", "", false);
//            var categoryViewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryDto.Value);

//            //Get Current authors, publishers and categories
//            var bookAuthors = await _serviceManager.BookService.GetBookAuthors(id, false);
//            var bookPublishers = await _serviceManager.BookService.GetBookPublishers(id, false);
//            var bookCategories = await _serviceManager.BookService.GetBookCategories(id, false);


//            var updateBookViewModel = new UpdateBookViewModel()
//            {
//                BookId = book.BookId,
//                Title = book.Title,
//                PublishedYear = book.PublishedYear,
//                Authors = authorViewModel,
//                Publishers = publisherViewModel,
//                Categories = categoryViewModel,
//                // can also be done with automapper
//                SelectedAuthorIds = bookAuthors.Select(ba => ba.AuthorId),
//                SelectedPublisherIds = bookPublishers.Select(ba => ba.PublisherId),
//                SelectedCategoryIds = bookCategories.Select(bc => bc.CategoryId),
//            };


//            return View(updateBookViewModel);

//        }

//        [HttpPost]
//        public async Task<IActionResult> EditBook(UpdateBookViewModel updateBookViewModel)
//        {
//            if (!ModelState.IsValid)
//            {
//                // Repopulate dropdowns
//                var AuthorDto = await _serviceManager.AuthorService.GetAllAuthors("", "", "", false);
//                updateBookViewModel.Authors = _mapper.Map<IEnumerable<AuthorViewModel>>(AuthorDto.Value);

//                var publisherDto = await _serviceManager.PublisherService.GetAllPublishers("", "", "", false);
//                updateBookViewModel.Publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(publisherDto.Value);

//                var categoryDto = await _serviceManager.CategoryService.GetAllCategories("", "", "", false);
//                updateBookViewModel.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryDto.Value);

//                // Get current selections (if any)
//                var bookAuthors = await _serviceManager.BookService.GetBookAuthors(updateBookViewModel.BookId, false);
//                var bookPublishers = await _serviceManager.BookService.GetBookPublishers(updateBookViewModel.BookId, false);
//                var bookCategories = await _serviceManager.BookService.GetBookCategories(updateBookViewModel.BookId, false);

//                updateBookViewModel.SelectedAuthorIds = bookAuthors.Select(ba => ba.AuthorId);
//                updateBookViewModel.SelectedPublisherIds = bookPublishers.Select(ba => ba.PublisherId);
//                updateBookViewModel.SelectedCategoryIds = bookCategories.Select(bc => bc.CategoryId);

//                return View(updateBookViewModel);
//            }

//            var bookDto = _mapper.Map<BookDto>(updateBookViewModel);

//            await _serviceManager.BookService.UpdateBook(bookDto,
//                updateBookViewModel.SelectedAuthorIds,
//                updateBookViewModel.SelectedPublisherIds,
//                updateBookViewModel.SelectedCategoryIds,
//                true);

//            TempData["SuccessMessage"] = "Book Updated Successfully";

//            return RedirectToAction("Books");

//        }

//        public async Task<IActionResult> DeleteBook(Guid id)
//        {
//            await _serviceManager.BookService.DeleteBook(id, false);
//            TempData["DeleteSuccessMessage"] = "Book Deleted Successfully";
//            return RedirectToAction("Books");

           

//        }

//        private async Task<CreateBookViewModel> PopulateCreateBookViewModel(CreateBookViewModel model = null)
//        {
//            var authorDto = await _serviceManager.AuthorService.GetAllAuthors("", "", "", false);
//            var publisherDto = await _serviceManager.PublisherService.GetAllPublishers("", "", "", false);
//            var categoryDto = await _serviceManager.CategoryService.GetAllCategories("", "", "", false);

//            var viewModel = model ?? new CreateBookViewModel();
//            viewModel.Authors = _mapper.Map<IEnumerable<AuthorViewModel>>(authorDto.Value);
//            viewModel.Publishers = _mapper.Map<IEnumerable<PublisherViewModel>>(publisherDto.Value);
//            viewModel.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryDto.Value);

//            return viewModel;
//        }


//    }
//}
