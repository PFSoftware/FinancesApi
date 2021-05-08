using PFSoftware.FinancesApi.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFSoftware.FinancesApi.Models.Domain
{
    /// <summary>Represents an account where money is credited/debited.</summary>
    public class Account
    {
        /// <summary>Name of the <see cref="Account"/>.</summary>
        public string Name { get; set; }

        /// <summary>Type of the <see cref="Account"/>.</summary>
        public AccountType AccountType { get; set; }

        /// <summary>Collection of all the transactions in the account</summary>
        public List<FinancialTransaction> AllTransactions { get; set; } = new List<FinancialTransaction>();

        #region Transaction Management

        /// <summary>Adds a transaction to this account.</summary>
        /// <param name="transaction">Transaction to be added</param>
        internal void AddTransaction(FinancialTransaction transaction)
        {
            AllTransactions.Add(transaction);
            Sort();
        }

        /// <summary>Modifies a transaction in this account.</summary>
        /// <param name="index">Index of transaction to be modified</param>
        /// <param name="transaction">Transaction to replace current in list</param>
        internal void ModifyTransaction(int index, FinancialTransaction transaction)
        {
            if (transaction.Account == Name)
                AllTransactions[index] = transaction;
            else
                RemoveTransaction(index);
            Sort();
        }

        /// <summary>Removes a transaction from this account.</summary>
        /// <param name="transaction">Transaction to be added</param>
        internal void RemoveTransaction(FinancialTransaction transaction) => AllTransactions.Remove(transaction);

        /// <summary>Removes a transaction from this account at a specific index.</summary>
        /// <param name="index">Location in the List to remove the transaction</param>
        internal void RemoveTransaction(int index) => AllTransactions.RemoveAt(index);

        #endregion Transaction Management

        /// <summary>Sorts all Transactions in the Account by date.</summary>
        internal void Sort() => AllTransactions = AllTransactions.OrderByDescending(transaction => transaction.Date)
            .ThenByDescending(transaction => transaction.Id).ToList();

        #region Override Operators

        private static bool Equals(Account left, Account right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return string.Equals(left.Name, right.Name, StringComparison.OrdinalIgnoreCase) && left.AccountType == right.AccountType && (left.AllTransactions.Count == right.AllTransactions.Count && !left.AllTransactions.Except(right.AllTransactions).Any());
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Account);

        public bool Equals(Account otherAccount) => Equals(this, otherAccount);

        public static bool operator ==(Account left, Account right) => Equals(left, right);

        public static bool operator !=(Account left, Account right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public sealed override string ToString() => Name;

        #endregion Override Operators
    }
}