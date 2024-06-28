using AutoMapper;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using LibraryManagement.ViewModels.Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;
        public PublisherController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Publishers(string sortBy, string sortOrder, string searchString)
        {

            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            ViewData["CurrentSearchString"] = searchString;

            var publisherDto = await _serviceManager.PublisherService.GetAllPublishers(sortBy,sortOrder,searchString,false);

            var publisherViewModel = _mapper.Map<IEnumerable<PublisherViewModel>>(publisherDto);

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
        public async Task<IActionResult> CreatePublisher(CreatePublisherViewModel createPublisherViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createPublisherViewModel);  
            }

            var publisherDto = _mapper.Map<CreatePublisherDto>(createPublisherViewModel);

            await _serviceManager.PublisherService.CreatePublisher(publisherDto, false);

            return RedirectToAction("Publishers");   

        }

        public async Task<IActionResult> UpdatePublisher(Guid id)
        {
            var publisher = await _serviceManager.PublisherService.GetPublisher(id, false);

            var publisherViewModel = new PublisherViewModel()
            {
                PublisherId = publisher.PublisherId,
                PublisherName = publisher.PublisherName,
                Email = publisher.Email,
                PhoneNumber = publisher.PhoneNumber
            };

            return View(publisherViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePublisher(PublisherViewModel publisherViewModel) 
        {
            if (!ModelState.IsValid)
            {

            }

            var publisherDto = _mapper.Map<PublisherDto>(publisherViewModel);

            await _serviceManager.PublisherService.UpdatePublisher(publisherDto, true);

            TempData["SuccessMessage"] = "Publisher Updated Successfully";

            return View(publisherViewModel);




        }




    }
}
