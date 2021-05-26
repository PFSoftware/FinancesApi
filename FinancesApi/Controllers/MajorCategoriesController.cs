using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
            {
                return Ok(_mapper.Map<MajorCategoryViewModel>(MajorCategoryItem));
            }
            return NotFound();
        }

        //POST api/majorCategories
        [HttpPost]
        public ActionResult<MajorCategoryViewModel> CreateMajorCategory(MajorCategory majorCategory)
        {
            MajorCategory majorCategoryModel = _mapper.Map<MajorCategory>(majorCategory);
            _service.CreateMajorCategory(majorCategoryModel);

            var MajorCategoryViewModel = _mapper.Map<MajorCategoryViewModel>(majorCategoryModel);

            return CreatedAtRoute(nameof(GetMajorCategoryById), new { Id = MajorCategoryViewModel.Id }, MajorCategoryViewModel);
        }

        //PUT api/majorCategories/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMajorCategory(int id, MajorCategory majorCategory)
        {
            MajorCategory majorCategoryModelFromRepo = _service.GetMajorCategoryById(id);
            if (majorCategoryModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(majorCategory, majorCategoryModelFromRepo);

            _service.UpdateMajorCategory(id, majorCategoryModelFromRepo);

            return NoContent();
        }

        //PATCH api/majorCategories/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialMajorCategoryUpdate(int id, JsonPatchDocument<MajorCategory> patchDoc)
        {
            var majorCategoryModelFromRepo = _service.GetMajorCategoryById(id);
            if (majorCategoryModelFromRepo == null)
            {
                return NotFound();
            }

            var MajorCategoryToPatch = _mapper.Map<MajorCategory>(majorCategoryModelFromRepo);
            patchDoc.ApplyTo(MajorCategoryToPatch, ModelState);

            if (!TryValidateModel(MajorCategoryToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(MajorCategoryToPatch, majorCategoryModelFromRepo);

            _service.UpdateMajorCategory(id, majorCategoryModelFromRepo);

            return NoContent();
        }

        //DELETE api/majorCategories/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMajorCategory(int id)
        {
            var majorCategoryModelFromRepo = _service.GetMajorCategoryById(id);
            if (majorCategoryModelFromRepo == null)
            {
                return NotFound();
            }
            _service.DeleteMajorCategory(majorCategoryModelFromRepo);

            return NoContent();
        }
    }
}