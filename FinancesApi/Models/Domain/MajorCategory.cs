using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PFSoftware.FinancesApi.Models.Domain
{
    /// <summary>Represents a major category for <see cref="FinancialTransaction"/>s.</summary>
    public class MajorCategory
    {
        /// <summary>The ID of the <see cref="MajorCategory"/>.</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>Name of <see cref="MajorCategory"/>.</summary>
        [Required]
        public string Name { get; set; }

        /// <summary>List of <see cref="MinorCategory"/> related to the <see cref="MajorCategory"/>.</summary>
        public List<MinorCategory> MinorCategories { get; set; } = new List<MinorCategory>();

        private static bool Equals(MajorCategory left, MajorCategory right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return left.Id == right.Id
                && left.Name == right.Name
                && left.MinorCategories.Count == right.MinorCategories.Count
                && !left.MinorCategories.Except(right.MinorCategories).Any()
                && !right.MinorCategories.Except(left.MinorCategories).Any();
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as MajorCategory);

        public bool Equals(MajorCategory otherTransaction) => Equals(this, otherTransaction);

        public static bool operator ==(MajorCategory left, MajorCategory right) => Equals(left, right);

        public static bool operator !=(MajorCategory left, MajorCategory right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public override string ToString() => Name;
    }
}