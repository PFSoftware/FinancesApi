using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
            MinorCategory MinorCategoryItem = _service.GetMinorCategoryById(id);
            if (MinorCategoryItem != null)
            {
                return Ok(_mapper.Map<MinorCategoryViewModel>(MinorCategoryItem));
            }
            return NotFound();
        }

        //POST api/minorCategories
        [HttpPost]
        public ActionResult<MinorCategoryViewModel> CreateMinorCategory(MinorCategory minorCategory)
        {
            MinorCategory minorCategoryModel = _mapper.Map<MinorCategory>(minorCategory);
            _service.CreateMinorCategory(minorCategoryModel);

            var MinorCategoryViewModel = _mapper.Map<MinorCategoryViewModel>(minorCategoryModel);

            return CreatedAtRoute(nameof(GetMinorCategoryById), new { Id = MinorCategoryViewModel.Id }, MinorCategoryViewModel);
        }

        //PUT api/minorCategories/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMinorCategory(int id, MinorCategory minorCategory)
        {
            MinorCategory minorCategoryModelFromRepo = _service.GetMinorCategoryById(id);
            if (minorCategoryModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(minorCategory, minorCategoryModelFromRepo);

            _service.UpdateMinorCategory(id, minorCategoryModelFromRepo);

            return NoContent();
        }

        //PATCH api/minorCategories/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialMinorCategoryUpdate(int id, JsonPatchDocument<MinorCategory> patchDoc)
        {
            var minorCategoryModelFromRepo = _service.GetMinorCategoryById(id);
            if (minorCategoryModelFromRepo == null)
            {
                return NotFound();
            }

            var MinorCategoryToPatch = _mapper.Map<MinorCategory>(minorCategoryModelFromRepo);
            patchDoc.ApplyTo(MinorCategoryToPatch, ModelState);

            if (!TryValidateModel(MinorCategoryToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(MinorCategoryToPatch, minorCategoryModelFromRepo);

            _service.UpdateMinorCategory(id, minorCategoryModelFromRepo);

            return NoContent();
        }

        //DELETE api/minorCategories/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMinorCategory(int id)
        {
            var minorCategoryModelFromRepo = _service.GetMinorCategoryById(id);
            if (minorCategoryModelFromRepo == null)
            {
                return NotFound();
            }
            _service.DeleteMinorCategory(minorCategoryModelFromRepo);

            return NoContent();
        }
    }
}