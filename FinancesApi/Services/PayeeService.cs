using PFSoftware.FinancesApi.Data;
using PFSoftware.FinancesApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PFSoftware.FinancesApi.Services
{
    public class PayeeService
    {
        private readonly AppDbContext _context;

        public PayeeService(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePayee(Payee payee)
        {
            if (payee == null)
                throw new ArgumentNullException(nameof(payee));

            _context.Payees.Add(payee);
            _context.SaveChanges();
        }

        public void DeletePayee(Payee payee)
        {
            if (payee == null)
                throw new ArgumentNullException(nameof(payee));

            _context.Payees.Remove(payee);
            _context.SaveChanges();
        }

        public IEnumerable<Payee> GetAllPayees()
        {
            return _context.Payees.ToList();
        }

        public Payee GetPayeeById(int id)
        {
            return _context.Payees.FirstOrDefault(v => v.Id == id);
        }

        public void UpdatePayee(int id, Payee payee)
        {
            //Nothing
        }
    }
}