using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using KNMFin.Yahoo.Industries;
using KNMFin.Yahoo.Quotes;
using KNMFin.Yahoo.Sectors;
using KNMFin.Yahoo.HistoricalQuotes;
using KNMFin.Yahoo.CompanyQuote;



using KNMFinExcel.Yahoo;


using KNMFin.Google;

namespace DebugTest
{
    static class TestYahoo
    {

        public static List<string> CreateTickerListFromCSV( string fileName )
        {
            List<string> list = new List<string>( );
            using ( System.IO.StreamReader sr = new System.IO.StreamReader( fileName ) )
            {
                string [] separator = new string [] { "\r\n" };

                String [] lines = sr.ReadToEnd( ).Split( separator, StringSplitOptions.None );
                return lines.ToList<string>( );
            }
        }



        public static string [] GetDJIATickers()
        {
            return new string []{ "MMM", "AXP", "T", "BA", "CAT", "CVX", "CSCO", "KO", "DD", "XOM","GE", "GS", "HD", "INTC", "IBM", "JBJ", "JPM", "MCD", "MRK",
                                   "MSFT", "NKE", "PFE",  "PG", "TRV", "UNH", "UTX", "VZ", "V", "WMT", "DIS" 
                                };
        }

        public static Dictionary<string, Dictionary<string, Dictionary<string, double?>> > testIndustry()
        {
            var industriesToQuery = new Industry[]{Industry.Accident_and_Health_Insurance_, Industry.Basic_Materials_Wholesale_, Industry.Diversified_Investments_, Industry.Toy_and_Hobby_Stores_};
            return KNMFin.Yahoo.Industries.Quote.IndustryQuery( industriesToQuery );
        }

        public static List<KNMFin.Yahoo.HistoricalQuotes.StockPriceResult> testHistoricalPrices()
        {
            var tickersToQuery = new string [] { "aapl", "bac", "gs", "mcd", "sbux", "mmm", "t", "v", "flws" };
            return KNMFin.Yahoo.HistoricalQuotes.Quote.QueryStockPriceInformation( tickersToQuery, new DateTime( 1900, 1, 1 ), DateTime.Now, Frequency.Daily );
        }

        public static Dictionary<string, Dictionary<string, string>> testCompanies()
        {
            var tickersToQuery = new string [] { "aapl", "bac", "gs", "mcd", "sbux", "mmm", "t", "v", "flws" };
            
            // HARDCODED FILE PATH
            return KNMFin.Yahoo.CompanyQuote.Quote.GetCompanyQuotes( TestYahoo.CreateTickerListFromCSV( @"C:\Users\KNM\Documents\GitHub\KNMFin\DebugTest\DATA\sp500tickers.csv" ), QuoteProperties.SetOfAll.ToArray<QuoteProperties>( ) );
        }

        public static Dictionary<string, Dictionary<string, double>> testSectors()
        {
            return KNMFin.Yahoo.Sectors.Quote.SectorQuery( );
        }
    }

    static class TestGoogle
    {
        public static List<KNMFin.Google.CompanyInfo> testCompanyInfo()
        {
            var tickersToQuery = new string [] { "aapl", "bac", "gs", "mcd", "sbux", "mmm", "t", "v", "flws" };
            var results = new List<KNMFin.Google.CompanyInfo>();
            foreach ( string ticker in tickersToQuery )
                results.Add( new CompanyInfo( ticker, true ) );

            return results;
        }
    }




    // Intention: to ad-hoc test procedures -- nothing permanent is expected to be contained here

    // Currently: 
    //
    // - set the breakpoint to the final bracket in the main function and each var should contain relevant data
    //   , provided that there are no internet connectivity issues

    // TODO: Fix below note!
    // Note: Ctrl+F: HARDCODED FILE PATH to find the hardcoded file paths. Should change them to get working examples
    class Program
    {
        static void Main( string [] args )
        {

            var iamagod = QuoteProperties.SetOfAll.ToArray<QuoteProperties>( );
            

            var wut = KNMFin.Yahoo.CompanyQuote.Quote.GetCompanyMarketQuotations( TestYahoo.CreateTickerListFromCSV( @"C:\Users\KNM\Documents\GitHub\KNMFin\DebugTest\DATA\sp500tickers.csv" ), QuoteProperties.SetOfAll.ToArray<QuoteProperties>( ) );
            
            KNMFinExcel.Yahoo.ExcelYahoo.SaveToExcel( @"C:\users\knm\desktop\wut.xlsx", wut );
            
            Console.WriteLine( "Beginning: Yahoo Industry Test" );
            var yahooIndustryQuery = TestYahoo.testIndustry( );
            Console.WriteLine( "Beginning: Yahoo Historical Price Test" );
            var yahooHistoricalPriceQuery = TestYahoo.testHistoricalPrices( );
            var list = new List<KNMFin.Yahoo.HistoricalQuotes.StockPriceResult>();
            foreach(var item in yahooHistoricalPriceQuery)
                list.Add(item);

            // HARD-CODED DIRECTORY -- CHANGE THIS IF TESTING SOMEWHERE ELSE
            // ExcelYahoo.SaveToExcel( @"C:\users\knm\desktop\bigsean", list );            

            Console.WriteLine( "Beginning: Yahoo Company Test" );
       //     var yahooCompaniesQuery = TestYahoo.testCompanies( );
            // HARDCODED FILE PATH
         //   KNMFinExcel.Yahoo.ExcelYahoo.SaveMarketQuotes( @"C:\users\knm\desktop\marketquotes1.xlsx", yahooCompaniesQuery, true ); 

            KNMFinExcel.Yahoo.ExcelYahoo.SaveMarketQuotes( @"C:\users\knm\desktop\restricted_quer.xlsx", KNMFin.Yahoo.CompanyQuote.Quote.GetCompanyQuotes( TestYahoo.CreateTickerListFromCSV( @"C:\Users\KNM\Documents\GitHub\KNMFin\DebugTest\DATA\sp500tickers.csv" ), QuoteProperties.SetOfAll.ToArray<QuoteProperties>()), true );
            KNMFinExcel.Yahoo.ExcelYahoo.SaveMarketQuotes( @"C:\users\knm\desktop\restricted_query1.xlsx", KNMFin.Yahoo.CompanyQuote.Quote.GetCompanyQuotes( TestYahoo.CreateTickerListFromCSV( @"C:\Users\KNM\Documents\GitHub\KNMFin\DebugTest\DATA\sp500tickers.csv" ),
                new QuoteProperties [] { QuoteProperties.Name, QuoteProperties.Revenue, QuoteProperties.MarketCapitalization, QuoteProperties.EBITDA, QuoteProperties.BookValuePerShare, QuoteProperties.PriceBook } ), true );
            // TODO: Large number of companies presented issue -- new special cases for presence of commas in response csv
           // var wut = yahooCompaniesQuery;
            int DEBUG_POINT = 1;
            Console.WriteLine( "Beginning: Yahoo Sector Test" );
            var yahooSectorsQuery = TestYahoo.testSectors( );

            Console.WriteLine( "Beginning: Google Test" );
            var googleCompanyInfoQuery = TestGoogle.testCompanyInfo( );
            DEBUG_POINT = 2;
        
        }
    }
}
        