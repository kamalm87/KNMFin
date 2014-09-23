using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using KNMFin.Google;

namespace KNMFinExcel.Google
{
    public static class ExcelGoogle
    {

        public static void SaveBalanceSheets( string fileName, CompanyInfo ci )
        {
            fileName = fileName.Contains( ".xslx" ) ? fileName : fileName + ".xlsx";
            System.IO.FileInfo fi = new System.IO.FileInfo( fileName );
            pck = new ExcelPackage( fi );

            foreach ( BalanceSheet bs in ci.BalanceSheets )
            {
                CreateIndividualBalanceSheet( bs );
            }
            pck.Save( );
            pck = null;

        }

        static void CreateIndividualBalanceSheet( BalanceSheet bs )
        {
            
            string tabName = ( bs.Period == Period.Annual ? "A" : "Q" )
                             + "_" +  bs.PeriodEnd.ToShortDateString( ).Replace( "/", "_" );

            var ws = pck.Workbook.Worksheets.Add( tabName );

            ws.Cells [ 1, 1 ].Value = "Balance Sheet for " + ( bs.Period == Period.Annual ? " Year" : " Quarter" ) + " Ending " + bs.PeriodEnd.ToShortDateString( );
            ws.Cells [ "A1:H1" ].Merge = true;
            ws.Cells [ 3, 1 ].Value = "Assets";
            ws.Cells [ "A3:B3" ].Merge = true;
            
            
            // HEADERS
            ws.Cells [ 4, 1 ].Value = "Current Assets";
            ws.Cells [ 5, 1 ].Value = "Cash & Equivalents";
            ws.Cells [ 6, 1 ].Value = "Short Term Investments ";
            ws.Cells [ 7, 1 ].Value = "Accounts Receivable - Trade, Net";
            ws.Cells [ 8, 1 ].Value = "Receivables - Other";
            ws.Cells [ 9, 1 ].Value = "Total Inventory";
            ws.Cells [ 10, 1 ].Value = "Prepaid Expenses";
            ws.Cells [ 11, 1 ].Value = "Other Current Assets, Total";
            ws.Cells [ 12, 1 ].Value = "Total Current Assets";
            ws.Cells [ 13, 1 ].Value = "Long-Term Assets";
            ws.Cells [ 14, 1 ].Value = "Property/Plant/Equipment, Total - Gross";
            ws.Cells [ 15, 1 ].Value = "Goodwill, Net";
            ws.Cells [ 16, 1 ].Value = "Intangibles, Net";
            ws.Cells [ 17, 1 ].Value = "Long-Term Investments";
            ws.Cells [ 18, 1 ].Value = "Other Long Term Assets, Total";
            ws.Cells [   19 ,1].Value = "Total Long-Term Assets";
            ws.Cells [   20, 1 ].Value = "Total Assets";
            
            // VALUES
            ws.Cells [ 4, 2 ].Value = ""; // This a header--blank
            ws.Cells [ 5, 2 ].Value = bs.Cash_and_Equivalents;
            ws.Cells [ 6, 2 ].Value = bs.Short_Term_Investments;
            ws.Cells [ 7, 2 ].Value =  bs.Accounts_Receivable__Trade__Net; // "Accounts Receivable - Trade, Net";
            ws.Cells [ 8, 2 ].Value = bs.Receivables__Other; // "Receivables - Other";
            ws.Cells [ 9, 2 ].Value = bs.Total_Inventory; //"Total Inventory";
            ws.Cells [ 10, 2 ].Value = bs.Prepaid_Expenses; //"Prepaid Expenses";
            ws.Cells [ 11, 2 ].Value = bs.Other_Current_Assets__Total; //"Other Current Assets, Total";
            ws.Cells [ 12, 2 ].Value = bs.Total_Current_Assets; //"Total Current Assets";
            ws.Cells [ 13, 2 ].Value = "";  // This a header--blank //"Long-Term Assets";
            ws.Cells [ 14, 2 ].Value = bs.Property_and_Plant_and_Equipment__Total__Gross; //"Property/Plant/Equipment, Total - Gross";
            ws.Cells [ 15, 2 ].Value = bs.Goodwill__Net;  //"Goodwill, Net";
            ws.Cells [ 16, 2 ].Value = bs.Intangibles__Net; //"Intangibles, Net";
            ws.Cells [ 17, 2 ].Value = bs.Long_Term_Investments; //"Long-Term Investments";
            ws.Cells [ 18, 2 ].Value = bs.Other_Long_Term_Assets__Total; // "Other Long Term Assets, Total";
            ws.Cells [ 19, 2 ].Value = ""; // ??????? // "Total Long-Term Assets";
            ws.Cells [ 20, 2 ].Value = bs.Total_Assets; // "Total Assets";

            for ( int i = 4; i < 21; i++ )
                ws.Cells [ i, 2 ].Style.Numberformat.Format = "\"$\"#,##0;";

          
            ws.Cells [ "A3:B3" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A3:A20" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "B3:B20" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A20:B20" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;



            ws.Cells [ 3, 4 ].Value  = "Liabilities";
            ws.Cells [ 4, 4 ].Value  = "Current Liabilities";
            ws.Cells [ 5, 4 ].Value  = "   Accounts Payable";
            ws.Cells [ 6, 4 ].Value  = "Accrued Expensess ";
            ws.Cells [ 7, 4 ].Value  = "   Notes Payable/Short-Term Debt";
            ws.Cells [ 8, 4 ].Value  = "   Current Port. Of LT Debt/Capital Leases";
            ws.Cells [ 9, 4 ].Value  = "   Other Current Liabilities, Total";
            ws.Cells [ 10, 4 ].Value = "   Total Current Liabilities";
            ws.Cells [ 11, 4 ].Value = "Long-Term Liabilities";
            ws.Cells [ 12, 4 ].Value = "   Long-Term Debt";
            ws.Cells [ 13, 4 ].Value = "   Capital Lease Obligations";
            ws.Cells [ 14, 4 ].Value = "   Total Long Term Debt";
            ws.Cells [ 15, 4 ].Value = "   Total Debt";
            ws.Cells [ 16, 4 ].Value = "   Deferred Income Tax";
            ws.Cells [ 17, 4 ].Value = "   Minority Interest";
            ws.Cells [ 18, 4 ].Value = "   Other Liabilities, Total";
            ws.Cells [ 19, 4 ].Value = "   Total Long-Term Liabilities";
            ws.Cells [ 20, 4 ].Value = "   Total Liabilities";



            ws.Cells [ 3, 5 ].Value = ""; // "Liabilities";
            ws.Cells [ 4, 5 ].Value =  ""; // "Current Liabilities";
            ws.Cells [ 5, 5 ].Value =  bs.Accounts_Payable; // "   Accounts Payable";
            ws.Cells [ 6, 5 ].Value =  bs.Accrued_Expenses; // "Accrued Expensess ";
            ws.Cells [ 7, 5 ].Value =  bs.Notes_Payable_and_Short_Term_Debt; // "   Notes Payable/Short-Term Debt";
            ws.Cells [ 8, 5 ].Value =  bs.Current_Port_of_LT_Debt_and_Capital_Leases; // "   Current Port. Of LT Debt/Capital Leases";
            ws.Cells [ 9, 5 ].Value =  bs.Other_Current_liabilities__Total; // "   Other Current Liabilities, Total";
            ws.Cells [ 10, 5 ].Value = bs.Total_Current_Liabilities; // "   Total Current Liabilities";
            ws.Cells [ 11, 5 ].Value =  ""; // "Long-Term Liabilities";
            ws.Cells [ 12, 5 ].Value =  bs.Long_Term_Debt; // "   Long-Term Debt";
            ws.Cells [ 13, 5 ].Value =  bs.Capital_Lease_Obligations; // "   Capital Lease Obligations";
            ws.Cells [ 14, 5 ].Value =  bs.Total_Long_Term_Debt; // "   Total Long Term Debt";
            ws.Cells [ 15, 5 ].Value =  bs.Total_Debt; // "   Total Debt";
            ws.Cells [ 16, 5 ].Value =  bs.Deferred_Income_Tax; // "   Deferred Income Tax";
            ws.Cells [ 17, 5 ].Value =  bs.Minority_Interest; // "   Minority Interest";
            ws.Cells [ 18, 5 ].Value =  bs.Other_Liabilities__Total; // "   Other Liabilities, Total";
            
            //ws.Cells [ 19, 5 ].Value; // TODO: Add 0 or nulls to prevent errors!
        
            /*

                ( bs.Capital_Lease_Obligations + bs.Total_Long_Term_Debt + bs.Total_Debt
                  + bs.Deferred_Income_Tax + bs.Minority_Interest + bs.Other_Liabilities__Total);// "   Total Long-Term Liabilities";
         */

            ws.Cells [ 20, 5 ].Value = bs.Total_Liabilities; // "   Total Liabilities";


        
            ws.Cells [ "D3:BE" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "D3:E20" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "D3:E20" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "D20:E20" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;

            

            for ( int i = 4; i < 21; i++ )
                ws.Cells [ i, 5 ].Style.Numberformat.Format = "\"$\"#,##0.00;";
            

            ws.Cells [ 22, 1 ].Value = "Equity";
            ws.Cells [ 23, 1 ].Value = "Redeemable Preferred Stock, Total";
            ws.Cells [ 24, 1 ].Value = "   Preferred Stock Non-Redeemable, Net";
            ws.Cells [ 25, 1 ].Value = "Common Stock, Total";
            ws.Cells [ 26, 1 ].Value = "Additional Paid-in Capital";
            ws.Cells [ 27, 1 ].Value = "Retained Earnings (Accumulated Deficit)";
            ws.Cells [ 28, 1 ].Value = "Treasury Stock-Common";
            ws.Cells [ 29, 1 ].Value = "Other Equity, Total";
            ws.Cells [ 30, 1 ].Value = "Total Equity";
            ws.Cells [ 31, 1 ].Value = "Total Liabilities & Shareholders' Equity";
            ws.Cells [ 32, 1 ].Value = "Shares Out - Common Stock Primary Issue";
            ws.Cells [ 33, 1 ].Value = "Total Common Shares Outstanding";

            ws.Cells [ 22, 2 ].Value = "";// "Equity";
            ws.Cells [ 23, 2 ].Value = bs.Redeemable_Preferred_Stock__Total; // "Redeemable Preferred Stock, Total";
            ws.Cells [ 24, 2 ].Value = bs.Preferred_Stock__Non_Redeemable__Net; // "   Preferred Stock Non-Redeemable, Net";
            ws.Cells [ 25, 2 ].Value = bs.Common_Stock__Total; // "Common Stock, Total";
            ws.Cells [ 26, 2 ].Value = bs.Additional_PaidIn_Capital;// "Additional Paid-in Capital";
            ws.Cells [ 27, 2 ].Value = bs.Retained_Earnings__Accumulated_Deficit_; // "Retained Earnings (Accumulated Deficit)";
            ws.Cells [ 28, 2 ].Value = bs.Treasury_Stock__Common; // "Treasury Stock-Common";
            ws.Cells [ 29, 2 ].Value = bs.Other_Equity__Total; // "Other Equity, Total";
            ws.Cells [ 30, 2 ].Value = bs.Total_Equity; // "Total Equity";
            ws.Cells [ 31, 2 ].Value = bs.Total_Liabilities_and_Shareholders_Equity;// "Total Liabilities & Shareholders' Equity";
            ws.Cells [ 32, 2 ].Value = bs.Shares_Outs__Common_Stock_Primary_Issue; // "Shares Out - Common Stock Primary Issue";
            ws.Cells [ 33, 2 ].Value = bs.Total_Common_Shares_Outstanding;  // "Total Common Shares Outstanding";


            ws.Cells [ "A22:B22" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A22:A33" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "B22:B33" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A33:B33" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;


            /*

            for ( int i = 22; i < 34; i++ )
                ws.Cells [ i, 2 ].Style.Numberformat.Format = "\"$\"#,##0;";
            */
        }

        public static void SaveIncomeStatements( string fileName, CompanyInfo ci )
        {
            fileName = fileName.Contains( ".xslx" ) ? fileName : fileName + ".xlsx";
            System.IO.FileInfo fi = new System.IO.FileInfo( fileName );
            pck = new ExcelPackage( fi );

            foreach ( IncomeStatement IS in ci.IncomeStatements )
            {
                CreateIndividualIncomeStatement( IS );
            }
            pck.Save( );
            pck = null;
        }

        static void CreateIndividualIncomeStatement( IncomeStatement IS )
        {
            string tabName = ( IS.Period == Period.Annual ? "A" : "Q" )
                            + "_" + IS.PeriodEnd.ToShortDateString( ).Replace( "/", "_" );

            var ws = pck.Workbook.Worksheets.Add( tabName );

            ws.Cells [ 1, 1 ].Value = "Income Statement for " + ( IS.Period == Period.Annual ? " Year" : " Quarter" ) + " Ending " + IS.PeriodEnd.ToShortDateString( );
            ws.Cells [ "A1:H1" ].Merge = true;


            ws.Cells [ 3, 1 ].Value = "Revenue";
            ws.Cells [ 3, 2 ].Value = IS.Revenue;
            ws.Cells [ 4, 1 ].Value = "Other Revenue, Total";
            ws.Cells [ 4, 2 ].Value = IS.Other_Revenue_Total;
            
            
            
            
            ws.Cells [ 5, 1 ].Value = "Total Revenue";
            ws.Cells [ 5, 2 ].Value = IS.Total_Revenue;

            ws.Cells [ "A5:B5" ].Style.Font.Bold = true;
            ws.Cells [ "A5:B5" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A5:B5" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A5:A5" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "B5:B5" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;



            ws.Cells [ 6, 1 ].Value = "Cost of Revenue, Total";
            ws.Cells [ 6, 2 ].Value = IS.Cost_of_Revenue_Total;

            ws.Cells [ 7, 1 ].Value = "Gross Profit";
            ws.Cells [ 7, 2 ].Value = IS.Gross_Profit;


            ws.Cells [ "A7:B7" ].Style.Font.Bold = true;
            ws.Cells [ "A7:B7" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A7:B7" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A7:A7" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "B7:B7" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;


            ws.Cells [ 8, 1 ].Value = "Selling/General/Admin. Expenses, Total";
            ws.Cells [ 8, 2 ].Value = IS.Selling_and_General_and_Admin_Expenses_Total;
            ws.Cells [ 9, 1 ].Value = "Research & Development";
            ws.Cells [ 9, 2 ].Value = IS.Research_and_Development;
            ws.Cells [ 10, 1 ].Value = "Depreciation/Amortization";
            ws.Cells [ 10, 2 ].Value = IS.Depreciation_and_Amortization;
            ws.Cells [ 11, 1 ].Value = "Interest Expense (Income) - Net Operating";
            ws.Cells [ 11, 2 ].Value = IS.Interest_Expense__Income___less_Net_Operating;
            ws.Cells [ 12, 1 ].Value = "Unusual Expense (Income)";
            ws.Cells [ 12, 2 ].Value = IS.Unusual_Expense___Income__;
            ws.Cells [ 13, 1 ].Value = "Other Operating Expenses, Total";
            ws.Cells [ 14, 2 ].Value = IS.Other_Operating_Expenses_Total;
            ws.Cells [ 15, 1 ].Value = "Total Operating Expense";
            ws.Cells [ 15, 2 ].Value = IS.Total_Operating_Expense;


            ws.Cells [ 16, 1 ].Value = "Operating Income";
            ws.Cells [ 16, 2 ].Value = IS.Operating_Income;


            ws.Cells [ "A16:B16" ].Style.Font.Bold = true;
            ws.Cells [ "A16:B16" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A16:B16" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A16:A16" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "B16:B16" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;


            ws.Cells [ 17, 1 ].Value = "Interest Income (Expense), Net Non-Operating";
            ws.Cells [ 17, 2 ].Value = IS.Interest_Income__Expense___Net_NonOperating;
            ws.Cells [ 18, 1 ].Value = "Gain (Loss) on Sale of Assets";
            ws.Cells [ 18, 2 ].Value = IS.Gain___Loss___on_Sale_of_Assets;
            ws.Cells [ 19, 1 ].Value = "Other, Net";
            ws.Cells [ 19, 2 ].Value = IS.Other_Net;
            ws.Cells [ 20, 1 ].Value = "Income Before Tax";
            ws.Cells [ 20, 2 ].Value = IS.Income_Before_Tax;
            ws.Cells [ 21, 1 ].Value = "Income After Tax";
            ws.Cells [ 21, 2 ].Value = IS.Income_After_Tax;
            ws.Cells [ 22, 1 ].Value = "Minority Interest";
            ws.Cells [ 22, 2 ].Value = IS.Minority_Interest;
            ws.Cells [ 23, 1 ].Value = "Equity In Affiliates";
            ws.Cells [ 23, 2 ].Value = IS.Equity_In_Affiliates;
            ws.Cells [ 24, 1 ].Value = "Net Income Before Extra. Items";
            ws.Cells [ 24, 2 ].Value = IS.Net_Income_Before_Extra_Items;
            ws.Cells [ 25, 1 ].Value = "Accounting Change";
            ws.Cells [ 25, 2 ].Value = IS.Accounting_Change;
            ws.Cells [ 26, 1 ].Value = "Discontinued Operations";
            ws.Cells [ 26, 2 ].Value = IS.Discontinued_Operations;
            ws.Cells [ 27, 1 ].Value = "Extraodinary Item";
            ws.Cells [ 27, 2 ].Value = IS.Extraordinary_Item;


            ws.Cells [ 28, 1 ].Value = "Net Income";
            ws.Cells [ 28, 2 ].Value = IS.Net_Income;

            ws.Cells["A28:B28"].Style.Font.Bold = true;
            ws.Cells [ "A28:B28" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A28:B28" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A28:A28" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "B28:B28" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;



            ws.Cells [ 29, 1 ].Value = "Preferred Dividends";
            ws.Cells [ 29, 2 ].Value = IS.Preferred_Dividends;
            ws.Cells [ 30, 1 ].Value = "Income Available to Common Excl. Items";
            ws.Cells [ 30, 2 ].Value = IS.Income_Available_to_Common_Excl_Extra_Items;
            ws.Cells [ 31, 1 ].Value = "Income Available to Common Incl. Items";
            ws.Cells [ 31, 2 ].Value = IS.Income_Available_to_Common_Incl_Extra_Items;
            ws.Cells [ 32, 1 ].Value = "Basic Weighted Average Shares";
            ws.Cells [ 32, 2 ].Value = IS.Basic_Weighted_Average_Shares;
            ws.Cells [ 33, 1 ].Value = "Basic EPS Excluding Extroadinary Items";
            ws.Cells [ 33, 2 ].Value = IS.Basic_EPS_Excluding_Extraordinary_Items;
            ws.Cells [ 34, 1 ].Value = "Basic EPS Including Extroadinary Items";
            ws.Cells [ 34, 2 ].Value = IS.Basic_EPS_Including_Extraordinary_Items;
            ws.Cells [ 35, 1 ].Value = "Dividends per Share - Common Stock Primary Issue";
            ws.Cells [ 35, 2 ].Value = IS.Dividends_per_Share__less__Common_Stock_Primary_Issue;
            ws.Cells [ 36, 1 ].Value = "Gross Dividends - Common Stock";
            ws.Cells [ 36, 2 ].Value = IS.Gross_Dividends__less__Common_Stock;
            ws.Cells [ 37, 1 ].Value = "Net Income after Stock Based Comp. Expense";
            ws.Cells [ 37, 2 ].Value = IS.Net_Income_after_Stock_Based_Comp_Expense;
            ws.Cells [ 38, 1 ].Value = "Basic EPS after Stock Based Comp. Expense";
            ws.Cells [ 38, 2 ].Value = IS.Basic_EPS_after_Stock_Based_Comp_Expense;
            ws.Cells [ 39, 1 ].Value = "Diluted EPS after Stock Based Comp. Expense";
            ws.Cells [ 39, 2 ].Value = IS.Diluted_EPS_after_Stock_Based_Comp_Expense;
            ws.Cells [ 40, 1 ].Value = "Depreciation, Supplemental";
            ws.Cells [ 40, 2 ].Value = IS.Depreciation_Supplemental;
            ws.Cells [ 41, 1 ].Value = "Total Special Items";
            ws.Cells [ 41, 2 ].Value = IS.Total_Special_Items;
            ws.Cells [ 42, 1 ].Value = "Normalized Income Before Taxes";
            ws.Cells [ 42, 2 ].Value = IS.Normalized_Income_Before_Taxes;
            ws.Cells [ 43, 1 ].Value = "Effect of Special Items on Income Taxes";
            ws.Cells [ 43, 2 ].Value = IS.Effect_of_Special_Items_on_Income_Taxes;
            ws.Cells [ 44, 1 ].Value = "Income Taxes Ex. Impact of Special Items";
            ws.Cells [ 44, 2 ].Value = IS.Income_Taxes_Ex_Impact_of_Special_Items;
            ws.Cells [ 45, 1 ].Value = "Normalized Income After Taxes";
            ws.Cells [ 45, 2 ].Value = IS.Normalized_Income_After_Taxes;
            ws.Cells [ 46, 1 ].Value = "Normalized Income Avail to Common";
            ws.Cells [ 46, 2 ].Value = IS.Normalized_Income_Avail_to_Common;
            ws.Cells [ 47, 1 ].Value = "Basic Normalized EPS";
            ws.Cells [ 47, 2 ].Value = IS.Basic_Normalized_EPS;
            ws.Cells [ 48, 1 ].Value = "Diluted Normalized EPS";
            ws.Cells [ 48, 2 ].Value = IS.Diluted_Normalized_EPS;



            ws.Cells [ "A3:B3" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells [ "A3:A48" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells [ "B3:B48" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells [ "A48:B48" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;



            for ( int i = 3; i < 49; i++ )
                ws.Cells [ i, 2 ].Style.Numberformat.Format = "\"$\"#,##0;";
        }

        public static void SaveCashFlowStatements( string fileName, CompanyInfo ci )
        {
            fileName = fileName.Contains( ".xslx" ) ? fileName : fileName + ".xlsx";
            System.IO.FileInfo fi = new System.IO.FileInfo( fileName );
            pck = new ExcelPackage( fi );

            foreach ( CashFlowStatement cfs in ci.CashFlowStatements )
            {
                SaveCashFlowStatement( cfs );
            }
            pck.Save( );
            pck = null;
        }

        static void SaveCashFlowStatement( CashFlowStatement cfs )
        {
            string tabName = ( cfs.Period == Period.Annual ? "A" : "Q" )
                + "_" + cfs.PeriodEnd.ToShortDateString( ).Replace( "/", "_" );

            var ws = pck.Workbook.Worksheets.FirstOrDefault( i=> i.Name == tabName) == null ?
                pck.Workbook.Worksheets.Add( tabName ) : pck.Workbook.Worksheets.Add( tabName + "_2" );

            ws.Cells [ 1, 1 ].Value = "Cashflow Statement for " + ( cfs.Period == Period.Annual ? " Year" : " Quarter" ) + " Ending " + cfs.PeriodEnd.ToShortDateString( );
            ws.Cells [ "A1:H1" ].Merge = true;


            ws.Cells [ 3, 1 ].Value = "Net Income/Starting Line";
            ws.Cells [ 3, 2 ].Value = cfs.Net_Income_and_Starting_Line;
            ws.Cells [ 4, 1 ].Value = "Depreciation/Depletion";
            ws.Cells [ 4, 2 ].Value = cfs.Depreciation_and_Depletion;
            ws.Cells [ 5, 1 ].Value = "Amortization";
            ws.Cells [ 5, 2 ].Value = cfs.Amortization;
            ws.Cells [ 6, 1 ].Value = "Deferred Taxes";
            ws.Cells [ 6, 2 ].Value = cfs.Deferred_Taxes;
            ws.Cells [ 7, 1 ].Value = "Non-Cash Items";
            ws.Cells [ 7, 2 ].Value = cfs.NonCash_Items;
            ws.Cells [ 8, 1 ].Value = "Changes in Working Capital";
            ws.Cells [ 8, 2 ].Value = cfs.Changes_in_Working_Capital;

            ws.Cells [ 9, 1 ].Value = "Cash from Operating Activities";
            ws.Cells [ 9, 2 ].Value = cfs.Cash_from_Operating_Activities;


            ws.Cells["A9:B9"].Style.Font.Bold = true;
            ws.Cells [ "A9:B9" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A9:B9" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A9:A9" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "B9:B9" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;



            ws.Cells [ 10, 1 ].Value = "Capital Expenditures";
            ws.Cells [ 10, 2 ].Value = cfs.Capital_Expenditures;
            ws.Cells [ 11, 1 ].Value = "Other Investing Cash Flow Items, Total";
            ws.Cells [ 11, 2 ].Value = cfs.Other_Investing_Cash_Flow_Items__Total;

            ws.Cells [ 12, 1 ].Value = "Cash from Investing Activities";
            ws.Cells [ 12, 2 ].Value = cfs.Cash_from_Investing_Activities;

            ws.Cells [ "A12:B12" ].Style.Font.Bold = true;
            ws.Cells [ "A12:B12" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A12:B12" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A12:A12" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "B12:B12" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;



            ws.Cells [ 13, 1 ].Value = "Financing Cash Flow Items";
            ws.Cells [ 13, 2 ].Value = cfs.Financing_Cash_Flow_Items;
            ws.Cells [ 14, 1 ].Value = "Total Cash Dividends Paid";
            ws.Cells [ 14, 2 ].Value = cfs.Total_Cash_Dividends_Paid;
            ws.Cells [ 15, 1 ].Value = "Issuance (Retirement) of Stock, Net";
            ws.Cells [ 15, 2 ].Value = cfs.Issuance__Retirement__of_Stock__Net;
            ws.Cells [ 16, 1 ].Value = "Issuance (Retirement) of Debt, Net";
            ws.Cells [ 16, 2 ].Value = cfs.Issuance__Retirement__of_Debt__Net;

            ws.Cells [ 17, 1 ].Value = "Cash from Financing Activities";
            ws.Cells [ 17, 2 ].Value = cfs.Cash_from_Financing_Activities;
            ws.Cells [ "A17:B17" ].Style.Font.Bold = true;
            ws.Cells [ "A17:B17" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A17:B17" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "A17:A17" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
            ws.Cells [ "B17:B17" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;


            ws.Cells [ 18, 1 ].Value = "Foreign Exchange Effects";
            ws.Cells [ 18, 2 ].Value = cfs.Foreign_Exchange_Effects;
            ws.Cells [ 19, 1 ].Value = "Net Change in Cash";
            ws.Cells [ 19, 2 ].Value = cfs.Net_Change_in_Cash;
            ws.Cells [ 20, 1 ].Value = "Cash Interest Paid, Supplemental";
            ws.Cells [ 20, 2 ].Value = cfs.Cash_Interest_Paid__Supplemental;
            ws.Cells [ 21, 1 ].Value = "Cash Taxes Paid, Supplemental";
            ws.Cells [ 21, 2 ].Value = cfs.Cash_Taxes_Paid__Supplemental;


            ws.Cells [ "A3:B3" ].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells [ "A3:A21" ].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells [ "B3:B21" ].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells [ "A21:B21" ].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


            for ( int i = 3; i < 22; i++ )
            {
                if ( ws.Cells [ i, 2 ].Value != null && Convert.ToDouble( ws.Cells [ i, 2 ].Value ) > 0 )
                    ws.Cells [ i, 2 ].Style.Numberformat.Format = "\"$\"#,##0;";
            }
                
                //else


        }

        static ExcelPackage pck;
    }
}
