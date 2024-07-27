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
    public static class CategoryReposiotryExtensions
    {
        public static IQueryable<Category> Search(this IQueryable<Category> categories, string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return categories;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return categories.Where(c => c.Title.ToLower().Contains(lowerCaseSearchTerm));

        }

        public static IQueryable<Category> Sort(this IQueryable<Category> categories, string? orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return categories.OrderBy(c => c.CreatedDate);
            }
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Category>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return categories.OrderBy(c => c.CreatedDate);
            }

            return categories.OrderBy(orderQuery);




        }
    }
}

