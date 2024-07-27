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
    public static class PublisherRepositoryExtensions
    {
        public static IQueryable<Publisher> Search(this IQueryable<Publisher> publishers, string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return publishers;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return publishers.Where(p => p.PublisherName.ToLower().Contains(lowerCaseSearchTerm));

        }

        public static IQueryable<Publisher> Sort(this IQueryable<Publisher> publishers, string? orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return publishers.OrderBy(c => c.CreatedDate);
            }
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Publisher>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return publishers.OrderBy(c => c.CreatedDate);
            }

            return publishers.OrderBy(orderQuery);


        }
    }
}
