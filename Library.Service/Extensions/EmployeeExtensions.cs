using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Extensions
{
    public static class EmployeeExtensions
    {
        public static IQueryable<Employee> Sort(this IQueryable<Employee> users, string propertyName, string sortOrder)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                propertyName = "Email";
                sortOrder = "ASC";
            }

            var parameter = Expression.Parameter(typeof(Employee), "u");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = sortOrder.Equals("DESC", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy";
            var methodCallExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(Employee), property.Type }, users.Expression, Expression.Quote(lambda));

            return users.Provider.CreateQuery<Employee>(methodCallExpression);
        }
    }
 }

