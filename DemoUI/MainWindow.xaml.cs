using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KNMFinUI;

namespace DemoUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DemoUI_Data Data;


        public MainWindow()
        {
            InitializeComponent( );
            this.Data = new DemoUI_Data( );
            foreach ( string s in this.Data.QuotePropertyNames )
                lbMarketQuoteProperties.Items.Add( s );
        }

        private void btnDEBUG_Click( object sender, RoutedEventArgs e ){
            var daData = this.Data;
            int DEBUG_BP = 1;
        }

        private void btnQuery_Click( object sender, RoutedEventArgs e )
        {
            List<string> tickersToQuery = new List<string>( );
            foreach ( string s in lbSlatedQueries.Items )
                tickersToQuery.Add( s );
            lbSlatedQueries.Items.Clear( );

            DateTime dtBeg = dtpBegDate.SelectedDate != null ? dtpBegDate.SelectedDate.Value : DateTime.Now;
            DateTime dtEnd = dtpEndDate.SelectedDate != null ? dtpEndDate.SelectedDate.Value : DateTime.Now;

            KNMFin.Yahoo.Quotes.QuoteProperties[] testQps = new KNMFin.Yahoo.Quotes.QuoteProperties[]{ KNMFin.Yahoo.Quotes.QuoteProperties.Name, KNMFin.Yahoo.Quotes.QuoteProperties.MarketCapitalization, KNMFin.Yahoo.Quotes.QuoteProperties.Revenue };


            Task.Factory.StartNew( () =>
            {
                foreach ( string s in tickersToQuery )
                {
                    if ( !Data.Queries.ContainsKey( s ) ){
                        Data.Queries.Add( s, new CompanyQuery( s, true, true, true, new Tuple<DateTime, DateTime>( dtBeg, dtEnd ), testQps ) );
                        Dispatcher.Invoke( ()=>{lbQueryResults.Items.Add( s );});
                        
                    }
                    else
                    {
                        Data.Queries [ s ] = new CompanyQuery( s, true, true, true, new Tuple<DateTime, DateTime>( dtBeg, dtEnd ), testQps );
                        Dispatcher.Invoke( () => { lbQueryResults.Items.Add( s ); } );
                    }
                }
            } );
            
            
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {

        }

        private void btnAddTicker_Click( object sender, RoutedEventArgs e )
        {
            this.Data.Tickers.Add( tbTickerCandidateToAdd.Text );
            lbSlatedQueries.Items.Add( tbTickerCandidateToAdd.Text );
        }

        private void lbQueryResults_SelectionChanged( object sender, SelectionChangedEventArgs e ){
            
            Task.Factory.StartNew( () =>
            {
                Dispatcher.Invoke( () =>
                {
                    this.Data.FocusedQuery = Data.Queries [ e.AddedItems [ 0 ].ToString( ) ];
                    UpdateResultTabControl( this.Data.FocusedQuery );
                    PriceInfo.Children.Add( KNMFinUI.Yahoo.YahooFinanceUI.IndividualHistoricalPriceResult( this, this.Data.FocusedQuery.YahooQuery.HistoricalPrices ) );
                } );
            } );
            
        }

        void UpdateResultTabControl( CompanyQuery cq ){
            UpdateGoogleSummaryInfo( cq );
            UpdateGoogleFinancialStatementInfo( cq );
        }

        void UpdateGoogleSummaryInfo(CompanyQuery cq)
        {
            lblGCompanyName.Content = cq.GoogleQuery.CompanyInfo.Name;
            lblGTicker.Content = cq.GoogleQuery.CompanyInfo.Ticker;
            lblGExchange.Content = cq.GoogleQuery.CompanyInfo.Exchange;

            lblGHL.Content = cq.GoogleQuery.CompanyInfo.RangeLow;
            lblGHR.Content = cq.GoogleQuery.CompanyInfo.RangeHigh;

            lblGYHL.Content = cq.GoogleQuery.CompanyInfo.FiftyTwoWeekLow;
            lblGYHR.Content = cq.GoogleQuery.CompanyInfo.FiftyTwoWeekHigh;

            lblGOpen.Content = cq.GoogleQuery.CompanyInfo.Open;
            lblGClose.Content = cq.GoogleQuery.CompanyInfo.Close;

            lblGPE.Content = cq.GoogleQuery.CompanyInfo.PriceEarnings;
            lblEPS.Content = cq.GoogleQuery.CompanyInfo.EarningsPerShare;
            
            lblGInstOwn.Content = cq.GoogleQuery.CompanyInfo.InstitutionalOwnership;
            lblGBeta.Content = cq.GoogleQuery.CompanyInfo.Beta;
            lblGMC.Content = cq.GoogleQuery.CompanyInfo.MarketCap;
            lblGShares.Content = cq.GoogleQuery.CompanyInfo.Shares;
        }

        void UpdateGoogleFinancialStatementInfo( CompanyQuery cq )
        {
            var curLB = lbBSA;
            if ( curLB.Items.Count != 0 ) curLB.Items.Clear( );

            foreach ( var item in cq.GoogleQuery.CompanyInfo.BalanceSheets.Where( i => i.Period == KNMFin.Google.Period.Annual ) )
                curLB.Items.Add( item.PeriodEnd.ToShortDateString( ) );

            curLB = lbBSQ;
            if ( curLB.Items.Count != 0 ) curLB.Items.Clear( );
            
            foreach ( var item in cq.GoogleQuery.CompanyInfo.BalanceSheets.Where( i => i.Period == KNMFin.Google.Period.Quarter ) )
                curLB.Items.Add( item.PeriodEnd.ToShortDateString( ) );

            curLB = lbISA;
            if ( curLB.Items.Count != 0 ) curLB.Items.Clear( );
            foreach ( var item in cq.GoogleQuery.CompanyInfo.IncomeStatements.Where( i => i.Period == KNMFin.Google.Period.Annual ) )
                curLB.Items.Add( item.PeriodEnd.ToShortDateString( ) );

            curLB = lbISQ;
            if ( curLB.Items.Count != 0 ) curLB.Items.Clear( );

            foreach ( var item in cq.GoogleQuery.CompanyInfo.IncomeStatements.Where( i => i.Period == KNMFin.Google.Period.Quarter ) )
                curLB.Items.Add( item.PeriodEnd.ToShortDateString( ) );

            curLB = lbSoCFA;
            if ( curLB.Items.Count != 0 ) curLB.Items.Clear( );
            foreach ( var item in cq.GoogleQuery.CompanyInfo.CashFlowStatements.Where( i => i.Period == KNMFin.Google.Period.Annual ) )
                curLB.Items.Add( item.PeriodEnd.ToShortDateString( ) );

            curLB = lbSoCFQ;
            if ( curLB.Items.Count != 0 ) curLB.Items.Clear( );

            foreach ( var item in cq.GoogleQuery.CompanyInfo.CashFlowStatements.Where( i => i.Period == KNMFin.Google.Period.Quarter ) )
                curLB.Items.Add( item.PeriodEnd.ToShortDateString( ) );
           
        }

        private void FocusedFinancialStatementChanged( object sender, SelectionChangedEventArgs e )
        {
            if ( e.AddedItems.Count == 0 ) return;
            
            List<KNMFin.Google.BalanceSheet> balanceSheets = null;
            List<KNMFin.Google.IncomeStatement> incomeStatements = null;
            List<KNMFin.Google.CashFlowStatement> socf = null;

            var sDateString = e.AddedItems[0].ToString();

            balanceSheets = ( sender == lbBSA || sender == lbBSQ ) ? this.Data.FocusedQuery.GoogleQuery.CompanyInfo.BalanceSheets : null;
            
            if(balanceSheets == null){

                incomeStatements = ( sender == lbISA || sender == lbISQ ) ? this.Data.FocusedQuery.GoogleQuery.CompanyInfo.IncomeStatements : null;
                if ( balanceSheets == null && incomeStatements == null )
                {
                    socf = ( sender == lbSoCFA || sender == lbSoCFQ ) ? this.Data.FocusedQuery.GoogleQuery.CompanyInfo.CashFlowStatements : null;
                    var itemToQuery = socf.Where( i => i.PeriodEnd.ToShortDateString( ) == sDateString ).FirstOrDefault( );
                    gridFinancialStatement.Children.Clear( );
                    gridFinancialStatement.Children.Add( GoogleFinanceUI.SoCF( itemToQuery ) );
                    return;
                }
                else{   
                    var itemToQuery = incomeStatements.Where( i => i.PeriodEnd.ToShortDateString( ) == sDateString ).FirstOrDefault( );
                    gridFinancialStatement.Children.Clear( );
                    gridFinancialStatement.Children.Add( GoogleFinanceUI.IncomeStatementCanvas( itemToQuery ) );
                    return;
                }
            }
            else{
                var itemToQuery = balanceSheets.Where( i => i.PeriodEnd.ToShortDateString( ) == sDateString ).FirstOrDefault();
                gridFinancialStatement.Children.Clear( );
                gridFinancialStatement.Children.Add( GoogleFinanceUI.BalanceSheetCanvas(itemToQuery) );
                return;
            }
        }

        private void tbTickerCandidateToAdd_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.Key == System.Windows.Input.Key.Return )
            {
                var ticker = tbTickerCandidateToAdd.Text;
                this.Data.Tickers.Add( ticker );
                lbSlatedQueries.Items.Add( ticker );
                tbTickerCandidateToAdd.Text = string.Empty;
                // int db = 1;
            }

            
        }

        private void lbMarketQuoteProperties_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.Key == Key.Enter )
            {
                var wutAreYou = lbMarketQuoteProperties.SelectedItem;
                lbMarketQuotePropertiesSlated.Items.Add( wutAreYou.ToString() );
            }
            
            int db = 1;
        }

        private void lbMarketQuotePropertiesSlated_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.Key == Key.Delete )
            {
                lbMarketQuotePropertiesSlated.Items.Remove( lbMarketQuotePropertiesSlated.SelectedItem );
            }
        }

        private void btnExcelHistoricalPrices_Click( object sender, RoutedEventArgs e )
        {
            Microsoft.Win32.SaveFileDialog fd = new Microsoft.Win32.SaveFileDialog( );
            
            fd.InitialDirectory = Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop );
            
            fd.FileName = DefaultExcelFileName( );
            
            fd.ShowDialog( );

            if ( fd.ShowDialog( ).Value ){
                var queryPriceResults = this.Data.Queries.Values.Select( i => i.YahooQuery.HistoricalPrices ).ToList<KNMFin.Yahoo.HistoricalQuotes.StockPriceResult>( );
                Task.Factory.StartNew( () => KNMFinExcel.Yahoo.ExcelYahoo.SaveToExcel( fd.FileName, queryPriceResults ) );
            }
        }

        private void btnGoogleFinancialStatements_BS_Click( object sender, RoutedEventArgs e )
        {
            // This process only works if there's a focused query
            if ( Data.FocusedQuery == null ) return;

            Microsoft.Win32.SaveFileDialog fd = new Microsoft.Win32.SaveFileDialog( );
            fd.InitialDirectory = Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop );
            fd.FileName = DefaultExcelFileName( );
            fd.ShowDialog( );
            
            
            if ( fd.ShowDialog( ).Value )
            {
                
                var ci = this.Data.FocusedQuery.GoogleQuery.CompanyInfo;
                        KNMFinExcel.Google.ExcelGoogle.SaveCashFlowStatements( fd.FileName, ci );
            }
        }




        private string DefaultExcelFileName()
        {
            StringBuilder sb = new StringBuilder( );
            sb.Append( "Prices_" ).Append( DateTime.Now.ToShortDateString( ).Replace( "/", "-" ) ).Append( "_" ).Append( DateTime.Now.Hour ).Append( "_" ).Append( DateTime.Now.Minute ).Append( "_" + DateTime.Now.Second + ".xlsx" );
            return sb.ToString( );
        }

        private void btnExcelBS_Click( object sender, RoutedEventArgs e )
        {
            if ( Data.FocusedQuery == null ) return;

            Microsoft.Win32.SaveFileDialog fd = new Microsoft.Win32.SaveFileDialog( );
            fd.InitialDirectory = Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop );
            fd.FileName = DefaultExcelFileName( );
            fd.ShowDialog( );
            var ci = this.Data.FocusedQuery.GoogleQuery.CompanyInfo;

            var blah = fd.ShowDialog( );
            if ( fd.ShowDialog( ).Value )
            {
                KNMFinExcel.Google.ExcelGoogle.SaveBalanceSheets( fd.FileName, ci );
            }
        }

        private void btnExcelIS_Click( object sender, RoutedEventArgs e )
        {
            if ( Data.FocusedQuery == null ) return;

            Microsoft.Win32.SaveFileDialog fd = new Microsoft.Win32.SaveFileDialog( );
            fd.InitialDirectory = Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop );
            fd.FileName = DefaultExcelFileName( );
            fd.ShowDialog( );
            var ci = this.Data.FocusedQuery.GoogleQuery.CompanyInfo;
            var blah = fd.ShowDialog( );
            if ( fd.ShowDialog( ).Value )
            {
                KNMFinExcel.Google.ExcelGoogle.SaveIncomeStatements( fd.FileName, ci );
            }
        }

        private void btnExcelSoCF_Click( object sender, RoutedEventArgs e )
        {
            if ( Data.FocusedQuery == null ) return;

            Microsoft.Win32.SaveFileDialog fd = new Microsoft.Win32.SaveFileDialog( );
            fd.InitialDirectory = Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop );
            fd.FileName = DefaultExcelFileName( );
            fd.ShowDialog( );

            var ci = this.Data.FocusedQuery.GoogleQuery.CompanyInfo;

            var blah = fd.ShowDialog( );
            if ( fd.ShowDialog( ).Value )
            {
                KNMFinExcel.Google.ExcelGoogle.SaveCashFlowStatements( fd.FileName, ci );
            }
        }

        private void btnExcelALL_Click( object sender, RoutedEventArgs e )
        {
            string baseDir = Environment.GetFolderPath( System.Environment.SpecialFolder.Desktop ) + "\\";
            string ticker = this.Data.FocusedQuery.GoogleQuery.CompanyInfo.Ticker;

            var testPath1 = baseDir + ticker + "_BS.xlsx";
            var testPath2 = baseDir + ticker + "_BS.xlsx";
            var testPath3 = baseDir + ticker + "_BS.xlsx";
            int DEBUG = 1;
            var ci = this.Data.FocusedQuery.GoogleQuery.CompanyInfo;
            Task.Factory.StartNew( () =>
            {
                KNMFinExcel.Google.ExcelGoogle.SaveBalanceSheets( baseDir + ticker + "_BS.xlsx", ci );
                KNMFinExcel.Google.ExcelGoogle.SaveIncomeStatements( baseDir + ticker + "_IS.xlsx", ci );
                KNMFinExcel.Google.ExcelGoogle.SaveCashFlowStatements( baseDir + ticker + "_SoCF.xlsx", ci );
            } );
            
        }
    }
}
