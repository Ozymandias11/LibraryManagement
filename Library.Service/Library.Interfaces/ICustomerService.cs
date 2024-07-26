using FluentResults;
using Library.Data.RequestFeatures;
using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
    public interface ICustomerService
    {
        Task<(IEnumerable<CustomerDto> customers, MetaData metaData)> GetAllCustomers(CustomerParameters customerParameters ,bool trackChanges);
           
        Task<IEnumerable<CustomerDto>> GetAllCustomersUnfiltered(bool trackChanges);
        Task<Result<CustomerDto>> GetCustomer(Guid id, bool trackChanges);  
        Task<Result<CustomerDto>> GetCustomerByPersonalId(string id, bool trackChanges);    
        Task<Result> CreateCustomer(CreateCustomerDto customer, bool trackChanges);
        Task<Result> DeleteCustomer(Guid id, bool trackChanges);    
        Task<Result> UpdateCustomer(CustomerDto customerDto , bool trackChanges);
        Task<int> GetTotalCustomersCount();
    }
}
