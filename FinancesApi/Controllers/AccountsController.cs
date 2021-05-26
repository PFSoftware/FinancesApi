using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
            Account AccountItem = _service.GetAccountById(id);
            if (AccountItem != null)
            {
                return Ok(_mapper.Map<AccountViewModel>(AccountItem));
            }
            return NotFound();
        }

        //POST api/accounts
        [HttpPost]
        public ActionResult<AccountViewModel> CreateAccount(Account account)
        {
            Account accountModel = _mapper.Map<Account>(account);
            _service.CreateAccount(accountModel);

            var AccountViewModel = _mapper.Map<AccountViewModel>(accountModel);

            return CreatedAtRoute(nameof(GetAccountById), new { Id = AccountViewModel.Id }, AccountViewModel);
        }

        //PUT api/accounts/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateAccount(int id, Account account)
        {
            Account accountModelFromRepo = _service.GetAccountById(id);
            if (accountModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(account, accountModelFromRepo);

            _service.UpdateAccount(id, accountModelFromRepo);

            return NoContent();
        }

        //PATCH api/accounts/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialAccountUpdate(int id, JsonPatchDocument<Account> patchDoc)
        {
            var accountModelFromRepo = _service.GetAccountById(id);
            if (accountModelFromRepo == null)
            {
                return NotFound();
            }

            var AccountToPatch = _mapper.Map<Account>(accountModelFromRepo);
            patchDoc.ApplyTo(AccountToPatch, ModelState);

            if (!TryValidateModel(AccountToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(AccountToPatch, accountModelFromRepo);

            _service.UpdateAccount(id, accountModelFromRepo);

            return NoContent();
        }

        //DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id)
        {
            var accountModelFromRepo = _service.GetAccountById(id);
            if (accountModelFromRepo == null)
            {
                return NotFound();
            }
            _service.DeleteAccount(accountModelFromRepo);

            return NoContent();
        }
    }
}