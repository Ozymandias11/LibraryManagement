using AutoMapper;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
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
        public async Task CreateCategory(CreateCategoryDto categoryDto, bool trackChanges)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);
            _repositoryManager.CategoryRepository.CreateCategory(categoryEntity);

            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteCategory(Guid id, bool trackChanges)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategory(id, trackChanges);

             _repositoryManager.CategoryRepository.DeleteCatgeory(category);

            await _repositoryManager.SaveAsync();   




        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories(
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

            switch (sortBy)
            {
                case "Title":
                    categories = sortOrder == "Title_Asc" ? categories.OrderBy(c => c.Title) : categories.OrderByDescending(c => c.Title);
                    break;
                default:
                    categories = categories.OrderBy(c => c.CreatedDate);
                    break;
            }


            var categoreisDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoreisDto;
        }

        public async Task<CategoryDto> GetCategory(Guid id, bool trackChanges)
        {
            var category = await _repositoryManager.CategoryRepository.GetCategory(id, trackChanges);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task UpdateCategory(CategoryDto categoryDto, bool trackChanges)
        {
            var categoryEntity = await _repositoryManager.CategoryRepository.GetCategory(categoryDto.CategoryId, trackChanges);
            _mapper.Map(categoryDto, categoryEntity);

            await _repositoryManager.SaveAsync();
        }
    }
}
