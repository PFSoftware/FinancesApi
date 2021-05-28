using PFSoftware.FinancesApi.Constants;
using PFSoftware.FinancesApi.Models.Domain;
using System.Collections.Generic;

namespace PFSoftware.FinancesApi.Models.Api.Requests
{
    public class CreateEditAccountRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public AccountType AccountType { get; set; }
        public List<FinancialTransaction> Transactions { get; set; }
    }
}