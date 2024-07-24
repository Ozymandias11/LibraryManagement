using Bogus;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TestData
{
   public class PublisherFaker
    {
        public static Faker<Publisher> Create()
        {
            return new Faker<Publisher>()
                .RuleFor(p => p.PublisherId, f => Guid.NewGuid())
                .RuleFor(p => p.PublisherName, f => f.Company.CompanyName())
                .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(p => p.Email, (f, P) => f.Internet.Email(P.PublisherName))
                .RuleFor(p => p.CreatedDate, f => f.Date.Recent())
                .RuleFor(p => p.DeletedDate, f => null);
        }
    }
}
