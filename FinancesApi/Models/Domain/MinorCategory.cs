using System.ComponentModel.DataAnnotations;

namespace PFSoftware.FinancesApi.Models.Domain
{
    /// <summary>A minor category associated with a <see cref="MajorCategory"/>.</summary>
    public class MinorCategory
    {
        /// <summary>The ID of the <see cref="MinorCategory"/>.</summary>
        [Key]
        public int Id { get; set; }

        /// <summary>The name of the <see cref="MinorCategory"/>.</summary>
        [Required]
        public string Name { get; set; }

        /// <summary>The ID of the major category associated with this <see cref="MinorCategory"/>.</summary>
        public int MajorCategoryId { get; set; }

        /// <summary>The major category associated with this <see cref="MinorCategory"/>.</summary>
        public MajorCategory MajorCategory { get; set; }

        private static bool Equals(MinorCategory left, MinorCategory right)
        {
            if (left is null && right is null) return true;
            if (left is null ^ right is null) return false;
            return left.Id == right.Id
                && left.Name == right.Name
                && left.MajorCategoryId == right.MajorCategoryId;
        }

        public sealed override bool Equals(object obj) => Equals(this, obj as MinorCategory);

        public bool Equals(MinorCategory otherTransaction) => Equals(this, otherTransaction);

        public static bool operator ==(MinorCategory left, MinorCategory right) => Equals(left, right);

        public static bool operator !=(MinorCategory left, MinorCategory right) => !Equals(left, right);

        public sealed override int GetHashCode() => base.GetHashCode() ^ 17;

        public override string ToString() => Name;
    }
}