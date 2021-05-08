namespace PFSoftware.FinancesApi.Models.ViewModels
{
    /// <summary>Represents a payee for a <see cref="FinancialTransactionViewModel"/>.</summary>
    public class PayeeViewModel
    {
        /// <summary>The ID of the <see cref="PayeeViewModel"/>.</summary>
        public int Id { get; set; }

        /// <summary>The name of the <see cref="PayeeViewModel"/>.</summary>
        public string Name { get; set; }
    }
}