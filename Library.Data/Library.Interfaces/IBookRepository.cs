﻿using Library.Data.RequestFeatures;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface IBookRepository
    {
        Task<PagedList<Book>> GetAllBooks(BookParameters bookParameters ,bool trackChanges);
        Task<IEnumerable<Book>> GetBooksForDropDown(bool trackChanges); 
        Task<Book?> GetBook(Guid id, bool trackChanges);
        void CreateBook(Book book);
        void DeleteBook(Book book);
    }
}
