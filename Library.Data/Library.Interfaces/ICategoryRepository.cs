using Library.Data.RequestFeatures;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface ICategoryRepository
    {
        Task<PagedList<Category>> GetAllCategories(CategoryParameters categoryParameters, bool trackChanges);
        Task<IEnumerable<Category>> GetAllCategoriesForDropDown(bool trackChanges);
        Task<Category?> GetCategory(Guid id, bool trackChanges);
        Task<Category?> GetCatgeoryByTitle(string title, bool trackChanges);    
        Task<IEnumerable<Category>> GetCategoryOfBooks(Guid id, bool trackChanges); 
        Task<IEnumerable<Category>> GetCategoriesById(IEnumerable<Guid> ids, bool trackChanges);
        void CreateCategory(Category category);
        void DeleteCatgeory(Category category);
    }
}
