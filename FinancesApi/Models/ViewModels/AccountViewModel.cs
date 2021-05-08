using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PFSoftware.FinancesApi.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFSoftware.FinancesApi.Models.ViewModels
{
    /// <summary>Represents an account where money is credited/debited.</summary>
    public class AccountViewModel
    {
        /// <summary>Name of the <see cref="AccountViewModel"/>.</summary>
        public string Name { get; set; }

        /// <summary>Type of the <see cref="AccountViewModel"/>.</summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AccountType AccountType { get; set; }

        /// <summary>Collection of all the transactions in the account</summary>
        public List<FinancialTransactionViewModel> AllTransactions { get; set; } = new List<FinancialTransactionViewModel>();

        /// <summary>Balance of the account</summary>
        public decimal Balance => AllTransactions.Sum(transaction => (-1 * transaction.Outflow) + transaction.Inflow);

        /// <summary>Balance of the account, formatted to currency</summary>
        public string BalanceToString => Balance.ToString("C2");

        /// <summary>Balance of the account, formatted to currency, with preceding text</summary>
        public string BalanceToStringWithText => $"Balance: {BalanceToString}";

        public sealed override string ToString() => Name;
    }
}