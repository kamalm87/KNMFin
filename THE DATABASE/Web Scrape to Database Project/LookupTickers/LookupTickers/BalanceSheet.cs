//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LookupTickers
{
    using System;
    using System.Collections.Generic;
    
    public partial class BalanceSheet
    {
        public BalanceSheet()
        {
            this.InfoCompanies = new HashSet<InfoCompany>();
        }
    
        public int ID { get; set; }
        public bool Annual { get; set; }
        public System.DateTime PeriodEnd { get; set; }
        public Nullable<decimal> Cash_and_Equivalents { get; set; }
        public Nullable<decimal> Short_Term_Investments { get; set; }
        public Nullable<decimal> Cash_and_Short_Term_Investments { get; set; }
        public Nullable<decimal> Accounts_Receivable__Trade__Net { get; set; }
        public Nullable<decimal> Receivables__Other { get; set; }
        public Nullable<decimal> Total_Receivables__Net { get; set; }
        public Nullable<decimal> Total_Inventory { get; set; }
        public Nullable<decimal> Prepaid_Expenses { get; set; }
        public Nullable<decimal> Other_Current_Assets__Total { get; set; }
        public Nullable<decimal> Total_Current_Assets { get; set; }
        public Nullable<decimal> Property_and_Plant_and_Equipment__Total__Gross { get; set; }
        public Nullable<decimal> Accumulated_Depreciation__Total { get; set; }
        public Nullable<decimal> Goodwill__Net { get; set; }
        public Nullable<decimal> Intangibles__Net { get; set; }
        public Nullable<decimal> Long_Term_Investments { get; set; }
        public Nullable<decimal> Other_Long_Term_Assets__Total { get; set; }
        public Nullable<decimal> Total_Assets { get; set; }
        public Nullable<decimal> Accounts_Payable { get; set; }
        public Nullable<decimal> Accrued_Expenses { get; set; }
        public Nullable<decimal> Notes_Payable_and_Short_Term_Debt { get; set; }
        public Nullable<decimal> Current_Port_of_LT_Debt_and_Capital_Leases { get; set; }
        public Nullable<decimal> Other_Current_liabilities__Total { get; set; }
        public Nullable<decimal> Total_Current_Liabilities { get; set; }
        public Nullable<decimal> Long_Term_Debt { get; set; }
        public Nullable<decimal> Capital_Lease_Obligations { get; set; }
        public Nullable<decimal> Total_Long_Term_Debt { get; set; }
        public Nullable<decimal> Total_Debt { get; set; }
        public Nullable<decimal> Deferred_Income_Tax { get; set; }
        public Nullable<decimal> Minority_Interest { get; set; }
        public Nullable<decimal> Other_Liabilities__Total { get; set; }
        public Nullable<decimal> Total_Liabilities { get; set; }
        public Nullable<decimal> Redeemable_Preferred_Stock__Total { get; set; }
        public Nullable<decimal> Preferred_Stock__Non_Redeemable__Net { get; set; }
        public Nullable<decimal> Common_Stock__Total { get; set; }
        public Nullable<decimal> Additional_PaidIn_Capital { get; set; }
        public Nullable<decimal> Retained_Earnings__Accumulated_Deficit_ { get; set; }
        public Nullable<decimal> Treasury_Stock__Common { get; set; }
        public Nullable<decimal> Other_Equity__Total { get; set; }
        public Nullable<decimal> Total_Equity { get; set; }
        public Nullable<decimal> Total_Liabilities_and_Shareholders_Equity { get; set; }
        public Nullable<decimal> Shares_Outs__Common_Stock_Primary_Issue { get; set; }
        public Nullable<decimal> Total_Common_Shares_Outstanding { get; set; }
    
        public virtual ICollection<InfoCompany> InfoCompanies { get; set; }
    }
}