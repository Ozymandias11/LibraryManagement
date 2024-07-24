using Bogus;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TestData
{
   public class CategoryFaker
    {
        public static Faker<Category> Create()
        {
            return new Faker<Category>()
                .RuleFor(c => c.CategoryId, f => Guid.NewGuid())
                .RuleFor(c => c.Title, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.CreatedDate, f => f.Date.Recent())
                .RuleFor(c => c.DeletedDate, f => null);
        }
    }
}
