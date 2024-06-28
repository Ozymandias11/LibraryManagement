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
        Task<IEnumerable<CategoryDto>> GetAllCategories(
            string sortBy, 
            string sortOrder, 
            string searchString, 
            bool trackChanges);
        Task<CategoryDto> GetCategory(Guid id, bool trackChanges); 
        Task CreateCategory(CreateCategoryDto categoryDto, bool trackChanges);
        Task DeleteCategory(Guid id, bool trackChanges);
        Task UpdateCategory(CategoryDto categoryDto, bool trackChanges);
    }
}
