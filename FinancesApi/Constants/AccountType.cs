using System.Runtime.Serialization;

namespace PFSoftware.FinancesApi.Constants
{
    /// <summary>Represents the various types of accounts available to select.</summary>
    public enum AccountType
    {
        /// <summary>Cash carried on hand.</summary>
        [EnumMember(Value = "Cash")]
        Cash,

        /// <summary>Checking account.</summary>
        [EnumMember(Value = "Checking")]
        Checking,

        /// <summary>Credit card.</summary>
        [EnumMember(Value = "Credit Card")]
        CreditCard,

        /// <summary>Merchant account.</summary>
        [EnumMember(Value = "Merchant")]
        Merchant,

        /// <summary>Prepaid account.</summary>
        [EnumMember(Value = "Prepaid")]
        Prepaid,

        /// <summary>Savings account.</summary>
        [EnumMember(Value = "Savings")]
        Savings
    }
}