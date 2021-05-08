using System;

namespace PFSoftware.FinancesApi.Models.Domain
{
    /// <summary>Represents a monetary transaction in an account.</summary>
    public class FinancialTransaction
    {
        #region Modifying Properties

        /// <summary>ID of the <see cref="FinancialTransaction"/>.</summary>
        public int Id { get; set; }

        /// <summary>Date the <see cref="FinancialTransaction"/> occurred.</summary>
        public DateTime Date { get; set; }

        /// <summary>The entity the <see cref="FinancialTransaction"/> revolves around.</summary>
        public string Payee { get; set; }

        /// <summary>Primary category regarding the <see cref="FinancialTransaction"/>.</summary>
        public string MajorCategory { get; set; }

        /// <summary>Secondary category regarding the <see cref="FinancialTransaction"/>.</summary>
        public string MinorCategory { get; set; }

        /// <summary>Extra information regarding the <see cref="FinancialTransaction"/>.</summary>
        public string Memo { get; set; }

        /// <summary>How much money left the account during <see cref="FinancialTransaction"/>.</summary>
        public decimal Outflow { get; set; }

        /// <summary>How much money entered the account during <see cref="FinancialTransaction"/>.</summary>
        public decimal Inflow { get; set; }

        /// <summary>ID of the <see cref="Account"/> associated with the <see cref="FinancialTransaction"/>.</summary>
        public int AccountId { get; set; }

        /// <summary>Account associated with the <see cref="FinancialTransaction"/>.</summary>
        public Account Account { get; set; }

        #endregion Modifying Properties

        #region Override Operators

        private static bool Equals(FinancialTransaction left, FinancialTransaction right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return left.Id == right.Id && DateTime.Equals(left.Date, right.Date)
                   && string.Equals(left.Payee, right.Payee, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(left.MajorCategory, right.MajorCategory, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(left.MinorCategory, right.MinorCategory, StringComparison.OrdinalIgnoreCase)
                   && string.Equals(left.Memo, right.Memo, StringComparison.OrdinalIgnoreCase)
                   && left.Outflow == right.Outflow && left.Inflow == right.Inflow
                   && left.Account == right.Account;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as FinancialTransaction);

        public bool Equals(FinancialTransaction otherTransaction) => Equals(this, otherTransaction);

        public static bool operator ==(FinancialTransaction left, FinancialTransaction right) => Equals(left, right);

        public static bool operator !=(FinancialTransaction left, FinancialTransaction right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        #endregion Override Operators
    }
}