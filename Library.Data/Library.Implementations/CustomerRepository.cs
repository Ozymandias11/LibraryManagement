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
       

        public async Task<IEnumerable<Customer>> GetAllCustomers(int page, int pageSize, bool trackChanges) =>
            await FindByCondition(c => c.DeletedDate == null, trackChanges)
                  .Skip((page - 1) * pageSize)
                  .Take(pageSize)
                  .ToListAsync();

        public async Task<IEnumerable<Customer>> GetAllCustomersUnfiltered(bool trackChanges)
            => await FindByCondition(c => c.DeletedDate == null, trackChanges)
                  .ToListAsync();
       

        public async Task<Customer?> GetCustomer(Guid id, bool trackChanges) =>
            await FindByCondition(c => c.CustomerId == id, trackChanges).FirstOrDefaultAsync();

        public Task<int> GetTotalCustomersCount() => FindAll(false).CountAsync();
        
            
        
    }
}
