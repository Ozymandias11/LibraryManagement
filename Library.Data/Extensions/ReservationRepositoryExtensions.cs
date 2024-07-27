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
   public static class ReservationRepositoryExtensions
    {
        public static IQueryable<Reservation> Search(this IQueryable<Reservation> reservations, string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return reservations;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return reservations.Where(r => r.Customer.CustomerPersonalId.Contains(lowerCaseSearchTerm));

        }

        public static IQueryable<Reservation> Sort(this IQueryable<Reservation> reservations, string? orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return reservations.OrderBy(c => c.CreatedDate);
            }
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Reservation>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return reservations.OrderBy(c => c.CreatedDate);
            }

            return reservations.OrderBy(orderQuery);




        }
    }
}
