using System.Collections.Generic;

namespace PFSoftware.FinancesApi.Models.ViewModels
{
    /// <summary>Represents a major category for <see cref="FinancialTransactionViewModel"/>s.</summary>
    public class MajorCategoryViewModel
    {
        /// <summary>The ID of the <see cref="MajorCategoryViewModel"/>.</summary>
        public int Id { get; set; }

        /// <summary>Name of <see cref="MajorCategoryViewModel"/>.</summary>
        public string Name { get; set; }

        /// <summary>List of <see cref="MinorCategory"/> related to the <see cref="MajorCategoryViewModel"/>.</summary>
        public List<MinorCategoryViewModel> MinorCategories { get; set; } = new List<MinorCategoryViewModel>();

        public override string ToString() => Name;
    }
}