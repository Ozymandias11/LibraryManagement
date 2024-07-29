using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Library.Data.RequestFeatures;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using Library.Service.Library.Interfaces;
using Library.Service.Logging;
using LibraryManagement.ActionFilters;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        public CustomerController(IServiceManager serviceManager, IMapper mapper, INotyfService notyf)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;  
            _notyf = notyf;

        }

        public async Task<IActionResult> Customers([FromQuery] CustomerParameters customerParameters) 
        {

            var (customerDtos, metaData) = await _serviceManager.CustomerService.GetAllCustomers(customerParameters, false);  

            var customerViewModels = _mapper.Map<IEnumerable<CustomerViewModel>>(customerDtos);

            var pagedViewModel = new PagedViewModel<CustomerViewModel>(customerViewModels, metaData);
           
            return View(pagedViewModel);
        }

        public IActionResult CreateCustomer() 
        {
            var createCustomerViewModel = new CreateCustomerViewModel();
            return View(createCustomerViewModel);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel createCustomerViewModel) 
        { 
            var createCustomerDto = _mapper.Map<CreateCustomerDto>(createCustomerViewModel);    

            var result = await _serviceManager.CustomerService.CreateCustomer(createCustomerDto, false);

            if (result.IsFailed)
            {
                _notyf.Warning("Something went wrong please try again");
                return View(createCustomerViewModel);
                    
            }

            return RedirectToAction("Customers");


        }

        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _serviceManager.CustomerService.DeleteCustomer(id, false);

            if (result.IsFailed)
            {
                _notyf.Warning("Something Went wrong please try again");
            }

            return RedirectToAction("Customers");
        }

        public async Task<IActionResult> UpdateCustomer(Guid id)
        {
            var result = await _serviceManager.CustomerService.GetCustomer(id, false);

            if (result.IsFailed)
            {
                return View("PageNotFound");
            }


            var updateCustomerViewModel = _mapper.Map<UpdateCustomerViewModel>(result.Value);


            return View(updateCustomerViewModel);



        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerViewModel model)
        {

            var customerDto = _mapper.Map<CustomerDto>(model);

            var result = await _serviceManager.CustomerService.UpdateCustomer(customerDto, true);

            if (result.IsFailed)
            {
                _notyf.Error("Customer update has failed, please try again");
                return View(model);
            }

            _notyf.Success("Customer updated successfully");
            return View(model);
        }

        public async Task<IActionResult> GetCustomersForDropDown()
        {
            var customers = await _serviceManager.CustomerService.GetAllCustomersUnfiltered(false);
            return Json(customers.Select(c => new { id = c.CustomerId, name = c.CustomerPersonalId }));
        }
    }
}
