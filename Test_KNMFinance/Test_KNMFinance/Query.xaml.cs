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
using System.Windows.Shapes;

namespace Test_KNMFinance
{
    /// <summary>
    /// Interaction logic for Query.xaml
    /// </summary>
    public partial class Query : Window
    {
        public Query()
        {
            InitializeComponent( );
        }

        GoogleFinanceUI gUI = new GoogleFinanceUI( );
        private void btnGQuery_Click( object sender, RoutedEventArgs e ){
            
            string s = tbGQuery.Text;
            
                var c = gUI.QueryCompanyTicker( s );
                PopulateFormWithCurrentCompanyInfo( c );
            
        }


        private void PopulateFormWithCurrentCompanyInfo( KNMFin.Google.CompanyInfo c )
        {
            if ( c != null && c.Ticker != null && c.Ticker != string.Empty )
            {
                
                
                    summaryCanvas.Children.Clear( );
                    summaryCanvas.Children.Add( gUI.CompanyInfoCanvas( c ) );
                    if ( !lbNameCompany.Items.Contains( c.Name ) )
                    {
                        lbNameCompany.Items.Add( c.Name );
                        lbTickerCompany.Items.Add( c.Ticker );
                    }


                    ResetForm( );
                    foreach ( var i in c.BalanceSheets )
                    {
                        if ( i.Period == KNMFin.Google.Period.Annual )
                            lbABS.Items.Add( i.PeriodEnd.ToShortDateString( ) );
                        else
                            lbQBS.Items.Add( i.PeriodEnd.ToShortDateString( ) );
                    }
                    foreach ( var i in c.IncomeStatements )
                    {
                        if ( i.Period == KNMFin.Google.Period.Annual )
                            lbAIS.Items.Add( i.PeriodEnd.ToShortDateString( ) );
                        else
                            lbQIS.Items.Add( i.PeriodEnd.ToShortDateString( ) );
                    }
                    foreach ( var i in c.CashFlowStatements )
                    {
                        if ( i.Period == KNMFin.Google.Period.Annual )
                            lbASoCF.Items.Add( i.PeriodEnd.ToShortDateString( ) );
                        else
                            lbQSoCF.Items.Add( i.PeriodEnd.ToShortDateString( ) );
                    }
                    lbTickerCompany.SelectedItem = c.Ticker;
                
            }
        }

        private void PopulateBalanceSheet( KNMFin.Google.BalanceSheet bs )
        {
            
                bsAssets.Children.Clear( );
                bsLiabilities.Children.Clear( );
                bsEquity.Children.Clear( );
                bsAssets.Children.Add( gUI.BSAsset( bs ) );
                bsLiabilities.Children.Add( gUI.BSLiabilities( bs ) );
                bsEquity.Children.Add( gUI.BSEquities( bs ) );
            
        }

        private void PopulateIncomeStatement( KNMFin.Google.IncomeStatement iSt )
        {
            
                bsAssets.Children.Clear( );
                bsLiabilities.Children.Clear( );
                bsEquity.Children.Clear( );
                bsAssets.Children.Add( gUI.ISIBT( iSt ) );
                bsLiabilities.Children.Add( gUI.ISDilutedEPS( iSt ) );
                bsEquity.Children.Add( gUI.ISDilutedNormalizedEPS( iSt ) );
            
        }

        private void PopulateCashflowStatement( KNMFin.Google.CashFlowStatement cs )
        {
            
                bsAssets.Children.Clear( );
                bsLiabilities.Children.Clear( );
                bsEquity.Children.Clear( );
                bsAssets.Children.Add( gUI.SoCF( cs ) );
            
            
        }

        private void lbBS_SelectionChanged( object sender, SelectionChangedEventArgs e ){
            
            var cTicker = lbTickerCompany.SelectedItem.ToString();
            var daQuery = e.AddedItems [ 0 ].ToString( );
            ( (ListBox)sender ).UnselectAll( );
            
            var bs = gUI.QueryCompanyTicker( cTicker ).BalanceSheets.Where( i => i.PeriodEnd.ToShortDateString( ) == e.AddedItems[0].ToString( ) ).FirstOrDefault();
            if ( bs == null ) return;
            PopulateBalanceSheet( bs );
        }

        private void lbAIS_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            var cTicker = lbTickerCompany.SelectedItem.ToString( );
                
            var daQuery = e.AddedItems [ 0 ].ToString( );
            ( (ListBox)sender ).UnselectAll( );        
            var iSt = gUI.QueryCompanyTicker( cTicker ).IncomeStatements.Where( i => i.PeriodEnd.ToShortDateString( ) == e.AddedItems [ 0 ].ToString( ) ).FirstOrDefault( );
            if ( iSt == null ) return;
            PopulateIncomeStatement( iSt );
        }

        private void lbASoCF_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            var cTicker = lbTickerCompany.SelectedItem.ToString( );
            ( (ListBox)sender ).UnselectAll( );
            var cf = gUI.QueryCompanyTicker( cTicker ).CashFlowStatements.Where( i => i.PeriodEnd.ToShortDateString( ) == e.AddedItems [ 0 ].ToString( ) ).FirstOrDefault( );
            if ( cf == null ) return;
            
            PopulateCashflowStatement( cf );
        }

        private void lbSoCF_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            
            var cTicker = lbTickerCompany.SelectedItem.ToString( );
            var daQuery = e.AddedItems [ 0 ].ToString( );
            var cf = gUI.QueryCompanyTicker( cTicker ).CashFlowStatements.Where( i => i.PeriodEnd.ToShortDateString( ) == e.AddedItems [ 0 ].ToString( ) ).FirstOrDefault( );
            if ( cf == null ) return;
            PopulateCashflowStatement( cf );
            
        }

        private void lbTickerCompany_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            var query = e.AddedItems [ 0 ].ToString( );
            if ( query == null || query == string.Empty ) return;
            var c = gUI.QueryCompanyTicker( query );
            PopulateFormWithCurrentCompanyInfo( c );
        
        }

        private void lbNameCompany_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            var query = e.AddedItems [ 0 ].ToString( );
            if ( query == null || query == string.Empty ) return;
            var c = gUI.QueryCompanyName( query );
            PopulateFormWithCurrentCompanyInfo( c );
        }

        private void ResetForm(){
            lbABS.Items.Clear( );
            lbQBS.Items.Clear( );
            lbAIS.Items.Clear( );
            lbQIS.Items.Clear( );
            lbASoCF.Items.Clear( );
            lbQSoCF.Items.Clear( );
            bsAssets.Children.Clear( );
            bsEquity.Children.Clear( );
            bsLiabilities.Children.Clear( );
        }
    }
}
