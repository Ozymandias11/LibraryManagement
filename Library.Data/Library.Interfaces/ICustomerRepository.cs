using Library.Data.RequestFeatures;
using Library.Model.Helpers;
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
        Task<PagedList<Customer>> GetAllCustomers(CustomerParameters customerParameters ,bool trackChanges);
        Task<IEnumerable<Customer>> GetAllCustomersUnfiltered(bool trackChanges);   
        Task<Customer?> GetCustomer(Guid id, bool trackChanges);
        Task<Customer?> GetCustomerByPersonalId(String id, bool trackChanges);
        Task<IEnumerable<MonthlyRegistrationReport>> GetMonthlyRegistrations(int year);
        Task<int> GetTotalCustomersCount();
        void CreateCustomer(Customer customer); 
        void DeleteCusotmer(Customer customer);
    }
}
