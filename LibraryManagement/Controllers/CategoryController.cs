using AutoMapper;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using Library.Service.Logging;
using LibraryManagement.ActionFilters;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace LibraryManagement.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public CategoryController(
            IServiceManager serviceManager, 
            IMapper mapper,
            ILoggerManager loggerManager)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _loggerManager = loggerManager; 
        }

        public async Task<IActionResult> Categories(string sortBy, string sortOrder, string searchString)
        {
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewData["CurrentSearchString"] = searchString;

            var categories = await _serviceManager.CategoryService.GetAllCategories(sortBy, sortOrder, searchString,false);

            if (categories.IsFailed)
            {
                _loggerManager.LogError($"Error getting all Categories:  {string.Join(", ", categories.Errors.Select(e => e.Message))}");
                return View("Error");   
            }

            var categoryViewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categories.Value);
            return View(categoryViewModel);
        }

        public IActionResult CreateCategory()
        {
            var createCategoryViewModel = new CreateCategoryViewModel();
            return View(createCategoryViewModel);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel categoryViewModel)
        {

            var createCategoryDto = _mapper.Map<CreateCategoryDto>(categoryViewModel);

            var result = await _serviceManager.CategoryService.CreateCategory(createCategoryDto, false);

            if (result.IsFailed)
            {
                var errorMessage = result.Errors.FirstOrDefault()?.Message ?? "An error Occured while creating Catgeory";
                _loggerManager.LogError($"An error occured while createing Category {errorMessage}");
                categoryViewModel.ErrorMessage = errorMessage;
            }

            return RedirectToAction("Categories");
        }

        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            var category = await _serviceManager.CategoryService.GetCategory(id, false);

            if (category.IsFailed)
            {
                _loggerManager.LogError($"The category with id {id} was not found");
            }

            var categoryViewModel = new CategoryViewModel()
            {
                CategoryId = category.Value.CategoryId,
                Title = category.Value.Title
            };

            return View(categoryViewModel);

        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel categoryViewModel)
        {

            var categoryDto = _mapper.Map<CategoryDto>(categoryViewModel);

            await _serviceManager.CategoryService.UpdateCategory(categoryDto, true);

            return RedirectToAction("Categories");

        }
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _serviceManager.CategoryService.DeleteCategory(id, false);
            return RedirectToAction("Categories");
        }

    }
}
