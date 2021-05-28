using Microsoft.EntityFrameworkCore;
using PFSoftware.FinancesApi.Constants;
using PFSoftware.FinancesApi.Data;
using PFSoftware.FinancesApi.Models.Api.Requests;
using PFSoftware.FinancesApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PFSoftware.FinancesApi.Services
{
    public class AccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void DeleteAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context
                .Accounts
                .Include(x => x.Transactions).ThenInclude(x => x.MajorCategory)
                .Include(x => x.Transactions).ThenInclude(x => x.MinorCategory)
                .Include(x => x.Transactions).ThenInclude(x => x.Payee)
                .ToList();
        }

        public Account GetAccountById(int id)
        {
            return _context
                .Accounts
                .Include(x => x.Transactions).ThenInclude(x => x.MajorCategory)
                .Include(x => x.Transactions).ThenInclude(x => x.MinorCategory)
                .Include(x => x.Transactions).ThenInclude(x => x.Payee)
                .FirstOrDefault(v => v.Id == id);
        }

        public void UpdateAccount(CreateEditAccountRequest request, Account account)
        {
            if (!string.IsNullOrWhiteSpace(request.Name))
                account.Name = request.Name;
            if (request.AccountType != AccountType.None)
                account.AccountType = request.AccountType;
            if (request.Transactions != null && request.Transactions.Count > 0)
                account.Transactions = new List<FinancialTransaction>(request.Transactions);
            _context.SaveChanges();
        }
    }
}