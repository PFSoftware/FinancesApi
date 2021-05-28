using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PFSoftware.FinancesApi.Models.Api.Requests;
using PFSoftware.FinancesApi.Models.Domain;
using PFSoftware.FinancesApi.Models.ViewModels;
using PFSoftware.FinancesApi.Services;
using System.Collections.Generic;

namespace PFSoftware.FinancesApi.Controllers
{
    [Route("api/minorcategories")]
    [ApiController]
    public class MinorCategoriesController : ControllerBase
    {
        private readonly MinorCategoryService _service;
        private readonly IMapper _mapper;

        public MinorCategoriesController(MinorCategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET api/minorCategories
        [HttpGet]
        public ActionResult<IEnumerable<MinorCategoryViewModel>> GetAllMinorCategories()
        {
            IEnumerable<MinorCategory> minorCategoryItems = _service.GetAllMinorCategories();

            return Ok(_mapper.Map<IEnumerable<MinorCategoryViewModel>>(minorCategoryItems));
        }

        //GET api/minorCategories/{id}
        [HttpGet("{id}", Name = "GetMinorCategoryById")]
        public ActionResult<MinorCategoryViewModel> GetMinorCategoryById(int id)
        {
            MinorCategory minorCategoryItem = _service.GetMinorCategoryById(id);
            if (minorCategoryItem != null)
                return Ok(_mapper.Map<MinorCategoryViewModel>(minorCategoryItem));

            return NotFound();
        }

        //POST api/minorCategories
        [HttpPost]
        public ActionResult<MinorCategoryViewModel> CreateMinorCategory(CreateEditMinorCategoryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.MajorCategoryId == 0)
                return Problem("A valid name and majorCategoryId is required.");

            MinorCategory newMinorCategory = _mapper.Map<MinorCategory>(request);
            _service.CreateMinorCategory(newMinorCategory);

            MinorCategoryViewModel minorCategoryViewModel = _mapper.Map<MinorCategoryViewModel>(newMinorCategory);

            return CreatedAtRoute(nameof(GetMinorCategoryById), new { Id = minorCategoryViewModel.Id }, minorCategoryViewModel);
        }

        //POST api/minorCategories/{id}
        [HttpPost("{id}")]
        public ActionResult UpdateMinorCategory(int id, CreateEditMinorCategoryRequest request)
        {
            MinorCategory minorCategory = _service.GetMinorCategoryById(id);
            if (minorCategory == null)
                return NotFound();

            if (request.Id != null && request.Id != id)
                return ValidationProblem("The ID in the model doesn't match the ID the request was made on.");

            _service.UpdateMinorCategory(request, minorCategory);

            return NoContent();
        }

        //DELETE api/minorCategories/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMinorCategory(int id)
        {
            MinorCategory minorCategory = _service.GetMinorCategoryById(id);
            if (minorCategory == null)
                return NotFound();

            _service.DeleteMinorCategory(minorCategory);
            return NoContent();
        }
    }
}