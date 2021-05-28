using System;
using System.ComponentModel.DataAnnotations;

namespace PFSoftware.FinancesApi.Models.Domain
{
    /// <summary>Represents a monetary transaction in an account.</summary>
    public class FinancialTransaction
    {
        /// <summary>ID of the <see cref="FinancialTransaction"/>.</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Date the <see cref="FinancialTransaction"/> occurred.</summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>The ID of the payee.</summary>
        [Required]
        public int PayeeId { get; set; }

        /// <summary>The entity the <see cref="FinancialTransaction"/> revolves around.</summary>
        public Payee Payee { get; set; }

        /// <summary>The ID of the major Category.</summary>
        [Required]
        public int MajorCategoryId { get; set; }

        /// <summary>Primary category regarding the <see cref="FinancialTransaction"/>.</summary>
        public MajorCategory MajorCategory { get; set; }

        /// <summary>The ID of the minor category.</summary>
        [Required]
        public int MinorCategoryId { get; set; }

        /// <summary>Secondary category regarding the <see cref="FinancialTransaction"/>.</summary>
        public MinorCategory MinorCategory { get; set; }

        /// <summary>Extra information regarding the <see cref="FinancialTransaction"/>.</summary>
        public string Memo { get; set; }

        /// <summary>How much money left the account during <see cref="FinancialTransaction"/>.</summary>
        public decimal Outflow { get; set; }

        /// <summary>How much money entered the account during <see cref="FinancialTransaction"/>.</summary>
        public decimal Inflow { get; set; }

        /// <summary>ID of the account associated with the <see cref="FinancialTransaction"/>.</summary>
        [Required]
        public int AccountId { get; set; }

        /// <summary>Account associated with the <see cref="FinancialTransaction"/>.</summary>
        public Account Account { get; set; }

        private static bool Equals(FinancialTransaction left, FinancialTransaction right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return left.Id == right.Id
                && DateTime.Equals(left.Date, right.Date)
                && left.PayeeId == right.PayeeId
                && left.MajorCategoryId == right.MajorCategoryId
                && left.MinorCategoryId == right.MinorCategoryId
                && string.Equals(left.Memo, right.Memo, StringComparison.OrdinalIgnoreCase)
                && left.Outflow == right.Outflow
                && left.Inflow == right.Inflow
                && left.AccountId == right.AccountId;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as FinancialTransaction);

        public bool Equals(FinancialTransaction otherTransaction) => Equals(this, otherTransaction);

        public static bool operator ==(FinancialTransaction left, FinancialTransaction right) => Equals(left, right);

        public static bool operator !=(FinancialTransaction left, FinancialTransaction right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;
    }
}