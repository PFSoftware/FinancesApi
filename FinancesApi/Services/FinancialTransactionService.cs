using Microsoft.EntityFrameworkCore;
using PFSoftware.FinancesApi.Data;
using PFSoftware.FinancesApi.Models.Api.Requests;
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

        public void UpdateFinancialTransaction(CreateEditFinancialTransactionRequest request, FinancialTransaction financialTransaction)
        {
            if (request.Date != DateTime.MinValue)
                financialTransaction.Date = request.Date;
            if (request.PayeeId != 0)
                financialTransaction.PayeeId = request.PayeeId;
            if (request.MajorCategoryId != 0)
                financialTransaction.MajorCategoryId = request.MajorCategoryId;
            if (request.MinorCategoryId != 0)
                financialTransaction.MinorCategoryId = request.MinorCategoryId;
            if (!string.IsNullOrWhiteSpace(request.Memo))
                financialTransaction.Memo = request.Memo;
            if (request.Outflow != 0m)
                financialTransaction.Outflow = request.Outflow;
            if (request.Inflow != 0m)
                financialTransaction.Inflow = request.Inflow;
            if (request.AccountId != 0)
                financialTransaction.AccountId = request.AccountId;
            _context.SaveChanges();
        }
    }
}