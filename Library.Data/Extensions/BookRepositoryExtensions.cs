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
   public static class BookRepositoryExtensions
    {

        public static IQueryable<Book> Search(this IQueryable<Book> books, string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return books;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return books.Where(b => b.Title.ToLower().Contains(lowerCaseSearchTerm));

        }

        public static IQueryable<Book> Sort(this IQueryable<Book> books, string? orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return books.OrderBy(c => c.CreatedDate);
            }
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return books.OrderBy(c => c.CreatedDate);
            }

            return books.OrderBy(orderQuery);




        }

    }
}
