using AutoMapper;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace LibraryManagement.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public CategoryController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Categories(string sortBy, string sortOrder, string searchString)
        {
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewData["CurrentSearchString"] = searchString;

            var categories = await _serviceManager.CategoryService.GetAllCategories(sortBy, sortOrder, searchString,false);
            var categoryViewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return View(categoryViewModel);
        }

        public IActionResult CreateCategory()
        {
            var createCategoryViewModel = new CreateCategoryViewModel();
            return View(createCategoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            var createCategoryDto = _mapper.Map<CreateCategoryDto>(categoryViewModel);
            await _serviceManager.CategoryService.CreateCategory(createCategoryDto, false);
            return RedirectToAction("Categories");
        }

        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            var category = await _serviceManager.CategoryService.GetCategory(id, false);

            var categoryViewModel = new CategoryViewModel()
            {
                CategoryId = category.CategoryId,
                Title = category.Title
            };

            return View(categoryViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                View();
            }

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
