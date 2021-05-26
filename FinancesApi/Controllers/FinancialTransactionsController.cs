using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PFSoftware.FinancesApi.Models.Domain;
using PFSoftware.FinancesApi.Models.ViewModels;
using PFSoftware.FinancesApi.Services;
using System.Collections.Generic;

namespace PFSoftware.FinancesApi.Controllers
{
    [Route("api/financialtransactions")]
    [ApiController]
    public class FinancialTransactionsController : ControllerBase
    {
        private readonly FinancialTransactionService _service;
        private readonly IMapper _mapper;

        public FinancialTransactionsController(FinancialTransactionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET api/financialTransactions
        [HttpGet]
        public ActionResult<IEnumerable<FinancialTransactionViewModel>> GetAllFinancialTransactions()
        {
            IEnumerable<FinancialTransaction> financialTransactionItems = _service.GetAllFinancialTransactions();

            return Ok(_mapper.Map<IEnumerable<FinancialTransactionViewModel>>(financialTransactionItems));
        }

        //GET api/financialTransactions/{id}
        [HttpGet("{id}", Name = "GetFinancialTransactionById")]
        public ActionResult<FinancialTransactionViewModel> GetFinancialTransactionById(int id)
        {
            FinancialTransaction FinancialTransactionItem = _service.GetFinancialTransactionById(id);
            if (FinancialTransactionItem != null)
            {
                return Ok(_mapper.Map<FinancialTransactionViewModel>(FinancialTransactionItem));
            }
            return NotFound();
        }

        //POST api/financialTransactions
        [HttpPost]
        public ActionResult<FinancialTransactionViewModel> CreateFinancialTransaction(FinancialTransaction financialTransaction)
        {
            FinancialTransaction financialTransactionModel = _mapper.Map<FinancialTransaction>(financialTransaction);
            _service.CreateFinancialTransaction(financialTransactionModel);

            var FinancialTransactionViewModel = _mapper.Map<FinancialTransactionViewModel>(financialTransactionModel);

            return CreatedAtRoute(nameof(GetFinancialTransactionById), new { Id = FinancialTransactionViewModel.Id }, FinancialTransactionViewModel);
        }

        //PUT api/financialTransactions/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateFinancialTransaction(int id, FinancialTransaction financialTransaction)
        {
            FinancialTransaction financialTransactionModelFromRepo = _service.GetFinancialTransactionById(id);
            if (financialTransactionModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(financialTransaction, financialTransactionModelFromRepo);

            _service.UpdateFinancialTransaction(id, financialTransactionModelFromRepo);

            return NoContent();
        }

        //PATCH api/financialTransactions/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialFinancialTransactionUpdate(int id, JsonPatchDocument<FinancialTransaction> patchDoc)
        {
            var financialTransactionModelFromRepo = _service.GetFinancialTransactionById(id);
            if (financialTransactionModelFromRepo == null)
            {
                return NotFound();
            }

            var FinancialTransactionToPatch = _mapper.Map<FinancialTransaction>(financialTransactionModelFromRepo);
            patchDoc.ApplyTo(FinancialTransactionToPatch, ModelState);

            if (!TryValidateModel(FinancialTransactionToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(FinancialTransactionToPatch, financialTransactionModelFromRepo);

            _service.UpdateFinancialTransaction(id, financialTransactionModelFromRepo);

            return NoContent();
        }

        //DELETE api/financialTransactions/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteFinancialTransaction(int id)
        {
            var financialTransactionModelFromRepo = _service.GetFinancialTransactionById(id);
            if (financialTransactionModelFromRepo == null)
            {
                return NotFound();
            }
            _service.DeleteFinancialTransaction(financialTransactionModelFromRepo);

            return NoContent();
        }
    }
}