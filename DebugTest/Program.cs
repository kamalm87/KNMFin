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
            return KNMFin.Yahoo.CompanyQuote.Quote.GetCompanyQuotes( tickersToQuery.ToList<string>( ), QuoteProperties.SetOfAll.ToArray<QuoteProperties>( ) );
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
        static void Main( string [] args )
        {
            Console.WriteLine( "Begining: Yahoo Industry Test" );
            var yahooIndustryQuery = TestYahoo.testIndustry( );
            Console.WriteLine( "Begining: Yahoo Hisotrical Price Test" );
            var yahooHistoricalPriceQuery = TestYahoo.testHistoricalPrices( );
            var list = new List<KNMFin.Yahoo.HistoricalQuotes.StockPriceResult>();
            foreach(var item in yahooHistoricalPriceQuery)
                list.Add(item);

            // HARD-CODED DIRECTORY -- CHANGE THIS IF TESTING SOMEWHERE ELSE
            // ExcelYahoo.SaveToExcel( @"C:\users\knm\desktop\bigsean", list );            

            Console.WriteLine( "Begining: Yahoo Company Test" );
            var yahooCompaniesQuery = TestYahoo.testCompanies( );
            var wut = yahooCompaniesQuery;
            int DEBUG_POINT = 1;
            Console.WriteLine( "Begining: Yahoo Sector Test" );
            var yahooSectorsQuery = TestYahoo.testSectors( );

            Console.WriteLine( "Begining: Google Test" );
            var googleCompanyInfoQuery = TestGoogle.testCompanyInfo( );
        
        }
    }
}
        