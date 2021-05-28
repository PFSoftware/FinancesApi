using System;

namespace PFSoftware.FinancesApi.Models.Api.Requests
{
    public class CreateEditFinancialTransactionRequest
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public int PayeeId { get; set; }
        public int MajorCategoryId { get; set; }
        public int MinorCategoryId { get; set; }
        public string Memo { get; set; }
        public decimal Outflow { get; set; }
        public decimal Inflow { get; set; }
        public int AccountId { get; set; }
    }
}