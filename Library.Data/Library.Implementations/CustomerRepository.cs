using Library.Data.Extensions;
using Library.Data.Library.Interfaces;
using Library.Data.RequestFeatures;
using Library.Model.Helpers;
using Library.Model.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateCustomer(Customer customer) => Create(customer);
       

        public void DeleteCusotmer(Customer customer) => Delete(customer);
       

        public async Task<PagedList<Customer>> GetAllCustomers(CustomerParameters customerParameters,bool trackChanges)
        {
            var customers = await FindByCondition(c => c.DeletedDate == null, trackChanges)
                .Search(customerParameters.SearchTerm)
                .Sort(customerParameters.OrderBy)
                .ToListAsync();

            return PagedList<Customer>
                .ToPagedList(customers, customerParameters.PageNumber, customerParameters.PageSize);

        }
         

        public async Task<IEnumerable<Customer>> GetAllCustomersUnfiltered(bool trackChanges)
            => await FindByCondition(c => c.DeletedDate == null, trackChanges)
                  .ToListAsync();
       

        public async Task<Customer?> GetCustomer(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.CustomerId == id, trackChanges).FirstOrDefaultAsync();

        public async Task<Customer?> GetCustomerByPersonalId(string id, bool trackChanges)
            => await FindByCondition(c => c.CustomerPersonalId == id, trackChanges).FirstOrDefaultAsync();
      

        public Task<int> GetTotalCustomersCount() => FindAll(false).CountAsync();


        public async Task<IEnumerable<MonthlyRegistrationReport>> GetMonthlyRegistrations(int year)
        {
            var yearParam = new SqlParameter("@Year", SqlDbType.Int) { Value = year };

            string storedProcedure = "EXEC dbo.GetMonthlyRegistrations @Year";

            var result = await ExecuteStoredProcedureAsync<MonthlyRegistrationReport>(storedProcedure, yearParam);

            return result;


        }



    }
}
