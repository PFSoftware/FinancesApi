using Microsoft.EntityFrameworkCore;
using PFSoftware.FinancesApi.Data;
using PFSoftware.FinancesApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PFSoftware.FinancesApi.Services
{
    public class FinancialTransactionService
    {
        private readonly AppDbContext _context;

        public FinancialTransactionService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateFinancialTransaction(FinancialTransaction financialTransaction)
        {
            if (financialTransaction == null)
                throw new ArgumentNullException(nameof(financialTransaction));

            _context.FinancialTransactions.Add(financialTransaction);
            _context.SaveChanges();
        }

        public void DeleteFinancialTransaction(FinancialTransaction financialTransaction)
        {
            if (financialTransaction == null)
                throw new ArgumentNullException(nameof(financialTransaction));

            _context.FinancialTransactions.Remove(financialTransaction);
            _context.SaveChanges();
        }

        public IEnumerable<FinancialTransaction> GetAllFinancialTransactions()
        {
            return _context
                .FinancialTransactions
                .Include(x => x.Account)
                .Include(x => x.MajorCategory)
                .Include(x => x.MinorCategory)
                .Include(x => x.Payee)
                .ToList();
        }

        public FinancialTransaction GetFinancialTransactionById(int id)
        {
            return _context
                .FinancialTransactions
                .Include(x => x.Account)
                .Include(x => x.MajorCategory)
                .Include(x => x.MinorCategory)
                .Include(x => x.Payee)
                .FirstOrDefault(v => v.Id == id);
        }

        public void UpdateFinancialTransaction(int id, FinancialTransaction financialTransaction)
        {
            //Nothing
        }
    }
}