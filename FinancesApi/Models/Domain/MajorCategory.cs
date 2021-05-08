using System.Collections.Generic;

namespace PFSoftware.FinancesApi.Models.Domain
{
    /// <summary>Represents a major category for <see cref="FinancialTransaction"/>s.</summary>
    public class MajorCategory
    {
        /// <summary>Name of <see cref="MajorCategory"/>.</summary>
        public string Name { get; set; }

        /// <summary>List of <see cref="MinorCategoryViewModel"/> related to the <see cref="MajorCategory"/>.</summary>
        public List<MinorCategory> MinorCategories { get; set; } = new List<MinorCategory>();

        public override string ToString() => Name;
    }
}