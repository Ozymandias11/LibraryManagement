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
        private readonly IResultHandlerService _resultHandlerService;
        public CustomerController(IServiceManager serviceManager, IMapper mapper, IResultHandlerService resultHandlerService)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _resultHandlerService = resultHandlerService;   

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

            _resultHandlerService.HandleResult(result, "Creating customer");

            return RedirectToAction("Customers");


        }

        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var result = await _serviceManager.CustomerService.DeleteCustomer(id, false);
            _resultHandlerService.HandleResult(result, $"Deleting customer with id {id}");
            return RedirectToAction("Customers");
        }

        public async Task<IActionResult> UpdateCustomer(Guid id)
        {
            var GetcustomerResult = await _serviceManager.CustomerService.GetCustomer(id, false);

            _resultHandlerService.HandleResult(GetcustomerResult, $"getting customer with id {id}");


            var updateCustomerViewModel = _mapper.Map<UpdateCustomerViewModel>(GetcustomerResult.Value);
           

            return View(updateCustomerViewModel);



        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerViewModel model)
        {

            var customerDto = _mapper.Map<CustomerDto>(model);

            var result = await _serviceManager.CustomerService.UpdateCustomer(customerDto, true);

            _resultHandlerService.HandleResult(result, $"updating customer with id {model.CustomerId}");

            TempData["SuccessMessage"] = "customer Updated Successfully";

            return View(model);


        }
    }
}
