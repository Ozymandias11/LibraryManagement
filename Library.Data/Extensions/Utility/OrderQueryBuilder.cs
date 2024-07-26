using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Extensions.Utility
{
   public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            var orderParameters = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();

            foreach(var param in orderParameters)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");

            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return orderQuery;

        }
    }
}
