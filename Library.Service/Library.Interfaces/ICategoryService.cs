using FluentResults;
using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
   public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryDto>>> GetAllCategories(
            string sortBy, 
            string sortOrder, 
            string searchString, 
            bool trackChanges);
        Task<Result<CategoryDto>> GetCategory(Guid id, bool trackChanges); 
        Task<Result> CreateCategory(CreateCategoryDto categoryDto, bool trackChanges);
        Task<Result> DeleteCategory(Guid id, bool trackChanges);
        Task<Result> UpdateCategory(CategoryDto categoryDto, bool trackChanges);
    }
}
