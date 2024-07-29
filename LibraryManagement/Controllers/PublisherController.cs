using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Library.Data.RequestFeatures;
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
        private readonly INotyfService _notyf;

        public PublisherController(IServiceManager serviceManager, IMapper mapper, INotyfService notyf)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
            _notyf = notyf;
        }

        public async Task<IActionResult> Publishers([FromQuery] PublisherParameters publisherParameters)
        {
            var (publisherDtos, metaData) = await _serviceManager.PublisherService.GetAllPublishers(publisherParameters ,false);

            var publisherViewModels = _mapper.Map<IEnumerable<PublisherViewModel>>(publisherDtos);

            var pagedViewModel = new PagedViewModel<PublisherViewModel>(publisherViewModels, metaData);

            return View(pagedViewModel);


        }
        public IActionResult CreatePublisher()
        {
            var createPublisherViewModel = new CreatePublisherViewModel();

            return View(createPublisherViewModel);
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePublisher(CreatePublisherViewModel createPublisherViewModel)
        {

            var createPublisherDto = _mapper.Map<CreatePublisherDto>(createPublisherViewModel);

            var result = await _serviceManager.PublisherService.CreatePublisher(createPublisherDto ,false);

            if (result.IsFailed)
            {
                _notyf.Warning("Something went wrong please try again");
                return View(createPublisherViewModel);
            }

            _notyf.Success("Publisher created successfully");
            return RedirectToAction("Publishers");

            

        }

        public async Task<IActionResult> UpdatePublisher(Guid id)
        {
            var result = await _serviceManager.PublisherService.GetPublisher(id, false);

            if (result.IsFailed)
            {
                return View("PageNotFound");
            }

           var publisherViewModel = _mapper.Map<PublisherViewModel>(result.Value);    

           return View(publisherViewModel);

        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePublisher(PublisherViewModel model)
        {
            var publisherDto = _mapper.Map<PublisherDto>(model);

             var result = await _serviceManager.PublisherService.UpdatePublisher(publisherDto, true);

            if(result.IsFailed)
            {
                _notyf.Error("Publisher update has failed, please try again");
                return View(model);
            }

            _notyf.Success("Publisher updated successfully");
            return View(model);

        }


        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            var result = await _serviceManager.PublisherService.DeletePublisher(id, false);

            if (result.IsFailed)
            {
                _notyf.Warning("Something Went wrong please try again");
            }

            return RedirectToAction("Publishers");
        }

        // below are methods used for populating dropowns

        public async Task<IActionResult> GetPublishersForDropDown()
        {
            var publishersDto = await _serviceManager.PublisherService.GetAllPublishersForDropDown(false);
            return Json(publishersDto.Select(p => new { id = p.PublisherId, name = p.PublisherName }));
        }

        public async Task<IActionResult> GetBookPublishers(Guid id)
        {
            var bookPublishers = await _serviceManager.PublisherService.GetBookPublishers(id, false);
            return Json(bookPublishers.Select(p => p.PublisherId));
        }

        public async Task<IActionResult> GetBookPublishersForSelect2(Guid id)
        {
            var bookPublishers = await _serviceManager.PublisherService.GetBookPublishers(id, false);
            return Json(bookPublishers.Select(p => new { id = p.PublisherId, name = p.PublisherName }));
        }

    }
}
