using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PFSoftware.FinancesApi.Models.Domain;
using PFSoftware.FinancesApi.Models.ViewModels;
using PFSoftware.FinancesApi.Services;
using System.Collections.Generic;

namespace PFSoftware.FinancesApi.Controllers
{
    [Route("api/payees")]
    [ApiController]
    public class PayeesController : ControllerBase
    {
        private readonly PayeeService _service;
        private readonly IMapper _mapper;

        public PayeesController(PayeeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET api/payees
        [HttpGet]
        public ActionResult<IEnumerable<PayeeViewModel>> GetAllPayees()
        {
            IEnumerable<Payee> payeeItems = _service.GetAllPayees();

            return Ok(_mapper.Map<IEnumerable<PayeeViewModel>>(payeeItems));
        }

        //GET api/payees/{id}
        [HttpGet("{id}", Name = "GetPayeeById")]
        public ActionResult<PayeeViewModel> GetPayeeById(int id)
        {
            Payee PayeeItem = _service.GetPayeeById(id);
            if (PayeeItem != null)
            {
                return Ok(_mapper.Map<PayeeViewModel>(PayeeItem));
            }
            return NotFound();
        }

        //POST api/payees
        [HttpPost]
        public ActionResult<PayeeViewModel> CreatePayee(Payee payee)
        {
            Payee payeeModel = _mapper.Map<Payee>(payee);
            _service.CreatePayee(payeeModel);

            var PayeeViewModel = _mapper.Map<PayeeViewModel>(payeeModel);

            return CreatedAtRoute(nameof(GetPayeeById), new { Id = PayeeViewModel.Id }, PayeeViewModel);
        }

        //PUT api/payees/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePayee(int id, Payee payee)
        {
            Payee payeeModelFromRepo = _service.GetPayeeById(id);
            if (payeeModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(payee, payeeModelFromRepo);

            _service.UpdatePayee(id, payeeModelFromRepo);

            return NoContent();
        }

        //PATCH api/payees/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialPayeeUpdate(int id, JsonPatchDocument<Payee> patchDoc)
        {
            var payeeModelFromRepo = _service.GetPayeeById(id);
            if (payeeModelFromRepo == null)
            {
                return NotFound();
            }

            var PayeeToPatch = _mapper.Map<Payee>(payeeModelFromRepo);
            patchDoc.ApplyTo(PayeeToPatch, ModelState);

            if (!TryValidateModel(PayeeToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(PayeeToPatch, payeeModelFromRepo);

            _service.UpdatePayee(id, payeeModelFromRepo);

            return NoContent();
        }

        //DELETE api/payees/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePayee(int id)
        {
            var payeeModelFromRepo = _service.GetPayeeById(id);
            if (payeeModelFromRepo == null)
            {
                return NotFound();
            }
            _service.DeletePayee(payeeModelFromRepo);

            return NoContent();
        }
    }
}