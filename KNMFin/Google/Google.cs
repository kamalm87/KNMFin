using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace KNMFin.Google
{
        public class CompanyInfo
        {

            private static string baseURL = @"https://www.google.com/finance?q=";
            private System.Net.WebClient client = new System.Net.WebClient( );
            private System.IO.MemoryStream ms = new System.IO.MemoryStream( );
            private HtmlDocument html = new HtmlDocument( );
            private static Dictionary<string, int> _monthMap = new Dictionary<string, int>( );
            private static bool IsInit;
            // Header details
            public String Name { get; set; }
            public String Ticker { get; set; }
            public String Exchange { get; set; }

            // Economic factoids
            public double? RangeLow { get; set; }
            public double? RangeHigh { get; set; }
            public double? FiftyTwoWeekLow { get; set; }
            public double? FiftyTwoWeekHigh { get; set; }
            public double? Close { get; set; }
            public double? Open { get; set; }
            public double? VolumeAverage { get; set; }
            public double? VolumeTotal { get; set; }
            public double? MarketCap { get; set; }
            public double? PriceEarnings { get; set; }
            public double? Dividend { get; set; }
            public double? DividendYield { get; set; }
            public double? EarningsPerShare { get; set; }
            public double? Shares { get; set; }
            public double? Beta { get; set; }
            public double? InstitutionalOwnership { get; set; }

            // Key stats and ratios
            /// <summary>
            /// 
            /// </summary>
            public KeyStatItems [] KeyStats = new KeyStatItems [ 2 ];
            public int MostRecentFiscalYear;
            public Quarter MostRecentFiscalQuarter;

            // Sector, Industry
            public string Sector { get; set; }
            public string Industry { get; set; }

            // Bottom info
            public string Description { get; set; }
            public List<ImportantPerson> OfficersAndDirectors { get; set; }
            // TODO:
            // private List<object> Relatedcompanies ;


            public List<IncomeStatement> IncomeStatements { get; set; }
            public List<BalanceSheet> BalanceSheets { get; set; }
            public List<CashFlowStatement> CashFlowStatements { get; set; }

            public Address Address { get; set; }

            // Misc
            public DateTime QueryTime { get; set; }
            public string EdgarURLLink { get; set; }



            public CompanyInfo( string ticker, bool FetchFinancials = false )
            {
                if ( !IsInit ) InitStaticContainers( );
                using ( var stream = client.OpenRead( baseURL + ticker ) )
                {
                    stream.CopyTo( ms );
                    ms.Position = 0;
                }
                html.Load( ms );

                var nodes = html.DocumentNode;
                var test = nodes.SelectNodes( "//div" ).Where( i => i.Attributes.Where( j => j.Value == "sfe-section" ).Count( ) > 0 );

                if ( test.Count( ) == 0 )
                {
                    var didYouMeanLink = nodes.SelectNodes( "//div//font//a" );
                    if ( didYouMeanLink != null && didYouMeanLink.Count( ) != 0 )
                    {
                        string didYouMeanSuggestion = didYouMeanLink.Where( i => i.Attributes.Where( j => j.Name == "href" ).Count( ) != 0 ).Select( k => k.Attributes [ 0 ].Value ).FirstOrDefault( ).ToString( );
                        var nQ = baseURL.Substring( 0, baseURL.Length - 3 ) + "/" + didYouMeanSuggestion;
                        ms.SetLength( 0 );

                        using ( var stream = client.OpenRead( baseURL.Substring( 0, baseURL.Length - 3 ) + "/" + didYouMeanSuggestion ) )
                        {
                            stream.CopyTo( ms );
                            ms.Position = 0;
                        }
                        html.Load( ms );
                        var newNodes = html.DocumentNode;
                        var newTest = newNodes.SelectNodes( "//div" ).Where( i => i.Attributes.Where( j => j.Value == "sfe-section" ).Count( ) > 0 );
                        if ( newTest.Count( ) != 0 )
                        {
                            ProcessResponseWithResults( newNodes );
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if ( test.Count( ) != 0 )
                {
                    ProcessResponseWithResults( nodes );
                }
                if ( FetchFinancials && Ticker != string.Empty && Ticker != null )
                {
                    ProcessFinancialStatements( nodes );
                }
            }

            private void ProcessFinancialStatements( HtmlNode baseNode )
            {
                var q1 = baseNode.SelectSingleNode( "//div[contains(@class, 'fjfe-nav')]" );
                var q2 = q1.SelectNodes( ".//ul//li//a" ).Where( h => h.InnerText == "Financials" )
                    .Select( i => i.Attributes.Select( j => j.Value.Replace( "%", ":" ).Replace( "&amp;", "&" ) ) ).FirstOrDefault( ).FirstOrDefault( );
                var fs = Google.FSF.CreateFinancialStatements( "http://www.google.com" + q2 );
                IncomeStatements = fs.Item1;
                BalanceSheets = fs.Item2;
                CashFlowStatements = fs.Item3;
            }

            private void ProcessResponseWithResults( HtmlNode nodes )
            {
                var ttt = nodes.SelectNodes( "//div[contains(@id, 'price-panel')]//span[contains(@class, 'pr')]" ).Select( i => i.InnerText.Replace( "\n", "" ) ).FirstOrDefault( );


                SetMetaData( nodes );
                SetFinancialRatios( nodes.SelectNodes( "//table[contains(@class,'snap-data')]" ) );
                SetKeyRatios( nodes.SelectNodes( "//div[contains(@class, 'sfe-section')]" ) );
                SetAddress( nodes );
                SetSectorsAndIndustries( nodes.SelectNodes( "//div[contains(@class, 'sfe-section')]" ) );
                SetRelatedPersons( nodes.SelectNodes( "//div[contains(@class, 'sfe-section')]" ) );
                QueryTime = DateTime.Now;
                ms.SetLength( 0 );
            }

            private void SetMetaData( HtmlNode node )
            {
                var closingPriceResults = node.SelectNodes( "//div[contains(@id, 'price-panel')]//span[contains(@class, 'pr')]" )
                                        .Select( i => i.InnerText.Replace( "\n", "" ) ).FirstOrDefault( );
                Close = closingPriceResults != null
                    ? (double?)Convert.ToDouble( closingPriceResults )
                    : null;
                Name = node.SelectSingleNode( ".//meta[contains(@itemprop, 'name')]" ).Attributes
                        .Where( i => i.Name == "content" ).Select( i => i.Value ).FirstOrDefault( );
                Ticker = node.SelectSingleNode( ".//meta[contains(@itemprop, 'tickerSymbol')]" ).Attributes
                        .Where( i => i.Name == "content" ).Select( i => i.Value ).FirstOrDefault( );
                Exchange = node.SelectSingleNode( ".//meta[contains(@itemprop, 'exchange')]" ).Attributes
                        .Where( i => i.Name == "content" ).Select( i => i.Value ).FirstOrDefault( );
            }

            private void SetFinancialRatios( HtmlNodeCollection nodes )
            {
                if ( nodes.Count( ) == 0 ) return;
                // First Column contains ([] represents index in array):
                // Range[0], 52Week[1], Open[2], Vol/Avg[3], Market Cap[4], P/E[5]
                string [] firstCol = nodes [ 0 ].ChildNodes.Where( i => i.InnerText != "\n" )
                    .Select( ( i ) => i.InnerText ).ToArray<string>( ).Select( i => i.Replace( "\n", " " )
                    .Substring( 1, i.Length - 1 ) ).ToArray<string>( );
                var range = ParseRange( firstCol [ 0 ] );
                RangeLow = range [ 0 ];
                RangeHigh = range [ 1 ];
                var _52Week = Parse52Week( firstCol [ 1 ] );
                FiftyTwoWeekLow = _52Week [ 0 ];
                FiftyTwoWeekHigh = _52Week [ 1 ];
                Open = ParseOpen( firstCol [ 2 ] ) != null ? Convert.ToDouble( ParseOpen( firstCol [ 2 ] ) ) : 0;
                var vol = ParseVolume( firstCol [ 3 ] );
                VolumeAverage = vol [ 0 ];
                VolumeTotal = vol [ 1 ];
                MarketCap = ParseMarketCap( firstCol [ 4 ] );
                PriceEarnings = ParsePriceEarnings( firstCol [ 5 ] );
                // Second Column contains ([] represents index in array):
                // Div/Yield[0], EPS[1], Shares[2], Beta[3], Instituional Ownership[4]
                string [] secondCol = nodes [ 1 ].ChildNodes.Where( i => i.InnerText != "\n" )
                        .Select( i => i.InnerText ).ToArray<string>( )
                        .Select( i => i.Replace( "\n", " " ) ).ToArray<string>( );
                var divs = ParseDividend( secondCol [ 0 ] );
                Dividend = divs != null ? divs [ 0 ] : null;
                DividendYield = divs != null ? divs [ 1 ] : null;
                EarningsPerShare = ParseEarningsPerShare( secondCol [ 1 ] );
                Shares = ParseShares( secondCol [ 2 ] );
                Beta = ParseBeta( secondCol [ 3 ] );
                InstitutionalOwnership = ParseInstituionalOwnership( secondCol [ 4 ] );
            }

            private void SetKeyRatios( HtmlNodeCollection Nodes )
            {
                if ( Nodes.Count == 0 ) return;

                var rawStats = Nodes [ 2 ].Descendants( ).Where( i => i.Name == "tr" ).Select( i => i.InnerText )
                                .ToArray<string>( ).Select( i => i.Replace( "\n", " " ) ).ToArray<string>( );

                if ( rawStats.Count( ) == 0 ) return;

                KeyStatItems quarterKS = new KeyStatItems( ), yearKS = new KeyStatItems( );

                int indexOne = rawStats [ 0 ].IndexOf( ")" );
                string quarterCol = rawStats [ 0 ].Substring( 0, indexOne + 1 ).Replace( " ", "" );
                var qNumChar = quarterCol [ 1 ].ToString( );
                var qNumInt = Int32.Parse( qNumChar );


                MostRecentFiscalYear = Convert.ToInt32( rawStats [ 0 ].Substring( indexOne + 2, rawStats [ 0 ].Length - ( indexOne + 2 ) ) );
                int index1 = rawStats [ 0 ].IndexOf( "(" );
                int index2 = rawStats [ 0 ].IndexOf( "&" );
                int index3 = rawStats [ 0 ].IndexOf( ";" );

                string quarterNumber = qNumChar;
                string quarterMonth = rawStats [ 0 ].Substring( index1 + 1, index2 - ( index1 + 1 ) ).Replace( " ", "" );
                int quarterYear = Convert.ToInt32( rawStats [ 0 ].Substring( index3 + 1, indexOne - ( index3 + 1 ) ) ) + 2000;
                MostRecentFiscalQuarter.Month = _monthMap [ quarterMonth ];
                MostRecentFiscalQuarter.QuarterNumber = Convert.ToInt32( qNumChar );
                MostRecentFiscalQuarter.Year = Convert.ToInt32( quarterYear );

                int whiteS = rawStats [ 1 ].IndexOf( "n " );
                int indexP1 = rawStats [ 1 ].IndexOf( "%" );
                int indexP2 = rawStats [ 1 ].IndexOf( "%", indexP1 + 1 );

                quarterKS.NetProfitMargin = Convert.ToDouble( rawStats [ 1 ].Substring( whiteS + 1, indexP1 - ( whiteS + 1 ) ) );
                yearKS.NetProfitMargin = Convert.ToDouble( rawStats [ 1 ].Substring( indexP1 + 1, indexP2 - ( indexP1 + 1 ) ) );

                // operating margin
                whiteS = rawStats [ 1 ].IndexOf( "n ", whiteS + 1 );
                indexP1 = rawStats [ 1 ].IndexOf( "%", indexP2 + 1 );
                indexP2 = rawStats [ 1 ].IndexOf( "%", indexP1 + 1 );
                quarterKS.OperatingMargin = Convert.ToDouble( rawStats [ 1 ].Substring( whiteS + 1, indexP1 - ( whiteS + 1 ) ) );
                yearKS.OperatingMargin = Convert.ToDouble( rawStats [ 1 ].Substring( indexP1 + 1, indexP2 - ( indexP1 + 1 ) ) );
                // operating margin

                // EBITD margin
                whiteS = rawStats [ 1 ].IndexOf( "- ", whiteS + 1 );
                indexP1 = rawStats [ 1 ].IndexOf( "%", whiteS + 1 );
                yearKS.EBITDmargin = Convert.ToDouble( rawStats [ 1 ].Substring( whiteS + 1, indexP1 - ( whiteS + 1 ) ) );
                // EBITD margin

                // return on average assets
                whiteS = rawStats [ 1 ].IndexOf( "s ", whiteS + 1 );
                indexP1 = rawStats [ 1 ].IndexOf( "%", whiteS + 1 );
                indexP2 = rawStats [ 1 ].IndexOf( "%", indexP1 + 1 );
                var bb = rawStats [ 1 ].Substring( whiteS + 1, indexP1 - ( whiteS + 1 ) );
                var cc = rawStats [ 1 ].Substring( indexP1 + 1, indexP2 - ( indexP1 + 1 ) );



                if ( rawStats [ 1 ].Substring( whiteS + 1, indexP1 - ( whiteS + 1 ) ).Contains( " - " ) )
                {
                    yearKS.ReturnOnAverageEquity = Convert.ToDouble( rawStats [ 1 ].Substring( whiteS + 1, indexP1 - ( whiteS + 1 ) ).Substring( 3 ) );
                }
                if ( rawStats [ 1 ].Substring( indexP1 + 1, indexP2 - ( indexP1 + 1 ) ).Contains( "Return on average equity" ) )
                {
                    int tempIndex = rawStats [ 1 ].Substring( indexP1 + 1, indexP2 - ( indexP1 + 1 ) ).IndexOf( " - " );
                    yearKS.ReturnOnAverageEquity = Convert.ToDouble( rawStats [ 1 ].Substring( indexP1 + 1, indexP2 - ( indexP1 + 1 ) ).Substring( tempIndex + 3 ) );
                }

                else
                {
                    quarterKS.ReturnOnAverageAssets = Convert.ToDouble( rawStats [ 1 ].Substring( whiteS + 1, indexP1 - ( whiteS + 1 ) ).Replace( " ", "" ) );
                    yearKS.ReturnOnAverageAssets = Convert.ToDouble( rawStats [ 1 ].Substring( indexP1 + 1, indexP2 - ( indexP1 + 1 ) ) );

                    whiteS = rawStats [ 1 ].IndexOf( "y ", whiteS + 1 );
                    indexP1 = rawStats [ 1 ].IndexOf( "%", whiteS + 1 );
                    indexP2 = rawStats [ 1 ].IndexOf( "%", indexP1 + 1 );
                    if ( indexP1 != -1 && indexP2 != -1 )
                    {
                        quarterKS.ReturnOnAverageEquity = Convert.ToDouble( rawStats [ 1 ].Substring( whiteS + 1, indexP1 - ( whiteS + 1 ) ) );
                        yearKS.ReturnOnAverageEquity = Convert.ToDouble( rawStats [ 1 ].Substring( indexP1 + 1, indexP2 - ( indexP1 + 1 ) ) );
                    }
                }

                // return on average assets

                // return on average equity

                // return on average equity

                // employees 
                whiteS = rawStats [ 1 ].IndexOf( "s ", whiteS + 1 );
                indexP1 = rawStats [ 1 ].IndexOf( " - ", whiteS + 1 );
                if ( index1 != -1 && whiteS != -1 )
                    quarterKS.Employees = Convert.ToDouble( rawStats [ 1 ].Substring( whiteS + 1, indexP1 - ( whiteS + 1 ) ) );

                whiteS = rawStats [ 1 ].IndexOf( "e", whiteS + 1 );
                string cdpScore = rawStats [ 1 ].Substring( whiteS + 5, rawStats [ 1 ].Length - ( whiteS + 5 ) );
                if ( cdpScore.Length > 3 && cdpScore.Length < 10 )
                {
                    yearKS.CDPscore = new Tuple<int, string>( Int32.Parse( cdpScore.Substring( 0, cdpScore.IndexOf( " " ) ) ),
                                    rawStats [ 1 ].Substring( whiteS + 5, rawStats [ 1 ].Length - ( whiteS + 5 ) ) );
                }

                // CDP score


                KeyStats [ (int)KeyStatPeriod.MostRecentQuarter ] = quarterKS;
                KeyStats [ (int)KeyStatPeriod.MostRecentFiscalYear ] = yearKS;
            }

            private void SetAddress( HtmlNode node )
            {

                var baseQ = node.SelectNodes( "//div" ).Where( i => i.Attributes.Where( j => j.Value == "sfe-section" ).Count( ) > 0 )
                           .Where( k => k.ChildNodes.Count > 7 ).Where( m => m.ChildNodes.Where( n => n.Name == "br" ).Count( ) > 3 ).Select( o => o.ChildNodes.ToArray<HtmlNode>( ) ).FirstOrDefault( );
                if ( baseQ == null ) return;

                string [] vals = baseQ.Where( p => p.Name == "#text" ).Select( q => q.InnerText )
                    .Where( c => c != "\n" ).ToArray<string>( );
                Address a = new Address( );

                // var vals = nodes [3].ChildNodes.Where( i => i.InnerText != String.Empty && i.InnerText != "\n" && i.InnerText != "Map" ).Select( i => i.InnerText ).ToArray<string>( );

                int indexFirst, indexSecond;
                indexFirst = vals [ 0 ].IndexOf( " " );
                a.StreetName = vals [ 0 ];
                indexFirst = vals [ 1 ].IndexOf( "," );
                a.City = vals [ 1 ].Substring( 0, indexFirst ).First( ) + String.Join( "", vals [ 1 ].Substring( 0, indexFirst ).ToLower( ).Skip( 1 ) );
                indexSecond = vals [ 1 ].IndexOf( " ", indexFirst + 2 );
                if ( indexSecond != -1 )
                    a.State = vals [ 1 ].Substring( indexFirst + 2, indexSecond - indexFirst - 2 );

                a.ZipCode = vals [ 1 ].Substring( indexSecond + 1, vals [ 1 ].Length - indexSecond - 1 );
                a.Country = vals [ 2 ].Replace( "\n", "" ).Replace( "-", "" );
                indexFirst = vals [ 3 ].IndexOf( " " );
                a.Phone = vals [ 3 ].Substring( 0, indexFirst );
                if ( vals.Length > 4 )
                {
                    indexFirst = vals [ 4 ].IndexOf( " " );
                    a.Fax = vals [ 4 ].Substring( 0, indexFirst );
                }
                Address = a;
            }

            private void SetSectorsAndIndustries( HtmlNodeCollection nodes )
            {
                if ( nodes.Count( ) < 8 ) return;

                var externalLinks = nodes [ 5 ].ChildNodes.Select( i => new { i.InnerText, i.InnerHtml } );
                var sectorAndIndustry = nodes [ 6 ].ChildNodes.Where( i => i.InnerText != "\n" ).Select( i => i.InnerText ).ToArray<string>( );
                int sectorIndex = sectorAndIndustry [ 0 ].IndexOf( "Sector:" ) + 7;
                int sectorIndexEnd = sectorAndIndustry [ 0 ].IndexOf( " &gt;" );
                if ( sectorIndexEnd == -1 ) return;
                int industryIndex = sectorAndIndustry [ 0 ].IndexOf( "Industry:" ) + 9;
                int endingMark = sectorAndIndustry [ 0 ].IndexOf( "\n\n" ) + 9;
                Sector = sectorAndIndustry [ 0 ].Substring( sectorIndex + 1, sectorIndexEnd - sectorIndex - 1 ).Replace( "&amp;", "&" );
                Industry = sectorAndIndustry [ 0 ].Substring( industryIndex + 1, endingMark - industryIndex - 10 ).Replace( "&amp;", "&" );
                Description = nodes [ 7 ].ChildNodes.Where( i => i.InnerText != "\n" ).Select( i => i.InnerText ).ToArray<string>( ) [ 0 ].Replace( "More from Reuters &raquo;", "" ).Replace( "\n", "" );
            }

            private void SetRelatedPersons( HtmlNodeCollection nodes )
            {
                var baseQ = nodes [ nodes.Count( ) - 1 ].SelectSingleNode( ".//table[contains(@class, 'id-mgmt-table')]" );
                if ( baseQ == null ) return;

                OfficersAndDirectors = new List<ImportantPerson>( );
                var ppl = nodes [ nodes.Count( ) - 1 ].SelectSingleNode( ".//table[contains(@class, 'id-mgmt-table')]" )
                                .SelectNodes( ".//tr//td[contains(@class, 'p ')]" ).Select( i => i.InnerText.Replace( "\n\n", ": " )
                                .Replace( "\n", "" ) ).ToArray<string>( );
                foreach ( string person in ppl )
                {
                    var b = person.Split( ':' );
                    OfficersAndDirectors.Add( new ImportantPerson { Name = b [ 0 ], Role = b [ 1 ].Substring( 1 ) } );
                }
            }

            private static void InitStaticContainers()
            {
                _monthMap.Add( "Jan", 1 ); _monthMap.Add( "Feb", 2 ); _monthMap.Add( "Mar", 3 );
                _monthMap.Add( "Apr", 4 ); _monthMap.Add( "May", 5 ); _monthMap.Add( "Jun", 6 );
                _monthMap.Add( "Jul", 7 ); _monthMap.Add( "Aug", 8 ); _monthMap.Add( "Sep", 9 );
                _monthMap.Add( "Oct", 10 ); _monthMap.Add( "Nov", 11 ); _monthMap.Add( "Dec", 12 );

                IsInit = true;
            }

            // Auxiliary functions for SetFinancial Ratios
            private static bool ContainsDigits( string [] inputs )
            {
                foreach ( char c in inputs [ 0 ] )
                {
                    if ( Char.IsDigit( c ) ) return true;
                }
                return false;
            }

            private static bool ContainsDigits( string input )
            {
                foreach ( char c in input )
                {
                    if ( Char.IsDigit( c ) ) return true;
                }
                return false;
            }

            private static string [] ParseIfDigits( string [] inputs, Func<string [], bool> Predicate, Func<string [], string []> Parse )
            {
                if ( Predicate.Invoke( inputs ) )
                {
                    return Parse.Invoke( inputs );
                }
                else
                {
                    return null;
                }
            }

            private static string [] ReplaceInputDashes( string [] input )
            {
                return new string [] { input [ 0 ].Replace( "-", "" ), input [ 1 ].Replace( "-", "" ) };
            }

            private static Double? [] Parse52Week( string input )
            {
                if ( input.Contains( "&nbsp;&nbsp;" ) ) return new Double? [] { null, null };
                int index1 = input.IndexOf( " ", 5 ), index2 = input.IndexOf( "-" );
                string input1 = input.Substring( index1 + 2, input.Length - index1 - 2 );

                if ( ContainsDigits( new string [] { input1 } ) )
                {
                    string [] input2 = input1.Replace( "-", "" ).Split( ' ' ).Where( i => i != string.Empty ).ToArray<string>( );
                    Double? res1 = Convert.ToDouble( input2 [ 0 ] ), res2 = Convert.ToDouble( input2 [ 1 ] );
                    return new Double? [] { res1, res2 };
                }
                else
                {
                    return new Double? [] { null, null };
                }
            }

            private static Double? [] ParseRange( string input )
            {
                int index1 = input.IndexOf( " " ), index2 = input.IndexOf( " ", index1 + 1 );
                string input1 = input.Substring( index1 + 2, index2 );
                index1 = input.IndexOf( " - ", index2 + 1 );
                index2 = input.IndexOf( " ", index1 + 3 );
                string input2 = input.Substring( index1 + 3, input.Length - index1 - 5 );

                string [] inputs = new string [] { input1, input2 };
                inputs = ParseIfDigits( inputs, ContainsDigits, ReplaceInputDashes );
                Double? res1 = inputs != null ? Convert.ToDouble( inputs [ 0 ] ) : (double?)null,
                        res2 = inputs != null ? Convert.ToDouble( inputs [ 1 ] ) : (double?)null;
                return new Double? [] { res1, res2 };
            }

            private static Double? ParseOpen( string input )
            {
                int index1 = input.IndexOf( " " ), index2 = input.IndexOf( " ", index1 + 1 );
                string input1 = input.Substring( index1 + 1 ).Replace( " ", "" );

                if ( ContainsDigits( input1 ) )
                {
                    return Convert.ToDouble( input1 );
                }
                else
                {
                    return null;
                }
            }

            private static Double? [] ParseVolume( string input )
            {

                string temp = input.Replace( " ", "" );
                int index1 = temp.IndexOf( "." );
                int index2 = temp.IndexOf( "/", index1 );

                if ( index1 != -1 && index2 != -1 )
                {
                    Double? result1 = null, result2 = null;
                    double million = 1000000, billion = 1000000000;

                    string input1 = temp.Substring( index1 + 1, index2 - index1 - 1 );
                    string input2 = input.Substring( index2 + input1.Length ).Replace( " ", "" ).Replace( "/", "" );

                    result1 = input1.Contains( "M" ) ? Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) ) * million
                                                    : input1.Contains( "B" )
                                                        ? Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) ) * billion
                                                        : Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) );
                    if ( ContainsDigits( input2 ) )
                    {
                        result2 = input2.Contains( "M" ) ? Convert.ToDouble( input2.Substring( 0, input2.Length - 1 ) ) * million
                                                        : input2.Contains( "B" )
                                                            ? Convert.ToDouble( input2.Substring( 0, input2.Length - 1 ) ) * billion
                                                            : Convert.ToDouble( input2.Substring( 0, input2.Length - 1 ) );
                    }
                    else
                    {
                        result2 = null;
                    }
                    return new Double? [] { result1, result2 };
                }
                else
                {
                    return new Double? [] { null, null }; ;
                }
            }

            private static Double? ParseMarketCap( string input )
            {
                int index1 = input.IndexOf( " ", 4 );
                string input1 = input.Substring( index1 ).Replace( " ", "" );

                if ( ContainsDigits( input1 ) )
                {
                    double million = 1000000, billion = 1000000000;
                    return input1.Contains( "B" ) ? Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) ) * billion
                        : input1.Contains( "M" ) ? Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) ) * million
                        : Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) );
                }
                return null;
            }

            private static Double? ParsePriceEarnings( string input )
            {
                int index1 = input.IndexOf( " " );
                string input1 = input.Substring( index1 ).Replace( " ", "" );
                if ( ContainsDigits( input1 ) )
                {
                    return Convert.ToDouble( input1 );
                }
                else
                {
                    return null;
                }
            }

            private static Double? [] ParseDividend( string input )
            {
                if ( input.Contains( "&nbsp;&nbsp;" ) ) return null;

                int index1 = input.IndexOf( " ", 2 );
                if ( index1 != -1 )
                {
                    int index2 = input.IndexOf( "/", index1 + 1 );
                    string input1 = input.Substring( index1 + 1, index2 - index1 - 1 ).Replace( " ", "" );
                    string input2 = input.Substring( index2 + 1, input.Length - index2 - 2 ).Replace( " ", "" ).Replace( "/", "" );
                    return new Double? [] { Char.IsDigit( input1 [ 0 ] ) ? Convert.ToDouble( input1 ) : (double?)null,
                                        Char.IsDigit( input2 [ 0 ] ) ? Convert.ToDouble( input2 ) : (double?)null
                                        };
                }
                else
                {
                    return new Double? [] { null, null };
                }
            }

            private static Double? ParseEarningsPerShare( string input )
            {
                int index1 = input.IndexOf( " ", 2 );
                if ( index1 != -1 )
                {
                    string input1 = input.Substring( index1 + 1, input.Length - index1 - 1 ).Replace( "*", "" );
                    return Convert.ToDouble( input1 );
                }
                else
                {
                    return null;
                }
            }

            private static Double? ParseShares( string input )
            {
                int index1 = input.IndexOf( " ", 2 );
                if ( index1 != -1 )
                {
                    string input1 = input.Substring( index1 + 1 ).Replace( " ", "" );
                    double million = 1000000, billion = 1000000000;
                    return input1.Contains( "M" ) ? Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) ) * million
                        : input1.Contains( "B" ) ? Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) ) * billion
                        : Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) );
                }
                else
                {
                    return null;
                }
            }

            private static Double? ParseBeta( string input )
            {
                int index1 = input.IndexOf( " ", 2 );
                if ( index1 != -1 )
                {
                    string input1 = input.Substring( index1 + 1 ).Replace( " ", "" );
                    if ( ContainsDigits( input1 ) )
                    {
                        return Convert.ToDouble( input1 );
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }

            private static Double? ParseInstituionalOwnership( string input )
            {
                int index1 = input.IndexOf( " ", 9 );

                if ( index1 != -1 )
                {
                    string input1 = input.Substring( index1 + 1 ).Replace( " ", "" );
                    if ( ContainsDigits( input1 ) )
                    {
                        return Convert.ToDouble( input1.Substring( 0, input1.Length - 1 ) ) / 100.0;
                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    return null;
                }
            }
        }

        public enum KeyStatPeriod
        {
            MostRecentQuarter = 0,
            MostRecentFiscalYear = 1
        }

        public struct KeyStatItems
        {
            public double NetProfitMargin;
            public double OperatingMargin;
            public double EBITDmargin;
            public double ReturnOnAverageAssets;
            public double ReturnOnAverageEquity;
            public double? Employees;
            public Tuple<int, String> CDPscore;
        }

        public struct Quarter
        {
            public int QuarterNumber, Month, Year;
        }

        public class ImportantPerson
        {

            public string Name { get; set; }
            public string Role { get; set; }
            public Int16 Age { get; set; }


            public Tuple<string, string> BioAndCompensation { get; set; }
            public Tuple<string, string> TradingActivity { get; set; }
        }

        public struct Address
        {
            public string StreetName { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string Country { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
        }

        static class FSF
        {

            public static Tuple<List<IncomeStatement>, List<BalanceSheet>, List<CashFlowStatement>> CreateFinancialStatements( string urlPath )
            {

                List<Tuple<Tuple<string, List<string>>, Dictionary<string, List<string>>>> ParsedData = ParseFinancialData( urlPath );

                List<IncomeStatement> IncomeStatements = new List<IncomeStatement>( );
                List<BalanceSheet> BalanceSheets = new List<BalanceSheet>( );
                List<CashFlowStatement> CashFlowStatements = new List<CashFlowStatement>( );

                foreach ( var info in ParsedData )
                {
                    if ( info.Item2.Count == 49 )
                    {
                        IncomeStatements.AddRange( CreateIncomeStatements( info.Item1.Item2, info.Item2 ) );
                    }
                    if ( info.Item2.Count == 42 )
                    {
                        BalanceSheets.AddRange( CreateBalanceSheets( info.Item1.Item2, info.Item2 ) );
                    }
                    if ( info.Item2.Count == 19 )
                    {
                        CashFlowStatements.AddRange( CreateCashFlowStatements( info.Item1.Item2, info.Item2 ) );
                    }
                }
                return new Tuple<List<IncomeStatement>, List<BalanceSheet>, List<CashFlowStatement>>( IncomeStatements, BalanceSheets, CashFlowStatements );
            }

            static DateTime ParseDateFromColumnHeader( string input )
            {

                int index1, index2;
                index1 = input.IndexOf( "-" );
                index2 = input.IndexOf( "-", index1 + 1 );
                int year = Convert.ToInt16( input.Substring( index1 - 4, 4 ) ),
                    month = Convert.ToInt16( input.Substring( index1 + 1, input.Length - index2 - 1 ) ),
                    day = Convert.ToInt16( input.Substring( input.Length - 2, 2 ) );
                return new DateTime( year, month, day );
            }

            static List<IncomeStatement> CreateIncomeStatements( List<string> ColumnHeaders, Dictionary<string, List<string>> Data )
            {
                if ( Data.Count != 49 || ColumnHeaders.Count != Data.FirstOrDefault( ).Value.Count ) throw new Exception( "The format has changed--inaccurate data" );

                decimal multipler = 1000000;

                Dictionary<int, IncomeStatement> tempMapping = new Dictionary<int, IncomeStatement>( );

                for ( int i = 0; i < ColumnHeaders.Count; i++ )
                {
                    Period p;
                    if ( ColumnHeaders [ i ].Contains( "weeks" ) )
                    {
                        p = ColumnHeaders [ i ].Contains( "13 weeks" ) ? Period.Quarter : Period.Annual;
                    }
                    else if ( ColumnHeaders [ i ].Contains( "months" ) )
                    {
                        p = ColumnHeaders [ i ].Contains( "3 months" ) ? Period.Quarter : Period.Annual;
                    }
                    else
                    {
                        p = ComparePeriodDiff( ColumnHeaders [ 0 ], ColumnHeaders [ 1 ] );
                    }

                    DateTime dt = ParseDateFromColumnHeader( ColumnHeaders [ i ] );
                    tempMapping.Add( i, new IncomeStatement( p, dt ) );
                }


                foreach ( var item in Data.Keys )
                {
                    // 0
                    if ( item == "Revenue" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Revenue = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 1
                    if ( item == "Other Revenue, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Other_Revenue_Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 2
                    if ( item == "Total Revenue" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Revenue = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 3
                    if ( item == "Cost of Revenue, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Cost_of_Revenue_Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 4
                    if ( item == "Gross Profit" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Gross_Profit = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 5
                    if ( item == "Selling/General/Admin. Expenses, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Selling_and_General_and_Admin_Expenses_Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 6
                    if ( item == "Research & Development" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Research_and_Development = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 7
                    if ( item == "Depreciation/Amortization" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Depreciation_and_Amortization = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 8
                    if ( item == "Interest Expense(Income) - Net Operating" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Interest_Expense__Income___less_Net_Operating = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 9
                    if ( item == "Unusual Expense (Income)" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Unusual_Expense___Income__ = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 10
                    if ( item == "Other Operating Expenses, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Revenue = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 11
                    if ( item == "Total Operating Expense" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Operating_Expense = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 12
                    if ( item == "Operating Income" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Operating_Income = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 13
                    if ( item == "Interest Income(Expense), Net Non-Operating" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Interest_Income__Expense___Net_NonOperating = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 14
                    if ( item == "Gain (Loss) on Sale of Assets" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Gain___Loss___on_Sale_of_Assets = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 15
                    if ( item == "Other, Net" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Other_Net = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 16
                    if ( item == "Income Before Tax" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Income_Before_Tax = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 17
                    if ( item == "Income After Tax" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Income_After_Tax = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 18
                    if ( item == "Minority Interest" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Minority_Interest = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 19
                    if ( item == "Equity In Affiliates" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Minority_Interest = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 20
                    if ( item == "Net Income Before Extra. Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Net_Income_Before_Extra_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 21
                    if ( item == "Accounting Change" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Accounting_Change = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 22
                    if ( item == "Discontinued Operations" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Discontinued_Operations = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 23
                    if ( item == "Extraordinary Item" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Extraordinary_Item = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 24
                    if ( item == "Net Income" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Net_Income = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 25
                    if ( item == "Preferred Dividends" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Preferred_Dividends = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 26
                    if ( item == "Income Available to Common Excl. Extra Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Income_Available_to_Common_Excl_Extra_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 27
                    if ( item == "Income Available to Common Incl. Extra Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Income_Available_to_Common_Incl_Extra_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 28
                    if ( item == "Basic Weighted Average Shares" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Basic_Weighted_Average_Shares = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 29
                    if ( item == "Basic EPS Excluding Extraordinary Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Basic_EPS_Excluding_Extraordinary_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 30
                    if ( item == "Basic EPS Including Extraordinary Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Basic_EPS_Including_Extraordinary_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 31
                    if ( item == "Dilution Adjustment" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Dilution_Adjustment = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 32
                    if ( item == "Diluted Weighted Average Shares" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Diluted_Weighted_Average_Shares = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 33
                    if ( item == "Diluted EPS Excluding Extraordinary Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Diluted_EPS_Excluding_Extraordinary_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 34
                    if ( item == "Diluted EPS Including Extraordinary Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Diluted_EPS_Including_Extraordinary_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 35
                    if ( item == "Dividends per Share - Common Stock Primary Issue" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Dividends_per_Share__less__Common_Stock_Primary_Issue = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 36
                    if ( item == "Gross Dividends - Common Stock" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Gross_Dividends__less__Common_Stock = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 37
                    if ( item == "Net Income after Stock Based Comp. Expense" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Net_Income_after_Stock_Based_Comp_Expense = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 38
                    if ( item == "Basic EPS after Stock Based Comp. Expense" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Basic_EPS_after_Stock_Based_Comp_Expense = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 39
                    if ( item == "Diluted EPS after Stock Based Comp. Expense" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Diluted_EPS_after_Stock_Based_Comp_Expense = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 40
                    if ( item == "Depreciation, Supplemental" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Depreciation_Supplemental = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 41
                    if ( item == "Total Special Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Special_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 42
                    if ( item == "Normalized Income Before Taxes" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Normalized_Income_Before_Taxes = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 43
                    if ( item == "Effect of Special Items on Income Taxes" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Effect_of_Special_Items_on_Income_Taxes = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 44
                    if ( item == "Income Taxes Ex. Impact of Special Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Income_Taxes_Ex_Impact_of_Special_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 45
                    if ( item == "Normalized Income After Taxes" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Normalized_Income_After_Taxes = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 46
                    if ( item == "Normalized Income Avail to Common" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Normalized_Income_Avail_to_Common = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 47
                    if ( item == "Basic Normalized EPS" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Basic_Normalized_EPS = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 48
                    if ( item == "Diluted Normalized EPS" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Diluted_Normalized_EPS = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                }

                List<IncomeStatement> IncomeStatements = new List<IncomeStatement>( );
                for ( int i = 0; i < tempMapping.Keys.Count; i++ )
                {
                    IncomeStatements.Add( tempMapping [ i ] );
                }
                return IncomeStatements;
            }

            static Period ComparePeriodDiff( string input1, string input2 )
            {
                var a = input1;
                var b = input2;
                int index1 = a.IndexOf( "of " );
                var year = Convert.ToInt16( a.Substring( index1 + 3, 4 ) );
                var month = Convert.ToInt16( a.Substring( index1 + 8, 2 ) );
                var day = Convert.ToInt16( a.Substring( index1 + 11 ) );
                DateTime dtA = new DateTime( year, month, day );
                year = Convert.ToInt16( b.Substring( index1 + 3, 4 ) );
                month = Convert.ToInt16( b.Substring( index1 + 8, 2 ) );
                day = Convert.ToInt16( b.Substring( index1 + 11 ) );
                DateTime dtB = new DateTime( year, month, day );

                return ( dtA - dtB ).TotalDays < 300 ? Period.Quarter : Period.Annual;
            }

            static List<BalanceSheet> CreateBalanceSheets( List<string> ColumnHeaders, Dictionary<string, List<string>> Data )
            {


                decimal multipler = 1000000;
                Dictionary<int, BalanceSheet> tempMapping = new Dictionary<int, BalanceSheet>( );

                for ( int i = 0; i < ColumnHeaders.Count; i++ )
                {
                    Period p;
                    if ( ColumnHeaders [ i ].Contains( "weeks" ) )
                    {
                        p = ColumnHeaders [ i ].Contains( "13 weeks" ) ? Period.Quarter : Period.Annual;
                    }
                    else if ( ColumnHeaders [ i ].Contains( "months" ) )
                    {
                        p = ColumnHeaders [ i ].Contains( "3 months" ) ? Period.Quarter : Period.Annual;
                    }
                    else
                    {
                        p = ComparePeriodDiff( ColumnHeaders [ 0 ], ColumnHeaders [ 1 ] );
                    }

                    DateTime dt = ParseDateFromColumnHeader( ColumnHeaders [ i ] );
                    tempMapping.Add( i, new BalanceSheet( p, dt ) );
                }

                foreach ( var item in Data.Keys )
                {
                    // 0
                    if ( item == "Cash & Equivalents" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Cash_and_Equivalents = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 1
                    if ( item == "Short Term Investments" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Short_Term_Investments = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 2
                    if ( item == "Cash and Short Term Investments" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Cash_and_Short_Term_Investments = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 3
                    if ( item == "Accounts Receivable - Trade, Net" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Accounts_Receivable__Trade__Net = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 4
                    if ( item == "Receivables - Other" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Receivables__Other = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 5
                    if ( item == "Total Receivables, Net" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Receivables__Net = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 6
                    if ( item == "Total Inventory" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Inventory = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 7
                    if ( item == "Prepaid Expenses" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Prepaid_Expenses = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 8
                    if ( item == "Other Current Assets, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Other_Current_Assets__Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 9
                    if ( item == "Total Current Assets" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Current_Assets = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 10
                    if ( item == "Property/Plant/Equipment, Total - Gross" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Property_and_Plant_and_Equipment__Total__Gross = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 11
                    if ( item == "Accumulated Depreciation, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Accumulated_Depreciation__Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 12
                    if ( item == "Goodwill, Net" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Goodwill__Net = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 13
                    if ( item == "Intangibles, Net" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Intangibles__Net = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 14
                    if ( item == "Long Term Investments" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Long_Term_Investments = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 15
                    if ( item == "Other Long Term Assets, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Other_Long_Term_Assets__Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 16
                    if ( item == "Total Assets" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Assets = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 17
                    if ( item == "Accounts Payable" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Accounts_Payable = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 18
                    if ( item == "Accrued Expenses" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Accrued_Expenses = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 19
                    if ( item == "Notes Payable/Short Term Debt" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Notes_Payable_and_Short_Term_Debt = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 20
                    if ( item == "Current Port. of LT Debt/Capital Leases" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Current_Port_of_LT_Debt_and_Capital_Leases = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 21
                    if ( item == "Other Current liabilities, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Other_Current_liabilities__Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 22
                    if ( item == "Total Current Liabilities" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Current_Liabilities = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 23
                    if ( item == "Long Term Debt" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Long_Term_Debt = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 24
                    if ( item == "Capital Lease Obligations" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Capital_Lease_Obligations = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 25
                    if ( item == "Total Long Term Debt" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Long_Term_Debt = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 26
                    if ( item == "Total Debt" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Debt = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 27
                    if ( item == "Deferred Income Tax" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Deferred_Income_Tax = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 28
                    if ( item == "Minority Interest" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Minority_Interest = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 29
                    if ( item == "Other Liabilities, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Other_Liabilities__Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 30
                    if ( item == "Total Liabilities" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Liabilities = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 31
                    if ( item == "Redeemable Preferred Stock, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Redeemable_Preferred_Stock__Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 32
                    if ( item == "Preferred Stock - Non Redeemable, Net" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Preferred_Stock__Non_Redeemable__Net = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 33
                    if ( item == "Common Stock, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Common_Stock__Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 34
                    if ( item == "Additional Paid-In Capital" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Additional_PaidIn_Capital = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 35
                    if ( item == "Retained Earnings (Accumulated Deficit)" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Retained_Earnings__Accumulated_Deficit_ = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 36
                    if ( item == "Treasury Stock - Common" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Treasury_Stock__Common = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 37
                    if ( item == "Other Equity, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Other_Equity__Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 38
                    if ( item == "Total Equity" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Equity = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 39
                    if ( item == "Total Liabilities & Shareholders' Equity" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Liabilities_and_Shareholders_Equity = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 40
                    if ( item == "Shares Outs - Common Stock Primary Issue" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Shares_Outs__Common_Stock_Primary_Issue = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 41
                    if ( item == "Total Common Shares Outstanding" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Common_Shares_Outstanding = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }

                }


                List<BalanceSheet> BalanceSheets = new List<BalanceSheet>( );
                for ( int i = 0; i < tempMapping.Keys.Count; i++ )
                {
                    BalanceSheets.Add( tempMapping [ i ] );
                }
                return BalanceSheets;
            }

            static List<CashFlowStatement> CreateCashFlowStatements( List<string> ColumnHeaders, Dictionary<string, List<string>> Data )
            {


                decimal multipler = 1000000;

                Dictionary<int, CashFlowStatement> tempMapping = new Dictionary<int, CashFlowStatement>( );

                for ( int i = 0; i < ColumnHeaders.Count; i++ )
                {
                    Period p;
                    if ( ColumnHeaders [ i ].Contains( "weeks" ) )
                    {
                        p = ColumnHeaders [ i ].Contains( "13 weeks" ) ? Period.Quarter : Period.Annual;
                    }
                    else if ( ColumnHeaders [ i ].Contains( "months" ) )
                    {
                        p = ColumnHeaders [ i ].Contains( "3 months" ) ? Period.Quarter : Period.Annual;
                    }
                    else
                    {
                        p = ComparePeriodDiff( ColumnHeaders [ 0 ], ColumnHeaders [ 1 ] );
                    }

                    DateTime dt = ParseDateFromColumnHeader( ColumnHeaders [ i ] );
                    tempMapping.Add( i, new CashFlowStatement( p, dt ) );
                }

                foreach ( var item in Data.Keys )
                {
                    // 0
                    if ( item == "Net Income/Starting Line" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Net_Income_and_Starting_Line = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 1
                    if ( item == "Depreciation/Depletion" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Depreciation_and_Depletion = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 2
                    if ( item == "Amortization" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Amortization = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 3
                    if ( item == "Deferred Taxes" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Deferred_Taxes = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 4
                    if ( item == "Non-Cash Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].NonCash_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 5
                    if ( item == "Changes in Working Capital" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Changes_in_Working_Capital = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 6
                    if ( item == "Cash from Operating Activities" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Cash_from_Operating_Activities = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 7
                    if ( item == "Capital Expenditures" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Capital_Expenditures = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 8
                    if ( item == "Other Investing Cash Flow Items, Total" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Other_Investing_Cash_Flow_Items__Total = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 9
                    if ( item == "Cash from Investing Activities" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Cash_from_Investing_Activities = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 10
                    if ( item == "Financing Cash Flow Items" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Financing_Cash_Flow_Items = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 11
                    if ( item == "Total Cash Dividends Paid" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Total_Cash_Dividends_Paid = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 12
                    if ( item == "Issuance (Retirement) of Stock, Net" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Issuance__Retirement__of_Stock__Net = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 13
                    if ( item == "Issuance (Retirement) of Debt, Net" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Issuance__Retirement__of_Debt__Net = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 14
                    if ( item == "Cash from Financing Activities" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Cash_from_Financing_Activities = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 15
                    if ( item == "Foreign Exchange Effects" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Foreign_Exchange_Effects = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 16
                    if ( item == "Net Change in Cash" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Net_Change_in_Cash = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 17
                    if ( item == "Cash Interest Paid, Supplemental" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Cash_Interest_Paid__Supplemental = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }
                    // 18
                    if ( item == "Cash Taxes Paid, Supplemental" ) { for ( int i = 0; i < Data.FirstOrDefault( ).Value.Count; i++ ) { tempMapping [ i ].Cash_Taxes_Paid__Supplemental = Data [ item ] [ i ] != "-" ? Convert.ToDecimal( Data [ item ] [ i ] ) * multipler : (decimal?)null; } }

                }

                List<CashFlowStatement> CashFlowStatements = new List<CashFlowStatement>( );
                for ( int i = 0; i < tempMapping.Keys.Count; i++ )
                {
                    CashFlowStatements.Add( tempMapping [ i ] );
                }
                return CashFlowStatements;
            }

            static List<Tuple<Tuple<string, List<string>>, Dictionary<string, List<string>>>> ParseFinancialData( string urlPath )
            {
                List<Tuple<Tuple<string, List<string>>, Dictionary<string, List<string>>>> Details = new List<Tuple<Tuple<string, List<string>>, Dictionary<string, List<string>>>>( );

                // maybe remove these later
                System.Net.WebClient wc = new System.Net.WebClient( );
                System.IO.MemoryStream ms = new System.IO.MemoryStream( );

                using ( var stream = wc.OpenRead( urlPath ) )
                {
                    stream.CopyTo( ms );
                    ms.Position = 0;
                }
                HtmlDocument html = new HtmlDocument( );
                html.Load( ms );
                var nodes = html.DocumentNode;
                var tables = nodes.SelectNodes( "//table[contains(@class, 'gf-table rgt')]" );
                foreach ( var table in tables )
                {
                    string CurrencyDenomination = string.Empty;
                    List<string> PeriodHeaders = new List<string>( );
                    Dictionary<string, List<string>> Map = new Dictionary<string, List<string>>( );

                    var header = table.ChildNodes.Where( i => i.Name == "thead" ).FirstOrDefault( ).ChildNodes.FirstOrDefault( ).ChildNodes.Where( j => j.Name == "th" );
                    foreach ( var detail in header )
                    {
                        if ( detail.Attributes.FirstOrDefault( ).Value == "lm lft nwp" )
                        {
                            CurrencyDenomination = detail.InnerText.Replace( "\n", "" );
                        }
                        else
                        {
                            PeriodHeaders.Add( detail.InnerText.Replace( "\n", "" ) );
                        }
                    }

                    var rows = table.ChildNodes.Where( i => i.Name == "tbody" ).FirstOrDefault( ).ChildNodes.Where( i => i.Name == "tr" );
                    foreach ( var row in rows )
                    {
                        var td = row.ChildNodes.Where( i => i.Name == "td" );
                        var rowHeader = td.Where( i => i.Attributes.FirstOrDefault( ).Value.Contains( "lft" ) ).FirstOrDefault( ).InnerText.Replace( "&quot;", "\"" ).Replace( "&amp;", "&" ).Replace( "&#39;", "'" ).Replace( "\n", "" );
                        var trs = td.Where( i => i.Attributes.FirstOrDefault( ) != null && i.Attributes.FirstOrDefault( ).Value.Contains( "r" ) );
                        List<string> rowData = new List<string>( );
                        foreach ( var tr in trs )
                        {
                            rowData.Add( tr.InnerText );
                        }
                        Map.Add( rowHeader, rowData );
                    }
                    Details.Add( new Tuple<Tuple<string, List<string>>, Dictionary<string, List<string>>>( new Tuple<string, List<string>>( CurrencyDenomination, PeriodHeaders ), Map ) );
                }
                return Details;
            }
        }

        public enum Period
        {
            Quarter = 0,
            Annual = 1
        }

        public class BalanceSheet
        {
            public BalanceSheet() { }
            public BalanceSheet( Period Period, DateTime date ) { this.Period = Period; this.PeriodEnd = date; }

            public Period Period;
            public DateTime PeriodEnd;

            public Decimal? Cash_and_Equivalents;
            public Decimal? Short_Term_Investments;
            public Decimal? Cash_and_Short_Term_Investments;
            public Decimal? Accounts_Receivable__Trade__Net;
            public Decimal? Receivables__Other;
            public Decimal? Total_Receivables__Net;
            public Decimal? Total_Inventory;
            public Decimal? Prepaid_Expenses;
            public Decimal? Other_Current_Assets__Total;
            public Decimal? Total_Current_Assets;
            public Decimal? Property_and_Plant_and_Equipment__Total__Gross;
            public Decimal? Accumulated_Depreciation__Total;
            public Decimal? Goodwill__Net;
            public Decimal? Intangibles__Net;
            public Decimal? Long_Term_Investments;
            public Decimal? Other_Long_Term_Assets__Total;
            public Decimal? Total_Assets;
            public Decimal? Accounts_Payable;
            public Decimal? Accrued_Expenses;
            public Decimal? Notes_Payable_and_Short_Term_Debt;
            public Decimal? Current_Port_of_LT_Debt_and_Capital_Leases;
            public Decimal? Other_Current_liabilities__Total;
            public Decimal? Total_Current_Liabilities;
            public Decimal? Long_Term_Debt;
            public Decimal? Capital_Lease_Obligations;
            public Decimal? Total_Long_Term_Debt;
            public Decimal? Total_Debt;
            public Decimal? Deferred_Income_Tax;
            public Decimal? Minority_Interest;
            public Decimal? Other_Liabilities__Total;
            public Decimal? Total_Liabilities;
            public Decimal? Redeemable_Preferred_Stock__Total;
            public Decimal? Preferred_Stock__Non_Redeemable__Net;
            public Decimal? Common_Stock__Total;
            public Decimal? Additional_PaidIn_Capital;
            public Decimal? Retained_Earnings__Accumulated_Deficit_;
            public Decimal? Treasury_Stock__Common;
            public Decimal? Other_Equity__Total;
            public Decimal? Total_Equity;
            public Decimal? Total_Liabilities_and_Shareholders_Equity;
            public Decimal? Shares_Outs__Common_Stock_Primary_Issue;
            public Decimal? Total_Common_Shares_Outstanding;

        }

        public class IncomeStatement
        {
            public IncomeStatement() { }
            public IncomeStatement( Period Period, DateTime date ) { this.Period = Period; this.PeriodEnd = date; }
            public Period Period;
            public DateTime PeriodEnd;

            public Decimal? Revenue;
            public Decimal? Other_Revenue_Total;
            public Decimal? Total_Revenue;
            public Decimal? Cost_of_Revenue_Total;
            public Decimal? Gross_Profit;
            public Decimal? Selling_and_General_and_Admin_Expenses_Total;
            public Decimal? Research_and_Development;
            public Decimal? Depreciation_and_Amortization;
            public Decimal? Interest_Expense__Income___less_Net_Operating;
            public Decimal? Unusual_Expense___Income__;
            public Decimal? Other_Operating_Expenses_Total;
            public Decimal? Total_Operating_Expense;
            public Decimal? Operating_Income;
            public Decimal? Interest_Income__Expense___Net_NonOperating;
            public Decimal? Gain___Loss___on_Sale_of_Assets;
            public Decimal? Other_Net;
            public Decimal? Income_Before_Tax;
            public Decimal? Income_After_Tax;
            public Decimal? Minority_Interest;
            public Decimal? Equity_In_Affiliates;
            public Decimal? Net_Income_Before_Extra_Items;
            public Decimal? Accounting_Change;
            public Decimal? Discontinued_Operations;
            public Decimal? Extraordinary_Item;
            public Decimal? Net_Income;
            public Decimal? Preferred_Dividends;
            public Decimal? Income_Available_to_Common_Excl_Extra_Items;
            public Decimal? Income_Available_to_Common_Incl_Extra_Items;
            public Decimal? Basic_Weighted_Average_Shares;
            public Decimal? Basic_EPS_Excluding_Extraordinary_Items;
            public Decimal? Basic_EPS_Including_Extraordinary_Items;
            public Decimal? Dilution_Adjustment;
            public Decimal? Diluted_Weighted_Average_Shares;
            public Decimal? Diluted_EPS_Excluding_Extraordinary_Items;
            public Decimal? Diluted_EPS_Including_Extraordinary_Items;
            public Decimal? Dividends_per_Share__less__Common_Stock_Primary_Issue;
            public Decimal? Gross_Dividends__less__Common_Stock;
            public Decimal? Net_Income_after_Stock_Based_Comp_Expense;
            public Decimal? Basic_EPS_after_Stock_Based_Comp_Expense;
            public Decimal? Diluted_EPS_after_Stock_Based_Comp_Expense;
            public Decimal? Depreciation_Supplemental;
            public Decimal? Total_Special_Items;
            public Decimal? Normalized_Income_Before_Taxes;
            public Decimal? Effect_of_Special_Items_on_Income_Taxes;
            public Decimal? Income_Taxes_Ex_Impact_of_Special_Items;
            public Decimal? Normalized_Income_After_Taxes;
            public Decimal? Normalized_Income_Avail_to_Common;
            public Decimal? Basic_Normalized_EPS;
            public Decimal? Diluted_Normalized_EPS;
        }

        public class CashFlowStatement
        {
            public CashFlowStatement() { }
            public CashFlowStatement( Period Period, DateTime date ) { this.Period = Period; this.PeriodEnd = date; }

            public Period Period;
            public DateTime PeriodEnd;
            public Decimal? Net_Income_and_Starting_Line;
            public Decimal? Depreciation_and_Depletion;
            public Decimal? Amortization;
            public Decimal? Deferred_Taxes;
            public Decimal? NonCash_Items;
            public Decimal? Changes_in_Working_Capital;
            public Decimal? Cash_from_Operating_Activities;
            public Decimal? Capital_Expenditures;
            public Decimal? Other_Investing_Cash_Flow_Items__Total;
            public Decimal? Cash_from_Investing_Activities;
            public Decimal? Financing_Cash_Flow_Items;
            public Decimal? Total_Cash_Dividends_Paid;
            public Decimal? Issuance__Retirement__of_Stock__Net;
            public Decimal? Issuance__Retirement__of_Debt__Net;
            public Decimal? Cash_from_Financing_Activities;
            public Decimal? Foreign_Exchange_Effects;
            public Decimal? Net_Change_in_Cash;
            public Decimal? Cash_Interest_Paid__Supplemental;
            public Decimal? Cash_Taxes_Paid__Supplemental;

        }
}
