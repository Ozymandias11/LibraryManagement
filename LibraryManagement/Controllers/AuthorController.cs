using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Library.Data.RequestFeatures;
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
        private readonly INotyfService _notyf;
        public AuthorController(IServiceManager serviceManager, IMapper mapper, INotyfService notyf)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Authors([FromQuery] AuthorParameters authorParameters)
        {

            var (authorDtos, metaData) = await _serviceManager.AuthorService.GetAllAuthors(authorParameters, false);

            var authorsViewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(authorDtos);

            var pagedViewModel = new PagedViewModel<AuthorViewModel>(authorsViewModel, metaData);

            return View(pagedViewModel);

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
                _notyf.Warning("Something went wrong please try again");
                return View(createAuthorViewModel);
            }

            return RedirectToAction("Authors");

        }

        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var result = await _serviceManager.AuthorService.DeleteAuthor(id, false);

            if (result.IsFailed)
            {
                _notyf.Error("Something went wrong, please try again");
            }

            return RedirectToAction("Authors");
        }


        public async Task<IActionResult> UpdateAuthor(Guid id)
        {
            var result = await _serviceManager.AuthorService.GetAuthor(id, false);

            if (result.IsFailed)
            {
                return View("PageNotFound");
            }

            var authorViewModel = _mapper.Map<AuthorViewModel>(result.Value);

            return View(authorViewModel);
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateAuthor(AuthorViewModel model)
        {
            var authorDto = _mapper.Map<AuthorDto>(model);

            var result = await _serviceManager.AuthorService.UpdateAuthor(authorDto, true);

            if (result.IsFailed)
            {
                _notyf.Error("Updating Author has failed, please try again");
                return View(model);
            }

            _notyf.Success("Author updated successfully");
            return View(model);

        }

    }

}



