using FluentResults;
using Library.Data.RequestFeatures;
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
        Task<(IEnumerable<CategoryDto> categories, MetaData metaData)> GetAllCategories(CategoryParameters categoryParameters, bool trackChanges);
        Task<IEnumerable<CategoryDto>> GetAllCategoriesForDropDown(bool trackChanges);
        Task<IEnumerable<CategoryDto>> GetBookCategories(Guid bookId, bool trackChanges);
        Task<Result<CategoryDto>> GetCategory(Guid id, bool trackChanges); 
        Task<Result> CreateCategory(CreateCategoryDto categoryDto, bool trackChanges);
        Task<Result> DeleteCategory(Guid id, bool trackChanges);
        Task<Result> UpdateCategory(CategoryDto categoryDto, bool trackChanges);
    }
}
