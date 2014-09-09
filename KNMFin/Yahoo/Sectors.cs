using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNMFin.Yahoo
{
    namespace Sectors{
        // Only used for response sorting order. 
        public enum Sort
        {
            Up = 'u',
            Down = 'd'
        }
        public enum Sector
        {
            Basic_Materials = 1,
            Conglomerates = 2,
            Consumer_Goods = 3,
            Financial = 4,
            Healthcare = 5,
            Industrial_Goods = 6,
            Services = 7,
            Technology = 8,
            Utilities = 9
        }

        // Class used to request aggregate sector financial infromation on:
        // 1) 1-Day Price Chg% 2) ROE 3)  Div. Yield %	4) Net Profit Margin (mrq)	
        // 5) P/E 6) Debt to Equity	6) Price to Book 7) Price To Free Cash Flow (mrq)	
        // 8) Market Cap (Ordering here is arbitrary and irrelevant)
        //
        // For the following sectors:
        // 1) Basic_Materials 2) Conglomerates 3) Consumer_Goods 4) Financial
        // 5) Healthcare 6) Industrial_Goods 7) Services 8) Technology 9) Utilities 
        public static class Quote
        {
            // (This should be used and the parameters should be disregarded)
            // The "default" query -- wraps the parametized routine with the default paramters:
            //  - MarketQuoteProperties.Name
            //  - Sort.UP
            // (Sorting order is irrelevant for how the parsing is currently implemented) 
            public static Dictionary<string, Dictionary<string, double>> SectorQuery(){

                return SectorQuery( KNMFin.Yahoo.Quotes.MarketQuoteProperties.Name, Sort.Up );
            }
            
            // Returns information for following each sector in the enum Sector
            //
            // Each sector is mapped to dictionary containg key -> value (string -> double) mappings with the following keys:
            // 1) 1-Day Price Chg% 2) ROE 3)  Div. Yield %	4) Net Profit Margin (mrq)	
            // 5) P/E 6) Debt to Equity	6) Price to Book 7) Price To Free Cash Flow (mrq)	
            // 8) Market Cap (Ordering here is arbitrary and irrelevant)
            // (Parameters are currently irrelevant for how the parsing is currently implemented )
            public static Dictionary<string, Dictionary<string, double>> SectorQuery( KNMFin.Yahoo.Quotes.MarketQuoteProperties SortBy, KNMFin.Yahoo.Sectors.Sort SortDirection )
            {
                string requestURL = baseURL + SortBy.ToString( ) + (char)SortDirection + endURL;

                string qResult = string.Empty;

                try{
                    qResult = client.DownloadString( requestURL );
                } catch{
                    return new Dictionary<string, Dictionary<string, double>>( );
                }

                string [] stringSeparator = new string [] { "\n" };
                var resultLines = qResult.Split( stringSeparator, StringSplitOptions.RemoveEmptyEntries );
  

                string [] columns = resultLines [ 0 ].Split( ',' );
                
                string[] cols = new string[columns.Length-1];
                for ( int i = 1; i < columns.Length; i++ )
                    cols [ i - 1 ] = columns [ i ].Replace( "\\", "" ).Replace( "\"", "" );

                Dictionary<string, Dictionary<string, double>> results = new Dictionary<string, Dictionary<string, double>>( );
                
                for ( int i = 1; i < resultLines.Length-1; i++ ){
                    
                    var rows = resultLines [ i ].Split(',');
                    Dictionary<string, double> industryData = new Dictionary<string,double>();
                    
                    // % formatted cases:
                    //                   * 1-Day Price Chg %   
                    //                   * ROE %
                    //                   * Div. Yield %
                    //                   * Net Profit Margin (mrq)
                    industryData.Add(cols[0], Convert.ToDouble(rows[1])/100);
                    industryData.Add( cols [ 3 ], Convert.ToDouble( rows [ 4 ] ) / 100 );
                    industryData.Add( cols [ 4 ], Convert.ToDouble( rows [ 5 ] ) / 100 );
                    industryData.Add( cols [ 7 ], Convert.ToDouble( rows [ 8 ] ) / 100 );

                    // standard numeric cases:
                    //                        * P/E
                    //                        * Debt to Equity
                    //                        * Price to Book
                    //                        * Price To Free Cash Flow (mrq)
                    industryData.Add(cols[2], Convert.ToDouble(rows[3]));
                    industryData.Add( cols [ 5 ], Convert.ToDouble( rows [ 6 ] ) );
                    industryData.Add( cols [ 6 ], Convert.ToDouble( rows [ 7 ] ) );
                    industryData.Add( cols [ 8 ], Convert.ToDouble( rows [ 9 ] ) );

                    // special numeric case:
                    //                      * Market Cap
                    //                          - contains 'B" to represent a billion
                    string tempValue = rows [ 2 ].Substring( 0, rows [ 2 ].Length - 1 );
                    industryData.Add( cols [ 1 ], Convert.ToDouble( tempValue ) * billion );
                    
                    results.Add( rows [ 0 ], industryData );
                }

                return results;
            }

            // Internal request components 
            private static readonly string baseURL = @"http://biz.yahoo.com/p/csv/s_";
            private static readonly string endURL = @".csv";
            private static System.Net.WebClient client = new System.Net.WebClient( );
            private static readonly double billion = 1000000000;
        }                                                    
    }
}
