using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data
{
    public static class RepositoryExtensions
    {
        public static void DetachEntries<T>(this RepositoryContext context, IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                context.Entry(entity).State = EntityState.Detached;
            }
        }
    }
}
