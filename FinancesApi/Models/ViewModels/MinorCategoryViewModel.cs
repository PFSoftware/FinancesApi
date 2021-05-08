namespace PFSoftware.FinancesApi.Models.ViewModels
{
    /// <summary>A minor category associated with a <see cref="MajorCategoryViewModel"/>.</summary>
    public class MinorCategoryViewModel
    {
        /// <summary>The ID of the <see cref="MinorCategoryViewModel"/>.</summary>
        public int Id { get; set; }

        /// <summary>The name of the <see cref="MinorCategoryViewModel"/>.</summary>
        public string Name { get; set; }
    }
}