using Bogus;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TestData
{
    public class AuthorFaker
    {
        public static Faker<Author> Create()
        {
            return new Faker<Author>()
                .RuleFor(a => a.AuthorId, f => Guid.NewGuid())
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName())
                .RuleFor(a => a.DateOfBirth, f => f.Date.Past(80))
                .RuleFor(a => a.CreatedDate, f => f.Date.Recent())
                .RuleFor(a => a.DeletedDate, f => null);
        }

    }
}
