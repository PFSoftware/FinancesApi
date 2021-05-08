using System;

namespace PFSoftware.FinancesApi.Models.ViewModels
{
    /// <summary>Represents a monetary transaction in an account.</summary>
    public class FinancialTransactionViewModel
    {
        #region Modifying Properties

        /// <summary>ID of the <see cref="FinancialTransactionViewModel"/>.</summary>
        public int Id { get; set; }

        /// <summary>Date the <see cref="FinancialTransactionViewModel"/> occurred.</summary>
        public DateTime Date { get; set; }

        /// <summary>The entity the <see cref="FinancialTransactionViewModel"/> revolves around.</summary>
        public string Payee { get; set; }

        /// <summary>Primary category regarding the <see cref="FinancialTransactionViewModel"/>.</summary>
        public string MajorCategory { get; set; }

        /// <summary>Secondary category regarding the <see cref="FinancialTransactionViewModel"/>.</summary>
        public string MinorCategory { get; set; }

        /// <summary>Extra information regarding the <see cref="FinancialTransactionViewModel"/>.</summary>
        public string Memo { get; set; }

        /// <summary>How much money left the account during <see cref="FinancialTransactionViewModel"/>.</summary>
        public decimal Outflow { get; set; }

        /// <summary>How much money entered the account during <see cref="FinancialTransactionViewModel"/>.</summary>
        public decimal Inflow { get; set; }

        /// <summary>ID of the <see cref="AccountViewModel"/> associated with the <see cref="FinancialTransactionViewModel"/>.</summary>
        public int AccountId { get; set; }

        /// <summary>Account associated with the <see cref="FinancialTransactionViewModel"/>.</summary>
        public AccountViewModel Account { get; set; }

        #endregion Modifying Properties

        #region Helper Properties

        /// <summary>Date the transaction occurred, formatted properly</summary>
        public string DateToString => Date.ToString("yyyy/MM/dd");

        /// <summary>How much money entered the account during Transaction, formatted to currency</summary>
        public string InflowToString => Inflow.ToString("C2");

        /// <summary>How much money left the account during Transaction, formatted to currency</summary>
        public string OutflowToString => Outflow.ToString("C2");

        #endregion Helper Properties

        public sealed override string ToString() => string.Join(" - ", DateToString, Account, Payee);
    }
}