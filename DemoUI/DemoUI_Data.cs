using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoUI
{
    public class DemoUI_Data
    {
        public DemoUI_Data()
        {
            QuotePropertyNames = KNMFin.Yahoo.Quotes.QuoteProperties.GetAllNames( );
            QuoteProperties = KNMFin.Yahoo.Quotes.QuoteProperties.GetAllNameValuePairs( );
            Tickers = new HashSet<string>( );
            TickerGroups = new Dictionary<string, List<string>>( );
            NameToTicker = new Dictionary<string, string>( );
            TickerToName = new Dictionary<string, string>( );
            Queries = new Dictionary<string, CompanyQuery>( );
            int db = 1;
        }

        public HashSet<string> Tickers { get; set; }
        Dictionary<string, List<string>> TickerGroups { get; set; }
        Dictionary<string, string> NameToTicker { get; set; }
        Dictionary<string, string> TickerToName { get; set; }
        public Dictionary<string, string> QuoteProperties { get; set; }
        public string [] QuotePropertyNames { get; set; }
        public Dictionary<string, CompanyQuery> Queries { get; set; }
        public CompanyQuery FocusedQuery { get; set; }
    }

    public class CompanyQuery
    {
        public CompanyQuery(string ticker, bool qGoogle, bool qFinancialStatements, bool qYahoo, Tuple<DateTime, DateTime> DateRange = null,  KNMFin.Yahoo.Quotes.QuoteProperties[] qps = null)
        {
            if ( ticker == null || ticker == string.Empty ) return;

            if ( qGoogle ){
                GoogleQuery = new GoogleQuery( ticker, qFinancialStatements );
            }

            if ( qYahoo ){
                YahooQuery = new YahooCompanyQuery(ticker, DateRange, qps);
            }
        }


        public CompanyQuery( List<string> tickers, bool qGoogle, bool  qFinancialStatements,  bool qYahoo, Tuple<DateTime, DateTime> DateRange = null, Dictionary<string, string> QuotePropertyPairs = null )
        {
           
        }


        public GoogleQuery GoogleQuery { get; set; }
        public YahooCompanyQuery YahooQuery { get; set; }
    }

    public class GoogleQuery
    {
        public GoogleQuery( string ticker, bool qFinancialStatements )
        {
            if(ticker == null || ticker == string.Empty) return;

            CompanyInfo = new KNMFin.Google.CompanyInfo( ticker, qFinancialStatements );
        }

        public KNMFin.Google.CompanyInfo CompanyInfo { get; set; }
    }

    public class YahooCompanyQuery
    {
        public YahooCompanyQuery( string ticker, Tuple<DateTime, DateTime> DateRange, KNMFin.Yahoo.Quotes.QuoteProperties [] qps )
        {
            if ( ticker == null || ticker == string.Empty ) return;

            if ( DateRange != null )
            {
                DateTime beg = DateRange.Item1;
                DateTime end = DateRange.Item2;
                HistoricalPrices = KNMFin.Yahoo.HistoricalQuotes.Quote.QueryStockPriceInformation( ticker, beg, end, KNMFin.Yahoo.HistoricalQuotes.Frequency.Daily );
            }
            if ( qps != null && qps.Length > 0 )
            {
                CompanyQuotes = KNMFin.Yahoo.CompanyQuote.Quote.GetCompanyQuotes( new List<string> { ticker }, qps );
;            }
        }

        public KNMFin.Yahoo.HistoricalQuotes.StockPriceResult HistoricalPrices { get; set; }
        public Dictionary<string, Dictionary<string, string>> CompanyQuotes { get; set; }
    }
            
}



