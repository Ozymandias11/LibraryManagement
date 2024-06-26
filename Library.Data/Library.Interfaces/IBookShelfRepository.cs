using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
   public interface IBookShelfRepository
    {
        void CreateBookCopyShelf(BookCopy bookCopy, Shelf shelf);
    }
}
