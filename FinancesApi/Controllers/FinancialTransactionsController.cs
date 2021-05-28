using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PFSoftware.FinancesApi.Models.Api.Requests;
using PFSoftware.FinancesApi.Models.Domain;
using PFSoftware.FinancesApi.Models.ViewModels;
using PFSoftware.FinancesApi.Services;
using System;
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
                return Ok(_mapper.Map<FinancialTransactionViewModel>(FinancialTransactionItem));

            return NotFound();
        }

        //POST api/financialTransactions
        [HttpPost]
        public ActionResult<FinancialTransactionViewModel> CreateFinancialTransaction(CreateEditFinancialTransactionRequest request)
        {
            if (request.Date == DateTime.MinValue || request.PayeeId == 0 || request.MajorCategoryId == 0 || request.MinorCategoryId == 0 || request.AccountId == 0)
                return Problem("Financial transactions require valid input for date, payeeId, majorCategoryId, minorCategoryId, and accountId.");

            FinancialTransaction newFinancialTransaction = _mapper.Map<FinancialTransaction>(request);
            _service.CreateFinancialTransaction(newFinancialTransaction);

            FinancialTransactionViewModel financialTransactionViewModel = _mapper.Map<FinancialTransactionViewModel>(newFinancialTransaction);

            return CreatedAtRoute(nameof(GetFinancialTransactionById), new { Id = financialTransactionViewModel.Id }, financialTransactionViewModel);
        }

        //POST api/financialTransactions/{id}
        [HttpPost("{id}")]
        public ActionResult UpdateFinancialTransaction(int id, CreateEditFinancialTransactionRequest request)
        {
            FinancialTransaction financialTransaction = _service.GetFinancialTransactionById(id);
            if (financialTransaction == null)
                return NotFound();

            if (request.Id != null && request.Id != id)
                return ValidationProblem("The ID in the model doesn't match the ID the request was made on.");

            _service.UpdateFinancialTransaction(request, financialTransaction);
            return NoContent();
        }

        //DELETE api/financialTransactions/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteFinancialTransaction(int id)
        {
            FinancialTransaction financialTransaction = _service.GetFinancialTransactionById(id);
            if (financialTransaction == null)
                return NotFound();

            _service.DeleteFinancialTransaction(financialTransaction);
            return NoContent();
        }
    }
}