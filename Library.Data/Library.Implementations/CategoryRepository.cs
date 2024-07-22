﻿using Library.Data.Library.Interfaces;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCategory(Category category) => Create(category);
       

        public void DeleteCatgeory(Category category) => Delete(category);


        public async Task<IEnumerable<Category>> GetAllCategories(bool trackChanges) =>
            await FindByCondition(c => c.DeletedDate == null, trackChanges).ToListAsync();

        public async Task<IEnumerable<Category>> GetCategoriesById(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(c => ids.Contains(c.CategoryId), trackChanges).ToListAsync();
      

        public async Task<Category?> GetCategory(Guid id, bool trackChanges) => 
            await FindByCondition(c => c.CategoryId == id, trackChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Category>> GetCategoryOfBooks(Guid id, bool trackChanges) => 
            await FindByCondition(c => c.BookCategories.Any(bc => bc.BookId == id), false).ToListAsync();

        public async Task<Category?> GetCatgeoryByTitle(string title, bool trackChanges)
            => await FindByCondition(c => c.Title.ToLower() ==  title.ToLower(), trackChanges).FirstOrDefaultAsync();
        
    }
}
