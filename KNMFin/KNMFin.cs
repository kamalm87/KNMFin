using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace KNMFin{
    namespace Yahoo{
        enum Quote
        {
            Name = 1,
            DividendYieldPercent,
            LongTermDebtToEquity,
            MarketCapitalizationInMillion,
            NetProfitMargin,
            OneDayPriceChangePercent,
            PriceEarningsRatio,
            PriceToBookValue,
            PriceToFreeCashFlow,
            ReturnOnEquityPercent
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

        public enum Frequency
        {
            Daily = 'd',
            Weekly = 'w',
            Monthly = 'm'
        }

        public enum Industry
        {
            Argicultural_Chemicals = 112,
            Aluminum = 132,
            Chemicals__Major_Diversified_ = 110,
            Copper_ = 131,
            Gold_ = 134,
            Independent_Oil_and_Gas_ = 121,
            Industrial_Metals_and_Minerals_ = 133,
            Major_Integrated_Oil_and_Gas_ = 120,
            Nonmetallic_Mineral_Mining_ = 136,
            Oil_and_Gas_Drilling_and_Exploration_ = 123,
            Oil_and_Gas_Equipment_and_Services_ = 124,
            Oil_and_Gas_Pipelines_ = 125,
            Oil_and_Gas_Refining_and_Marketing_ = 122,
            Silver_ = 135,
            Specialty_Chemicals_ = 113,
            Steel_and_Iron_ = 130,
            Synthetics_ = 111,
            Conglomerates_ = 210,
            Appliances_ = 310,
            Auto_Manufacturers__Major_ = 330,
            Auto_Parts_ = 333,
            Beverages__Brewers_ = 346,
            Beverages__Soft_Drinks_ = 348,
            Beverages__Wineries_and_Distillers_ = 347,
            Business_Equipment_ = 313,
            Cigarettes_ = 350,
            Cleaning_Products_ = 326,
            Confectioners_ = 345,
            Dairy_Products_ = 344,
            Electronic_Equipment_ = 314,
            Farm_Products_ = 341,
            Food__Major_Diversified_ = 340,
            Home_Furnishings_and_Fixtures_ = 311,
            Housewares_and_Accessories_ = 312,
            Meat_Products_ = 343,
            Office_Supplies_ = 327,
            Packaging_and_Containers_ = 325,
            Paper_and_Paper_Products_ = 324,
            Personal_Products_ = 323,
            Photographic_Equipment_and_Supplies_ = 318,
            Processed_and_Packaged_Goods_ = 342,
            Recreational_Goods_Other_ = 317,
            Recreational_Vehicles_ = 332,
            Rubber_and_Plastics_ = 322,
            Sporting_Goods_ = 316,
            Textile__Apparel_Clothing_ = 320,
            Textile__Apparel_Footwear_and_Accessories_ = 321,
            Tobacco_Products_Other_ = 351,
            Toys_and_Games_ = 315,
            Trucks_and_Other_Vehicles_ = 331,
            Accident_and_Health_Insurance_ = 431,
            Asset_Management_ = 422,
            Closed__End_Fund__Debt_ = 425,
            Closed__End_Fund__Equity_ = 426,
            Closed__End_Fund__Foreign_ = 427,
            Credit_Services_ = 424,
            Diversified_Investments_ = 423,
            Foreign_Money_Center_Banks_ = 417,
            Foreign_Regional_Banks_ = 418,
            Insurance_Brokers_ = 434,
            Investment_Brokerage__National_ = 420,
            Investment_Brokerage__Regional_ = 421,
            Life_Insurance_ = 430,
            Money_Center_Banks_ = 410,
            Mortgage_Investment_ = 447,
            Property_and_Casualty_Insurance_ = 432,
            Property_Management_ = 448,
            REIT__Diversified_ = 440,
            REIT__Healthcare_Facilities_ = 442,
            REIT__Hotel_Motel_ = 443,
            REIT__Industrial_ = 444,
            REIT__Office_ = 441,
            REIT__Residential_ = 445,
            REIT__Retail_ = 446,
            Real_Estate_Development_ = 449,
            Regional__Mid__Atlantic_Banks_ = 412,
            Regional__Midwest_Banks_ = 414,
            Regional__Northeast_Banks_ = 411,
            Regional__Pacific_Banks_ = 416,
            Regional__Southeast_Banks_ = 413,
            Regional__Southwest_Banks_ = 415,
            Savings_and_Loans_ = 419,
            Surety_and_Title_Insurance_ = 433,
            Biotechnology_ = 515,
            Diagnostic_Substances_ = 516,
            Drug_Delivery_ = 513,
            Drug_Manufacturers__Major_ = 510,
            Drug_Manufacturers__Other_ = 511,
            Drug_Related_Products_ = 514,
            Drugs__Generic_ = 512,
            Health_Care_Plans_ = 522,
            Home_Health_Care_ = 526,
            Hospitals_ = 524,
            Long__Term_Care_Facilities_ = 523,
            Medical_Appliances_and_Equipment_ = 521,
            Medical_Instruments_and_Supplies_ = 520,
            Medical_Laboratories_and_Research_ = 525,
            Medical_Practitioners_ = 527,
            Specialized_Health_Services_ = 528,
            Aerospace_Defense__Major_Diversified_ = 610,
            Aerospace_Defense_Products_and_Services_ = 611,
            Cement_ = 633,
            Diversified_Machinery_ = 622,
            Farm_and_Construction_Machinery_ = 620,
            General_Building_Materials_ = 634,
            General_Contractors_ = 636,
            Heavy_Construction_ = 635,
            Industrial_Electrical_Equipment_ = 627,
            Industrial_Equipment_and_Components_ = 621,
            Lumber_Wood_Production_ = 632,
            Machine_Tools_and_Accessories_ = 624,
            Manufactured_Housing_ = 631,
            Metal_Fabrication_ = 626,
            Pollution_and_Treatment_Controls_ = 623,
            Residential_Construction_ = 630,
            Small_Tools_and_Accessories_ = 625,
            Textile_Industrial_ = 628,
            Waste_Management_ = 637,
            Advertising_Agencies_ = 720,
            Air_Delivery_and_Freight_Services_ = 773,
            Air_Services_Other_ = 772,
            Apparel_Stores_ = 730,
            Auto_Dealerships_ = 744,
            Auto_Parts_Stores_ = 738,
            Auto_Parts_Wholesale_ = 750,
            Basic_Materials_Wholesale_ = 758,
            Broadcasting__Radio_ = 724,
            Broadcasting__TV_ = 723,
            Building_Materials_Wholesale_ = 751,
            Business_Services_ = 760,
            CATV_Systems_ = 725,
            Catalog_and_Mail_Order_Houses_ = 739,
            Computers_Wholesale_ = 755,
            Consumer_Services_ = 763,
            Department_Stores_ = 731,
            Discount_Variety_Stores_ = 732,
            Drug_Stores_ = 733,
            Drugs_Wholesale_ = 756,
            Education_and_Training_Services_ = 766,
            Electronics_Stores_ = 735,
            Electronics_Wholesale_ = 753,
            Entertainment__Diversified_ = 722,
            Food_Wholesale_ = 757,
            Gaming_Activities_ = 714,
            General_Entertainment_ = 716,
            Grocery_Stores_ = 734,
            Home_Furnishing_Stores_ = 737,
            Home_Improvement_Stores_ = 736,
            Industrial_Equipment_Wholesale_ = 752,
            Jewelry_Stores_ = 742,
            Lodging_ = 710,
            Major_Airlines_ = 770,
            Management_Services_ = 769,
            Marketing_Services_ = 721,
            Medical_Equipment_Wholesale_ = 754,
            Movie_Production_Theaters_ = 726,
            Music_and_Video_Stores_ = 743,
            Personal_Services_ = 762,
            Publishing__Books_ = 729,
            Publishing__Newspapers_ = 727,
            Publishing__Periodicals_ = 728,
            Railroads_ = 776,
            Regional_Airlines_ = 771,
            Rental_and_Leasing_Services_ = 761,
            Research_Services_ = 768,
            Resorts_and_Casinos_ = 711,
            Restaurants_ = 712,
            Security_and_Protection_Services_ = 765,
            Shipping_ = 775,
            Specialty_Eateries_ = 713,
            Specialty_Retail_Other_ = 745,
            Sporting_Activities_ = 715,
            Sporting_Goods_Stores_ = 740,
            Staffing_and_Outsourcing_Services_ = 764,
            Technical_Services_ = 767,
            Toy_and_Hobby_Stores_ = 741,
            Trucking_ = 774,
            Wholesale_Other_ = 759,
            Application_Software_ = 821,
            Business_Software_and_Services_ = 826,
            Communication_Equipment_ = 841,
            Computer_Based_Systems_ = 812,
            Computer_Peripherals_ = 815,
            Data_Storage_Devices_ = 813,
            Diversified_Communication_Services_ = 846,
            Diversified_Computer_Systems_ = 810,
            Diversified_Electronics_ = 836,
            Healthcare_Information_Services_ = 825,
            Information_and_Delivery_Services_ = 827,
            Information_Technology_Services_ = 824,
            Internet_Information_Providers_ = 851,
            Internet_Service_Providers_ = 850,
            Internet_Software_and_Services_ = 852,
            Long_Distance_Carriers_ = 843,
            Multimedia_and_Graphics_Software_ = 820,
            Networking_and_Communication_Devices_ = 814,
            Personal_Computers_ = 811,
            Printed_Circuit_Boards_ = 835,
            Processing_Systems_and_Products_ = 842,
            Scientific_and_Technical_Instruments_ = 837,
            Security_Software_and_Services_ = 823,
            Semiconductor__Broad_Line_ = 830,
            Semiconductor__Integrated_Circuits_ = 833,
            Semiconductor__Specialized_ = 832,
            Semiconductor_Equipment_and_Materials_ = 834,
            Semiconductor__Memory_Chips_ = 831,
            Technical_and_System_Software_ = 822,
            Telecom_Services__Domestic_ = 844,
            Telecom_Services__Foreign_ = 845,
            Wireless_Communications_ = 840,
            Diversified_Utilities_ = 913,
            Electric_Utilities_ = 911,
            Foreign_Utilities_ = 910,
            Gas_Utilities_ = 912,
            Water_Utilities_ = 914
        }

        public enum SortDirection
        {
            Up = 'u',
            Down = 'd'
        }

        // TODO: For company specific queries, add a third variable 
        // with a for the column mapping within code
        public sealed class QuoteProperties
        {
            private readonly String name;
            private readonly int value;

            public static readonly QuoteProperties AfterHoursChangeRealtime = new QuoteProperties( 1, "c8" );
            public static readonly QuoteProperties AnnualizedGain = new QuoteProperties( 2, "g3" );
            public static readonly QuoteProperties Ask = new QuoteProperties( 3, "a0" );
            public static readonly QuoteProperties AskRealtime = new QuoteProperties( 4, "b2" );
            public static readonly QuoteProperties AskSize = new QuoteProperties( 5, "a5" );
            public static readonly QuoteProperties AverageDailyVolume = new QuoteProperties( 6, "a2" );
            public static readonly QuoteProperties Bid = new QuoteProperties( 7, "b0" );
            public static readonly QuoteProperties BidRealtime = new QuoteProperties( 8, "b3" );
            public static readonly QuoteProperties BidSize = new QuoteProperties( 9, "b6" );
            public static readonly QuoteProperties BookValuePerShare = new QuoteProperties( 10, "b4" );

            public static readonly QuoteProperties Change = new QuoteProperties( 11, "c1" );
            public static readonly QuoteProperties Change_ChangeInPercent = new QuoteProperties( 12, "c0" );
            public static readonly QuoteProperties ChangeFromFiftydayMovingAverage = new QuoteProperties( 13, "m7" );
            public static readonly QuoteProperties ChangeFromTwoHundreddayMovingAverage = new QuoteProperties( 14, "m5" );
            public static readonly QuoteProperties ChangeFromYearHigh = new QuoteProperties( 15, "k4" );
            public static readonly QuoteProperties ChangeFromYearLow = new QuoteProperties( 16, "j5" );
            public static readonly QuoteProperties ChangeInPercent = new QuoteProperties( 17, "p2" );
            public static readonly QuoteProperties ChangeInPercentRealtime = new QuoteProperties( 18, "k2" );
            public static readonly QuoteProperties ChangeRealtime = new QuoteProperties( 19, "c6" );
            public static readonly QuoteProperties Commission = new QuoteProperties( 20, "c3" );

            public static readonly QuoteProperties Currency = new QuoteProperties( 21, "c4" );
            public static readonly QuoteProperties DaysHigh = new QuoteProperties( 22, "h0" );
            public static readonly QuoteProperties DaysLow = new QuoteProperties( 23, "g0" );
            public static readonly QuoteProperties DaysRange = new QuoteProperties( 24, "m0" );
            public static readonly QuoteProperties DaysRangeRealtime = new QuoteProperties( 25, "m2" );
            public static readonly QuoteProperties DaysValueChange = new QuoteProperties( 26, "w1" );
            public static readonly QuoteProperties DaysValueChangeRealtime = new QuoteProperties( 27, "w4" );
            public static readonly QuoteProperties DividendPayDate = new QuoteProperties( 28, "r1" );
            public static readonly QuoteProperties TrailingAnnualDividendYield = new QuoteProperties( 29, "d0" );
            public static readonly QuoteProperties TrailingAnnualDividendYieldInPercent = new QuoteProperties( 30, "y0" );

            public static readonly QuoteProperties DilutedEPS = new QuoteProperties( 31, "e0" );
            public static readonly QuoteProperties EBITDA = new QuoteProperties( 32, "j4" );
            public static readonly QuoteProperties EPSEstimateCurrentYear = new QuoteProperties( 33, "e7" );
            public static readonly QuoteProperties EPSEstimateNextQuarter = new QuoteProperties( 34, "e9" );
            public static readonly QuoteProperties EPSEstimateNextYear = new QuoteProperties( 35, "e8" );
            public static readonly QuoteProperties ExDividendDate = new QuoteProperties( 36, "q0" );
            public static readonly QuoteProperties FiftydayMovingAverage = new QuoteProperties( 37, "m3" );
            public static readonly QuoteProperties SharesFloat = new QuoteProperties( 38, "f6" );
            public static readonly QuoteProperties HighLimit = new QuoteProperties( 39, "l2" );
            public static readonly QuoteProperties HoldingsGain = new QuoteProperties( 40, "g4" );

            public static readonly QuoteProperties HoldingsGainPercent = new QuoteProperties( 41, "g1" );
            public static readonly QuoteProperties HoldingsGainPercentRealtime = new QuoteProperties( 42, "g5" );
            public static readonly QuoteProperties HoldingsGainRealtime = new QuoteProperties( 43, "g6" );
            public static readonly QuoteProperties HoldingsValue = new QuoteProperties( 44, "v1" );
            public static readonly QuoteProperties HoldingsValueRealtime = new QuoteProperties( 45, "v7" );
            public static readonly QuoteProperties LastTradeDate = new QuoteProperties( 46, "d1" );
            public static readonly QuoteProperties LastTradePriceOnly = new QuoteProperties( 47, "l1" );
            public static readonly QuoteProperties LastTradeRealtimeWithTime = new QuoteProperties( 48, "k1" );
            public static readonly QuoteProperties LastTradeSize = new QuoteProperties( 49, "k3" );
            public static readonly QuoteProperties LastTradeTime = new QuoteProperties( 50, "t1" );

            public static readonly QuoteProperties LastTradeWithTime = new QuoteProperties( 51, "l0" );
            public static readonly QuoteProperties LowLimit = new QuoteProperties( 52, "l3" );
            public static readonly QuoteProperties MarketCapitalization = new QuoteProperties( 53, "j1" );
            public static readonly QuoteProperties MarketCapRealtime = new QuoteProperties( 54, "j3" );
            public static readonly QuoteProperties MoreInfo = new QuoteProperties( 55, "i0" );
            public static readonly QuoteProperties Name = new QuoteProperties( 56, "n0" );
            public static readonly QuoteProperties Notes = new QuoteProperties( 57, "n4" );
            public static readonly QuoteProperties OneyrTargetPrice = new QuoteProperties( 58, "t8" );
            public static readonly QuoteProperties Open = new QuoteProperties( 59, "o0" );
            public static readonly QuoteProperties OrderBookRealtime = new QuoteProperties( 60, "i5" );

            public static readonly QuoteProperties PEGRatio = new QuoteProperties( 61, "r5" );
            public static readonly QuoteProperties PERatio = new QuoteProperties( 62, "r0" );
            public static readonly QuoteProperties PERatioRealtime = new QuoteProperties( 63, "r2" );
            public static readonly QuoteProperties PercentChangeFromFiftydayMovingAverage = new QuoteProperties( 64, "m8" );
            public static readonly QuoteProperties PercentChangeFromTwoHundreddayMovingAverage = new QuoteProperties( 65, "m6" );
            public static readonly QuoteProperties ChangeInPercentFromYearHigh = new QuoteProperties( 66, "k5" );
            public static readonly QuoteProperties PercentChangeFromYearLow = new QuoteProperties( 67, "j6" );
            public static readonly QuoteProperties PreviousClose = new QuoteProperties( 68, "p0" );
            public static readonly QuoteProperties PriceBook = new QuoteProperties( 69, "p6" );
            public static readonly QuoteProperties PriceEPSEstimateCurrentYear = new QuoteProperties( 70, "r6" );

            public static readonly QuoteProperties PriceEPSEstimateNextYear = new QuoteProperties( 71, "r7" );
            public static readonly QuoteProperties PricePaid = new QuoteProperties( 72, "p1" );
            public static readonly QuoteProperties PriceSales = new QuoteProperties( 73, "p5" );
            public static readonly QuoteProperties Revenue = new QuoteProperties( 74, "s6" );
            public static readonly QuoteProperties SharesOwned = new QuoteProperties( 75, "s1" );
            public static readonly QuoteProperties SharesOutstanding = new QuoteProperties( 76, "j2" );
            public static readonly QuoteProperties ShortRatio = new QuoteProperties( 77, "s7" );
            public static readonly QuoteProperties StockExchange = new QuoteProperties( 78, "x0" );
            public static readonly QuoteProperties Symbol = new QuoteProperties( 79, "s0" );
            public static readonly QuoteProperties TickerTrend = new QuoteProperties( 80, "t7" );

            public static readonly QuoteProperties TradeDate = new QuoteProperties( 81, "d2" );
            public static readonly QuoteProperties TradeLinks = new QuoteProperties( 82, "t6" );
            public static readonly QuoteProperties TradeLinksAdditional = new QuoteProperties( 83, "f0" );
            public static readonly QuoteProperties TwoHundreddayMovingAverage = new QuoteProperties( 84, "m4" );
            public static readonly QuoteProperties Volume = new QuoteProperties( 85, "v0" );
            public static readonly QuoteProperties YearHigh = new QuoteProperties( 86, "k0" );
            public static readonly QuoteProperties YearLow = new QuoteProperties( 87, "j0" );
            public static readonly QuoteProperties YearRange = new QuoteProperties( 88, "w0" );


            private QuoteProperties( int value, string name )
            {
                this.name = name;
                this.value = value;
            }

            public override string ToString()
            {
                return name;
            }
        }

        public sealed class MarketQuoteProperties
        {
            private readonly String name;
            private readonly int value;

            public static readonly MarketQuoteProperties Name = new MarketQuoteProperties( 1, "coname" );
            public static readonly MarketQuoteProperties DividendYieldPercent = new MarketQuoteProperties( 2, "yie" );
            public static readonly MarketQuoteProperties LongTermDebtToEquity = new MarketQuoteProperties( 3, "qto" );
            public static readonly MarketQuoteProperties MarketCapitalizationInMillion = new MarketQuoteProperties( 4, "mkt" );
            public static readonly MarketQuoteProperties NetProfitMargin = new MarketQuoteProperties( 5, "qpm" );
            public static readonly MarketQuoteProperties OneDayPriceChangePercent = new MarketQuoteProperties( 6, "pr1" );
            public static readonly MarketQuoteProperties PriceEarningsRatio = new MarketQuoteProperties( 7, "pee" );
            public static readonly MarketQuoteProperties PriceToBookValue = new MarketQuoteProperties( 8, "pri" );
            public static readonly MarketQuoteProperties PriceToFreeCashFlow = new MarketQuoteProperties( 9, "prf" );
            public static readonly MarketQuoteProperties ReturnOnEquityPercent = new MarketQuoteProperties( 10, "ttm" );

            private MarketQuoteProperties( int value, string name )
            {
                this.name = name;
                this.value = value;
            }

            public override string ToString()
            {
                return name;
            }
        }


        // TODO: Consolidate into a single static class
        // Queries

        static class HistoricalQuotes
        {

            // http://real-chart.finance.yahoo.com/table.csv?s=MSFT&a=01&b=19&c=2010&d=09&e=19&f=2010&g=d&ignore=.csv
            static string baseURL = @"http://ichart.yahoo.com/table.csv?s=";
            static string endURL = @"&ignore=.csv";
            static System.Net.WebClient client = new System.Net.WebClient( );

            public static List<ReturnRow> QueryReturns( string Ticker, DateTime Start, DateTime End, Frequency Frequency )
            {
                var sb = new StringBuilder( );
                sb.Append( baseURL );
                sb.Append( Ticker );
                sb.Append( "&a=" + ( Start.Month - 1 ) + "&b=" + ( Start.Day ) + "&c=" + ( Start.Year ) );
                sb.Append( "&d=" + ( End.Month - 1 ) + "&e=" + ( End.Day ) + "&f=" + ( End.Year ) );
                sb.Append( "&g=" + (char)Frequency );
                sb.Append( endURL );
                var res = client.DownloadString( sb.ToString( ) );

                if ( res == null || res == string.Empty ) return null;

                var resLines = res.Split( '\n' );
                var returns = new List<ReturnRow>( );

                for ( int i = 1; i < resLines.Count( ); i++ )
                {

                    var temp = resLines [ i ];
                    if ( temp == string.Empty || temp == "\0" ) continue;


                    var row = temp.Split( ',' );
                    var date = row [ 0 ].Split( '-' );

                    returns.Add( new ReturnRow
                    {
                        Date = new DateTime( Convert.ToInt16( date [ 0 ] ), Convert.ToInt16( date [ 1 ] ), Convert.ToInt16( date [ 2 ] ) ),
                        Open = Convert.ToDecimal( row [ 1 ] ),
                        High = Convert.ToDecimal( row [ 2 ] ),
                        Low = Convert.ToDecimal( row [ 3 ] ),
                        Close = Convert.ToDecimal( row [ 4 ] ),
                        Volume = Convert.ToDecimal( row [ 5 ] ),
                        AdjClose = Convert.ToDecimal( row [ 6 ] )
                    } );
                }
                return returns;
            }

            public struct ReturnRow
            {
                public DateTime Date;
                public decimal Open;
                public decimal High;
                public decimal Low;
                public decimal Close;
                public decimal Volume;
                public decimal AdjClose;
            }
        }

        public static class SectorQuery
        {
            static System.Net.WebClient client = new System.Net.WebClient( );
            static string baseURL = @"http://biz.yahoo.com/p/csv/s_coname";
            static string endUrl = @".csv";
            static System.IO.MemoryStream ms = new System.IO.MemoryStream( );

            static decimal billion = 1000000000;
            static decimal trillion = 1000000000000;

            public static List<SectorResult> Query( SortDirection dir )
            {
                var res = client.DownloadString( baseURL + (char)dir + endUrl ).Split( '\n' );

                if ( res == null || res.Count( ) == 0 ) return null;

                List<SectorResult> sectorResults = new List<SectorResult>( );
                var colHeaders = res [ 0 ].Replace( "\"", "" ).Split( ',' );

                for ( int i = 1; i < res.Length; i++ )
                {
                    var row = res [ i ].Replace( "\"", "" ).Split( ',' );
                    if ( row [ 0 ] == "\0" ) continue;

                    sectorResults.Add( new SectorResult
                    {
                        SectorName = row [ 0 ],
                        PriceChange1Day = Convert.ToDecimal( row [ 1 ] ),
                        MarketCap = row [ 2 ].Contains( "B" ) ?
                                        Convert.ToDecimal( row [ 2 ].Replace( "B", "" ) ) * billion
                            // speculative else function--haven't encountered trillion designation
                                        : Convert.ToDecimal( row [ 2 ].Replace( "T", "" ) ) * trillion,
                        PriceEarnings = Convert.ToDecimal( row [ 3 ] ),
                        ReturnOnEquityPercentage = Convert.ToDecimal( row [ 4 ] ),
                        DividendYieldPercentage = Convert.ToDecimal( row [ 5 ] ),
                        DebtToEquity = Convert.ToDecimal( row [ 6 ] ),
                        PriceToBook = Convert.ToDecimal( row [ 7 ] ),
                        NetProfitMargin = Convert.ToDecimal( row [ 8 ] ),
                        PriceToFreeCashFlow = Convert.ToDecimal( row [ 9 ] )

                    } );
                }
                return sectorResults;
            }


            public class SectorResult
            {
                public string SectorName { get; set; }
                public decimal PriceChange1Day { get; set; }
                public decimal MarketCap { get; set; }
                public decimal PriceEarnings { get; set; }
                public decimal ReturnOnEquityPercentage { get; set; }
                public decimal DividendYieldPercentage { get; set; }
                public decimal DebtToEquity { get; set; }
                public decimal PriceToBook { get; set; }
                public decimal NetProfitMargin { get; set; }
                public decimal PriceToFreeCashFlow { get; set; }
            }
        }

        public static class IndustryQuery
        {
            static System.Net.WebClient client = new System.Net.WebClient( );
            static string baseURL = @"http://biz.yahoo.com/p/csv/";
            static string endUrl = @".csv";
            static System.IO.MemoryStream ms = new System.IO.MemoryStream( );

            static decimal billion = 1000000000;
            static decimal trillion = 1000000000000;

            public static List<IndustryResult> Query( Sector SectorOfIndustries, MarketQuoteProperties SortProperty, SortDirection SortDirection )
            {
                var res = client.DownloadString( baseURL + (int)SectorOfIndustries + SortProperty.ToString( ) + (char)SortDirection + endUrl ).Split( '\n' );
                if ( res == null || res.Count( ) < 2 ) return null;

                var industryResults = new List<IndustryResult>( );
                var colHeaders = res [ 0 ].Replace( "\"", "" ).Split( ',' );
                for ( int i = 1; i < res.Count( ); i++ )
                {
                    var row = res [ i ].Replace( "\"", "" ).Split( ',' );
                    if ( row [ 0 ] == "\0" ) continue;

                    industryResults.Add( new IndustryResult
                    {
                        SectorName = row [ 0 ],
                        PriceChange1Day = Convert.ToDecimal( row [ 1 ] ),
                        MarketCap = row [ 2 ].Contains( "B" ) ?
                                        Convert.ToDecimal( row [ 2 ].Replace( "B", "" ) ) * billion
                            // speculative ternary else function--haven't encountered trillion designation
                            // no idea what the altnernative would be/is
                                        : Convert.ToDecimal( row [ 2 ].Replace( "T", "" ) ) * trillion,
                        PriceEarnings = Convert.ToDecimal( row [ 3 ] ),
                        ReturnOnEquityPercentage = Convert.ToDecimal( row [ 4 ] ),
                        DividendYieldPercentage = Convert.ToDecimal( row [ 5 ] ),
                        DebtToEquity = Convert.ToDecimal( row [ 6 ] ),
                        PriceToBook = Convert.ToDecimal( row [ 7 ] ),
                        NetProfitMargin = Convert.ToDecimal( row [ 8 ] ),
                        PriceToFreeCashFlow = Convert.ToDecimal( row [ 9 ] )

                    } );
                }
                return industryResults;
            }

            public class IndustryResult
            {
                public string SectorName { get; set; }
                public decimal PriceChange1Day { get; set; }
                public decimal MarketCap { get; set; }
                public decimal PriceEarnings { get; set; }
                public decimal ReturnOnEquityPercentage { get; set; }
                public decimal DividendYieldPercentage { get; set; }
                public decimal DebtToEquity { get; set; }
                public decimal PriceToBook { get; set; }
                public decimal NetProfitMargin { get; set; }
                public decimal PriceToFreeCashFlow { get; set; }
            }

            public enum SortDirection
            {
                Up = 'u',
                Down = 'd'
            }
        }

        // TODO: (?) Add mapping for company specific queries
        public static class CompanyQuery
        {
            private static System.Net.WebClient client = new System.Net.WebClient( );
            private static string baseURL = @"http://biz.yahoo.com/p/csv/";
            private static string baseCompanyQuoteURL = @"http://download.finance.yahoo.com/d/quotes.csv?s=";
            private static string endUrl = @".csv";
            private static System.IO.MemoryStream ms = new System.IO.MemoryStream( );

            private static decimal million = 1000000;
            private static decimal billion = 1000000000;
            private static decimal trillion = 1000000000000;



            // TODO: add recursive case for Tickers.Count > 50
            // TODO: URL format conversion for tickers
            public static List<List<string>> QueryCompanies( List<string> Tickers, List<QuoteProperties> Quotes )
            {
                StringBuilder sb = new StringBuilder( );
                sb.Append( baseCompanyQuoteURL );
                foreach ( string ticker in Tickers ){
                    if ( ticker == Tickers [ 0 ] ){
                        sb.Append( ticker );
                    } else {
                        sb.Append( "," + ticker );
                    }
                }
                sb.Append( "&f=" );
                foreach ( var item in Quotes )
                {
                    sb.Append( item.ToString( ) );
                }
                sb.Append( endUrl );
                var res = client.DownloadString( sb.ToString( ) );

                var resultsQuote = new List<List<string>>( );
                return resultsQuote;
            }


            public static List<CompanyResult> QueryIndustry( Industry IndustryOfCompanies, MarketQuoteProperties SortProperty, SortDirection SortDirection ){

                var res = client.DownloadString( baseURL + (int)IndustryOfCompanies + SortProperty.ToString( ) + (char)SortDirection + endUrl ).Split( '\n' );
                var qURL = baseURL + (int)IndustryOfCompanies + SortProperty.ToString( ) + (char)SortDirection + endUrl;

                if ( res == null || res.Count( ) == 0 ) return null;


                var companyResults = new List<CompanyResult>( );
                var colHeaders = res [ 0 ].Replace( "\"", "" ).Split( ',' );

                for ( int i = 1; i < res.Length; i++ )
                {
                    var rowTemp1 = res [ i ].Split( '"' );
                    if ( rowTemp1 [ 0 ] == "\0" ) continue;


                    var rowTemp2 = rowTemp1.Where( j => j != string.Empty ).ToArray<string>( );

                    var rowTemp3 = rowTemp2 [ 0 ].Replace( "\"", "" );
                    var rowTemp4 = new List<string>( );
                    for ( int t = 1; t < rowTemp2.Count( ); t++ )
                    {
                        var temp = rowTemp2 [ t ].Split( ',' );
                        foreach ( string s in temp )
                        {
                            if ( s != string.Empty )
                                rowTemp4.Add( s );
                        }

                    }

                    //     rowTemp2 [ 1 ].Split( ',' ).Where( j => j != string.Empty ).ToArray<string>( );

                    var row = rowTemp4;
                    if ( row [ 0 ] == "\0" ) continue;

                    companyResults.Add( new CompanyResult
                    {
                        SectorName = rowTemp3,
                        PriceChange1Day = row [ 0 ] == "NA" ? 0 : Convert.ToDecimal( row [ 0 ] ),
                        MarketCap = row [ 1 ] == "NA" ? 0
                                    : row [ 1 ].Contains( "B" ) ?
                                            Convert.ToDecimal( row [ 1 ].Replace( "B", "" ) ) * billion
                            // speculative else function--haven't encountered trillion designation
                                            : row [ 1 ].Contains( "M" ) ? Convert.ToDecimal( row [ 1 ].Replace( "M", "" ) ) * million
                                                                    : Convert.ToDecimal( row [ 1 ].Replace( "T", "" ) ) * trillion,
                        PriceEarnings = row [ 2 ] == "NA" ? 0 : Convert.ToDecimal( row [ 2 ] ),
                        ReturnOnEquityPercentage = row [ 3 ] == "NA" ? 0 : Convert.ToDecimal( row [ 3 ] ),
                        DividendYieldPercentage = row [ 4 ] == "NA" ? 0 : Convert.ToDecimal( row [ 4 ] ),
                        DebtToEquity = row [ 5 ] == "NA" ? 0 : Convert.ToDecimal( row [ 5 ] ),
                        PriceToBook = row [ 6 ] == "NA" ? 0 : Convert.ToDecimal( row [ 6 ] ),
                        NetProfitMargin = row [ 7 ] == "NA" ? 0 : Convert.ToDecimal( row [ 7 ] ),
                        PriceToFreeCashFlow = row [ 8 ] == "NA" ? 0 : Convert.ToDecimal( row [ 8 ] )

                    } );
                }

                return companyResults;
            }

            public class CompanyResult
            {
                public string SectorName { get; set; }
                public decimal PriceChange1Day { get; set; }
                public decimal MarketCap { get; set; }
                public decimal PriceEarnings { get; set; }
                public decimal ReturnOnEquityPercentage { get; set; }
                public decimal DividendYieldPercentage { get; set; }
                public decimal DebtToEquity { get; set; }
                public decimal PriceToBook { get; set; }
                public decimal NetProfitMargin { get; set; }
                public decimal PriceToFreeCashFlow { get; set; }
            }
        }
    }

    namespace Google{
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


            public Address Address { get; set; }

            // Misc
            public DateTime QueryTime { get; set; }
            public string EdgarURLLink { get; set; }


            public CompanyInfo( string ticker )
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


                // BLACK MAGIC
                /*
                var t = nodes.SelectNodes( "//div" ).Where( i => i.Attributes.Where( j => j.Value == "sfe-section" ).Count( ) > 0 )
                    .Where( k => k.ChildNodes.Count > 7 ).Where( m => m.ChildNodes.Where( n => n.Name == "br" ).Count( ) > 3 ).Select( o => o.ChildNodes.ToArray<HtmlNode>( ) ).FirstOrDefault( ).Where( a => a.Name == "#text" ).Select( b => b.InnerText )
                    .Where( c => c != "\n" );
                 * */



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

                int indexFirst, indexSecond, digits = 0;
                string [] firstCol = nodes [ 0 ].ChildNodes.Where( i => i.InnerText != "\n" )
                    .Select( ( i ) => i.InnerText ).ToArray<string>( ).Select( i => i.Replace( "\n", " " )
                    .Substring( 1, i.Length - 1 ) ).ToArray<string>( );
                indexFirst = firstCol [ 0 ].IndexOf( " " );
                indexSecond = firstCol [ 0 ].IndexOf( " ", indexFirst + 1 );
                var tempVal1 = firstCol [ 0 ].Substring( indexFirst + 2, indexSecond );
                indexFirst = firstCol [ 0 ].IndexOf( " - ", indexSecond + 1 );
                indexSecond = firstCol [ 0 ].IndexOf( " ", indexFirst + 3 );
                var tempVal2 = firstCol [ 0 ].Substring( indexFirst + 3, firstCol [ 0 ].Length - indexFirst - 5 );
                foreach ( char c in tempVal1 )
                {
                    if ( Char.IsDigit( c ) ) ++digits;
                }
                if ( digits == 0 )
                {
                    RangeLow = null; // Property assignment 
                    RangeHigh = null; // Property assignment 
                }
                else
                {
                    RangeLow = Convert.ToDouble( tempVal1.Replace( "-", "" ) ); // Property assignment 
                    RangeHigh = Convert.ToDouble( tempVal2.Replace( "-", "" ) ); // Property assignment 
                }

                indexFirst = firstCol [ 1 ].IndexOf( " ", 5 );
                indexSecond = firstCol [ 1 ].IndexOf( "-" );

                tempVal1 = firstCol [ 1 ].Substring( indexFirst + 2, indexSecond - indexFirst - 2 );
                FiftyTwoWeekLow = Convert.ToDouble( tempVal1 ); // Property assignment 
                tempVal2 = firstCol [ 1 ].Substring( indexSecond + 2, firstCol [ 1 ].Length - indexSecond - 4 );
                FiftyTwoWeekHigh = Convert.ToDouble( tempVal2 ); // Property assignment 
                indexFirst = firstCol [ 2 ].IndexOf( " " );
                tempVal1 = firstCol [ 2 ].Substring( indexFirst + 1 ).Replace( " ", "" );
                digits = 0;
                foreach ( char c in tempVal1 )
                {
                    if ( Char.IsDigit( c ) ) ++digits;
                }
                if ( digits == 0 )
                {
                    Open = null;
                }
                else
                {
                    Open = Convert.ToDouble( tempVal1 ); // Property assignment  
                }
                tempVal2 = firstCol [ 3 ].Replace( " ", "" );
                indexFirst = tempVal2.IndexOf( "." );
                indexSecond = tempVal2.IndexOf( "/", indexFirst );
                tempVal1 = tempVal2.Substring( indexFirst + 1, indexSecond - indexFirst - 1 );
                if ( tempVal1.Contains( "M" ) || tempVal1.Contains( "B" ) )
                {
                    VolumeAverage = Convert.ToDouble( tempVal1.Substring( 0, tempVal1.Length - 2 ) ); // Property assignment 
                }
                else
                {
                    VolumeAverage = Convert.ToDouble( tempVal1 ); // Property assignment 
                }
                if ( tempVal2.Contains( "M" ) )
                {
                    double million = 1000000;
                    double intermed = Convert.ToDouble( tempVal2.Substring( indexSecond + 1, tempVal2.Length - indexSecond - 2 ) );
                    VolumeTotal = intermed * million; // Property assignment 
                    VolumeAverage = VolumeAverage < 1000 ? VolumeAverage * million : VolumeAverage; // Property assignment 
                }
                else
                {
                    double million = 1000000;
                    double billion = 1000000000;
                    double intermed = Convert.ToDouble( tempVal2.Substring( indexSecond + 1, tempVal2.Length - indexSecond - 2 ) );
                    VolumeTotal = intermed < 1000 ? intermed * billion : intermed; // Property assignment 
                    VolumeAverage = VolumeAverage < 1000 ? VolumeAverage * billion : VolumeAverage; // Property assignment 
                }
                indexFirst = firstCol [ 4 ].IndexOf( " ", 4 );
                tempVal1 = firstCol [ 4 ].Substring( indexFirst ).Replace( " ", "" );
                MarketCap = ( tempVal1 [ tempVal1.Length - 1 ] ).ToString( ).ToUpper( ) == "B" // Property assignment
                    ? Convert.ToDouble( tempVal1.Substring( 0, tempVal1.Length - 2 ) ) * 1000000000
                    : Convert.ToDouble( tempVal1.Substring( 0, tempVal1.Length - 2 ) ) * 1000000;
                indexFirst = firstCol [ 5 ].IndexOf( " " );
                tempVal1 = firstCol [ 5 ].Substring( indexFirst + 1 ).Replace( " ", "" );
                digits = 0;
                foreach ( char c in tempVal1 )
                {
                    if ( Char.IsDigit( c ) ) ++digits;
                }
                if ( digits == 0 )
                {
                    PriceEarnings = null;
                }
                else
                {
                    PriceEarnings = Convert.ToDouble( tempVal1 );  // Property assignment  
                }
                string [] secondCol = nodes [ 1 ].ChildNodes.Where( i => i.InnerText != "\n" )
                        .Select( i => i.InnerText ).ToArray<string>( )
                        .Select( i => i.Replace( "\n", " " ) ).ToArray<string>( );
                digits = 0;
                foreach ( char c in secondCol [ 0 ] )
                {
                    if ( Char.IsDigit( c ) ) ++digits;
                }
                if ( digits == 0 )
                {
                    Dividend = null; // Property assignment
                    DividendYield = null; // Property assignment
                }
                else
                {
                    indexFirst = secondCol [ 0 ].IndexOf( " ", 2 );
                    indexSecond = secondCol [ 0 ].IndexOf( "/", indexFirst + 1 );
                    if ( indexFirst != -1 && indexSecond != -1 )
                    {
                        tempVal1 = secondCol [ 0 ].Substring( indexFirst + 1, indexSecond - indexFirst - 1 ).Replace( " ", "" ).Replace( "*", "" );
                        tempVal2 = secondCol [ 0 ].Substring( indexSecond + 1, secondCol [ 0 ].Length - indexSecond - 2 );
                        if ( Char.IsDigit( tempVal1 [ 0 ] ) ) Dividend = Convert.ToDouble( tempVal1 ); // Property assignment
                        if ( Char.IsDigit( tempVal2 [ 0 ] ) ) DividendYield = Convert.ToDouble( tempVal2 ); // Property assignment
                    }

                }
                indexFirst = secondCol [ 1 ].IndexOf( " ", 2 );
                if ( indexFirst != -1 )
                {
                    tempVal1 = secondCol [ 1 ].Substring( indexFirst + 1, secondCol [ 1 ].Length - indexFirst - 1 ).Replace( "*", "" );
                }

                digits = 0;
                foreach ( char c in tempVal1 )
                {
                    if ( Char.IsDigit( c ) ) ++digits;
                }
                if ( digits == 0 )
                {
                    EarningsPerShare = null;
                }
                else
                {
                    EarningsPerShare = Convert.ToDouble( tempVal1 ); // Property assignment
                }
                indexFirst = secondCol [ 2 ].IndexOf( " ", 2 );
                tempVal1 = secondCol [ 2 ].Substring( indexFirst + 1 ).Replace( " ", "" );
                Shares = tempVal1.Contains( "M" ) // Property assignment
                    ? Convert.ToDouble( tempVal1.Substring( 0, tempVal1.Length - 1 ) ) * 1000000
                    : tempVal1.Contains( "B" ) ? Convert.ToDouble( tempVal1.Substring( 0, tempVal1.Length - 1 ) ) * 1000000
                    : Convert.ToDouble( tempVal1.Substring( 0, tempVal1.Length - 1 ) ) * 1000000;
                indexFirst = secondCol [ 3 ].IndexOf( " ", 2 );
                tempVal1 = secondCol [ 3 ].Substring( indexFirst + 1 ).Replace( " ", "" );
                digits = 0;
                foreach ( char c in tempVal1 )
                {
                    if ( Char.IsDigit( c ) ) ++digits;
                }
                if ( digits == 0 )
                {
                    Beta = null; // Property assignment
                }
                else
                {
                    Beta = Convert.ToDouble( tempVal1 ); // Property assignment
                }
                indexFirst = secondCol [ 4 ].IndexOf( " ", 9 );
                tempVal1 = secondCol [ 4 ].Substring( indexFirst + 1 ).Replace( " ", "" );
                digits = 0;
                foreach ( char c in tempVal1 )
                {
                    if ( Char.IsDigit( c ) ) ++digits;
                }
                if ( digits == 0 )
                {
                    InstitutionalOwnership = null;
                }
                else
                {
                    InstitutionalOwnership = Convert.ToDouble( tempVal1.Replace( " ", "" ).Substring( 0, tempVal1.Length - 1 ) ) / 100.0; // Property assignment
                }
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
    }
}
