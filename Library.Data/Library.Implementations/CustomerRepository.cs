using Library.Data.Library.Interfaces;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
       

        public async Task<IEnumerable<Customer>> GetAllCustomers(bool trackChanges) =>
            await FindByCondition(c => c.DeletedDate == null, trackChanges).ToListAsync();

        public async Task<Customer?> GetCustomer(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.CustomerId == id, trackChanges).FirstOrDefaultAsync();
       
    }
}
