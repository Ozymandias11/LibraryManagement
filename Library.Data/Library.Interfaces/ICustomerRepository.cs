using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
   public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomers(bool trackChanges);
        Task<Customer?> GetCustomer(Guid id, bool trackChanges); 
        void CreateCustomer(Customer customer); 
        void DeleteCusotmer(Customer customer);
    }
}
