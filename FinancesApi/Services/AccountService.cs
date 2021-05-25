using Microsoft.EntityFrameworkCore;
using PFSoftware.FinancesApi.Data;
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
                .Include(x => x.Transactions)
                .ToList();
        }

        public Account GetAccountById(int id)
        {
            return _context
                .Accounts
                .Include(x => x.Transactions)
                .FirstOrDefault(v => v.Id == id);
        }

        public void UpdateAccount(int id, Account account)
        {
            //Nothing
        }
    }
}