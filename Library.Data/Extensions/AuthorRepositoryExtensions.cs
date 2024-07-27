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
    public static class AuthorRepositoryExtensions
    {
        public static IQueryable<Author> Search(this IQueryable<Author> authors, string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return authors;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return authors.Where(a => a.FirstName.ToLower().Contains(lowerCaseSearchTerm) ||
                                   a.LastName.Contains(lowerCaseSearchTerm));

        }

        public static IQueryable<Author> Sort(this IQueryable<Author> authors, string? orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return authors.OrderBy(c => c.CreatedDate);
            }
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Customer>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return authors.OrderBy(c => c.CreatedDate);
            }

            return authors.OrderBy(orderQuery);




        }
    }
}
