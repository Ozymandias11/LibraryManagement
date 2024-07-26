using AutoMapper;
using FluentResults;
using Library.Data.NewFolder;
using Library.Data.RequestFeatures;
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

        public async Task<(IEnumerable<CustomerDto> customers, MetaData metaData)> GetAllCustomers(CustomerParameters customerParameters ,bool trackChanges)
            
        {
            var customersWithMetaData = await _repositoryManager.CustomerRepository.GetAllCustomers(customerParameters,trackChanges);

            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customersWithMetaData);

            return (customersDto, customersWithMetaData.MetaData);
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

        public async Task<Result<CustomerDto>> GetCustomerByPersonalId(string id, bool trackChanges)
        {
            var customer = await _repositoryManager.CustomerRepository.GetCustomerByPersonalId(id, trackChanges);
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
    }
}
