using Library.Data.Extensions.Utility;
using Library.Model.Models;
using System.Linq.Dynamic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Extensions
{
    public static class CustomerRepositoryExtensions
    {
       public static IQueryable<Customer> Search(this IQueryable<Customer> customers, string searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm))
                return customers;   

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return customers.Where(c => c.FirstName.ToLower().Contains(lowerCaseSearchTerm) || 
                                   c.LastName.Contains(lowerCaseSearchTerm));

        }  

        public static IQueryable<Customer> Sort(this IQueryable<Customer> customers, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return customers.OrderBy(c => c.CreatedDate);
            }
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Customer>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return customers.OrderBy(c => c.CreatedDate);
            }

            return customers.OrderBy(orderQuery);




        }
    }
}
