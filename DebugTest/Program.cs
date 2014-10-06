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

    class Program
    {
        static string AppDataFolder()
        {
            string dataPath = Environment.CurrentDirectory;
            int indexOfBaseDirectory = dataPath.IndexOf( "\\bin" );
            string baseDir = dataPath.Substring( 0, indexOfBaseDirectory );
            string dataFilePath = baseDir + "\\DATA\\";
            return dataFilePath;
        }

        static string BaseOutputDir(){
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
        }

        static string CreateNamedOutputFile( string testName, string extensionName )
        {
            return testName + "__" + DateTime.Now.Hour.ToString( ) + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Millisecond.ToString( ) + extensionName;
        }

        static string AppDataDir = AppDataFolder();
        static string OutputDir = BaseOutputDir();



        static List<KNMFin.Yahoo.HistoricalQuotes.StockPriceResult> StockReturns( List<String> Tickers, DateTime beg, DateTime end, Frequency freq = Frequency.Daily )
        {
            var yahooHistoricalPriceQuery = KNMFin.Yahoo.HistoricalQuotes.Quote.QueryStockPriceInformation( Tickers, beg, end, freq);
            var list = new List<KNMFin.Yahoo.HistoricalQuotes.StockPriceResult>( );
            foreach ( var item in yahooHistoricalPriceQuery )
                list.Add( item );
            return list;
        }

        // - Note: The ExcelYahoo functions are expected to save the files to the Desktop
        static void Main( string [] args )
        {
            // Creates lists of tickers, represented by stocks representing the sp500 and dow jones industrial (as of 10/6/14)
            // (Note: there are some extra tickers for the sp500 list)
            var sp500TickerList = TestYahoo.CreateTickerListFromCSV( AppDataDir + "sp500tickers.csv" );
            var djiaTickerList = TestYahoo.GetDJIATickers( ).ToList<string>( );

            // Queries Sector/Industries
            var yahooIndustryQuery = TestYahoo.testIndustry( );
            var yahooSectorsQuery = TestYahoo.testSectors( );

            // Queries all the possibile market quotations (88: Including Market Cap, Revenue, etc.) for every company in the SP500 index
            var sp500quotes = KNMFin.Yahoo.CompanyQuote.Quote.GetCompanyMarketQuotations( sp500TickerList, QuoteProperties.SetOfAll.ToArray<QuoteProperties>( ) );
            KNMFinExcel.Yahoo.ExcelYahoo.SaveToExcel( OutputDir + CreateNamedOutputFile( "quotations", ".xlsx" ), sp500quotes );

            // Queries the daily historical prices of every company in SP500 index from the first available return in 2014 to the current date
            // - Each company's returns will be in a worksheet with company's ticker name as the worksheet's name
            // - Will take some time. Will generate a 3MB spreadsheet containg 500+ worksheets
            var sp500_2014Returns = StockReturns( sp500TickerList, new DateTime( 2014, 1, 1 ), DateTime.Now );
            ExcelYahoo.SaveToExcel( OutputDir + CreateNamedOutputFile( "SP500returns_2014", ".xlsx" ), sp500_2014Returns );

            // Queries the daily historical prices of every company in the Dow Jones Industrial Average from the first available date to the current date
            // - Each company's returns will be in a worksheet with company's ticker name as the worksheet's name
            // - Will take a significant amount of time. Will generate ~10MB spreadsheet.
            var djia_AllReturns = StockReturns( djiaTickerList, new DateTime( 1900, 1, 1 ), DateTime.Now );
            ExcelYahoo.SaveToExcel( OutputDir + CreateNamedOutputFile( "DJIAreturns_2014", ".xlsx" ), djia_AllReturns);

            KNMFinExcel.Yahoo.ExcelYahoo.SaveMarketQuotes( OutputDir + CreateNamedOutputFile("restricted_quotes", ".xlsx"), KNMFin.Yahoo.CompanyQuote.Quote.GetCompanyQuotes( sp500TickerList,
                new QuoteProperties [] { QuoteProperties.Name, QuoteProperties.Revenue, QuoteProperties.MarketCapitalization, QuoteProperties.EBITDA, QuoteProperties.BookValuePerShare, QuoteProperties.PriceBook } ), true );
            // End of all Yahoo queries
            int DEBUG_POINT = 1;

            var googleCompanyInfoQuery = TestGoogle.testCompanyInfo( );
            // End of all Google queries
            DEBUG_POINT = 2;
        
        }
    }
}
        