namespace PFSoftware.FinancesApi.Models.Domain
{
    /// <summary>A minor category associated with a <see cref="MajorCategory"/>.</summary>
    public class MinorCategory
    {
        /// <summary>The ID of the <see cref="MinorCategory"/>.</summary>
        public int Id { get; set; }

        /// <summary>The name of the <see cref="MinorCategory"/>.</summary>
        public string Name { get; set; }
    }
}