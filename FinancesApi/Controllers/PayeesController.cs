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
                return Ok(_mapper.Map<PayeeViewModel>(PayeeItem));

            return NotFound();
        }

        //POST api/payees
        [HttpPost]
        public ActionResult<PayeeViewModel> CreatePayee(CreateEditPayeeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return Problem("A valid name is required.");

            Payee payee = _mapper.Map<Payee>(request);
            _service.CreatePayee(payee);

            PayeeViewModel payeeViewModel = _mapper.Map<PayeeViewModel>(payee);

            return CreatedAtRoute(nameof(GetPayeeById), new { Id = payeeViewModel.Id }, payeeViewModel);
        }

        //POST api/payees/{id}
        [HttpPost("{id}")]
        public ActionResult UpdatePayee(int id, CreateEditPayeeRequest request)
        {
            Payee payee = _service.GetPayeeById(id);
            if (payee == null)
                return NotFound();

            if (request.Id != null && request.Id != id)
                return ValidationProblem("The ID in the model doesn't match the ID the request was made on.");

            _service.UpdatePayee(request, payee);
            return NoContent();
        }

        //DELETE api/payees/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePayee(int id)
        {
            Payee payeeModelFromRepo = _service.GetPayeeById(id);
            if (payeeModelFromRepo == null)
            {
                return NotFound();
            }
            _service.DeletePayee(payeeModelFromRepo);

            return NoContent();
        }
    }
}