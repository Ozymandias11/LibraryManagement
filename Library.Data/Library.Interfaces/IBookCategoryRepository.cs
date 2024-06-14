using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface IBookCategoryRepository
    {
        void CreateBookCategory(Book book, Category category);
        void RemoveRange(IEnumerable<BookCategory> bookCategories);
    }
}
