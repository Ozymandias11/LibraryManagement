using AutoMapper;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using Library.Service.Logging;
using LibraryManagement.ActionFilters;
using LibraryManagement.Migrations;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public AuthorController(
            IServiceManager serviceManager, 
            IMapper mapper,
             ILoggerManager loggermanager)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _loggerManager = loggermanager;
        }

        public async Task<IActionResult> Authors(string sortBy, string sortOrder, string searchString)
        {

            ViewBag.SortBy = sortBy;    
            ViewBag.SortOrder = sortOrder;
            ViewData["CurrentSearchString"] = searchString;

            var authorDtos = await _serviceManager.AuthorService.GetAllAuthors(sortBy, sortOrder, searchString,false);

            if (authorDtos.IsFailed)
            {
                _loggerManager.LogError($"Error getting all authors:  {string.Join(", ", authorDtos.Errors.Select(e => e.Message))}");
                return View("Error");
            }

            var authorsViewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(authorDtos.Value);
            return View(authorsViewModel);

        }


        public IActionResult CreateAuthor()
        {
            var createAuthorViewModel = new CreateAuthorViewModel();

            return View(createAuthorViewModel);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAuthor(CreateAuthorViewModel createAuthorViewModel)
        {
            var createAuhtorDto = _mapper.Map<CreateAuthorDto>(createAuthorViewModel);

            var result = await _serviceManager.AuthorService.CreateAuthor(createAuhtorDto, false);

            if (result.IsFailed)
            {
                var errorMessage = result.Errors.FirstOrDefault()?.Message ?? "An error Occured while creating Author";
                _loggerManager.LogError($"An error occured while createing author {errorMessage}");
                createAuthorViewModel.ErrorMessage = errorMessage;

                return View(createAuthorViewModel);
            }

            return RedirectToAction("Authors");

        }

        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await _serviceManager.AuthorService.DeleteAuthor(id, false);
            return RedirectToAction("Authors");
        }


        public async Task<IActionResult> UpdateAuthor(Guid id)
        {


            var author = await _serviceManager.AuthorService.GetAuthor(id, false);

            if (author.IsFailed)
            {
             
                _loggerManager.LogError($"The Author with id {id} was not found");
                return View("PageNotFound");
            }


            var authorViewModel = new AuthorViewModel()
            {
                AuthorId = author.Value.AuthorId,
                FirstName = author.Value.FirstName,
                LastName = author.Value.LastName,
                DateOfBirth = author.Value.DateOfBirth
            };

            return  View(authorViewModel);



        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAuthor(AuthorViewModel authorViewModel)
        {


            var authorDto = _mapper.Map<AuthorDto>(authorViewModel);
            await _serviceManager.AuthorService.UpdateAuthor(authorDto, true);

            TempData["SuccessMessage"] = "Author Updated Successfully";

            return View(authorViewModel);





        }




    }

}



