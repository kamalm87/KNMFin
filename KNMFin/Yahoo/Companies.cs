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

            // TODO: Large number of companies presented issue -- new special cases for presence of commas in response csv
            public static Dictionary<string, Dictionary<string, string>> GetCompanyQuotes(List<string> CompanyTickers, params Quotes.QuoteProperties[] QuoteProperties )
            {
                // Sanitize the CompanyTicker list of null/empty strings and duplicate tickers
                CompanyTickers = CompanyTickers.Where( i => i != string.Empty && i != null ).Distinct( ).ToList<string>( );
                
                Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>( );
                StringBuilder sb = new StringBuilder( baseURL );

                // * SPECIAL CASE IF LOGIC * 
                // Public Yahoo URL requests are limited to 200 tickers, so if the amount of tickers exceeds that amount
                // The CompanyTicker's list will be iteratively 'spliced', with the spliced tickers being called recursively
                // and added to the base function's results.
                if ( CompanyTickers.Count > 200 )
                {
                    int numberOfSplices = (int)Math.Floor(CompanyTickers.Count / 200.0);
                    for ( int i = 0; i < numberOfSplices; i++ )
                    {
                        var tempList = CompanyTickers.Skip( ( i + 1 ) * 200 ).Take( 200 ).ToList<string>( );
                        var recursiveResults = GetCompanyQuotes( tempList, QuoteProperties );
                        foreach ( KeyValuePair<string, Dictionary<string, string>> vals in recursiveResults )
                            result.Add( vals.Key, vals.Value );
                    }
                    CompanyTickers = CompanyTickers.Take( 200 ).ToList<string>();
                }

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

                Dictionary<String, MarketData> res = new Dictionary<string, MarketData>( );

                foreach ( KeyValuePair<string, Dictionary<string, string>> kvp in result )
                {
                    res.Add( kvp.Key, new MarketData( kvp.Value ) );
                }

                
                return result;
            }





            public static Dictionary<string, MarketData> GetCompanyMarketQuotations( List<string> CompanyTickers, params Quotes.QuoteProperties [] QuoteProperties )
            {
                var res = new Dictionary<string, MarketData>( );
                CompanyTickers = CompanyTickers.Where( i => i != string.Empty && i != null ).Distinct( ).ToList<string>( );

                Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>( );
                StringBuilder sb = new StringBuilder( baseURL );


                if ( CompanyTickers.Count > 200 )
                {
                    int numberOfSplices = (int)Math.Floor( CompanyTickers.Count / 200.0 );
                    for ( int i = 0; i < numberOfSplices; i++ )
                    {
                        var tempList = CompanyTickers.Skip( ( i + 1 ) * 200 ).Take( 200 ).ToList<string>( );
                        var recursiveResults = GetCompanyMarketQuotations( tempList, QuoteProperties );
                        foreach ( KeyValuePair<string,MarketData> vals in recursiveResults )
                            res.Add( vals.Key, vals.Value );
                    }
                    CompanyTickers = CompanyTickers.Take( 200 ).ToList<string>( );
                }



                // This block builds the request URL
                foreach ( string companyTicker in CompanyTickers )
                {

                    if ( companyTicker == CompanyTickers [ 0 ] )
                        sb.Append( companyTicker );
                    else
                        sb.Append( "," + companyTicker );
                }
                sb.Append( "&f=" );


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
                if ( itemsToRemove.Count > 0 )
                {
                    foreach ( var qp in itemsToRemove )
                        sanitizedQP.Remove( qp );
                }
                string baseQuery = null;

                if ( specialCases.Count != 0 )
                    baseQuery = sb.ToString( );

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
                    return null;
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

                if ( baseQuery != null )
                {
                    foreach ( var qp in specialCases )
                    {
                        var query = SpecialCaseQuoteQuery( baseQuery, CompanyTickers, qp );
                        if ( query != null )
                        {
                            for ( int i = 0; i < CompanyTickers.Count; i++ )
                                result [ CompanyTickers [ i ] ].Add( query [ CompanyTickers [ i ] ].Keys.FirstOrDefault( ), query [ CompanyTickers [ i ] ].Values.FirstOrDefault( ) );
                        }
                    }
                }


                foreach ( KeyValuePair<string, Dictionary<string, string>> kvp in result ){
                    res.Add( kvp.Key, new MarketData( kvp.Value ) );
                }
                return res;
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

        public class MarketQuotation
        {
            public MarketQuotation( Dictionary<string, string> ParsedQuotes )
            {
                Data = new Dictionary<MarketQuotation, MarketData>( );
                foreach ( KeyValuePair<string, string> values in ParsedQuotes )
                {
                    var wut = Quotes.QuoteProperties.SetOfAll.Where( i => i.GetDesription( ) == values.Key ).FirstOrDefault();
                    if ( wut != null )
                    {
                        // Data.Add(wut, values.Value)
                    }
                    var db = 1;
                }
            }

            Dictionary<MarketQuotation, MarketData> Data;
        }



        public class MarketData
        {
            public MarketData(){ Items = new List<MarketDataItem>( ); }
            public MarketData( Dictionary<string, string> Results )
            {
                Items = new List<MarketDataItem>( );

                foreach(KeyValuePair<string, string>  kvp in Results){
                    var mqp = Quotes.QuoteProperties.SetOfAll.Where( i => i.GetDesription( ) == kvp.Key ).FirstOrDefault();
                    Items.Add( new MarketDataItem( kvp.Value, mqp ) );
                }
            }
            public List<MarketDataItem> Items;

            public static void ParseNumber() { }
            public static void ParsePercentage() { }
            public static void ParseTruncatedCurrencyAmount( KeyValuePair<string, string> Item ) { }
        }

        public struct MarketDataItem{

            static decimal billion = 1000000000;
            static decimal million = 1000000;

            public MarketDataItem( string input, Quotes.QuoteProperties Quote )
            {
                _str = null;
                _data = null;
                this.Property = Quote;
                this.Type = Quote.Type;
                try
                {
                    if ( Property.Type == Quotes.ResultType.Date ) ParseDate( input );
                    if ( Property.Type == Quotes.ResultType.Number ) ParseNumber( input );
                    if ( Property.Type == Quotes.ResultType.Pair ) ParsePair( input );
                    if ( Property.Type == Quotes.ResultType.Percentage ) ParsePercentage( input );
                    if ( Property.Type == Quotes.ResultType.Range ) ParseRange( input );
                    if ( Property.Type == Quotes.ResultType.RangePercentage ) ParsePercentageRange( input );
                    if ( Property.Type == Quotes.ResultType.Text ) ParseText( input );
                    if ( Property.Type == Quotes.ResultType.TruncuatedCurrency ) ParseTruncateCurrency( input );
                }
                catch(Exception ex){}
                
            }

            Quotes.QuoteProperties Property;
            Quotes.ResultType Type;

            public string StringData()
            {
                return _str;
            }

            public string GetDescription()
            {
                return Property.GetDesription( );
            }

            public object ActualData(){
                return _data;
            }

            private object _data;
            private string _str;

            private void ParsePair( string input ) {
                input = input.Replace( "\"", "" ).Replace( "\\", "" ).Replace( "%", "" );
                int dashIndex = input.IndexOf( "-" );
                string s1 = input.Substring( 0, dashIndex ), s2 = input.Substring( dashIndex + 1 );
                if ( s1.Length > 3 && !s1.Contains( "N/A" ) )
                {
                    int spaceIndex = s1.IndexOf( " " );
                    string sub1 = s1.Substring( 0, spaceIndex ).Replace(" ", ""), sub2 = s1.Substring( spaceIndex + 1 ).Replace(" ", "");
                    int month = MonthMap [ sub1 ], day = Int16.Parse( sub2 ), year = DateTime.Now.Year;
                    DateTime dt = new DateTime( year, month, day );
                    decimal? i2 = Decimal.Parse(s2.Replace( "<b>", "" ).Replace( "</b>" , "").Replace(" ", ""));
                    _data = new Tuple<DateTime?, decimal?>( dt, i2 );
                    _str = dt.ToShortDateString( ) + ": " + i2.ToString( );
                    var xgonGive = 1;
                    return;
                }
                else
                {
                    decimal? i2 = Decimal.Parse( s2.Replace( "<b>", "" ).Replace( "</b>", "" ).Replace( " ", "" ) );
                    _data = new Tuple<DateTime?, decimal?>( null, i2 );
                    _str = "N/A"+ ": " + i2.ToString( );
                }
                
                return;
            }

            private void ParseDate( string input ) {
                
                int digits = DigitsInText( input );
                input = input.Replace( "\\", "" ).Replace( "\"", "" );

                if ( digits  == 0 ){
                    _data = null;
                    _str = "-";
                    return;
                }
                else if ( digits >= 6 )
                {
                    int firstSlash = input.IndexOf( "/" ), secondSlash = input.IndexOf( "/", firstSlash + 1 );
                    int month = Int16.Parse( input.Substring( 0, firstSlash ) ), day = Int16.Parse( input.Substring( firstSlash + 1, secondSlash - ( firstSlash + 1) ) ), year = Int16.Parse( input.Substring( secondSlash + 1 ));

                    _data = new DateTime( year, month, day );
                    _str = ( (DateTime)_data ).ToShortDateString( );
                    return;
                }
                else if ( DashesInText(input) == 2 )
                {
                    int dashOne = input.IndexOf( "-" ), dashTwo = input.IndexOf( "-", dashOne + 1 );
                    int day = Int16.Parse( input.Substring( 0, dashOne ) ), month = MonthMap [ input.Substring( dashOne + 1, 3 ) ], year = Int16.Parse( input.Substring( dashTwo ) ) + 2000;
                    _data = new DateTime( year, month, day );
                    _str = _data.ToString( );
                }
                else
                {
                    if ( input.Contains( "pm" ) || input.Contains( "am" ) )
                    {
                        int endIndex = input.Contains( "pm" ) ? input.IndexOf( "pm" ) : input.IndexOf( "am" );
                        string tempSubstring = input.Substring( 0, endIndex );
                        int colonIndex = tempSubstring.IndexOf( ":" );
                        int hour = Int32.Parse( tempSubstring.Substring( 0, colonIndex ).Replace( "-", "" ) ), minutes = Int32.Parse( tempSubstring.Substring( colonIndex + 1 ).Replace( "-", "" ) );
                        // Add time of day
                        DateTime dt = new DateTime( DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minutes, 0 );
                        _data = dt;
                        _str = dt.Hour.ToString( ) + ":" + (dt.Minute.ToString( ).Length == 2 ? dt.Minute.ToString( ) : "0" + dt.Minute.ToString( ));

                        return;
                     }
                    else
                    {
                        int spaceIndex = input.IndexOf( " " );
                        int month = MonthMap [ input.Substring( 0, spaceIndex ) ], day = Int16.Parse( input.Substring( spaceIndex + 1 ) );
                        _data = new DateTime( DateTime.Now.Year, month, day );
                        _str = ( (DateTime)_data ).ToShortDateString( );
                        return;
                    }
                }
            }

            private static Dictionary<string, int> MonthMap = new Dictionary<string, int>
            {
                    {"Jan", 1}, {"Feb", 2}, {"Mar", 3}, {"Apr", 4},
                    {"May", 4}, {"Jun", 6}, {"Jul", 7}, {"Aug", 8},
                    {"Sep", 9}, {"Oct", 10}, {"Nov", 11}, {"Dec", 12}
            };

            private void ParseRange( string input )
            {
                if ( DigitsInText( input ) == 0 )
                {
                    _data = null;
                    _str = "---";
                    return;
                }

                input = input.Replace( "\"", "" ).Replace( "\\", "" ).Replace("%", "");
                int dashes = DashesInText( input );
                if ( dashes >= 3 )
                {
                    _data = null;
                    _str = "---";
                    return;
                }

                if ( dashes == 2 )
                {
                    int indexOfDash = input.IndexOf( "-", 1 );
                    if ( input.Contains( "+" ) )
                    {
                        int indexOfSign = input.IndexOf( "+" );
                        _data = Decimal.Parse( input.Substring( indexOfSign + 1 ) ) / 100;
                        _str = ( ( (Decimal)_data ) * 100 ).ToString( );
                        return;
                    }
                    
                    _data =  Decimal.Parse(input.Substring( indexOfDash + 1 ))/100;
                    _str =  (( (Decimal)_data ) * 100).ToString();
                    return;
                }

                if ( dashes == 1 )
                {
                    int indexOfDash = input.IndexOf( "-" );
                    string s1 = input.Substring( 0, indexOfDash ), s2 = input.Substring(indexOfDash + 1);
                    _data = new Tuple<Decimal?, Decimal?>( s1 != string.Empty && s1 != " " ? (Decimal?)Decimal.Parse( s1 ) : null, s2 != string.Empty && s2 != " " ? (Decimal?)Decimal.Parse( s2 ) : null );
                    _str = ( (Tuple<Decimal?, Decimal?>)_data ).Item1.ToString( ) + " - " + ( (Tuple<Decimal?, Decimal?>)_data ).Item2.ToString( );
                    return;
                }
                return;
            }

            private void ParseNumber( string input ) {

                if ( DigitsInText( input ) == 0 )
                {
                    _data = null;
                    _str = "-";
                    return;
                }
                else
                {
                    _data = Decimal.Parse( input );
                    _str = _data.ToString( );
                }

                return;
            }
            
            private void ParseTruncateCurrency( string input ) {
                if ( DigitsInText( input ) == 0 || input == "0")
                {
                    _data = null;
                    _str = "-";
                    return;
                } else {
                    _data = input.Contains( "B" ) ? Decimal.Parse( input.Substring( 0, input.Length - 1 ) ) * billion : Decimal.Parse( input.Substring(0, input.Length - 1 )) * million;
                    _str = _data.ToString( );
                }
                return;
            }
   
            private void ParsePercentage( string input ) {
                
                if ( DigitsInText( input ) == 0 )
                {
                    _data = null;
                    _str = "-";
                    return;
                }
                else
                {
                    input = input.Replace( "\"", "" ).Replace( "\\", "" ).Replace("%", "");
                        
                    if ( input.Contains( "-" ) )
                    {
                        int indexOf = input.IndexOf( "-" );
                        string temp = input.Substring( indexOf + 1 );
                        _data = -1 *  Decimal.Parse( temp )/100;
                        _str = ( (Decimal)_data * 100 ).ToString( );
                    }
                    else if ( input.Contains( "+" ) )
                    {
                        int indexOf = input.IndexOf( "-" );
                        string temp = input.Substring( indexOf + 1 );
                        _data =  Decimal.Parse( temp )/100;
                        _str = ( (Decimal)_data * 100 ).ToString( );
                    } 

                }
                return;

            }
            
            private void ParsePercentageRange(string input){
                input = input.Replace( "\"", "" ).Replace( "\\", "" ).Replace( "%", "" ).Replace(" ", "");
                int dashIndex = input.IndexOf( "-" , 1);
                string s1 = input.Substring( 0, dashIndex ), s2 = input.Substring( dashIndex + 1 );

                bool negative = false;
                if ( s1 [ 0 ] == '-' ) negative = true;


                decimal? i1 = Char.IsDigit( s1 [ 1 ] ) ? (decimal?)Decimal.Parse( s1.Substring( 1 ) ) : null;
                if ( negative ) i1 = i1 * -1;
                if ( s2 [ 0 ] == '-' ) negative = true;

                decimal? i2 = Char.IsDigit( s2 [ 1 ] ) ? (decimal?)Decimal.Parse( s2.Substring( 1 ) ) : null;
                if ( negative ) i2 = i2 * -1;
                _data = new Tuple<Decimal?, Decimal?>( i1, i2 );
                _str = "";

                return;
            }
            
            private void ParseText( string input )
            {
                _data = input.Replace( "\"", "" ).Replace( "\\", "" );
                _str = _data.ToString();
                return;
            }

            private int DigitsInText( string input )
            {
                int count = 0;
                foreach ( char c in input ){
                    if ( Char.IsDigit( c ) ) count++;
                }
                return count;
            }
            private int DashesInText( string input )
            {
                int count = 0;
                foreach ( char c in input )
                {
                    if ( c == '-' ) count++;
                }
                return count;
            }

        }
    }
}
