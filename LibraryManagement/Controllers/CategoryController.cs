using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Library.Data.RequestFeatures;
using Library.Model.Models;
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
        private readonly INotyfService _notyf;
        public CategoryController(IServiceManager serviceManager, IMapper mapper, INotyfService notyf)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Categories([FromQuery] CategoryParameters categoryParameters)
        {
            var (categoryDtos, metaData) = await _serviceManager.CategoryService.GetAllCategories(categoryParameters, false);

            var categoryViewModel = _mapper.Map<IEnumerable<CategoryViewModel>>(categoryDtos);

            var pagedViewModel = new PagedViewModel<CategoryViewModel>(categoryViewModel, metaData);

            return View(pagedViewModel);
        }

        public IActionResult CreateCategory()
        {
            var createCategoryViewModel = new CreateCategoryViewModel();
            return View(createCategoryViewModel);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel createCategoryViewModel)
        {

            var createCategoryDto = _mapper.Map<CreateCategoryDto>(createCategoryViewModel);

            var result = await _serviceManager.CategoryService.CreateCategory(createCategoryDto, false);

            if (result.IsFailed)
            {
                _notyf.Warning("Someting went wrong please try again");
                return View(createCategoryViewModel);
            }

            _notyf.Success("Category created successfully");
            return RedirectToAction("Categories");




        }

        public async Task<IActionResult> UpdateCategory(Guid id)
        {
            var result = await _serviceManager.CategoryService.GetCategory(id, false);

            if (result.IsFailed)
            {
                return View("PageNotFound");
            }


            var categoryViewModel = _mapper.Map<CategoryViewModel>(result.Value); 

            return View(categoryViewModel);

        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel model)
        {

            var categoryDto = _mapper.Map<CategoryDto>(model);

            var result =  await _serviceManager.CategoryService.UpdateCategory(categoryDto, true);

            if(result.IsFailed)
            {
                _notyf.Error("Category update has failed, please try again");
                return View(model);
            }

            _notyf.Success("Category updated successfully");
            return RedirectToAction("Categories");

        }
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var result = await _serviceManager.CategoryService.DeleteCategory(id, false);

            if (result.IsFailed)
            {
                _notyf.Warning("Something Went wrong please try again");
            }

            return RedirectToAction("Categories");
        }


        // below are methods used for populating dropowns
        public async Task<IActionResult> GetCategoriesForDropDown()
        {
            var categoriesDto = await _serviceManager.CategoryService.GetAllCategoriesForDropDown(false);
            return Json(categoriesDto.Select(c => new { id = c.CategoryId, name = c.Title}));
        }

        public async Task<IActionResult> GetBookCategories(Guid id)
        {
            var bookCategories = await _serviceManager.CategoryService.GetBookCategories(id, false);
            return Json(bookCategories.Select(c => c.CategoryId));
        }

    }
}
