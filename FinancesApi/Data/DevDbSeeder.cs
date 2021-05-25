using PFSoftware.FinancesApi.Constants;
using PFSoftware.FinancesApi.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PFSoftware.FinancesApi.Data
{
    public class DevDbSeeder
    {
        private AppDbContext _context;

        public DevDbSeeder()
        {
        }

        public async Task SeedDatabase(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            await SeedCategories();
            await SeedPayees();
            await SeedAccounts();
        }

        private async Task SeedCategories()
        {
            _context.MajorCategories.Add(new MajorCategory
            {
                Name = "Income",
                MinorCategories = {
                    new MinorCategory { Name = "Cash"},
                    new MinorCategory { Name = "Check"},
                    new MinorCategory { Name = "Deposit"},
                    new MinorCategory { Name = "Interest"},
                    new MinorCategory { Name = "Other"},
                    new MinorCategory { Name = "Starting Balance"}
                }
            });
            _context.MajorCategories.Add(new MajorCategory
            {
                Name = "Monthly Bills",
                MinorCategories = {
                    new MinorCategory { Name = "Rent"},
                    new MinorCategory { Name = "Electricity"},
                }
            });
            _context.MajorCategories.Add(new MajorCategory
            {
                Name = "Everyday Expenses",
                MinorCategories = {
                    new MinorCategory { Name = "Groceries"},
                    new MinorCategory { Name = "Fast Food"}
                }
            });
            _context.MajorCategories.Add(new MajorCategory
            {
                Name = "Transfer",
                MinorCategories = {
                    new MinorCategory { Name = "Transfer"}
                }
            });

            await _context.SaveChangesAsync();
        }

        private async Task SeedPayees()
        {
            _context.Payees.Add(new Payee { Name = "Income" });
            _context.Payees.Add(new Payee { Name = "Taco Bell" });
            _context.Payees.Add(new Payee { Name = "Walmart" });
            _context.Payees.Add(new Payee { Name = "Electric Company" });
            _context.Payees.Add(new Payee { Name = "Transfer" });
            _context.Payees.Add(new Payee { Name = "Landlord" });

            await _context.SaveChangesAsync();
        }

        private async Task SeedAccounts()
        {
            DateTime date = DateTime.Parse("2021-05-10");

            Account account1 = new Account
            {
                Name = "Bank Account 1",
                AccountType = AccountType.Checking,
                Transactions = new List<FinancialTransaction>()
            };
            Account account2 = new Account
            {
                Name = "Credit Card 1",
                AccountType = AccountType.CreditCard,
                Transactions = new List<FinancialTransaction>()
            };

            account1.AddTransaction(new FinancialTransaction
            {
                Date = date,
                PayeeId = 1,
                MajorCategoryId = 1,
                MinorCategoryId = 6,
                Inflow = 3605.72m,
                AccountId = 1
            });
            account2.AddTransaction(new FinancialTransaction
            {
                Date = date,
                PayeeId = 2,
                MajorCategoryId = 3,
                MinorCategoryId = 10,
                Outflow = 9.62m,
                AccountId = 2
            });

            _context.Accounts.Add(account1);
            _context.Accounts.Add(account2);

            await _context.SaveChangesAsync();
        }
    }
}