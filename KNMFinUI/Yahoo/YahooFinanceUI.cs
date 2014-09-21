using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using KNMFin.Yahoo;

using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

using System.Windows.Data;

namespace KNMFinUI.Yahoo
{
    public static class YahooFinanceUI
    {
        static public CollectionViewSource itemCollectionViewSource;
    
        public static Grid IndividualHistoricalPriceResult( System.Windows.Window window, KNMFin.Yahoo.HistoricalQuotes.StockPriceResult spr )
        {
            
            
            var rArr = spr.StockPriceInformation.StockPriceInformation.ToArray<KNMFin.Yahoo.HistoricalQuotes.StockPriceRow>( );
            Array.Reverse( rArr );

            var resources = window.Resources [ "ItemCollectionViewSource" ];
            itemCollectionViewSource = null;
            itemCollectionViewSource = (CollectionViewSource)( window.FindResource( "ItemCollectionViewSource" ) );
            itemCollectionViewSource.Source = spr.StockPriceInformation.StockPriceInformation;

            DataGrid dg = new DataGrid( );
            dg.DataContext = window.Resources [ "ItemCollectionViewSource" ];
            dg.ItemsSource = (IEnumerable<KNMFin.Yahoo.HistoricalQuotes.StockPriceRow>)itemCollectionViewSource.Source;
            dg.AutoGenerateColumns = true;
            dg.CanUserAddRows = false;

            Grid grid = new Grid( );
            grid.RowDefinitions.Add( new RowDefinition( ) );
            grid.RowDefinitions.Add( new RowDefinition( ) );
            grid.RowDefinitions [ 0 ].Height = new System.Windows.GridLength( 200 );

            Canvas c = new Canvas( );
            c.Background = System.Windows.Media.Brushes.Coral;
            grid.Children.Add( c );


            dg.FontSize = 10;

            Grid.SetRow( c, 0 );


            Grid subGrid = new Grid( );
            subGrid.Background = System.Windows.Media.Brushes.Green;
            subGrid.ColumnDefinitions.Add( new ColumnDefinition( ) );
            subGrid.ColumnDefinitions.Add( new ColumnDefinition( ) );
            subGrid.ColumnDefinitions.Add( new ColumnDefinition( ) );


            subGrid.ColumnDefinitions [ 0 ].Width = new System.Windows.GridLength( 350 );
            subGrid.ColumnDefinitions [ 2 ].Width = new System.Windows.GridLength( 350 );
            subGrid.Children.Add( dg );
            Grid.SetColumn( dg, 0 );
            grid.Children.Add( subGrid );
            Grid.SetRow( subGrid, 1 );



            List<Tuple<string, double, double>> LogReturns = new List<Tuple<string, double, double>>( );
            for ( int i = 1; i < rArr.Length; i++ )
            {
                double adjCReturn = Math.Log( Convert.ToDouble( rArr [ i ].AdjClose / rArr [ i - 1 ].AdjClose ) );
                double cReturn = Math.Log( Convert.ToDouble( rArr [ i ].Close / rArr [ i - 1 ].Close ) );
                LogReturns.Add( new Tuple<string, double, double>( rArr [ i ].Date, adjCReturn, cReturn ) );
            }



            DataTable dt = new DataTable( );
            dt.Columns.Add( new DataColumn( "Date", typeof( string ) ) );
            dt.Columns.Add( new DataColumn( "Adj. Return", typeof( double ) ) );
            dt.Columns.Add( new DataColumn( "unAdj. Return", typeof( double ) ) );

            DataRow dr;


            for ( int i = 0; i < LogReturns.Count; i++ ){

                dr = dt.NewRow( );
                dr [ 0 ] = LogReturns [ i ].Item1;
                dr [ 1 ] = LogReturns [ i ].Item2;
                dr [ 2 ] = LogReturns [ i ].Item3;
                dt.Rows.Add( dr );
            }

            DataGrid dg2 = new DataGrid( );
            dg2.DataContext = dt;
            subGrid.Children.Add( dg2 );
            Grid.SetColumn( dg2, 2 );

            // ./subGrid.Children.Add( dg2 );
            // Grid.SetColumn( dg2, 2 );

            return grid;
        }
    }
}
