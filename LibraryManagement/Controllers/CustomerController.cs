using AutoMapper;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using Library.Service.Logging;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public CustomerController(IServiceManager serviceManager, IMapper mapper, ILoggerManager loggerManager)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _loggerManager = loggerManager; 
        }

        public async Task<IActionResult> Customers(
            string sortBy,
            string sortOrder,
            string searchString,
            int page = 1,
            int pageSize = 10) 
        {

            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.isPaginated = true;
            ViewData["CurrentSearchString"] = searchString;
            ViewData["CurrentPage"] = page;

            var customerDtos = await _serviceManager.CustomerService.GetAllCustomers(
                sortBy, 
                sortOrder, 
                searchString,
                page,
                pageSize,
                false);

            var customerViewModels = _mapper.Map<IEnumerable<CustomerViewModel>>(customerDtos);

            foreach (var customer in customerViewModels)
            {
                customer.CurrentPage = page;
                customer.PageSize = pageSize;
                customer.TotalCount = await _serviceManager.CustomerService.GetTotalCustomersCount();
            }


           




            return View(customerViewModels);
        }

        public IActionResult CreateCustomer() 
        {
            var createCustomerViewModel = new CreateCustomerViewModel();
            return View(createCustomerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel createCustomerViewModel) 
        { 
            if(!ModelState.IsValid)
            {
                return View(createCustomerViewModel);
            }


            var createCustomerDto = _mapper.Map<CreateCustomerDto>(createCustomerViewModel);    

            var result = await _serviceManager.CustomerService.CreateCustomer(createCustomerDto, false);

            if (result.IsFailed)
            {
                var errorMessage = result.Errors.FirstOrDefault()?.Message ?? "An error Occured while creatingc customer";
                _loggerManager.LogError($"An error occured while createing customer {errorMessage}");
             //   createCustomerViewModel.ErrorMessage = errorMessage;
            }

            return RedirectToAction("Customers");


        }

        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _serviceManager.CustomerService.DeleteCustomer(id, false);
            return RedirectToAction("Customers");
        }

        public async Task<IActionResult> UpdateCustomer(Guid id)
        {
            var GetcustomerResult = await _serviceManager.CustomerService.GetCustomer(id, false);

            if (GetcustomerResult.IsFailed)
            {
                _loggerManager.LogError($"The Customer with id {id} was not found");
            }

            var updateCustomerViewModel = new UpdateCustomerViewModel()
            {
                CustomerId = id,
                FirstName = GetcustomerResult.Value.FirstName,
                LastName = GetcustomerResult.Value.LastName,
                Email = GetcustomerResult.Value.Email,
                PhoneNumber = GetcustomerResult.Value.PhoneNumber,
                Address = GetcustomerResult.Value.Address
            };

            return View(updateCustomerViewModel);



        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var customerDto = _mapper.Map<CustomerDto>(model);

            await _serviceManager.CustomerService.UpdateCustomer(customerDto, true);


            TempData["SuccessMessage"] = "customer Updated Successfully";

            return View(model);


        }


    }
}
