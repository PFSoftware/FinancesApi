using System.ComponentModel.DataAnnotations;

namespace PFSoftware.FinancesApi.Models.Domain
{
    /// <summary>Represents a payee for a <see cref="FinancialTransaction"/>.</summary>
    public class Payee
    {
        /// <summary>The ID of the <see cref="Payee"/>.</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>The name of the <see cref="Payee"/>.</summary>
        [Required]
        public string Name { get; set; }

        private static bool Equals(Payee left, Payee right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return left.Id == right.Id
                && left.Name == right.Name;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as Payee);

        public bool Equals(Payee otherTransaction) => Equals(this, otherTransaction);

        public static bool operator ==(Payee left, Payee right) => Equals(left, right);

        public static bool operator !=(Payee left, Payee right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public override string ToString() => Name;
    }
}