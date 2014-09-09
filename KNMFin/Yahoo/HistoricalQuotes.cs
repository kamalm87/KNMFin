using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNMFin.Yahoo
{
    namespace HistoricalQuotes
    {
        // Enums representing the possible frequency options for a historical stock price request
        public enum Frequency{
            Daily = 'd',
            Weekly = 'w',
            Monthly = 'm'
        }

        // Class to request quotes of historical stock price information
        // * QueryStockPriceInformation: given a stock ticker, StartDate, EndDate, and a Frequency, 
        //   returns a StockPriceResult object representing the parsed response 
        //
        // * QueryStockPriceInformation: given list of tickers, StartDate, EndDate, and a Frequency, 
        //   returns a list StockPriceResult objects representing the parsed response
        //   for each of ticker in the parameter's list
        public static class Quote
        {
            // Queries historical stock price information for an individual ticker, date range, and a frequency
            // - if request is successful:
            //   - returns a StockPriceResult object with Results set to true and an initialized StockPrice
            //     object
            //
            // - if request is unsuccessful:
            //   - returns a StockPriceResult object with Results set to false and the StockPrice object set to null
            public static StockPriceResult QueryStockPriceInformation( string Ticker, DateTime Start, DateTime End, Frequency Frequency )
            {
                string requestURL = CreateRequestURL( Ticker, Start, End, Frequency );
                var responseString = string.Empty;

                try
                {
                    responseString = client.DownloadString( requestURL );
                }
                catch
                {
                    return new StockPriceResult( Ticker );
                }

                return new StockPriceResult( Ticker, responseString );
            }

            // Queries historical stock price information for an a list tickers, date range, and a frequency
            // - returns a list of StockPriceResult by using the individual QueryStockPriceInformation routine
            public static List<StockPriceResult> QueryStockPriceInformation( IList<string> Tickers, DateTime Start, DateTime End, Frequency Frequency )
            {
                List<StockPriceResult> results = new List<StockPriceResult>( );
                foreach ( string ticker in Tickers )
                    results.Add( QueryStockPriceInformation( ticker, Start, End, Frequency ) );

                return results;
            }

            // Internal base components of a stock price request
            private static string baseURL = @"http://ichart.yahoo.com/table.csv?s=";
            private static string endURL = @"&ignore=.csv";
            private static System.Net.WebClient client = new System.Net.WebClient( );
            
            // Internal routine to create a request URL given a ticker, date range, and a stock price frequency
            private static string CreateRequestURL( string ticker, DateTime start, DateTime end, Frequency frequency ){
                StringBuilder sb = new StringBuilder( );
                sb.Append( baseURL ).Append(ticker);
                sb.Append( "&a=" + ( start.Month - 1 ) + "&b=" + ( start.Day ) + "&c=" + ( start.Year ) );
                sb.Append( "&d=" + ( end.Month - 1 ) + "&e=" + ( end.Day ) + "&f=" + ( end.Year ) );
                sb.Append( "&g=" + (char)frequency );
                sb.Append(endURL);
                return sb.ToString( );
            }
        }

        // Represents the result of a query on a stock ticker's historical price quote
        // * Data:
        //   - Ticker: The stock/index/etc. ticker on which the request was performed on
        //   - Results: Whether the request returned parsed results
        //   - Return: The parsed results, represented by a StockPrice object
        // * Implementation:
        //   ( The success of a request is determined within the Quotes.QueryReturn routine )
        //   - No result request StockPriceResult(string ticker) constructor
        //     - Simply maps request's ticker, and assigns Results to false, and Return to null
        //   - Successful request StockPriceResult(string ticker, string results) constructor
        //     - Performance similar mapping, with Results to true, and constructs a StockPrice
        //       object from the results paramter, which is expected to be an unparsed string 
        //       containing rows of '\n' delimited stock price row infromation
        public class StockPriceResult{

            public StockPriceResult(string ticker){
                
                this.Results = false;
                this.StockPriceInformation = null;
                this.Ticker = ticker;
            }
            
            public StockPriceResult( string ticker, string results ){
                this.Results = true;
                this.StockPriceInformation = new StockPrice( results );
                this.Ticker = ticker;
            }

            public string Ticker { get; set;}
            public bool Results {get; set;}
            public StockPrice StockPriceInformation { get; set; }
        }

        // Represent the response of a StockPrice query
        // * Data:
        // - HasReturns: If the response contains stock price information
        // - Date Range: BegDate -> EndDate -- will be null if no stock price information
        // - StockPriceInformation: list of StockPriceRows containing stock price information
        //      -- will be null if no stock price information
        // * Implementation details:
        //  - RawReturnData constructor parameter determines whether a constructed object contains information
        //      - consturctor splits this parameter by delimited '\n' into string[] rawRows
        //      - if this string[] is null or has no 0 items, then this object will contain a null date range,
        //        an empty list, and HasReturns will be false
        //      - otherwise: the string[] will be processed by the internal ProcessReturns subroutine, which 
        //        will parse the stock price information  
        // * TODO:
        // - implement DescendingOrder parameter option -- will allow for ascending/descending return of response's 
        //   information
        public class StockPrice{

            public StockPrice( string RawReturnData, bool DescendingOrder = false ){
                
                StockPriceInformation = new List<StockPriceRow>( );
                
                var rawRows = RawReturnData.Split( '\n' );

                if ( rawRows == null || rawRows.Count( ) == 0 ){
                    
                    BegDate = null;
                    EndDate = null;
                    StockPriceInformation = new List<StockPriceRow>( );
                    HasReturns = false;
                }
                else{
                    var results = ProcessReturns( rawRows );
                    StockPriceInformation = results.Item1;
                    BegDate = results.Item2;
                    EndDate = results.Item3;
                    HasReturns = true;
                }
            }

            public DateTime? BegDate { get; set; }
            public DateTime? EndDate { get; set; }
            public List<StockPriceRow> StockPriceInformation { get; set; }
            public bool HasReturns { get; set; }

// Internal items:

            // Internal modularized subroutine to implement the processing of all the requests stock price rows
            // - Returns 1) a list of the stock price rows, 2) the first date of the range, 3) the final date of hte range
            // - Individual row parsing is implemented by the ProcessRow subroutine
            // - If parsing is unsuccessful, the routine returns nulls for the Tuple's items
            // - The Yahoo Finance public url API's responses come in descending order (earliest date first)
            //   , so the process assumes that first row of stock price data is the last date, while the last row
            //   is the first date in regards to the date range
            private static Tuple<List<StockPriceRow>, DateTime?, DateTime?> ProcessReturns( string [] RawRows ){
                var returns = new List<StockPriceRow>( );
                DateTime? b = null, e = null;
                string temp = string.Empty;
                
                for ( int i = 1; i < RawRows.Length; i++ ){

                    temp = RawRows [ i ];
                    if ( temp != string.Empty && temp != "\0" ){
                        returns.Add( ProcessRow( RawRows [ i ] ) );
                    }
                }

                if ( returns.Count( ) == 1 ){
                    b = returns [ 0 ].GetDateTime( );
                    e = b;
                }
                else{
                    
                    e = returns [ 0 ].GetDateTime( );
                    b = returns [ returns.Count - 1 ].GetDateTime( );
                }

                return new Tuple<List<StockPriceRow>, DateTime?, DateTime?>( returns, b, e );
            }
            
            // Internal modularized subroutine to implement the processing of an individual row of stock price data
            // - Returns a StockPriceRow object representing a row's identified by a date with associated information
            // - This process assumes that the input has be vetted and is valid, so there are no input integrity 
            //   checks at this point
            private static StockPriceRow ProcessRow( string input ){
                string[] columns = input.Split( ',' );
                
                string[] dateColumns = columns [ 0 ].Split( '-' );
                int year = Convert.ToInt16(dateColumns[0]),
                    month = Convert.ToInt16(dateColumns[1]),
                    day  = Convert.ToInt16(dateColumns[2]);

                StockPriceRow spr = new StockPriceRow( new DateTime( year, month, day ) );
                spr.Open = Convert.ToDecimal( columns [ 1 ] );
                spr.High = Convert.ToDecimal( columns [ 2 ] );
                spr.Low = Convert.ToDecimal( columns [ 3 ] );
                spr.Close = Convert.ToDecimal( columns [ 4 ] );
                spr.Volume = Convert.ToDecimal( columns [ 5 ] );
                spr.AdjClose = Convert.ToDecimal( columns [ 6 ] );

                return spr;
            }
        }

        // Represents a row of stock price returns from a successful stock query.
        // Each row has an identifying date with the following associated values:
        //  1) Open 2) High 3) Low 4) Close 5) Volume 6) Adjusted Close
        // A private DateTime is used to represent the row identifying date for
        // sorting purposes, while a public string is used to represent the date
        // for display purposes.
        public class StockPriceRow : IComparable<StockPriceRow>
        {
            public StockPriceRow() { }
            public StockPriceRow( DateTime dt ){
                _Date = dt;
                Date = _Date.ToShortDateString( );
            }

            private DateTime _Date;
            public string Date { get; set; }
            public decimal Open { get; set; }
            public decimal High { get; set; }
            public decimal Low { get; set; }
            public decimal Close { get; set; }
            public decimal Volume { get; set; }
            public decimal AdjClose { get; set; }
                
            public int CompareTo( StockPriceRow other ){
                if ( other == null ) return 1;

                if ( this._Date.CompareTo(other._Date) < 0 ) 
                    return -1;
                else if ( this._Date == other._Date ) 
                    return 0;
                else
                    return 1;
            }

            public DateTime GetDateTime(){
                return _Date;
            }
        }
    }
}
