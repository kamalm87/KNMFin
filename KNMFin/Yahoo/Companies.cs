using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNMFin.Yahoo
{
    namespace CompanyQuote
    {
        public static class Quote
        {

            // quote properties must explicitly be assigned into this array
            // The tickers must be valid for a successful response, or else the function will return null

            /// <summary>
            /// Query Quote Properties from a list of company tickers.  
            /// </summary>
            /// <param name="CompanyTickers"></param>
            /// <param name="QuoteProperties">Desired</param>
            /// <returns>A dictionary of company ticker names with an associated dictionary of Quote Property Names and associated values</returns>
            public static Dictionary<string, Dictionary<string, string>> GetCompanyQuotes(List<string> CompanyTickers, params Quotes.QuoteProperties[] QuoteProperties )
            {
                Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>( );
                StringBuilder sb = new StringBuilder( baseURL );

                
                // This block builds the request URL
                foreach(string companyTicker in CompanyTickers ){
                
                    if ( companyTicker == CompanyTickers [ 0 ] )
                        sb.Append( companyTicker );
                    else
                        sb.Append( "," + companyTicker );
                }
                sb.Append("&f=");


                var sanitizedQP = DistinctQuotePropertyList( QuoteProperties );
                List<Quotes.QuoteProperties> specialCases = new List<Quotes.QuoteProperties>( );
                List<Quotes.QuoteProperties> itemsToRemove = new List<Quotes.QuoteProperties>( );

                foreach ( Quotes.QuoteProperties qp in sanitizedQP )
                {
                    if ( Quotes.QuoteProperties._SpecialCases.Contains( qp ) )
                    {
                        specialCases.Add( qp );
                        itemsToRemove.Add( qp );
                    }
                }
                if(itemsToRemove.Count > 0)
                {
                    foreach ( var qp in itemsToRemove )
                        sanitizedQP.Remove( qp );
                }
                string baseQuery = null;

                if ( specialCases.Count != 0 )
                    baseQuery = sb.ToString();

                foreach ( Quotes.QuoteProperties qp in sanitizedQP )
                    sb.Append( qp.ToString( ) );
                sb.Append( endURL );

                var qResult = string.Empty;
                
                // Attempt to receive a response string from the request URL
                // If the request receives nothing, then function terminates, returning a null (in the catch block)
                try{

                    qResult = client.DownloadString( sb.ToString( ) );
                }
                catch
                {
                    // Unsucessful response => function terminates with no results => empty container
                    return new Dictionary<string,Dictionary<string,string>>();
                }


                // Successful reponse => function processes response data:

                // Split the response string into separate rows, which should be associated with a respective company ticker
                // Given from the CompanyTickers parameter
                string [] stringSeparator = new string [] { "\r\n" };   
                var resultLines = qResult.Split( stringSeparator, StringSplitOptions.RemoveEmptyEntries );
                
                // Parse each row, associating the respective company ticker with a Dictionary of Quote Property Name -> Value pairs
                int iteration = 0, innerIteration = 0;


                foreach ( string line in resultLines )
                {
                    Dictionary<string, string> subResults = new Dictionary<string, string>( );
                    var rows = line.Split( ',' );
                    if ( rows.Length == sanitizedQP.Count )
                    {
                        foreach ( string row in rows )
                        {
                            if ( !subResults.ContainsKey( sanitizedQP [ innerIteration ].GetDesription( ) ) )
                                subResults.Add( sanitizedQP [ innerIteration++ ].GetDesription( ), row );
                        }


                        result.Add( CompanyTickers [ iteration++ ], subResults );
                        innerIteration = 0;
                    }
                    else
                    {
                        for ( int i = 1; i < rows.Length; i++ )
                        {

                            if ( innerIteration > sanitizedQP.Count - 1 )
                                break;

                            if ( i == 1 )
                            {
                                subResults.Add( sanitizedQP [ innerIteration++ ].GetDesription( ), rows [ 0 ] + rows [ 1 ] );
                            }
                            else
                            {
                                    subResults.Add( sanitizedQP [ innerIteration++ ].GetDesription( ), rows [ i ] );
                            }
                        }

                        result.Add( CompanyTickers [ iteration++ ], subResults );
                        innerIteration = 0;
                    }
                }

                // Perform an individual request for each special case
                // If the query is successful, adds the first and only key, value pair into the inner result dictionary, 
                // for each associated company in the outter dictionary
                if ( baseQuery != null )
                {
                    foreach(var qp in specialCases){
                        var query = SpecialCaseQuoteQuery(baseQuery,CompanyTickers,  qp);
                        if ( query != null )
                        {
                            for ( int i = 0; i < CompanyTickers.Count; i++ )
                                result [ CompanyTickers [ i ] ].Add( query [ CompanyTickers [ i ] ].Keys.FirstOrDefault( ), query [ CompanyTickers [ i ] ].Values.FirstOrDefault( ) ); 
                        }
                    }
                }

                return result;
            }

            // For each company, the outter dictionary key, makes a single item query for the special case quote property
            // The inner dictionary is expected to be the quote property name, value pair
            // If request is unsuccessful, returns null 
            static private Dictionary<string, Dictionary<string, string>> SpecialCaseQuoteQuery( string baseQuery, List<string> tickers, Quotes.QuoteProperties qp )
            {
                baseQuery += qp.ToString( );
                baseQuery +=  endURL;

                var qResult = string.Empty;
                try{ qResult = client.DownloadString( baseQuery ); }
                catch{ return null; }

                string [] stringSeparator = new string [] { "\r\n" };
                var resultLines = qResult.Split( stringSeparator, StringSplitOptions.RemoveEmptyEntries );
                var result = new Dictionary<string, Dictionary<string, string>>( );
                for(int i = 0; i < tickers.Count; i++)                {
                    var valuePair = new Dictionary<string, string>( );
                    valuePair.Add( qp.GetDesription( ), resultLines [ i ] );
                    result.Add( tickers [ i ], valuePair );
                }

                return result;
            }
            // Added to prevent duplicates to prevent key insertion errors/redundancy
            static private List<Quotes.QuoteProperties> DistinctQuotePropertyList( Quotes.QuoteProperties[] qps )
            {
                var lQP = qps.ToList<Quotes.QuoteProperties>( );
                var lQPDistinct = lQP.Distinct( ).ToList<Quotes.QuoteProperties>();
                return lQPDistinct;
            }

            public static Dictionary<string, Dictionary<string, string>> GetCompanyQuotes( List<string> CompanyTickers, Dictionary<string, string> NameValuePairs )
            {
                Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>( );
                StringBuilder sb = new StringBuilder( baseURL );


                // This block builds the request URL
                foreach ( string companyTicker in CompanyTickers )
                {

                    if ( companyTicker == CompanyTickers [ 0 ] )
                        sb.Append( companyTicker );
                    else
                        sb.Append( "," + companyTicker );
                }
                sb.Append( "&f=" );
                foreach ( KeyValuePair<string, string> kvp in NameValuePairs )
                    sb.Append( kvp.Value );
                sb.Append( endURL );

                var qResult = string.Empty;

                // Attempt to receive a response string from the request URL
                // If the request receives nothing, then function terminates, returning a null (in the catch block)
                try
                {

                    qResult = client.DownloadString( sb.ToString( ) );
                }
                catch
                {
                    // Unsucessful response => function terminates with no results => empty container
                    return new Dictionary<string, Dictionary<string, string>>( );
                }


                // Successful reponse => function processes response data:

                // Split the response string into separate rows, which should be associated with a respective company ticker
                // Given from the CompanyTickers parameter
                string [] stringSeparator = new string [] { "\r\n" };
                var resultLines = qResult.Split( stringSeparator, StringSplitOptions.RemoveEmptyEntries );

                // Parse each row, associating the respective company ticker with a Dictionary of Quote Property Name -> Value pairs
                int iteration = 0;
                foreach ( string line in resultLines )
                {
                    Dictionary<string, string> subResults = new Dictionary<string, string>( );
                    var rows = line.Split( ',' );
                    result.Add( CompanyTickers [ iteration++ ], subResults );
                }

                return result;
            }
            // Internal request components
            private static readonly string baseURL = @"http://download.finance.yahoo.com/d/quotes.csv?s=";
            private static readonly string endURL = @"&e=.csv";
            private static System.Net.WebClient client = new System.Net.WebClient( );
        }        
    }
}
