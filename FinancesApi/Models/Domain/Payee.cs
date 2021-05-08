namespace PFSoftware.FinancesApi.Models.Domain
{
    /// <summary>Represents a payee for a <see cref="FinancialTransaction"/>.</summary>
    public class Payee
    {
        /// <summary>The ID of the <see cref="Payee"/>.</summary>
        public int Id { get; set; }

        /// <summary>The name of the <see cref="Payee"/>.</summary>
        public string Name { get; set; }
    }
}