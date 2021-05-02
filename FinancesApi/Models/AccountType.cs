using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFSoftware.FinancesApi.Models
{
    /// <summary>Represents the various types of accounts available to select.</summary>
    public enum AccountType
    {
        /// <summary>Cash carried on hand.</summary>
        Cash,

        /// <summary>Checking account.</summary>
        Checking,

        /// <summary>Credit card.</summary>
        CreditCard,

        /// <summary>Merchant account.</summary>
        Merchant,

        /// <summary>Prepaid account.</summary>
        Prepaid,

        /// <summary>Savings account.</summary>
        Savings
    }
}