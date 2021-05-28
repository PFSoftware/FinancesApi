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
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _service;
        private readonly IMapper _mapper;

        public AccountsController(AccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET api/accounts
        [HttpGet]
        public ActionResult<IEnumerable<AccountViewModel>> GetAllAccounts()
        {
            IEnumerable<Account> accountItems = _service.GetAllAccounts();

            return Ok(_mapper.Map<IEnumerable<AccountViewModel>>(accountItems));
        }

        //GET api/accounts/{id}
        [HttpGet("{id}", Name = "GetAccountById")]
        public ActionResult<AccountViewModel> GetAccountById(int id)
        {
            Account account = _service.GetAccountById(id);
            if (account != null)
                return Ok(_mapper.Map<AccountViewModel>(account));

            return NotFound();
        }

        //POST api/accounts
        [HttpPost]
        public ActionResult<AccountViewModel> CreateAccount(CreateEditAccountRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.AccountType == Constants.AccountType.None)
                return Problem("A valid name and accountType are required. Valid account types include: Cash, Checking, CreditCard, Merchant, Prepaid, Savings.");

            Account newAccount = _mapper.Map<Account>(request);
            _service.CreateAccount(newAccount);

            AccountViewModel AccountViewModel = _mapper.Map<AccountViewModel>(newAccount);

            return CreatedAtRoute(nameof(GetAccountById), new { Id = AccountViewModel.Id }, AccountViewModel);
        }

        //POST api/accounts/{id}
        [HttpPost("{id}")]
        public ActionResult UpdateAccount(int id, CreateEditAccountRequest request)
        {
            Account account = _service.GetAccountById(id);
            if (account == null)
                return NotFound();

            if (request.Id != null && request.Id != id)
                return ValidationProblem("The ID in the model doesn't match the ID the request was made on.");

            _service.UpdateAccount(request, account);
            return NoContent();
        }

        //DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id)
        {
            Account account = _service.GetAccountById(id);
            if (account == null)
                return NotFound();

            _service.DeleteAccount(account);
            return NoContent();
        }
    }
}