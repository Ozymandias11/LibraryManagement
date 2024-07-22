using AutoMapper;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using Library.Service.Logging;
using LibraryManagement.ActionFilters;
using LibraryManagement.Helper;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public PublisherController(
            IServiceManager serviceManager, 
            IMapper mapper,
            ILoggerManager loggerManager)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }

        public async Task<IActionResult> Publishers(string sortBy, string sortOrder, string searchString)
        {

            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewData["CurrentSearchString"] = searchString;

            var publisherDto = await _serviceManager.PublisherService.GetAllPublishers(sortBy,sortOrder,searchString,false);

            if (publisherDto.IsFailed)
            {
                _loggerManager.LogError($"Error getting all Categories:  {string.Join(", ", publisherDto.Errors.Select(e => e.Message))}");
                return View("Error");
            }

            var publisherViewModel = _mapper.Map<IEnumerable<PublisherViewModel>>(publisherDto.Value);

            return View(publisherViewModel);


        }


        public IActionResult CreatePublisher()
        {
            var createPublisherViewModel = new CreatePublisherViewModel();

            return View(createPublisherViewModel);
        }

        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            await _serviceManager.PublisherService.DeletePublisher(id, false);
            return RedirectToAction("Publishers");
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePublisher(CreatePublisherViewModel createPublisherViewModel)
        {

            var publisherDto = _mapper.Map<CreatePublisherDto>(createPublisherViewModel);

            var result = await _serviceManager.PublisherService.CreatePublisher(publisherDto, false);

            return this.HandleFailure(result, createPublisherViewModel, _loggerManager, nameof(Publishers), "Creating publishers");

        }

        public async Task<IActionResult> UpdatePublisher(Guid id)
        {
            var publisher = await _serviceManager.PublisherService.GetPublisher(id, false);

            if (publisher.IsFailed)
            {
                _loggerManager.LogError($"The publisher with id {id} was not found");
            }


            var publisherViewModel = new PublisherViewModel()
            {
                PublisherId = publisher.Value.PublisherId,
                PublisherName = publisher.Value.PublisherName,
                Email = publisher.Value.Email,
                PhoneNumber = publisher.Value.PhoneNumber
            };

            return View(publisherViewModel);

        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePublisher(PublisherViewModel publisherViewModel) 
        {
            var publisherDto = _mapper.Map<PublisherDto>(publisherViewModel);

            await _serviceManager.PublisherService.UpdatePublisher(publisherDto, true);

            TempData["SuccessMessage"] = "Publisher Updated Successfully";

            return View(publisherViewModel);




        }




    }
}
