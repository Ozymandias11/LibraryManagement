using Library.Data.Library.Interfaces;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class BookCategoryRepository : RepositoryBase<BookCategory>, IBookCategoryRepository
    {
        public BookCategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBookCategory(Book book, Category category)
        {
            var bookCategory = new BookCategory
            {
                BookId = book.BookId,
                CategoryId = category.CategoryId
            };

            Create(bookCategory);
        }

        public void RemoveRange(IEnumerable<BookCategory> bookCategories)
        {
            foreach (var bookCategory in bookCategories) 
            {
                Delete(bookCategory);
            }
        }
    }
}
