using AutoMapper;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using LibraryManagement.Migrations;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public AuthorController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Authors()
        {
            var authorDtos = await _serviceManager.AuthorService.GetAllAuthors(false);
            var authorsViewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(authorDtos);
            return View(authorsViewModel);

        }


        public IActionResult CreateAuthor()
        {
            var createAuthorViewModel = new CreateAuthorViewModel();

            return View(createAuthorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorViewModel createAuthorViewModel)
        {
            var createAuhtorDto = _mapper.Map<CreateAuthorDto>(createAuthorViewModel);

            await _serviceManager.AuthorService.CreateAuthor(createAuhtorDto, false);

            return RedirectToAction("Authors");

        }

        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await _serviceManager.AuthorService.DeleteAuthor(id, false);
            return RedirectToAction("Authors");
        }


    }

}



