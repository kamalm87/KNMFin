using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using KNMFin.Google;

namespace KNMFinUI
{
    public static class GoogleFinanceUI
    {
        static string FormattedString( Double? input )
        {
            if ( input == null ) return String.Empty;
            return string.Format( "{0:#,##0.##}", input );
        }

        static string FormattedCurrencyString( Double? input )
        {
            if ( input == null ) return "-";
            return string.Format( "${0:#,##0.##}", input );
        }

        static string FormattedString( Decimal? input )
        {
            if ( input == null ) return "-";
            return string.Format( "{0:#,##0.##}", input );
        }

        static string FormattedCurrencyString( Decimal? input )
        {
            if ( input == null ) return String.Empty;
            return string.Format( "${0:#,##0.##}", input );
        }

        static public Canvas CompanyInfoCanvas( CompanyInfo ci )
        {
            var canvas = new Canvas( );
            canvas.Name = "summaryCanvas";

            var name = new Label( );
            name.FontSize = 10;
            name.Content = ci.Name;
            canvas.Children.Add( name );
            Canvas.SetLeft( name, 0 );
            Canvas.SetTop( name, 0 );

            var ticker = new Label( );
            ticker.FontSize = 10;
            ticker.Content = ci.Ticker;
            canvas.Children.Add( ticker );
            Canvas.SetLeft( ticker, 0 );
            Canvas.SetTop( ticker, 15 );

            var exchange = new Label( );
            exchange.FontSize = 10;
            exchange.Content = "(" + ci.Exchange + ")";
            canvas.Children.Add( exchange );
            Canvas.SetLeft( exchange, ci.Ticker.Length * 8 );
            Canvas.SetTop( exchange, 15 );

            var oLbl = new Label( );
            oLbl.FontSize = 10;
            oLbl.Content = "Open";
            canvas.Children.Add( oLbl );
            Canvas.SetLeft( oLbl, 0 );
            Canvas.SetTop( oLbl, 30 );

            var Open = new Label( );
            Open.FontSize = 10;
            Open.Content = FormattedCurrencyString( ci.Open );
            canvas.Children.Add( Open );
            Canvas.SetLeft( Open, "Open".Length * 7 );
            Canvas.SetTop( Open, 30 );


            var cLbl = new Label( );
            cLbl.FontSize = 10;
            cLbl.Content = "Close";
            canvas.Children.Add( cLbl );
            Canvas.SetLeft( cLbl, "Open".Length * 7 + FormattedCurrencyString( ci.Open ).Length * 7 );
            Canvas.SetTop( cLbl, 30 );

            var Close = new Label( );
            Close.FontSize = 10;
            Close.Content = FormattedCurrencyString( ci.Close );
            canvas.Children.Add( Close );
            Canvas.SetLeft( Close, "Open".Length * 7 + "Close".Length * 7 + FormattedCurrencyString( ci.Open ).Length * 7 );
            Canvas.SetTop( Close, 30 );


            var mcLbl = new Label( );
            mcLbl.FontSize = 10;
            mcLbl.Content = "Market Cap:";
            canvas.Children.Add( mcLbl );
            Canvas.SetLeft( mcLbl, 0 );
            Canvas.SetTop( mcLbl, 45 );

            var mc = new Label( );
            mc.FontSize = 10;
            mc.Content = FormattedCurrencyString( ci.MarketCap );
            canvas.Children.Add( mc );
            Canvas.SetLeft( mc, "Market Cap:".Length * 6 );
            Canvas.SetTop( mc, 45 );


            var SharesLbl = new Label( );
            SharesLbl.FontSize = 10;
            SharesLbl.Content = "Shares:";
            canvas.Children.Add( SharesLbl );
            Canvas.SetLeft( SharesLbl, 0 );
            Canvas.SetTop( SharesLbl, 60 );

            var Shares = new Label( );
            Shares.FontSize = 10;
            Shares.Content = FormattedString( ci.Shares );
            canvas.Children.Add( Shares );
            Canvas.SetLeft( Shares, "Shares:".Length * 7 );
            Canvas.SetTop( Shares, 60 );


            var vaLbl = new Label( );
            vaLbl.FontSize = 10;
            vaLbl.Content = "Vol. Avg:";
            canvas.Children.Add( vaLbl );
            Canvas.SetLeft( vaLbl, 0 );
            Canvas.SetTop( vaLbl, 75 );

            var VolAvg = new Label( );
            VolAvg.FontSize = 10;
            VolAvg.Content = FormattedString( ci.VolumeAverage );
            canvas.Children.Add( VolAvg );
            Canvas.SetLeft( VolAvg, "Vol. AVg:".Length * 6 );
            Canvas.SetTop( VolAvg, 75 );


            var vdLbl = new Label( );
            vdLbl.FontSize = 10;
            vdLbl.Content = "Vol. Day";
            canvas.Children.Add( vdLbl );
            Canvas.SetLeft( vdLbl, "Vol. AVg:".Length * 6 + FormattedString( ci.VolumeAverage ).Length * 6 );
            Canvas.SetTop( vdLbl, 75 );

            var VolDay = new Label( );
            VolDay.FontSize = 10;
            VolDay.Content = FormattedString( ci.VolumeAverage );
            canvas.Children.Add( VolDay );
            Canvas.SetLeft( VolDay, "Vol. Avg:".Length * 12 + FormattedString( ci.VolumeAverage ).Length * 6 );
            Canvas.SetTop( VolDay, 75 );


            var rlLbl = new Label( );
            rlLbl.FontSize = 10;
            rlLbl.Content = "Low:";
            canvas.Children.Add( rlLbl );
            Canvas.SetLeft( rlLbl, 0 );
            Canvas.SetTop( rlLbl, 90 );

            var RangeLow = new Label( );
            RangeLow.FontSize = 10;
            RangeLow.Content = FormattedCurrencyString( ci.RangeLow );
            canvas.Children.Add( RangeLow );
            Canvas.SetLeft( RangeLow, "Low:".Length * 6 );
            Canvas.SetTop( RangeLow, 90 );


            var rhLbl = new Label( );
            rhLbl.FontSize = 10;
            rhLbl.Content = "High";
            canvas.Children.Add( rhLbl );
            Canvas.SetLeft( rhLbl, "Low:".Length * 6 + FormattedCurrencyString( ci.RangeLow ).Length * 6 );
            Canvas.SetTop( rhLbl, 90 );

            var RangeHigh = new Label( );
            RangeHigh.FontSize = 10;
            RangeHigh.Content = FormattedCurrencyString( ci.RangeHigh );
            canvas.Children.Add( RangeHigh );
            Canvas.SetLeft( RangeHigh, "High:".Length * 6 + "Low:".Length * 6 + FormattedString( ci.RangeLow ).Length * 6 );
            Canvas.SetTop( RangeHigh, 90 );


            var w52LLbl = new Label( );
            w52LLbl.FontSize = 10;
            w52LLbl.Content = "52 Week Low:";
            canvas.Children.Add( w52LLbl );
            Canvas.SetLeft( w52LLbl, 0 );
            Canvas.SetTop( w52LLbl, 105 );

            var Week52Low = new Label( );
            Week52Low.FontSize = 10;
            Week52Low.Content = FormattedCurrencyString( ci.FiftyTwoWeekLow );
            canvas.Children.Add( Week52Low );
            Canvas.SetLeft( Week52Low, "52 Week Low:".Length * 6 );
            Canvas.SetTop( Week52Low, 105 );

            var w52HLbl = new Label( );
            w52HLbl.FontSize = 10;
            w52HLbl.Content = "52 Week High:";
            canvas.Children.Add( w52HLbl );
            Canvas.SetLeft( w52HLbl, "52 Week Low:".Length * 6 + FormattedCurrencyString( ci.FiftyTwoWeekLow ).Length * 6 );
            Canvas.SetTop( w52HLbl, 105 );

            var Week52High = new Label( );
            Week52High.FontSize = 10;
            Week52High.Content = FormattedCurrencyString( ci.FiftyTwoWeekHigh );
            canvas.Children.Add( Week52High );
            Canvas.SetLeft( Week52High, "52 Week Low:".Length * 6 + "52 Week High:".Length * 6 + FormattedString( ci.FiftyTwoWeekLow ).Length * 6 );
            Canvas.SetTop( Week52High, 105 );

            var PELLbl = new Label( );
            PELLbl.FontSize = 10;
            PELLbl.Content = "P/E:";
            canvas.Children.Add( PELLbl );
            Canvas.SetLeft( PELLbl, 0 );
            Canvas.SetTop( PELLbl, 120 );

            var PE = new Label( );
            PE.FontSize = 10;
            PE.Content = FormattedString( ci.PriceEarnings );
            canvas.Children.Add( PE );
            Canvas.SetLeft( PE, "P/E".Length * 6 );
            Canvas.SetTop( PE, 120 );


            var EPSLLbl = new Label( );
            EPSLLbl.FontSize = 10;
            EPSLLbl.Content = "EPS:";
            canvas.Children.Add( EPSLLbl );
            Canvas.SetLeft( EPSLLbl, "P/E".Length * 6 + FormattedString( ci.PriceEarnings ).Length * 6 );
            Canvas.SetTop( EPSLLbl, 120 );

            var EPS = new Label( );
            EPS.FontSize = 10;
            EPS.Content = FormattedCurrencyString( ci.EarningsPerShare );
            canvas.Children.Add( EPS );
            Canvas.SetLeft( EPS, "EPS".Length * 6 + "PE:".Length * 6 + FormattedString( ci.PriceEarnings ).Length * 6 );
            Canvas.SetTop( EPS, 120 );



            var BetaLLbl = new Label( );
            BetaLLbl.FontSize = 10;
            BetaLLbl.Content = "Beta:";
            canvas.Children.Add( BetaLLbl );
            Canvas.SetLeft( BetaLLbl, 0 );
            Canvas.SetTop( BetaLLbl, 135 );

            var Beta = new Label( );
            Beta.FontSize = 10;
            Beta.Content = FormattedString( ci.Beta );
            canvas.Children.Add( Beta );
            Canvas.SetLeft( Beta, "Beta:".Length * 6 );
            Canvas.SetTop( Beta, 135 );


            var InstOwnLLbl = new Label( );
            InstOwnLLbl.FontSize = 10;
            InstOwnLLbl.Content = "Institutional Ownership:";
            canvas.Children.Add( InstOwnLLbl );
            Canvas.SetLeft( InstOwnLLbl, FormattedString( ci.Beta ).Length * 6 + "Beta:".Length * 6 );
            Canvas.SetTop( InstOwnLLbl, 135 );

            var InstOwn = new Label( );
            InstOwn.FontSize = 10;
            InstOwn.Content = FormattedString( ci.InstitutionalOwnership );
            canvas.Children.Add( InstOwn );
            Canvas.SetLeft( InstOwn, FormattedString( ci.Beta ).Length * 6 + "Institutional Ownership:".Length * 6 + "Beta:".Length * 6 );
            Canvas.SetTop( InstOwn, 135 );



            var DivLLbl = new Label( );
            DivLLbl.FontSize = 10;
            DivLLbl.Content = "Div:";
            canvas.Children.Add( DivLLbl );
            Canvas.SetLeft( DivLLbl, 0 );
            Canvas.SetTop( DivLLbl, 150 );

            var Div = new Label( );
            Div.FontSize = 10;
            Div.Content = FormattedString( ci.Dividend );
            canvas.Children.Add( Div );
            Canvas.SetLeft( Div, "Div:".Length * 6 );
            Canvas.SetTop( Div, 150 );


            var DivYieldLLbl = new Label( );
            DivYieldLLbl.FontSize = 10;
            DivYieldLLbl.Content = "Yield:";
            canvas.Children.Add( DivYieldLLbl );
            Canvas.SetLeft( DivYieldLLbl, FormattedString( ci.Dividend ).Length * 6 + "Div:".Length * 6 );
            Canvas.SetTop( DivYieldLLbl, 150 );

            var DivYield = new Label( );
            DivYield.FontSize = 10;
            DivYield.Content = FormattedCurrencyString( ci.DividendYield );
            canvas.Children.Add( DivYield );
            Canvas.SetLeft( DivYield, FormattedString( ci.Dividend ).Length * 6 + "Yield:".Length * 6 + "Div:".Length * 6 );
            Canvas.SetTop( DivYield, 150 );


            var IndustryLLbl = new Label( );
            IndustryLLbl.FontSize = 10;
            IndustryLLbl.Content = "Industry:";
            canvas.Children.Add( IndustryLLbl );
            Canvas.SetLeft( IndustryLLbl, 0 );
            Canvas.SetTop( IndustryLLbl, 165 );

            var Industry = new Label( );
            Industry.FontSize = 10;
            Industry.Content = ci.Industry;
            canvas.Children.Add( Industry );
            Canvas.SetLeft( Industry, "Industry:".Length * 6 );
            Canvas.SetTop( Industry, 165 );

            var SectorLLbl = new Label( );
            SectorLLbl.FontSize = 10;
            SectorLLbl.Content = "Sector:";
            canvas.Children.Add( SectorLLbl );
            Canvas.SetLeft( SectorLLbl, 0 );
            Canvas.SetTop( SectorLLbl, 180 );

            var Sector = new Label( );
            Sector.FontSize = 10;
            Sector.Content = ci.Sector;
            canvas.Children.Add( Sector );
            Canvas.SetLeft( Sector, "Sector:".Length * 6 );
            Canvas.SetTop( Sector, 180 );

            return canvas;
        }

        static public Canvas BalanceSheetCanvas( BalanceSheet bs )
        {

            Grid grid = new Grid( );
            grid.Width = 600;
            grid.Height = 600;
            grid.ColumnDefinitions.Add( new ColumnDefinition( ) );
            grid.ColumnDefinitions.Add( new ColumnDefinition( ) );

            Grid subGridLeft = new Grid( );
            subGridLeft.Width = 300;
            Grid subGridRight = new Grid( );
            subGridRight.Width = 300;




            subGridLeft.RowDefinitions.Add( new RowDefinition( ) );


            subGridRight.RowDefinitions.Add( new RowDefinition( ) );
            subGridRight.RowDefinitions.Add( new RowDefinition( ) );


            var cAssets = BSAsset( bs );
            var cLiabilities = BSLiabilities( bs );
            var cEquity = BSEquities( bs );


            subGridLeft.Children.Add( cAssets );
            Grid.SetRow( cAssets, 0 );


            subGridRight.Children.Add( cLiabilities );
            subGridRight.Children.Add( cEquity );
            Grid.SetRow( cLiabilities, 0 );
            Grid.SetRow( cEquity, 1 );


            grid.Children.Add( subGridLeft );
            grid.Children.Add( subGridRight );
            Grid.SetColumn( subGridLeft, 0 );
            Grid.SetColumn( subGridRight, 1 );
            var canvas = new Canvas( );

            canvas.Children.Add( grid );

            return canvas;
        }

        static Canvas BSAsset( BalanceSheet bs )
        {
            int maxLengthStr = "Property/Plant/Equipment, Total - Gross".Length * 5;

            // Assets
            var canvas = new Canvas( );
            canvas.Name = "bsAssets";
            var assets = new Label( );
            assets.Content = "Assets";
            canvas.Children.Add( assets );
            Canvas.SetLeft( assets, 0 );
            Canvas.SetTop( assets, 0 );
            assets.FontSize = 10;

            // Current Assets
            var currentAssets = new Label( );
            currentAssets.Content = "Current Assets";
            currentAssets.FontSize = 10;
            canvas.Children.Add( currentAssets );
            Canvas.SetLeft( currentAssets, 10 );
            Canvas.SetTop( currentAssets, 15 );

            // Cash & Equivalents
            var CashAndEquivalents = new Label( );
            CashAndEquivalents.Content = "Cash & Equivalents";
            CashAndEquivalents.FontSize = 10;
            canvas.Children.Add( CashAndEquivalents );
            Canvas.SetLeft( CashAndEquivalents, 20 );
            Canvas.SetTop( CashAndEquivalents, 30 );
            var CashAndEquivalentsData = new Label( );
            CashAndEquivalentsData.Content = FormattedCurrencyString( bs.Cash_and_Equivalents );
            CashAndEquivalentsData.FontSize = 10;
            canvas.Children.Add( CashAndEquivalentsData );
            Canvas.SetLeft( CashAndEquivalentsData, 20 + maxLengthStr );
            Canvas.SetTop( CashAndEquivalentsData, 30 );


            CashAndEquivalentsData.FontSize = 10;



            // Short Term Investments
            var ShortTermInvestments = new Label( );
            ShortTermInvestments.Content = "Short Term Investments";
            ShortTermInvestments.FontSize = 10;
            canvas.Children.Add( ShortTermInvestments );
            Canvas.SetLeft( ShortTermInvestments, 20 );
            Canvas.SetTop( ShortTermInvestments, 45 );
            var ShortTermInvestmentsData = new Label( );
            ShortTermInvestmentsData.Content = FormattedCurrencyString( bs.Short_Term_Investments );
            ShortTermInvestmentsData.FontSize = 10;
            canvas.Children.Add( ShortTermInvestmentsData );
            Canvas.SetLeft( ShortTermInvestmentsData, 20 + maxLengthStr );
            Canvas.SetTop( ShortTermInvestmentsData, 45 );



            ShortTermInvestmentsData.FontSize = 10;

            // Cash & Short Term Investments
            var CashShortTermInvestments = new Label( );
            CashShortTermInvestments.Content = "Cash & Short Term Investments";
            CashShortTermInvestments.FontSize = 10;
            canvas.Children.Add( CashShortTermInvestments );
            Canvas.SetLeft( CashShortTermInvestments, 10 );
            Canvas.SetTop( CashShortTermInvestments, 60 );
            var CashShortTermInvestmentsData = new Label( );
            CashShortTermInvestmentsData.Content = FormattedCurrencyString( bs.Cash_and_Short_Term_Investments );
            CashShortTermInvestmentsData.FontSize = 10;
            canvas.Children.Add( CashShortTermInvestmentsData );
            Canvas.SetLeft( CashShortTermInvestmentsData, 10 + maxLengthStr );
            Canvas.SetTop( CashShortTermInvestmentsData, 60 );

            // Accounts Receivable - Trade, Net
            var AccountsReceivableLessTrade = new Label( );
            AccountsReceivableLessTrade.Content = "Accounts Receivable - Trade, Net";
            AccountsReceivableLessTrade.FontSize = 10;
            canvas.Children.Add( AccountsReceivableLessTrade );
            Canvas.SetLeft( AccountsReceivableLessTrade, 10 );
            Canvas.SetTop( AccountsReceivableLessTrade, 75 );
            var AccountsReceivableLessTradeData = new Label( );
            AccountsReceivableLessTradeData.Content = FormattedCurrencyString( bs.Accounts_Receivable__Trade__Net );
            AccountsReceivableLessTradeData.FontSize = 10;
            canvas.Children.Add( AccountsReceivableLessTradeData );
            Canvas.SetLeft( AccountsReceivableLessTradeData, 10 + maxLengthStr );
            Canvas.SetTop( AccountsReceivableLessTradeData, 75 );

            // Receivables - Other
            var ReceivablesOther = new Label( );
            ReceivablesOther.Content = "Receivables - Other";
            ReceivablesOther.FontSize = 10;
            canvas.Children.Add( ReceivablesOther );
            Canvas.SetLeft( ReceivablesOther, 10 );
            Canvas.SetTop( ReceivablesOther, 90 );
            var ReceivablesOtherData = new Label( );
            ReceivablesOtherData.Content = FormattedCurrencyString( bs.Receivables__Other );
            ReceivablesOtherData.FontSize = 10;
            canvas.Children.Add( ReceivablesOtherData );
            Canvas.SetLeft( ReceivablesOtherData, 10 + maxLengthStr );
            Canvas.SetTop( ReceivablesOtherData, 90 );

            // Total Receivables, Net
            var TotalReceivablesOther = new Label( );
            TotalReceivablesOther.Content = "Total Receivables, Net";
            TotalReceivablesOther.FontSize = 10;
            canvas.Children.Add( TotalReceivablesOther );
            Canvas.SetLeft( TotalReceivablesOther, 10 );
            Canvas.SetTop( TotalReceivablesOther, 105 );
            var TotalReceivablesOtherData = new Label( );
            TotalReceivablesOtherData.Content = FormattedCurrencyString( bs.Total_Receivables__Net );
            TotalReceivablesOtherData.FontSize = 10;
            canvas.Children.Add( TotalReceivablesOtherData );
            Canvas.SetLeft( TotalReceivablesOtherData, 10 + maxLengthStr );
            Canvas.SetTop( TotalReceivablesOtherData, 105 );

            // Total Inventory
            var TotalInventory = new Label( );
            TotalInventory.Content = "Total Inventory";
            TotalInventory.FontSize = 10;
            canvas.Children.Add( TotalInventory );
            Canvas.SetLeft( TotalInventory, 10 );
            Canvas.SetTop( TotalInventory, 120 );
            var TotalInventoryData = new Label( );
            TotalInventoryData.Content = FormattedCurrencyString( bs.Total_Inventory );
            TotalInventoryData.FontSize = 10;
            canvas.Children.Add( TotalInventoryData );
            Canvas.SetLeft( TotalInventoryData, 10 + maxLengthStr );
            Canvas.SetTop( TotalInventoryData, 120 );

            // Prepaid Expenses
            var PrepaidExpenses = new Label( );
            PrepaidExpenses.Content = "Prepaid Expenses";
            PrepaidExpenses.FontSize = 10;
            canvas.Children.Add( PrepaidExpenses );
            Canvas.SetLeft( PrepaidExpenses, 10 );
            Canvas.SetTop( PrepaidExpenses, 135 );
            var PrepaidExpensesData = new Label( );
            PrepaidExpensesData.Content = FormattedCurrencyString( bs.Prepaid_Expenses );
            PrepaidExpensesData.FontSize = 10;
            canvas.Children.Add( PrepaidExpensesData );
            Canvas.SetLeft( PrepaidExpensesData, 10 + maxLengthStr );
            Canvas.SetTop( PrepaidExpensesData, 135 );

            // Other Current Assets, Total
            var OtherCurrentAssetsTotal = new Label( );
            OtherCurrentAssetsTotal.Content = "Other Current Assets, Total";
            OtherCurrentAssetsTotal.FontSize = 10;
            canvas.Children.Add( OtherCurrentAssetsTotal );
            Canvas.SetLeft( OtherCurrentAssetsTotal, 10 );
            Canvas.SetTop( OtherCurrentAssetsTotal, 150 );
            var OtherCurrentAssetsTotalData = new Label( );
            OtherCurrentAssetsTotalData.Content = FormattedCurrencyString( bs.Other_Current_Assets__Total );
            OtherCurrentAssetsTotalData.FontSize = 10;
            canvas.Children.Add( OtherCurrentAssetsTotalData );
            Canvas.SetLeft( OtherCurrentAssetsTotalData, 10 + maxLengthStr );
            Canvas.SetTop( OtherCurrentAssetsTotalData, 150 );


            // Total Current Assets
            var TotalCurrentAssets = new Label( );
            TotalCurrentAssets.Content = "Total Current Assets";
            TotalCurrentAssets.FontSize = 10;
            canvas.Children.Add( TotalCurrentAssets );
            Canvas.SetLeft( TotalCurrentAssets, 10 );
            Canvas.SetTop( TotalCurrentAssets, 165 );
            var TotalCurrentAssetsData = new Label( );
            TotalCurrentAssetsData.Content = FormattedCurrencyString( bs.Total_Current_Assets );
            TotalCurrentAssetsData.FontSize = 10;
            canvas.Children.Add( TotalCurrentAssetsData );
            Canvas.SetLeft( TotalCurrentAssetsData, 10 + maxLengthStr );
            Canvas.SetTop( TotalCurrentAssetsData, 165 );

            // Long-Term Assets
            var longTermAssets = new Label( );
            longTermAssets.Content = "Long-Term Assets";
            longTermAssets.FontSize = 10;
            canvas.Children.Add( longTermAssets );
            Canvas.SetLeft( longTermAssets, 10 );
            Canvas.SetTop( longTermAssets, 180 );

            // Property/Plant/Equipment, Total - Gross
            var PropertyPlantEquipment = new Label( );
            PropertyPlantEquipment.Content = "Property/Plant/Equipment, Total - Gross";
            PropertyPlantEquipment.FontSize = 10;
            canvas.Children.Add( PropertyPlantEquipment );
            Canvas.SetLeft( PropertyPlantEquipment, 10 );
            Canvas.SetTop( PropertyPlantEquipment, 195 );
            var PropertyPlantEquipmentData = new Label( );
            PropertyPlantEquipmentData.Content = FormattedCurrencyString( bs.Property_and_Plant_and_Equipment__Total__Gross );
            PropertyPlantEquipmentData.FontSize = 10;
            canvas.Children.Add( PropertyPlantEquipmentData );
            Canvas.SetLeft( PropertyPlantEquipmentData, 10 + maxLengthStr );
            Canvas.SetTop( PropertyPlantEquipmentData, 195 );

            // Accumulated Depreciation, Total                
            var AccumuldatedDepreciation = new Label( );
            AccumuldatedDepreciation.Content = "Accumulated Depreciation, Total";
            AccumuldatedDepreciation.FontSize = 10;
            canvas.Children.Add( AccumuldatedDepreciation );
            Canvas.SetLeft( AccumuldatedDepreciation, 10 );
            Canvas.SetTop( AccumuldatedDepreciation, 210 );
            var AccumuldatedDepreciationData = new Label( );
            AccumuldatedDepreciationData.Content = FormattedCurrencyString( bs.Accumulated_Depreciation__Total );
            AccumuldatedDepreciationData.FontSize = 10;
            canvas.Children.Add( AccumuldatedDepreciationData );
            Canvas.SetLeft( AccumuldatedDepreciationData, 10 + maxLengthStr );
            Canvas.SetTop( AccumuldatedDepreciationData, 210 );

            // Goodwill, Net
            var Goodwill = new Label( );
            Goodwill.Content = "Goodwill, Net";
            Goodwill.FontSize = 10;
            canvas.Children.Add( Goodwill );
            Canvas.SetLeft( Goodwill, 10 );
            Canvas.SetTop( Goodwill, 225 );
            var GoodwillData = new Label( );
            GoodwillData.Content = FormattedCurrencyString( bs.Goodwill__Net );
            GoodwillData.FontSize = 10;
            canvas.Children.Add( GoodwillData );
            Canvas.SetLeft( GoodwillData, 10 + maxLengthStr );
            Canvas.SetTop( GoodwillData, 225 );

            // Intangibles, Net
            var Intangibles = new Label( );
            Intangibles.Content = "Intangibles, Net";
            Intangibles.FontSize = 10;
            canvas.Children.Add( Intangibles );
            Canvas.SetLeft( Intangibles, 10 );
            Canvas.SetTop( Intangibles, 240 );
            var IntangiblesData = new Label( );
            IntangiblesData.Content = FormattedCurrencyString( bs.Intangibles__Net );
            IntangiblesData.FontSize = 10;
            canvas.Children.Add( IntangiblesData );
            Canvas.SetLeft( IntangiblesData, 10 + maxLengthStr );
            Canvas.SetTop( IntangiblesData, 240 );

            // Long Term Investments
            var LongTermInvestments = new Label( );
            LongTermInvestments.Content = "Long Term Investments";
            LongTermInvestments.FontSize = 10;
            canvas.Children.Add( LongTermInvestments );
            Canvas.SetLeft( LongTermInvestments, 10 );
            Canvas.SetTop( LongTermInvestments, 265 );
            var LongTermInvestmentsData = new Label( );
            LongTermInvestmentsData.Content = FormattedCurrencyString( bs.Long_Term_Investments );
            LongTermInvestmentsData.FontSize = 10;
            canvas.Children.Add( LongTermInvestmentsData );
            Canvas.SetLeft( LongTermInvestmentsData, 10 + maxLengthStr );
            Canvas.SetTop( LongTermInvestmentsData, 265 );

            // Other Long Term Assets, Total
            var OtherLongTermAssets = new Label( );
            OtherLongTermAssets.Content = "Other Long Term Assets, Total";
            OtherLongTermAssets.FontSize = 10;
            canvas.Children.Add( OtherLongTermAssets );
            Canvas.SetLeft( OtherLongTermAssets, 10 );
            Canvas.SetTop( OtherLongTermAssets, 280 );
            var OtherLongTermAssetsData = new Label( );
            OtherLongTermAssetsData.Content = FormattedCurrencyString( bs.Other_Long_Term_Assets__Total );
            OtherLongTermAssetsData.FontSize = 10;
            canvas.Children.Add( OtherLongTermAssetsData );
            Canvas.SetLeft( OtherLongTermAssetsData, 10 + maxLengthStr );
            Canvas.SetTop( OtherLongTermAssetsData, 280 );

            // Total Long-Term Assets
            var TotalLongTermAssets = new Label( );
            TotalLongTermAssets.Content = "Total Long-Term Assets";
            TotalLongTermAssets.FontSize = 10;
            canvas.Children.Add( TotalLongTermAssets );
            Canvas.SetLeft( TotalLongTermAssets, 10 );
            Canvas.SetTop( TotalLongTermAssets, 295 );
            var TotalLongTermAssetsData = new Label( );
            // TODO fix
            // TotalLongTermAssetsData.Content = FormattedCurrencyString( bs.Total );
            TotalLongTermAssetsData.FontSize = 10;
            canvas.Children.Add( TotalLongTermAssetsData );
            Canvas.SetLeft( TotalLongTermAssetsData, 10 + maxLengthStr );
            Canvas.SetTop( TotalLongTermAssetsData, 295 );

            // Total Assets
            var TotalAssets = new Label( );
            TotalAssets.Content = "Total Assets";
            TotalAssets.FontSize = 10;
            canvas.Children.Add( TotalAssets );
            Canvas.SetLeft( TotalAssets, 10 );
            Canvas.SetTop( TotalAssets, 310 );
            var TotalAssetsData = new Label( );
            TotalAssetsData.Content = FormattedCurrencyString( bs.Total_Assets );
            TotalAssetsData.FontSize = 10;
            canvas.Children.Add( TotalAssetsData );
            Canvas.SetLeft( TotalAssetsData, 10 + maxLengthStr );
            Canvas.SetTop( TotalAssetsData, 310 );

            return canvas;
        }

        static Canvas BSLiabilities( BalanceSheet bs )
        {
            var canvas = new Canvas( );
            canvas.Name = "bsLiabilities";
            // Liabilities
            var liabilities = new Label( );
            liabilities.Content = "Liabilities";
            liabilities.FontSize = 10;
            canvas.Children.Add( liabilities );
            Canvas.SetTop( liabilities, 0 );
            Canvas.SetLeft( liabilities, 0 );

            // Current Liabilities
            var currentLiabilities = new Label( );
            currentLiabilities.Content = "Current Liabilities";
            currentLiabilities.FontSize = 10;
            canvas.Children.Add( currentLiabilities );
            Canvas.SetTop( currentLiabilities, 15 );
            Canvas.SetLeft( currentLiabilities, 10 );

            // Accounts Payable
            var accountsPayable = new Label( );
            accountsPayable.Content = "Accounts Payable";
            accountsPayable.FontSize = 10;
            canvas.Children.Add( accountsPayable );
            Canvas.SetTop( accountsPayable, 30 );
            Canvas.SetLeft( accountsPayable, 20 );
            var accountsPayableData = new Label( );
            accountsPayableData.Content = FormattedCurrencyString( bs.Accounts_Payable );
            accountsPayableData.FontSize = 10;
            canvas.Children.Add( accountsPayableData );
            Canvas.SetTop( accountsPayableData, 30 );
            Canvas.SetLeft( accountsPayableData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Accrued Expenses
            var accuredExepnses = new Label( );
            accuredExepnses.Content = "Accrued Expenses";
            accuredExepnses.FontSize = 10;
            canvas.Children.Add( accuredExepnses );
            Canvas.SetTop( accuredExepnses, 45 );
            Canvas.SetLeft( accuredExepnses, 20 );
            var accuredExepnsesData = new Label( );
            accuredExepnsesData.Content = FormattedCurrencyString( bs.Accrued_Expenses );
            accuredExepnsesData.FontSize = 10;
            canvas.Children.Add( accuredExepnsesData );
            Canvas.SetTop( accuredExepnsesData, 45 );
            Canvas.SetLeft( accuredExepnsesData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Notes Payable/Short Term Debt
            var notesPayableST = new Label( );
            notesPayableST.Content = "Notes Payable/Short Term Debt";
            notesPayableST.FontSize = 10;
            canvas.Children.Add( notesPayableST );
            Canvas.SetTop( notesPayableST, 60 );
            Canvas.SetLeft( notesPayableST, 20 );
            var notesPayableSTData = new Label( );
            notesPayableSTData.Content = FormattedCurrencyString( bs.Notes_Payable_and_Short_Term_Debt );
            notesPayableSTData.FontSize = 10;
            canvas.Children.Add( notesPayableSTData );
            Canvas.SetTop( notesPayableSTData, 60 );
            Canvas.SetLeft( notesPayableSTData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Current Port. of LT Debt/Capital Leases
            var currentPortOfLT = new Label( );
            currentPortOfLT.Content = "Current Port. of LT Debt/Capital Leases";
            currentPortOfLT.FontSize = 10;
            canvas.Children.Add( currentPortOfLT );
            Canvas.SetTop( currentPortOfLT, 75 );
            Canvas.SetLeft( currentPortOfLT, 20 );
            var currentPortOfLTData = new Label( );
            currentPortOfLTData.Content = FormattedCurrencyString( bs.Current_Port_of_LT_Debt_and_Capital_Leases );
            currentPortOfLTData.FontSize = 10;
            canvas.Children.Add( currentPortOfLTData );
            Canvas.SetTop( currentPortOfLTData, 75 );
            Canvas.SetLeft( currentPortOfLTData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Other Current liabilities, Total
            var otherCLT = new Label( );
            otherCLT.Content = "Other Current liabilities, Total";
            otherCLT.FontSize = 10;
            canvas.Children.Add( otherCLT );
            Canvas.SetTop( otherCLT, 90 );
            Canvas.SetLeft( otherCLT, 20 );
            var otherCLTData = new Label( );
            otherCLTData.Content = FormattedCurrencyString( bs.Other_Current_liabilities__Total );
            otherCLTData.FontSize = 10;
            canvas.Children.Add( otherCLTData );
            Canvas.SetTop( otherCLTData, 90 );
            Canvas.SetLeft( otherCLTData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Total Current Liabilities
            var tCL = new Label( );
            tCL.Content = "Total Current Liabilities";
            tCL.FontSize = 10;
            canvas.Children.Add( tCL );
            Canvas.SetTop( tCL, 105 );
            Canvas.SetLeft( tCL, 20 );
            var tCLData = new Label( );
            tCLData.Content = FormattedCurrencyString( bs.Total_Current_Liabilities );
            tCLData.FontSize = 10;
            canvas.Children.Add( tCLData );
            Canvas.SetTop( tCLData, 105 );
            Canvas.SetLeft( tCLData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Long-Term Liabilities
            var ltL = new Label( );
            ltL.Content = "Long-Term Liabilities";
            ltL.FontSize = 10;
            canvas.Children.Add( ltL );
            Canvas.SetTop( ltL, 120 );
            Canvas.SetLeft( ltL, 20 );
            var ltLData = new Label( );
            // TODO: ADD Long-Term Liabilities to KNMFinance API
            // ltLData.Content = FormattedCurrencyString( bs.Lon );
            ltLData.FontSize = 10;
            canvas.Children.Add( ltLData );
            Canvas.SetTop( ltLData, 120 );
            Canvas.SetLeft( ltLData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Long-Term Debt
            var ltD = new Label( );
            ltD.Content = "Long-Term Debt";
            ltD.FontSize = 10;
            canvas.Children.Add( ltD );
            Canvas.SetTop( ltD, 135 );
            Canvas.SetLeft( ltD, 20 );
            var ltDData = new Label( );
            ltDData.FontSize = 10;
            ltDData.Content = FormattedCurrencyString( bs.Long_Term_Debt );
            canvas.Children.Add( ltDData );
            Canvas.SetTop( ltDData, 135 );
            Canvas.SetLeft( ltDData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Capital Lease Obligations
            var clo = new Label( );
            clo.Content = "Capital Lease Obligations";
            clo.FontSize = 10;
            canvas.Children.Add( clo );
            Canvas.SetTop( clo, 150 );
            Canvas.SetLeft( clo, 20 );
            var cloData = new Label( );
            cloData.Content = FormattedCurrencyString( bs.Capital_Lease_Obligations );
            cloData.FontSize = 10;
            canvas.Children.Add( cloData );
            Canvas.SetTop( cloData, 150 );
            Canvas.SetLeft( cloData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Total Long Term Debt
            var lttd = new Label( );
            lttd.Content = "Total Long Term Debt";
            lttd.FontSize = 10;
            canvas.Children.Add( lttd );
            Canvas.SetTop( lttd, 165 );
            Canvas.SetLeft( lttd, 20 );
            var lttdData = new Label( );
            lttdData.Content = FormattedCurrencyString( bs.Total_Long_Term_Debt );
            lttdData.FontSize = 10;
            canvas.Children.Add( lttdData );
            Canvas.SetTop( lttdData, 165 );
            Canvas.SetLeft( lttdData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Total Debt
            var td = new Label( );
            td.Content = "Total Debt";
            td.FontSize = 10;
            canvas.Children.Add( td );
            Canvas.SetTop( td, 180 );
            Canvas.SetLeft( td, 20 );
            var tdData = new Label( );
            tdData.FontSize = 10;
            tdData.Content = FormattedCurrencyString( bs.Total_Debt );
            canvas.Children.Add( tdData );
            Canvas.SetTop( tdData, 180 );
            Canvas.SetLeft( tdData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Deferred Income Tax
            var dit = new Label( );
            dit.Content = "Deferred Income Tax";
            dit.FontSize = 10;
            canvas.Children.Add( dit );
            Canvas.SetTop( dit, 195 );
            Canvas.SetLeft( dit, 20 );
            var ditData = new Label( );
            ditData.Content = FormattedCurrencyString( bs.Deferred_Income_Tax );
            ditData.FontSize = 10;
            canvas.Children.Add( ditData );
            Canvas.SetTop( ditData, 195 );
            Canvas.SetLeft( ditData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Minority Interest
            var mi = new Label( );
            mi.Content = "Minority Interest";
            mi.FontSize = 10;
            canvas.Children.Add( mi );
            Canvas.SetTop( mi, 210 );
            Canvas.SetLeft( mi, 20 );
            var miData = new Label( );
            miData.FontSize = 10;
            miData.Content = FormattedCurrencyString( bs.Minority_Interest );
            canvas.Children.Add( miData );
            Canvas.SetTop( miData, 210 );
            Canvas.SetLeft( miData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Other Liabilities, Total
            var olt = new Label( );
            olt.Content = "Other Liabilities, Total";
            olt.FontSize = 10;
            canvas.Children.Add( olt );
            Canvas.SetTop( olt, 225 );
            Canvas.SetLeft( olt, 20 );
            var oltData = new Label( );
            oltData.FontSize = 10;
            oltData.Content = FormattedCurrencyString( bs.Other_Current_liabilities__Total );
            canvas.Children.Add( oltData );
            Canvas.SetTop( oltData, 225 );
            Canvas.SetLeft( oltData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Total Long-Term Liabilities
            var tltl = new Label( );
            tltl.Content = "Total Long-Term Liabilities";
            tltl.FontSize = 10;
            canvas.Children.Add( tltl );
            Canvas.SetTop( tltl, 240 );
            Canvas.SetLeft( tltl, 20 );
            var tltlData = new Label( );
            tltlData.FontSize = 10;
            // TODO: ADD
            // tltlData.Content = FormattedCurrencyString( bs.Total_Lo);
            canvas.Children.Add( tltlData );
            Canvas.SetTop( tltlData, 240 );
            Canvas.SetLeft( tltlData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            // Total Liabilities
            var tl = new Label( );
            tl.Content = "Total Liabilities";
            tl.FontSize = 10;
            canvas.Children.Add( tl );
            Canvas.SetTop( tl, 265 );
            Canvas.SetLeft( tl, 20 );
            var tlData = new Label( );
            tlData.Content = FormattedCurrencyString( bs.Total_Liabilities );
            tlData.FontSize = 10;
            canvas.Children.Add( tlData );
            Canvas.SetTop( tlData, 265 );
            Canvas.SetLeft( tlData, 20 + "Current Port. of LT Debt/Capital Leases".Length * 5 );

            return canvas;
        }

        static Canvas BSEquities( BalanceSheet bs )
        {
            var canvas = new Canvas( );
            canvas.Name = "bsEquity";
            int maxLengthString = "Shares Outs - Common Stock Primary Issue".Length * 5;
            // Equity
            var Equity = new Label( );
            Equity.Content = "Equity";
            Equity.FontSize = 10;
            canvas.Children.Add( Equity );
            Canvas.SetTop( Equity, 0 );
            Canvas.SetLeft( Equity, 0 );

            // Redeemable Preferred Stock, Total
            var rpst = new Label( );
            rpst.Content = "Redeemable Preferred Stock, Total";
            rpst.FontSize = 10;
            canvas.Children.Add( rpst );
            Canvas.SetTop( rpst, 15 );
            Canvas.SetLeft( rpst, 10 );
            var rpstData = new Label( );
            rpstData.FontSize = 10;
            rpstData.Content = FormattedCurrencyString( bs.Redeemable_Preferred_Stock__Total );
            canvas.Children.Add( rpstData );
            Canvas.SetTop( rpstData, 15 );
            Canvas.SetLeft( rpstData, 10 + maxLengthString );

            // Preferred Stock - Non Redeemable, Net
            var psnrn = new Label( );
            psnrn.Content = "Redeemable Preferred Stock, Total";
            psnrn.FontSize = 10;
            canvas.Children.Add( psnrn );
            Canvas.SetTop( psnrn, 30 );
            Canvas.SetLeft( psnrn, 10 );
            var psnrnData = new Label( );
            psnrnData.Content = FormattedCurrencyString( bs.Redeemable_Preferred_Stock__Total );
            psnrnData.FontSize = 10;
            canvas.Children.Add( psnrnData );
            Canvas.SetTop( psnrnData, 30 );
            Canvas.SetLeft( psnrnData, 10 + maxLengthString );

            // Common Stock, Total
            var cst = new Label( );
            cst.Content = "Common Stock, Total";
            cst.FontSize = 10;
            canvas.Children.Add( cst );
            Canvas.SetTop( cst, 45 );
            Canvas.SetLeft( cst, 10 );
            var cstData = new Label( );
            cstData.Content = FormattedCurrencyString( bs.Common_Stock__Total );
            cstData.FontSize = 10;
            canvas.Children.Add( cstData );
            Canvas.SetTop( cstData, 45 );
            Canvas.SetLeft( cstData, 10 + maxLengthString );

            // Additional Paid-In Capital
            var apic = new Label( );
            apic.Content = "Additional Paid-In Capital";
            apic.FontSize = 10;
            canvas.Children.Add( apic );
            Canvas.SetTop( apic, 60 );
            Canvas.SetLeft( apic, 10 );
            var apicData = new Label( );
            apicData.FontSize = 10;
            apicData.Content = FormattedCurrencyString( bs.Redeemable_Preferred_Stock__Total );
            canvas.Children.Add( apicData );
            Canvas.SetTop( apicData, 60 );
            Canvas.SetLeft( apicData, 10 + maxLengthString );

            // Retained Earnings (Accumulated Deficit)
            var read = new Label( );
            read.Content = "Retained Earnings (Accumulated Deficit)";
            read.FontSize = 10;
            canvas.Children.Add( read );
            Canvas.SetTop( read, 75 );
            Canvas.SetLeft( read, 10 );
            var readData = new Label( );
            readData.Content = FormattedCurrencyString( bs.Retained_Earnings__Accumulated_Deficit_ );
            readData.FontSize = 10;
            canvas.Children.Add( readData );
            Canvas.SetTop( readData, 75 );
            Canvas.SetLeft( readData, 10 + maxLengthString );

            // Treasury Stock - Common
            var tsc = new Label( );
            tsc.Content = "Treasury Stock - Common";
            tsc.FontSize = 10;
            canvas.Children.Add( tsc );
            Canvas.SetTop( tsc, 90 );
            Canvas.SetLeft( tsc, 10 );
            var tscData = new Label( );
            tscData.Content = FormattedCurrencyString( bs.Treasury_Stock__Common );
            tscData.FontSize = 10;
            canvas.Children.Add( tscData );
            Canvas.SetTop( tscData, 90 );
            Canvas.SetLeft( tscData, 10 + maxLengthString );

            // Other Equity, Total
            var oet = new Label( );
            oet.Content = "Other Equity, Total";
            oet.FontSize = 10;
            canvas.Children.Add( oet );
            Canvas.SetTop( oet, 105 );
            Canvas.SetLeft( oet, 10 );
            var oetData = new Label( );
            oetData.FontSize = 10;
            oetData.Content = FormattedCurrencyString( bs.Other_Equity__Total );
            canvas.Children.Add( oetData );
            Canvas.SetTop( oetData, 105 );
            Canvas.SetLeft( oetData, 10 + maxLengthString );

            // Total Equity
            var te = new Label( );
            te.Content = "Total Equity";
            te.FontSize = 10;
            canvas.Children.Add( te );
            Canvas.SetTop( te, 120 );
            Canvas.SetLeft( te, 10 );
            var teData = new Label( );
            teData.FontSize = 10;
            teData.Content = FormattedCurrencyString( bs.Total_Equity );
            canvas.Children.Add( teData );
            Canvas.SetTop( teData, 120 );
            Canvas.SetLeft( teData, 10 + maxLengthString );

            // Total Liabilities & Shareholders' Equity
            var tlse = new Label( );
            tlse.Content = "Total Liabilities & Shareholders' Equity";
            tlse.FontSize = 10;
            canvas.Children.Add( tlse );
            Canvas.SetTop( tlse, 135 );
            Canvas.SetLeft( tlse, 10 );
            var tlseData = new Label( );
            tlseData.FontSize = 10;
            tlseData.Content = FormattedCurrencyString( bs.Total_Liabilities_and_Shareholders_Equity );
            canvas.Children.Add( tlseData );
            Canvas.SetTop( tlseData, 135 );
            Canvas.SetLeft( tlseData, 10 + maxLengthString );

            // Shares Outs - Common Stock Primary Issue
            var socspi = new Label( );
            socspi.Content = "Shares Outs - Common Stock Primary Issue";
            socspi.FontSize = 10;
            canvas.Children.Add( socspi );
            Canvas.SetTop( socspi, 150 );
            Canvas.SetLeft( socspi, 10 );
            var socspiData = new Label( );
            socspiData.Content = FormattedCurrencyString( bs.Shares_Outs__Common_Stock_Primary_Issue );
            socspiData.FontSize = 10;
            canvas.Children.Add( socspiData );
            Canvas.SetTop( socspiData, 150 );
            Canvas.SetLeft( socspiData, 10 + maxLengthString );

            // Total Common Shares Outstanding
            var tcso = new Label( );
            tcso.Content = "Total Common Shares Outstanding";
            tcso.FontSize = 10;
            canvas.Children.Add( tcso );
            Canvas.SetTop( tcso, 165 );
            Canvas.SetLeft( tcso, 10 );
            var tcsoData = new Label( );
            tcsoData.FontSize = 10;
            tcsoData.Content = FormattedCurrencyString( bs.Total_Common_Shares_Outstanding );
            canvas.Children.Add( tcsoData );
            Canvas.SetTop( tcsoData, 165 );
            Canvas.SetLeft( tcsoData, 10 + maxLengthString );

            return canvas;
        }

        static public Canvas IncomeStatementCanvas( IncomeStatement ist )
        {

            Grid grid = new Grid( );
            grid.Width = 700;
            grid.Height = 600;
            grid.ColumnDefinitions.Add( new ColumnDefinition( ) );
            grid.ColumnDefinitions.Add( new ColumnDefinition( ) );

            Grid subGridLeft = new Grid( );
            subGridLeft.Width = 350;
            Grid subGridRight = new Grid( );
            subGridRight.Width = 350;




            subGridLeft.RowDefinitions.Add( new RowDefinition( ) );


            subGridRight.RowDefinitions.Add( new RowDefinition( ) );
            subGridRight.RowDefinitions.Add( new RowDefinition( ) );


            var cISIBT = ISIBT( ist );
            var cISDilutedEPS = ISDilutedEPS( ist );
            var cISDilutedNomrmalizedEPS = ISDilutedNormalizedEPS( ist );


            subGridLeft.Children.Add( cISIBT );
            Grid.SetRow( cISIBT, 0 );


            subGridRight.Children.Add( cISDilutedEPS );
            subGridRight.Children.Add( cISDilutedNomrmalizedEPS );
            Grid.SetRow( cISDilutedEPS, 0 );
            Grid.SetRow( cISDilutedNomrmalizedEPS, 1 );


            grid.Children.Add( subGridLeft );
            grid.Children.Add( subGridRight );
            Grid.SetColumn( subGridLeft, 0 );
            Grid.SetColumn( subGridRight, 1 );
            var canvas = new Canvas( );

            canvas.Children.Add( grid );

            return canvas;
        }

        // first "part" of the I/S
        static Canvas ISIBT( IncomeStatement ist )
        {
            var canvas = new Canvas( );
            int maxStr = "Interest Income(Expense), Net Non-Operating".Length * 6;
            // Revenue
            var lRevenue = new Label( );
            lRevenue.Content = "Revenue";
            lRevenue.FontSize = 10;
            canvas.Children.Add( lRevenue );
            Canvas.SetLeft( lRevenue, 0 );
            Canvas.SetTop( lRevenue, 0 );
            var Revenue = new Label( );
            Revenue.Content = FormattedCurrencyString( ist.Revenue );
            Revenue.FontSize = 10;
            canvas.Children.Add( Revenue );
            Canvas.SetLeft( Revenue, 60 );
            Canvas.SetTop( Revenue, 0 );

            // Other Revenue, Total
            var lRevenueOther = new Label( );
            lRevenueOther.Content = "Other Revenue, Total";
            lRevenueOther.FontSize = 10;
            canvas.Children.Add( lRevenueOther );
            Canvas.SetLeft( lRevenueOther, 0 );
            Canvas.SetTop( lRevenueOther, 15 );
            var RevenueOther = new Label( );
            RevenueOther.Content = FormattedCurrencyString( ist.Other_Revenue_Total );
            RevenueOther.FontSize = 10;
            canvas.Children.Add( RevenueOther );
            Canvas.SetLeft( RevenueOther, 60 );
            Canvas.SetTop( RevenueOther, 15 );

            // Total Revenue
            var lRevenueTotal = new Label( );
            lRevenueTotal.Content = "Total Revenue";
            lRevenueTotal.FontSize = 10;
            canvas.Children.Add( lRevenueTotal );
            Canvas.SetLeft( lRevenueTotal, 0 );
            Canvas.SetTop( lRevenueTotal, 30 );
            var RevenueTotal = new Label( );
            RevenueTotal.Content = FormattedCurrencyString( ist.Total_Revenue );
            RevenueTotal.FontSize = 10;
            canvas.Children.Add( RevenueTotal );
            Canvas.SetLeft( RevenueTotal, maxStr );
            Canvas.SetTop( RevenueTotal, 30 );

            // Cost of Revenue, Total
            var lCostOfRevenue = new Label( );
            lCostOfRevenue.Content = "Cost of Revenue, Total";
            lCostOfRevenue.FontSize = 10;
            canvas.Children.Add( lCostOfRevenue );
            Canvas.SetLeft( lCostOfRevenue, 0 );
            Canvas.SetTop( lCostOfRevenue, 45 );
            var CostOfRevenue = new Label( );
            CostOfRevenue.Content = FormattedCurrencyString( ist.Cost_of_Revenue_Total );
            CostOfRevenue.FontSize = 10;
            canvas.Children.Add( CostOfRevenue );
            Canvas.SetLeft( CostOfRevenue, maxStr );
            Canvas.SetTop( CostOfRevenue, 45 );

            // Gross Profit
            var lGrossProfit = new Label( );
            lGrossProfit.Content = "Gross Profit";
            lGrossProfit.FontSize = 10;
            canvas.Children.Add( lGrossProfit );
            Canvas.SetLeft( lGrossProfit, 0 );
            Canvas.SetTop( lGrossProfit, 60 );
            var GrossProfit = new Label( );
            GrossProfit.Content = FormattedCurrencyString( ist.Gross_Profit );
            GrossProfit.FontSize = 10;
            canvas.Children.Add( GrossProfit );
            Canvas.SetLeft( GrossProfit, maxStr );
            Canvas.SetTop( GrossProfit, 60 );

            // Selling/General/Admin. Expenses, Total
            var lSGAET = new Label( );
            lSGAET.Content = "Selling/General/Admin. Expenses, Total";
            lSGAET.FontSize = 10;
            canvas.Children.Add( lSGAET );
            Canvas.SetLeft( lSGAET, 0 );
            Canvas.SetTop( lSGAET, 75 );
            var SGAET = new Label( );
            SGAET.Content = FormattedCurrencyString( ist.Selling_and_General_and_Admin_Expenses_Total );
            SGAET.FontSize = 10;
            canvas.Children.Add( SGAET );
            Canvas.SetLeft( SGAET, maxStr );
            Canvas.SetTop( SGAET, 75 );

            // Research & Development
            var lR_D = new Label( );
            lR_D.Content = "Research & Development";
            lR_D.FontSize = 10;
            canvas.Children.Add( lR_D );
            Canvas.SetLeft( lR_D, 0 );
            Canvas.SetTop( lR_D, 90 );
            var R_D = new Label( );
            R_D.Content = FormattedCurrencyString( ist.Research_and_Development );
            R_D.FontSize = 10;
            canvas.Children.Add( R_D );
            Canvas.SetLeft( R_D, maxStr );
            Canvas.SetTop( R_D, 90 );

            // Depreciation/Amortization
            var lD_A = new Label( );
            lD_A.Content = "Depreciation/Amortization";
            lD_A.FontSize = 10;
            canvas.Children.Add( lD_A );
            Canvas.SetLeft( lD_A, 0 );
            Canvas.SetTop( lD_A, 105 );
            var D_A = new Label( );
            D_A.Content = FormattedCurrencyString( ist.Depreciation_and_Amortization );
            D_A.FontSize = 10;
            canvas.Children.Add( D_A );
            Canvas.SetLeft( D_A, maxStr );
            Canvas.SetTop( D_A, 105 );

            // Interest Expense(Income) - Net Operating
            var lIEINO = new Label( );
            lIEINO.Content = "Interest Expense(Income) - Net Operating";
            lIEINO.FontSize = 10;
            canvas.Children.Add( lIEINO );
            Canvas.SetLeft( lIEINO, 0 );
            Canvas.SetTop( lIEINO, 120 );
            var IEINO = new Label( );
            IEINO.Content = FormattedCurrencyString( ist.Interest_Expense__Income___less_Net_Operating );
            IEINO.FontSize = 10;
            canvas.Children.Add( IEINO );
            Canvas.SetLeft( IEINO, maxStr );
            Canvas.SetTop( IEINO, 120 );

            // Unusual Expense (Income)
            var lUEI = new Label( );
            lUEI.Content = "Unusual Expense (Income)";
            lUEI.FontSize = 10;
            canvas.Children.Add( lUEI );
            Canvas.SetLeft( lUEI, 0 );
            Canvas.SetTop( lUEI, 135 );
            var UEI = new Label( );
            UEI.Content = FormattedCurrencyString( ist.Unusual_Expense___Income__ );
            UEI.FontSize = 10;
            canvas.Children.Add( UEI );
            Canvas.SetLeft( UEI, maxStr );
            Canvas.SetTop( UEI, 135 );

            // Other Operating Expenses, Total
            var lOOET = new Label( );
            lOOET.Content = "Other Operating Expenses, Total";
            lOOET.FontSize = 10;
            canvas.Children.Add( lOOET );
            Canvas.SetLeft( lOOET, 0 );
            Canvas.SetTop( lOOET, 150 );
            var OOET = new Label( );
            OOET.Content = FormattedCurrencyString( ist.Other_Operating_Expenses_Total );
            OOET.FontSize = 10;
            canvas.Children.Add( OOET );
            Canvas.SetLeft( OOET, maxStr );
            Canvas.SetTop( OOET, 150 );

            // Total Operating Expense
            var lTOE = new Label( );
            lTOE.Content = "Total Operating Expense";
            lTOE.FontSize = 10;
            canvas.Children.Add( lTOE );
            Canvas.SetLeft( lTOE, 0 );
            Canvas.SetTop( lTOE, 165 );
            var TOE = new Label( );
            TOE.Content = FormattedCurrencyString( ist.Total_Operating_Expense );
            TOE.FontSize = 10;
            canvas.Children.Add( TOE );
            Canvas.SetLeft( TOE, maxStr );
            Canvas.SetTop( TOE, 165 );

            // Operating Income
            var lOI = new Label( );
            lOI.Content = "Operating Income";
            lOI.FontSize = 10;
            canvas.Children.Add( lOI );
            Canvas.SetLeft( lOI, 0 );
            Canvas.SetTop( lOI, 180 );
            var OI = new Label( );
            OI.Content = FormattedCurrencyString( ist.Operating_Income );
            OI.FontSize = 10;
            canvas.Children.Add( OI );
            Canvas.SetLeft( OI, maxStr );
            Canvas.SetTop( OI, 180 );

            // Interest Income(Expense), Net Non-Operating
            var lIIENO = new Label( );
            lIIENO.Content = "Interest Income(Expense), Net Non-Operating";
            lIIENO.FontSize = 10;
            canvas.Children.Add( lIIENO );
            Canvas.SetLeft( lIIENO, 0 );
            Canvas.SetTop( lIIENO, 195 );
            var IIENO = new Label( );
            IIENO.Content = FormattedCurrencyString( ist.Interest_Income__Expense___Net_NonOperating );
            IIENO.FontSize = 10;
            canvas.Children.Add( IIENO );
            Canvas.SetLeft( IIENO, maxStr );
            Canvas.SetTop( IIENO, 195 );

            // Gain (Loss) on Sale of Assets
            var lGOSOA = new Label( );
            lGOSOA.Content = "Gain (Loss) on Sale of Assets";
            lGOSOA.FontSize = 10;
            canvas.Children.Add( lGOSOA );
            Canvas.SetLeft( lGOSOA, 0 );
            Canvas.SetTop( lGOSOA, 210 );
            var GOSOA = new Label( );
            GOSOA.Content = FormattedCurrencyString( ist.Gain___Loss___on_Sale_of_Assets );
            GOSOA.FontSize = 10;
            canvas.Children.Add( GOSOA );
            Canvas.SetLeft( GOSOA, maxStr );
            Canvas.SetTop( GOSOA, 210 );

            // Other, Net
            var lON = new Label( );
            lON.Content = "Other, Net";
            lON.FontSize = 10;
            canvas.Children.Add( lON );
            Canvas.SetLeft( lON, 0 );
            Canvas.SetTop( lON, 225 );
            var ON = new Label( );
            ON.Content = FormattedCurrencyString( ist.Other_Net );
            ON.FontSize = 10;
            canvas.Children.Add( ON );
            Canvas.SetLeft( ON, maxStr );
            Canvas.SetTop( ON, 225 );

            // Income Before Tax
            var lIBT = new Label( );
            lIBT.Content = "Income Before Tax";
            lIBT.FontSize = 10;
            canvas.Children.Add( lIBT );
            Canvas.SetLeft( lIBT, 0 );
            Canvas.SetTop( lIBT, 240 );
            var IBT = new Label( );
            IBT.Content = FormattedCurrencyString( ist.Income_Before_Tax );
            IBT.FontSize = 10;
            canvas.Children.Add( IBT );
            Canvas.SetLeft( IBT, maxStr );
            Canvas.SetTop( IBT, 240 );


            return canvas;
        }

        // second "part" of the I/S
        static Canvas ISDilutedEPS( IncomeStatement ist )
        {
            var canvas = new Canvas( );
            int max = "Income Available to Common Excl. Extra Items".Length * 6;

            // Income After Tax
            var lIncomeAfterTax = new Label( );
            lIncomeAfterTax.FontSize = 10;
            lIncomeAfterTax.Content = "Income After Tax";
            canvas.Children.Add( lIncomeAfterTax );
            Canvas.SetLeft( lIncomeAfterTax, 0 );
            Canvas.SetTop( lIncomeAfterTax, 0 );
            var IncomeAfterTax = new Label( );
            IncomeAfterTax.FontSize = 10;
            IncomeAfterTax.Content = FormattedCurrencyString( ist.Income_After_Tax );
            canvas.Children.Add( IncomeAfterTax );
            Canvas.SetLeft( IncomeAfterTax, max );
            Canvas.SetTop( IncomeAfterTax, 0 );

            // Minority Interest
            var lmi = new Label( );
            lmi.FontSize = 10;
            lmi.Content = "Income After Tax";
            canvas.Children.Add( lmi );
            Canvas.SetLeft( lmi, 0 );
            Canvas.SetTop( lmi, 15 );
            var mi = new Label( );
            mi.FontSize = 10;
            mi.Content = FormattedCurrencyString( ist.Minority_Interest );
            canvas.Children.Add( mi );
            Canvas.SetLeft( mi, max );
            Canvas.SetTop( mi, 15 );

            // Equity In Affiliates
            var leia = new Label( );
            leia.FontSize = 10;
            leia.Content = "Equity In Affiliates";
            canvas.Children.Add( leia );
            Canvas.SetLeft( leia, 0 );
            Canvas.SetTop( leia, 30 );
            var eia = new Label( );
            eia.FontSize = 10;
            eia.Content = FormattedCurrencyString( ist.Equity_In_Affiliates );
            canvas.Children.Add( eia );
            Canvas.SetLeft( eia, max );
            Canvas.SetTop( eia, 30 );

            // Net Income Before Extra. Items
            var lnibei = new Label( );
            lnibei.FontSize = 10;
            lnibei.Content = "Net Income Before Extra. Items";
            canvas.Children.Add( lnibei );
            Canvas.SetLeft( lnibei, 0 );
            Canvas.SetTop( lnibei, 45 );
            var nibei = new Label( );
            nibei.FontSize = 10;
            nibei.Content = FormattedCurrencyString( ist.Net_Income_Before_Extra_Items );
            canvas.Children.Add( nibei );
            Canvas.SetLeft( nibei, max );
            Canvas.SetTop( nibei, 46 );

            // Accounting Change
            var lac = new Label( );
            lac.FontSize = 10;
            lac.Content = "Accounting Change";
            canvas.Children.Add( lac );
            Canvas.SetLeft( lac, 0 );
            Canvas.SetTop( lac, 60 );
            var ac = new Label( );
            ac.FontSize = 10;
            ac.Content = FormattedCurrencyString( ist.Accounting_Change );
            canvas.Children.Add( ac );
            Canvas.SetLeft( ac, max );
            Canvas.SetTop( ac, 60 );

            // Discontinued Operations
            var ldo = new Label( );
            ldo.FontSize = 10;
            ldo.Content = "Discontinued Operations";
            canvas.Children.Add( ldo );
            Canvas.SetLeft( ldo, 0 );
            Canvas.SetTop( ldo, 75 );
            var dop = new Label( );
            dop.FontSize = 10;
            dop.Content = FormattedCurrencyString( ist.Discontinued_Operations );
            canvas.Children.Add( dop );
            Canvas.SetLeft( dop, max );
            Canvas.SetTop( dop, 75 );

            // Extraordinary Item
            var lei = new Label( );
            lei.FontSize = 10;
            lei.Content = "Extraordinary Item";
            canvas.Children.Add( lei );
            Canvas.SetLeft( lei, 0 );
            Canvas.SetTop( lei, 90 );
            var ei = new Label( );
            ei.FontSize = 10;
            ei.Content = FormattedCurrencyString( ist.Extraordinary_Item );
            canvas.Children.Add( ei );
            Canvas.SetLeft( ei, max );
            Canvas.SetTop( ei, 90 );

            // Net Income
            var lni = new Label( );
            lni.FontSize = 10;
            lni.Content = "Net Income";
            canvas.Children.Add( lni );
            Canvas.SetLeft( lni, 0 );
            Canvas.SetTop( lni, 105 );
            var ni = new Label( );
            ni.FontSize = 10;
            ni.Content = FormattedCurrencyString( ist.Net_Income );
            canvas.Children.Add( ni );
            Canvas.SetLeft( ni, max );
            Canvas.SetTop( ni, 105 );

            // Preferred Dividends
            var lpd = new Label( );
            lpd.FontSize = 10;
            lpd.Content = "Preferred Dividends";
            canvas.Children.Add( lpd );
            Canvas.SetLeft( lpd, 0 );
            Canvas.SetTop( lpd, 120 );
            var pd = new Label( );
            pd.FontSize = 10;
            pd.Content = FormattedCurrencyString( ist.Preferred_Dividends );
            canvas.Children.Add( pd );
            Canvas.SetLeft( pd, max );
            Canvas.SetTop( pd, 120 );

            // Income Available to Common Excl. Extra Items
            var liatceei = new Label( );
            liatceei.FontSize = 10;
            liatceei.Content = "Income Available to Common Excl. Extra Items";
            canvas.Children.Add( liatceei );
            Canvas.SetLeft( liatceei, 0 );
            Canvas.SetTop( liatceei, 135 );
            var iatceei = new Label( );
            iatceei.FontSize = 10;
            iatceei.Content = FormattedCurrencyString( ist.Income_Available_to_Common_Excl_Extra_Items );
            canvas.Children.Add( iatceei );
            Canvas.SetLeft( iatceei, max );
            Canvas.SetTop( iatceei, 135 );

            // Income Available to Common Incl. Extra Items
            var liatciei = new Label( );
            liatciei.FontSize = 10;
            liatciei.Content = "Income Available to Common Incl. Extra Items";
            canvas.Children.Add( liatciei );
            Canvas.SetLeft( liatciei, 0 );
            Canvas.SetTop( liatciei, 150 );
            var iatciei = new Label( );
            iatciei.FontSize = 10;
            iatciei.Content = FormattedCurrencyString( ist.Income_Available_to_Common_Incl_Extra_Items );
            canvas.Children.Add( iatciei );
            Canvas.SetLeft( iatciei, max );
            Canvas.SetTop( iatciei, 150 );

            // Basic Weighted Average Shares
            var lbwas = new Label( );
            lbwas.FontSize = 10;
            lbwas.Content = "Basic Weighted Average Shares";
            canvas.Children.Add( lbwas );
            Canvas.SetLeft( lbwas, 0 );
            Canvas.SetTop( lbwas, 175 );
            var bwas = new Label( );
            bwas.FontSize = 10;
            bwas.Content = FormattedCurrencyString( ist.Basic_Weighted_Average_Shares );
            canvas.Children.Add( bwas );
            Canvas.SetLeft( bwas, max );
            Canvas.SetTop( bwas, 175 );

            // Basic EPS Excluding Extraordinary Items
            var lbeeei = new Label( );
            lbeeei.FontSize = 10;
            lbeeei.Content = "Basic EPS Excluding Extraordinary Items";
            canvas.Children.Add( lbeeei );
            Canvas.SetLeft( lbeeei, 0 );
            Canvas.SetTop( lbeeei, 190 );
            var beeei = new Label( );
            beeei.FontSize = 10;
            beeei.Content = FormattedCurrencyString( ist.Basic_EPS_Excluding_Extraordinary_Items );
            canvas.Children.Add( beeei );
            Canvas.SetLeft( beeei, max );
            Canvas.SetTop( beeei, 190 );

            // Basic EPS Including Extraordinary Items
            var lbeiei = new Label( );
            lbeiei.FontSize = 10;
            lbeiei.Content = "Basic EPS Including Extraordinary Items";
            canvas.Children.Add( lbeiei );
            Canvas.SetLeft( lbeiei, 0 );
            Canvas.SetTop( lbeiei, 205 );
            var beiei = new Label( );
            beiei.FontSize = 10;
            beiei.Content = FormattedCurrencyString( ist.Basic_EPS_Including_Extraordinary_Items );
            canvas.Children.Add( beiei );
            Canvas.SetLeft( beiei, max );
            Canvas.SetTop( beiei, 205 );

            // Dilution Adjustment
            var lda = new Label( );
            lda.FontSize = 10;
            lda.Content = "Dilution Adjustment";
            canvas.Children.Add( lda );
            Canvas.SetLeft( lda, 0 );
            Canvas.SetTop( lda, 220 );
            var da = new Label( );
            da.FontSize = 10;
            da.Content = FormattedCurrencyString( ist.Dilution_Adjustment );
            canvas.Children.Add( da );
            Canvas.SetLeft( da, max );
            Canvas.SetTop( da, 220 );

            // Diluted Weighted Average Shares
            var ldwas = new Label( );
            ldwas.FontSize = 10;
            ldwas.Content = "Diluted Weighted Average Shares";
            canvas.Children.Add( ldwas );
            Canvas.SetLeft( ldwas, 0 );
            Canvas.SetTop( ldwas, 235 );
            var dwas = new Label( );
            dwas.FontSize = 10;
            dwas.Content = FormattedCurrencyString( ist.Diluted_Weighted_Average_Shares );
            canvas.Children.Add( dwas );
            Canvas.SetLeft( dwas, max );
            Canvas.SetTop( dwas, 235 );

            //Diluted EPS Excluding Extraordinary Items
            var ldeeeis = new Label( );
            ldeeeis.FontSize = 10;
            ldeeeis.Content = "Diluted EPS Excluding Extraordinary Items";
            canvas.Children.Add( ldeeeis );
            Canvas.SetLeft( ldeeeis, 0 );
            Canvas.SetTop( ldeeeis, 250 );
            var deeeis = new Label( );
            deeeis.FontSize = 10;
            deeeis.Content = FormattedCurrencyString( ist.Diluted_EPS_Excluding_Extraordinary_Items );
            canvas.Children.Add( deeeis );
            Canvas.SetLeft( deeeis, max );
            Canvas.SetTop( deeeis, 250 );

            return canvas;
        }

        // third "part" of the I/S
        static Canvas ISDilutedNormalizedEPS( IncomeStatement ist )
        {
            var canvas = new Canvas( );
            int max = "Dividends per Share - Common Stock Primary Issue".Length * 6;
            // Diluted EPS Including Extraordinary Items
            var ldeiei = new Label( );
            ldeiei.FontSize = 10;
            ldeiei.Content = "Diluted EPS Including Extraordinary Items";
            canvas.Children.Add( ldeiei );
            Canvas.SetLeft( ldeiei, 0 );
            Canvas.SetTop( ldeiei, 0 );
            var deiei = new Label( );
            deiei.FontSize = 10;
            deiei.Content = FormattedCurrencyString( ist.Diluted_EPS_Including_Extraordinary_Items );
            canvas.Children.Add( deiei );
            Canvas.SetLeft( deiei, max );
            Canvas.SetTop( deiei, 0 );

            // Dividends per Share - Common Stock Primary Issue
            var ldpscspi = new Label( );
            ldpscspi.FontSize = 10;
            ldpscspi.Content = "Dividends per Share - Common Stock Primary Issue";
            canvas.Children.Add( ldpscspi );
            Canvas.SetLeft( ldpscspi, 0 );
            Canvas.SetTop( ldpscspi, 15 );
            var dpscspi = new Label( );
            dpscspi.FontSize = 10;
            dpscspi.Content = FormattedCurrencyString( ist.Dividends_per_Share__less__Common_Stock_Primary_Issue );
            canvas.Children.Add( dpscspi );
            Canvas.SetLeft( dpscspi, max );
            Canvas.SetTop( dpscspi, 15 );

            // Gross Dividends - Common Stock
            var lgdcs = new Label( );
            lgdcs.FontSize = 10;
            lgdcs.Content = "Gross Dividends - Common Stock";
            canvas.Children.Add( lgdcs );
            Canvas.SetLeft( lgdcs, 0 );
            Canvas.SetTop( lgdcs, 30 );
            var gdcs = new Label( );
            gdcs.FontSize = 10;
            gdcs.Content = FormattedCurrencyString( ist.Gross_Dividends__less__Common_Stock );
            canvas.Children.Add( gdcs );
            Canvas.SetLeft( gdcs, max );
            Canvas.SetTop( gdcs, 30 );

            // Net Income after Stock Based Comp. Expense
            var lniasbce = new Label( );
            lniasbce.FontSize = 10;
            lniasbce.Content = "Net Income after Stock Based Comp. Expense";
            canvas.Children.Add( lniasbce );
            Canvas.SetLeft( lniasbce, 0 );
            Canvas.SetTop( lniasbce, 45 );
            var niasbce = new Label( );
            niasbce.FontSize = 10;
            niasbce.Content = FormattedCurrencyString( ist.Net_Income_after_Stock_Based_Comp_Expense );
            canvas.Children.Add( niasbce );
            Canvas.SetLeft( niasbce, max );
            Canvas.SetTop( niasbce, 45 );

            // Basic EPS after Stock Based Comp. Expense
            var lbeasbce = new Label( );
            lbeasbce.FontSize = 10;
            lbeasbce.Content = "Basic EPS after Stock Based Comp. Expense";
            canvas.Children.Add( lbeasbce );
            Canvas.SetLeft( lbeasbce, 0 );
            Canvas.SetTop( lbeasbce, 60 );
            var beasbce = new Label( );
            beasbce.FontSize = 10;
            beasbce.Content = FormattedCurrencyString( ist.Basic_EPS_after_Stock_Based_Comp_Expense );
            canvas.Children.Add( beasbce );
            Canvas.SetLeft( beasbce, max );
            Canvas.SetTop( beasbce, 60 );

            // Diluted EPS after Stock Based Comp. Expense
            var ldeasbce = new Label( );
            ldeasbce.FontSize = 10;
            ldeasbce.Content = "Diluted EPS after Stock Based Comp. Expense";
            canvas.Children.Add( ldeasbce );
            Canvas.SetLeft( ldeasbce, 0 );
            Canvas.SetTop( ldeasbce, 75 );
            var deasbce = new Label( );
            deasbce.FontSize = 10;
            deasbce.Content = FormattedCurrencyString( ist.Diluted_EPS_after_Stock_Based_Comp_Expense );
            canvas.Children.Add( deasbce );
            Canvas.SetLeft( deasbce, max );
            Canvas.SetTop( deasbce, 75 );

            // Depreciation, Supplemental
            var lds = new Label( );
            lds.FontSize = 10;
            lds.Content = "Depreciation, Supplemental";
            canvas.Children.Add( lds );
            Canvas.SetLeft( lds, 0 );
            Canvas.SetTop( lds, 90 );
            var ds = new Label( );
            ds.FontSize = 10;
            ds.Content = FormattedCurrencyString( ist.Depreciation_Supplemental );
            canvas.Children.Add( ds );
            Canvas.SetLeft( ds, max );
            Canvas.SetTop( ds, 90 );

            // Total Special Items
            var ltsi = new Label( );
            ltsi.FontSize = 10;
            ltsi.Content = "Total Special Items";
            canvas.Children.Add( ltsi );
            Canvas.SetLeft( ltsi, 0 );
            Canvas.SetTop( ltsi, 105 );
            var tsi = new Label( );
            tsi.FontSize = 10;
            tsi.Content = FormattedCurrencyString( ist.Total_Special_Items );
            canvas.Children.Add( tsi );
            Canvas.SetLeft( tsi, max );
            Canvas.SetTop( tsi, 105 );

            // Normalized Income Before Taxes
            var lnibt = new Label( );
            lnibt.FontSize = 10;
            lnibt.Content = "Normalized Income Before Taxes";
            canvas.Children.Add( lnibt );
            Canvas.SetLeft( lnibt, 0 );
            Canvas.SetTop( lnibt, 120 );
            var nibt = new Label( );
            nibt.FontSize = 10;
            nibt.Content = FormattedCurrencyString( ist.Normalized_Income_Before_Taxes );
            canvas.Children.Add( nibt );
            Canvas.SetLeft( nibt, max );
            Canvas.SetTop( nibt, 120 );

            // Effect of Special Items on Income Taxes
            var leosioit = new Label( );
            leosioit.FontSize = 10;
            leosioit.Content = "Effect of Special Items on Income Taxes";
            canvas.Children.Add( leosioit );
            Canvas.SetLeft( leosioit, 0 );
            Canvas.SetTop( leosioit, 135 );
            var eosioit = new Label( );
            eosioit.FontSize = 10;
            eosioit.Content = FormattedCurrencyString( ist.Effect_of_Special_Items_on_Income_Taxes );
            canvas.Children.Add( eosioit );
            Canvas.SetLeft( eosioit, max );
            Canvas.SetTop( eosioit, 135 );

            // Income Taxes Ex. Impact of Special Items
            var liteiosi = new Label( );
            liteiosi.FontSize = 10;
            liteiosi.Content = "Income Taxes Ex. Impact of Special Items";
            canvas.Children.Add( liteiosi );
            Canvas.SetLeft( liteiosi, 0 );
            Canvas.SetTop( liteiosi, 150 );
            var iteiosi = new Label( );
            iteiosi.FontSize = 10;
            iteiosi.Content = FormattedCurrencyString( ist.Income_Taxes_Ex_Impact_of_Special_Items );
            canvas.Children.Add( iteiosi );
            Canvas.SetLeft( iteiosi, max );
            Canvas.SetTop( iteiosi, 150 );

            // Normalized Income After Taxes
            var lniat = new Label( );
            lniat.FontSize = 10;
            lniat.Content = "Normalized Income After Taxes";
            canvas.Children.Add( lniat );
            Canvas.SetLeft( lniat, 0 );
            Canvas.SetTop( lniat, 165 );
            var niat = new Label( );
            niat.FontSize = 10;
            niat.Content = FormattedCurrencyString( ist.Normalized_Income_After_Taxes );
            canvas.Children.Add( niat );
            Canvas.SetLeft( niat, max );
            Canvas.SetTop( niat, 165 );

            // Normalized Income Avail to Common
            var lniatc = new Label( );
            lniatc.FontSize = 10;
            lniatc.Content = "Normalized Income Avail to Common";
            canvas.Children.Add( lniatc );
            Canvas.SetLeft( lniatc, 0 );
            Canvas.SetTop( lniatc, 180 );
            var niatc = new Label( );
            niatc.FontSize = 10;
            niatc.Content = FormattedCurrencyString( ist.Normalized_Income_Avail_to_Common );
            canvas.Children.Add( niatc );
            Canvas.SetLeft( niatc, max );
            Canvas.SetTop( niatc, 180 );

            // Basic Normalized EPS
            var lbne = new Label( );
            lbne.FontSize = 10;
            lbne.Content = "Basic Normalized EPS";
            canvas.Children.Add( lbne );
            Canvas.SetLeft( lbne, 0 );
            Canvas.SetTop( lbne, 195 );
            var bne = new Label( );
            bne.FontSize = 10;
            bne.Content = FormattedCurrencyString( ist.Basic_Normalized_EPS );
            canvas.Children.Add( bne );
            Canvas.SetLeft( bne, max );
            Canvas.SetTop( bne, 195 );

            // Diluted Normalized EPS
            var ldne = new Label( );
            ldne.FontSize = 10;
            ldne.Content = "Diluted Normalized EPS";
            canvas.Children.Add( ldne );
            Canvas.SetLeft( ldne, 0 );
            Canvas.SetTop( ldne, 210 );
            var dne = new Label( );
            dne.FontSize = 10;
            dne.Content = FormattedCurrencyString( ist.Diluted_Normalized_EPS );
            canvas.Children.Add( dne );
            Canvas.SetLeft( dne, max );
            Canvas.SetTop( dne, 210 );

            return canvas;

        }

        static public Canvas SoCF( CashFlowStatement cf )
        {
            var c = new Canvas( );
            int max = "Other Investing Cash Flow Items, Total".Length * 6;

            // Net Income/Starting Line
            var laz = new Label( );
            laz.FontSize = 10;
            laz.Content = "Net Income/Starting Line";
            c.Children.Add( laz );
            Canvas.SetLeft( laz, 0 );
            Canvas.SetTop( laz, 0 );
            var az = new Label( );
            az.FontSize = 10;
            az.Content = FormattedCurrencyString( cf.Net_Income_and_Starting_Line );
            c.Children.Add( az );
            Canvas.SetLeft( az, max );
            Canvas.SetTop( az, 0 );

            // Depreciation/Depletion
            var ldd = new Label( );
            ldd.FontSize = 10;
            ldd.Content = "Depreciation/Depletion";
            c.Children.Add( ldd );
            Canvas.SetLeft( ldd, 0 );
            Canvas.SetTop( ldd, 15 );
            var dd = new Label( );
            dd.FontSize = 10;
            dd.Content = FormattedCurrencyString( cf.Depreciation_and_Depletion );
            c.Children.Add( dd );
            Canvas.SetLeft( dd, max );
            Canvas.SetTop( dd, 15 );

            // Amortization
            var la = new Label( );
            la.FontSize = 10;
            la.Content = "Amortization";
            c.Children.Add( la );
            Canvas.SetLeft( la, 0 );
            Canvas.SetTop( la, 30 );
            var a = new Label( );
            a.FontSize = 10;
            a.Content = FormattedCurrencyString( cf.Amortization );
            c.Children.Add( a );
            Canvas.SetLeft( a, max );
            Canvas.SetTop( a, 30 );

            // Deferred Taxes
            var lt = new Label( );
            lt.FontSize = 10;
            lt.Content = "Deferred Taxes";
            c.Children.Add( lt );
            Canvas.SetLeft( lt, 0 );
            Canvas.SetTop( lt, 45 );
            var t = new Label( );
            t.FontSize = 10;
            t.Content = FormattedCurrencyString( cf.Deferred_Taxes );
            c.Children.Add( t );
            Canvas.SetLeft( t, max );
            Canvas.SetTop( t, 45 );

            // Non-Cash Items
            var lnci = new Label( );
            lnci.FontSize = 10;
            lnci.Content = "Non-Cash Items";
            c.Children.Add( lnci );
            Canvas.SetLeft( lnci, 0 );
            Canvas.SetTop( lnci, 60 );
            var nci = new Label( );
            nci.FontSize = 10;
            nci.Content = FormattedCurrencyString( cf.NonCash_Items );
            c.Children.Add( nci );
            Canvas.SetLeft( nci, max );
            Canvas.SetTop( nci, 60 );

            // Changes in Working Capital
            var lciwc = new Label( );
            lciwc.FontSize = 10;
            lciwc.Content = "Changes in Working Capital";
            c.Children.Add( lciwc );
            Canvas.SetLeft( lciwc, 0 );
            Canvas.SetTop( lciwc, 75 );
            var ciwc = new Label( );
            ciwc.FontSize = 10;
            ciwc.Content = FormattedCurrencyString( cf.Changes_in_Working_Capital );
            c.Children.Add( ciwc );
            Canvas.SetLeft( ciwc, max );
            Canvas.SetTop( ciwc, 75 );

            // Cash from Operating Activities
            var lcfoa = new Label( );
            lcfoa.FontSize = 10;
            lcfoa.Content = "Cash from Operating Activities";
            c.Children.Add( lcfoa );
            Canvas.SetLeft( lcfoa, 0 );
            Canvas.SetTop( lcfoa, 90 );
            var cfoa = new Label( );
            cfoa.FontSize = 10;
            cfoa.Content = FormattedCurrencyString( cf.Cash_from_Operating_Activities );
            c.Children.Add( cfoa );
            Canvas.SetLeft( cfoa, max );
            Canvas.SetTop( cfoa, 90 );

            //  Capital Expenditures
            var lce = new Label( );
            lce.FontSize = 10;
            lce.Content = "Capital Expenditures";
            c.Children.Add( lce );
            Canvas.SetLeft( lce, 0 );
            Canvas.SetTop( lce, 105 );
            var ce = new Label( );
            ce.FontSize = 10;
            ce.Content = FormattedCurrencyString( cf.Capital_Expenditures );
            c.Children.Add( ce );
            Canvas.SetLeft( ce, max );
            Canvas.SetTop( ce, 105 );

            // Other Investing Cash Flow Items, Total
            var loicfit = new Label( );
            loicfit.FontSize = 10;
            loicfit.Content = "Other Investing Cash Flow Items, Total";
            c.Children.Add( loicfit );
            Canvas.SetLeft( loicfit, 0 );
            Canvas.SetTop( loicfit, 120 );
            var oicfit = new Label( );
            oicfit.FontSize = 10;
            oicfit.Content = FormattedCurrencyString( cf.Other_Investing_Cash_Flow_Items__Total );
            c.Children.Add( oicfit );
            Canvas.SetLeft( oicfit, max );
            Canvas.SetTop( oicfit, 120 );

            // Cash from Investing Activities
            var lcfia = new Label( );
            lcfia.FontSize = 10;
            lcfia.Content = "Cash from Investing Activities";
            c.Children.Add( lcfia );
            Canvas.SetLeft( lcfia, 0 );
            Canvas.SetTop( lcfia, 135 );
            var cfia = new Label( );
            cfia.FontSize = 10;
            cfia.Content = FormattedCurrencyString( cf.Cash_from_Investing_Activities );
            c.Children.Add( cfia );
            Canvas.SetLeft( cfia, max );
            Canvas.SetTop( cfia, 135 );

            // Financing Cash Flow Items
            var lfcfi = new Label( );
            lfcfi.FontSize = 10;
            lfcfi.Content = "Financing Cash Flow Items";
            c.Children.Add( lfcfi );
            Canvas.SetLeft( lfcfi, 0 );
            Canvas.SetTop( lfcfi, 150 );
            var fcfi = new Label( );
            fcfi.FontSize = 10;
            fcfi.Content = FormattedCurrencyString( cf.Financing_Cash_Flow_Items );
            c.Children.Add( fcfi );
            Canvas.SetLeft( fcfi, max );
            Canvas.SetTop( fcfi, 150 );

            // Total Cash Dividends Paid
            var ltcdp = new Label( );
            ltcdp.FontSize = 10;
            ltcdp.Content = "Total Cash Dividends Paid";
            c.Children.Add( ltcdp );
            Canvas.SetLeft( ltcdp, 0 );
            Canvas.SetTop( ltcdp, 165 );
            var tcdp = new Label( );
            tcdp.FontSize = 10;
            tcdp.Content = FormattedCurrencyString( cf.Total_Cash_Dividends_Paid );
            c.Children.Add( tcdp );
            Canvas.SetLeft( tcdp, max );
            Canvas.SetTop( tcdp, 165 );

            // Issuance (Retirement) of Stock, Net
            var lirosn = new Label( );
            lirosn.FontSize = 10;
            lirosn.Content = "Issuance (Retirement) of Stock, Net";
            c.Children.Add( lirosn );
            Canvas.SetLeft( lirosn, 0 );
            Canvas.SetTop( lirosn, 180 );
            var irosn = new Label( );
            irosn.FontSize = 10;
            irosn.Content = FormattedCurrencyString( cf.Issuance__Retirement__of_Stock__Net );
            c.Children.Add( irosn );
            Canvas.SetLeft( irosn, max );
            Canvas.SetTop( irosn, 180 );

            // Issuance (Retirement) of Debt, Net
            var lirodn = new Label( );
            lirodn.FontSize = 10;
            lirodn.Content = "Issuance (Retirement) of Debt, Net";
            c.Children.Add( lirodn );
            Canvas.SetLeft( lirodn, 0 );
            Canvas.SetTop( lirodn, 195 );
            var irodn = new Label( );
            irodn.FontSize = 10;
            irodn.Content = FormattedCurrencyString( cf.Issuance__Retirement__of_Debt__Net );
            c.Children.Add( irodn );
            Canvas.SetLeft( irodn, max );
            Canvas.SetTop( irodn, 195 );

            // Cash from Financing Activities
            var lcffa = new Label( );
            lcffa.FontSize = 10;
            lcffa.Content = "Cash from Financing Activities";
            c.Children.Add( lcffa );
            Canvas.SetLeft( lcffa, 0 );
            Canvas.SetTop( lcffa, 210 );
            var cffa = new Label( );
            cffa.FontSize = 10;
            cffa.Content = FormattedCurrencyString( cf.Cash_from_Financing_Activities );
            c.Children.Add( cffa );
            Canvas.SetLeft( cffa, max );
            Canvas.SetTop( cffa, 210 );

            // Foreign Exchange Effects
            var lfee = new Label( );
            lfee.FontSize = 10;
            lfee.Content = "Foreign Exchange Effects";
            c.Children.Add( lfee );
            Canvas.SetLeft( lfee, 0 );
            Canvas.SetTop( lfee, 225 );
            var fee = new Label( );
            fee.FontSize = 10;
            fee.Content = FormattedCurrencyString( cf.Foreign_Exchange_Effects );
            c.Children.Add( fee );
            Canvas.SetLeft( fee, max );
            Canvas.SetTop( fee, 225 );

            // Net Change in Cash
            var lncic = new Label( );
            lncic.FontSize = 10;
            lncic.Content = "Net Change in Cash";
            c.Children.Add( lncic );
            Canvas.SetLeft( lncic, 0 );
            Canvas.SetTop( lncic, 240 );
            var ncic = new Label( );
            ncic.FontSize = 10;
            ncic.Content = FormattedCurrencyString( cf.Net_Change_in_Cash );
            c.Children.Add( ncic );
            Canvas.SetLeft( ncic, max );
            Canvas.SetTop( ncic, 240 );

            // Cash Interest Paid, Supplemental
            var lcips = new Label( );
            lcips.FontSize = 10;
            lcips.Content = "Cash Interest Paid, Supplemental";
            c.Children.Add( lcips );
            Canvas.SetLeft( lcips, 0 );
            Canvas.SetTop( lcips, 255 );
            var cips = new Label( );
            cips.FontSize = 10;
            cips.Content = FormattedCurrencyString( cf.Cash_Interest_Paid__Supplemental );
            c.Children.Add( cips );
            Canvas.SetLeft( cips, max );
            Canvas.SetTop( cips, 255 );

            // Cash Taxes Paid, Supplemental
            var lctps = new Label( );
            lctps.FontSize = 10;
            lctps.Content = "Cash Taxes Paid, Supplemental";
            c.Children.Add( lctps );
            Canvas.SetLeft( lctps, 0 );
            Canvas.SetTop( lctps, 270 );
            var ctps = new Label( );
            ctps.FontSize = 10;
            ctps.Content = FormattedCurrencyString( cf.Cash_Taxes_Paid__Supplemental );
            c.Children.Add( ctps );
            Canvas.SetLeft( ctps, max );
            Canvas.SetTop( ctps, 270 );

            return c;
        }
    }
}
