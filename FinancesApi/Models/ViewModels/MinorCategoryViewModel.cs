namespace PFSoftware.FinancesApi.Models.ViewModels
{
    /// <summary>A minor category associated with a <see cref="MajorCategoryViewModel"/>.</summary>
    public class MinorCategoryViewModel
    {
        /// <summary>The ID of the <see cref="MinorCategoryViewModel"/>.</summary>
        public int Id { get; set; }

        /// <summary>The name of the <see cref="MinorCategoryViewModel"/>.</summary>
        public string Name { get; set; }

        /// <summary>The ID of the major category associated with this <see cref="MinorCategoryViewModel"/>.</summary>
        public int MajorCategoryId { get; set; }

        /// <summary>The major category associated with this <see cref="MinorCategoryViewModel"/>.</summary>
        public MajorCategoryViewModel MajorCategory { get; set; }
    }
}