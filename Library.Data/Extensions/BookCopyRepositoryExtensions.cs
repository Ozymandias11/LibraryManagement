using Library.Data.Extensions.Utility;
using Library.Model.Models;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Extensions
{
    public static class BookCopyRepositoryExtensions
    {
        public static IQueryable<BookCopy> Search(this IQueryable<BookCopy> bookCopies, string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return bookCopies;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return bookCopies.Where(bc => bc.OriginalBook.Title.ToLower().Contains(lowerCaseSearchTerm));
                                             
        }

        public static IQueryable<BookCopy> Sort(this IQueryable<BookCopy> bookCopies, string? orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return bookCopies.OrderBy(c => c.CreatedDate);
            }
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Customer>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return bookCopies.OrderBy(c => c.CreatedDate);
            }

            return bookCopies.OrderBy(orderQuery);




        }
    }
}
