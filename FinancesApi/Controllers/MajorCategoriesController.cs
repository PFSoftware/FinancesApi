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
    [Route("api/majorcategories")]
    [ApiController]
    public class MajorCategoriesController : ControllerBase
    {
        private readonly MajorCategoryService _service;
        private readonly IMapper _mapper;

        public MajorCategoriesController(MajorCategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET api/majorCategories
        [HttpGet]
        public ActionResult<IEnumerable<MajorCategoryViewModel>> GetAllMajorCategories()
        {
            IEnumerable<MajorCategory> majorCategoryItems = _service.GetAllMajorCategories();

            return Ok(_mapper.Map<IEnumerable<MajorCategoryViewModel>>(majorCategoryItems));
        }

        //GET api/majorCategories/{id}
        [HttpGet("{id}", Name = "GetMajorCategoryById")]
        public ActionResult<MajorCategoryViewModel> GetMajorCategoryById(int id)
        {
            MajorCategory MajorCategoryItem = _service.GetMajorCategoryById(id);
            if (MajorCategoryItem != null)
                return Ok(_mapper.Map<MajorCategoryViewModel>(MajorCategoryItem));

            return NotFound();
        }

        //POST api/majorCategories
        [HttpPost]
        public ActionResult<MajorCategoryViewModel> CreateMajorCategory(CreateEditMajorCategoryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return Problem("A valid name is required.");

            MajorCategory newMajorCategory = _mapper.Map<MajorCategory>(request);
            _service.CreateMajorCategory(newMajorCategory);

            MajorCategoryViewModel MajorCategoryViewModel = _mapper.Map<MajorCategoryViewModel>(newMajorCategory);

            return CreatedAtRoute(nameof(GetMajorCategoryById), new { Id = MajorCategoryViewModel.Id }, MajorCategoryViewModel);
        }

        //POST api/majorCategories/{id}
        [HttpPost("{id}")]
        public ActionResult UpdateMajorCategory(int id, CreateEditMajorCategoryRequest request)
        {
            MajorCategory majorCategory = _service.GetMajorCategoryById(id);
            if (majorCategory == null)
                return NotFound();

            if (request.Id != null && request.Id != id)
                return ValidationProblem("The ID in the model doesn't match the ID the request was made on.");

            _service.UpdateMajorCategory(request, majorCategory);
            return NoContent();
        }

        //DELETE api/majorCategories/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMajorCategory(int id)
        {
            MajorCategory majorCategoryModelFromRepo = _service.GetMajorCategoryById(id);
            if (majorCategoryModelFromRepo == null)
                return NotFound();

            _service.DeleteMajorCategory(majorCategoryModelFromRepo);
            return NoContent();
        }
    }
}