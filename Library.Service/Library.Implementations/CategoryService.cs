using AutoMapper;
using FluentResults;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Errors.NotFoundError;
using Library.Service.Library.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CategoryService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager  = repositoryManager;    
            _mapper = mapper;
        }
        public async Task<Result> CreateCategory(CreateCategoryDto categoryDto, bool trackChanges)
        {
            var existingCategory = await _repositoryManager.CategoryRepository.GetCatgeoryByTitle(categoryDto.Title, trackChanges);

            if (existingCategory != null)
            {
                return Result.Fail($"A category with title {categoryDto.Title} already exists");
            }


            var categoryEntity = _mapper.Map<Category>(categoryDto);
            _repositoryManager.CategoryRepository.CreateCategory(categoryEntity);

            await _repositoryManager.SaveAsync();

            return Result.Ok();
        }

        public async Task<Result> DeleteCategory(Guid id, bool trackChanges)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategory(id, trackChanges);

            if(category == null)
            {
                return Result.Fail(new NotFoundError("Category", id));
            }

             _repositoryManager.CategoryRepository.DeleteCatgeory(category);

            await _repositoryManager.SaveAsync();

            return Result.Ok();




        }

        public async Task<Result<IEnumerable<CategoryDto>>> GetAllCategories(
            string sortBy, 
            string sortOrder, 
            string searchString,
            bool trackChanges)
        {
            var categories = await _repositoryManager.CategoryRepository.GetAllCategories(trackChanges);

            if(!string.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(c => c.Title.Contains(searchString));
            }

            categories = ApplySorting(categories, sortBy, sortOrder);


            var categoreisDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Result.Ok(categoreisDto);
        }

        public async Task<Result<CategoryDto>> GetCategory(Guid id, bool trackChanges)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategory(id, trackChanges);

            if(category == null)
            {
                return Result.Fail(new NotFoundError("Category", id));
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<Result> UpdateCategory(CategoryDto categoryDto, bool trackChanges)
        {
            var categoryEntity = await _repositoryManager.CategoryRepository.GetCategory(categoryDto.CategoryId, trackChanges);

            if(categoryEntity == null)
            {
                return Result.Fail(new NotFoundError("Category",  categoryDto.CategoryId)); 
            }


            _mapper.Map(categoryDto, categoryEntity);

            await _repositoryManager.SaveAsync();

            return Result.Ok();
        }

        private IEnumerable<Category> ApplySorting(IEnumerable<Category> categories, string sortBy, string sortOrder)
        {
            return categories = sortBy switch
            {
                "Title" => sortOrder == "Title_Asc" ? categories.OrderBy(c => c.Title) : categories.OrderByDescending(c => c.Title),
                _ => categories.OrderBy(c => c.CreatedDate),
            };
        }
    }
}
