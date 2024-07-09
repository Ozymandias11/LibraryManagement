using AutoMapper;
using FluentResults;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Errors.NotFoundError;
using Library.Service.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CustomerService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Result> CreateCustomer(CreateCustomerDto customer, bool trackChanges)
        {
            var customerEntity = _mapper.Map<Customer>(customer);
            _repositoryManager.CustomerRepository.CreateCustomer(customerEntity);
            await _repositoryManager.SaveAsync();
            return Result.Ok();
        }

        public async Task<Result> DeleteCustomer(Guid id, bool trackChanges)
        {
            var Customer = await _repositoryManager.CustomerRepository.GetCustomer(id, trackChanges);

            if(Customer == null)
            {
                return Result.Fail(new NotFoundError("Customer", id));
            }

            _repositoryManager.CustomerRepository.DeleteCusotmer(Customer);

            await _repositoryManager.SaveAsync();

            return Result.Ok(); 

        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers(
            string sortBy,
            string sortOrder,
            string searchString,
            int page,
            int pageSize,
            bool trackChanges)
        {
            var customers = await _repositoryManager.CustomerRepository.GetAllCustomers(page, pageSize, trackChanges);

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.FirstName.Contains(searchString) ||
                                                 c.LastName.Contains(searchString) ||
                                                 c.Email.Contains(searchString));
            }


            customers = ApplySorting(customers, sortBy, sortOrder);



            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customerDtos;    
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersUnfiltered(bool trackChanges)
        {
            var customers = await _repositoryManager.CustomerRepository.GetAllCustomersUnfiltered(trackChanges);
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersDto;

        }

        public async Task<Result<CustomerDto>> GetCustomer(Guid id, bool trackChanges)
        {
            var customer = await _repositoryManager.CustomerRepository.GetCustomer(id, trackChanges);

            if(customer == null)
            {
                return Result.Fail(new NotFoundError("Customer", id));
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);   

            return customerDto; 



        }

        public async Task<int> GetTotalCustomersCount() => await _repositoryManager.CustomerRepository.GetTotalCustomersCount();
      

        public async Task<Result> UpdateCustomer(CustomerDto customerDto, bool trackChanges)
        {
            var customerEntity = await _repositoryManager.CustomerRepository.GetCustomer(customerDto.CustomerId, trackChanges);

            if(customerEntity == null)
            {
                return Result.Fail(new NotFoundError("Customer", customerDto.CustomerId));
            }

            _mapper.Map(customerDto, customerEntity);

            await _repositoryManager.SaveAsync();

            return Result.Ok();


        }

        private IEnumerable<Customer> ApplySorting(IEnumerable<Customer> customers, string sortBy, string sortOrder) 
        {
            return customers = sortBy switch
            {
                "LastName" => sortOrder == "LastName_Asc" ? customers.OrderBy(c => c.LastName) :
                                                            customers.OrderByDescending(c => c.LastName),
                "FirstName" => sortOrder == "FirstName_Asc" ? customers.OrderBy(c => c.FirstName) :
                                                              customers.OrderByDescending(c => c.FirstName),
                "Email" => sortOrder == "Email_Asc" ? customers.OrderBy(c => c.Email) :
                                                            customers.OrderByDescending(c => c.Email),
                _ => customers.OrderBy(c => c.CreatedDate)
            };
        }
    }
}
