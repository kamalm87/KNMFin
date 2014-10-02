using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNMFin.Yahoo
{
    namespace Quotes
    {
        // QuoteProperties are used for Companies Quotations requests
        // For an arbitrary QuoteProperties object:
        // * variable name: what the QuoteProperties object represents
        //   (for more detail, see the description paramter (third) in the constructor)
        // * public Methods:
        //  - ToString: returns the name used in a request
        //  - GetDesription: returns the description used for data mapping
        public sealed class QuoteProperties
        {
            private readonly String description;
            private readonly String name;
            private readonly int value;

            public static readonly QuoteProperties AfterHoursChangeRealtime = new QuoteProperties( 1, "c8", "After Hours Change (Realtime)" );
            public static readonly QuoteProperties AnnualizedGain = new QuoteProperties( 2, "g3", "Annualized Gain" );
            public static readonly QuoteProperties Ask = new QuoteProperties( 3, "a0", "Ask" );
            public static readonly QuoteProperties AskRealtime = new QuoteProperties( 4, "b2", "Ask (Realtime)" );
            public static readonly QuoteProperties AskSize = new QuoteProperties( 5, "a5", "Ask Size" );
            public static readonly QuoteProperties AverageDailyVolume = new QuoteProperties( 6, "a2", "Average Daily Volume" );
            public static readonly QuoteProperties Bid = new QuoteProperties( 7, "b0", "Bid" );
            public static readonly QuoteProperties BidRealtime = new QuoteProperties( 8, "b3", "Bid (Realtime)" );
            public static readonly QuoteProperties BidSize = new QuoteProperties( 9, "b6", "Bid Size" );
            public static readonly QuoteProperties BookValuePerShare = new QuoteProperties( 10, "b4", "Book Value Per Share" );

            public static readonly QuoteProperties Change = new QuoteProperties( 11, "c1", "Change" );
            public static readonly QuoteProperties Change_ChangeInPercent = new QuoteProperties( 12, "c0", "Change Change In Percent" );
            public static readonly QuoteProperties ChangeFromFiftydayMovingAverage = new QuoteProperties( 13, "m7", "Change From Fiftyday Moving Average"  );
            public static readonly QuoteProperties ChangeFromTwoHundreddayMovingAverage = new QuoteProperties( 14, "m5", "Change From Two Hundredday Moving Average" );
            public static readonly QuoteProperties ChangeFromYearHigh = new QuoteProperties( 15, "k4", "Change From Year High" );
            public static readonly QuoteProperties ChangeFromYearLow = new QuoteProperties( 16, "j5", "Change From Year Low" );
            public static readonly QuoteProperties ChangeInPercent = new QuoteProperties( 17, "p2", "Change In Percent" );
            public static readonly QuoteProperties ChangeInPercentRealtime = new QuoteProperties( 18, "k2", "Change In Percent (Realtime)" );
            public static readonly QuoteProperties ChangeRealtime = new QuoteProperties( 19, "c6", "Change (Realtime)" );
            public static readonly QuoteProperties Commission = new QuoteProperties( 20, "c3", "Commission" );

            public static readonly QuoteProperties Currency = new QuoteProperties( 21, "c4", "Currency" );
            public static readonly QuoteProperties DaysHigh = new QuoteProperties( 22, "h0", "Days High" );
            public static readonly QuoteProperties DaysLow = new QuoteProperties( 23, "g0", "Days Low" );
            public static readonly QuoteProperties DaysRange = new QuoteProperties( 24, "m0", "Days Range" );
            public static readonly QuoteProperties DaysRangeRealtime = new QuoteProperties( 25, "m2", "Days Range (Realtime)" );
            public static readonly QuoteProperties DaysValueChange = new QuoteProperties( 26, "w1", "Days Value Change");
            public static readonly QuoteProperties DaysValueChangeRealtime = new QuoteProperties( 27, "w4", "Days Value Change (Realtime)" );
            public static readonly QuoteProperties DividendPayDate = new QuoteProperties( 28, "r1", "Dividend Pay Date" );
            public static readonly QuoteProperties TrailingAnnualDividendYield = new QuoteProperties( 29, "d0", "Trailing Annual Dividend Yield" );
            public static readonly QuoteProperties TrailingAnnualDividendYieldInPercent = new QuoteProperties( 30, "y0", "Trailing Annual Dividend Yield In Percent" );

            public static readonly QuoteProperties DilutedEPS = new QuoteProperties( 31, "e0", "Diluted E P S" );
            public static readonly QuoteProperties EBITDA = new QuoteProperties( 32, "j4", "E B I T D A" );
            public static readonly QuoteProperties EPSEstimateCurrentYear = new QuoteProperties( 33, "e7", "E P S Estimate Current Year" );
            public static readonly QuoteProperties EPSEstimateNextQuarter = new QuoteProperties( 34, "e9", "E P S Estimate Next Quarter" );
            public static readonly QuoteProperties EPSEstimateNextYear = new QuoteProperties( 35, "e8", "E P S Estimate Next Year" );
            public static readonly QuoteProperties ExDividendDate = new QuoteProperties( 36, "q0", "Ex Dividend Date" );
            public static readonly QuoteProperties FiftydayMovingAverage = new QuoteProperties( 37, "m3", "Fiftyday Moving Average" );
            
            public static readonly QuoteProperties HighLimit = new QuoteProperties( 39, "l2", "High Limit" );
            public static readonly QuoteProperties HoldingsGain = new QuoteProperties( 40, "g4", "Holdings Gain" );

            public static readonly QuoteProperties HoldingsGainPercent = new QuoteProperties( 41, "g1", "Holdings Gain Percent" );
            public static readonly QuoteProperties HoldingsGainPercentRealtime = new QuoteProperties( 42, "g5", "Holdings Gain Percent (Realtime)" );
            public static readonly QuoteProperties HoldingsGainRealtime = new QuoteProperties( 43, "g6", "Holdings Gain (Realtime)" );
            public static readonly QuoteProperties HoldingsValue = new QuoteProperties( 44, "v1", "Holdings Value" );
            public static readonly QuoteProperties HoldingsValueRealtime = new QuoteProperties( 45, "v7", "Holdings Value (Realtime)" );
            public static readonly QuoteProperties LastTradeDate = new QuoteProperties( 46, "d1", "Last Trade Date" );
            public static readonly QuoteProperties LastTradePriceOnly = new QuoteProperties( 47, "l1", "Last Trade Price Only" );
            public static readonly QuoteProperties LastTradeRealtimeWithTime = new QuoteProperties( 48, "k1", "Last Trade (Realtime) With Time"  );
            public static readonly QuoteProperties LastTradeSize = new QuoteProperties( 49, "k3", "Last Trade Size" );
            public static readonly QuoteProperties LastTradeTime = new QuoteProperties( 50, "t1", "Last Trade Time" );

            public static readonly QuoteProperties LastTradeWithTime = new QuoteProperties( 51, "l0", "Last Trade With Time" );
            public static readonly QuoteProperties LowLimit = new QuoteProperties( 52, "l3", "Low Limit" );
            public static readonly QuoteProperties MarketCapitalization = new QuoteProperties( 53, "j1", "Market Capitalization" );
            public static readonly QuoteProperties MarketCapRealtime = new QuoteProperties( 54, "j3", "Market Cap (Realtime)");
            public static readonly QuoteProperties MoreInfo = new QuoteProperties( 55, "i0", "More Info" );
            public static readonly QuoteProperties Name = new QuoteProperties( 56, "n0", "Name" );
            public static readonly QuoteProperties Notes = new QuoteProperties( 57, "n4", "Notes");
            public static readonly QuoteProperties OneyrTargetPrice = new QuoteProperties( 58, "t8", "Oneyr Target Price" );
            public static readonly QuoteProperties Open = new QuoteProperties( 59, "o0", "Open" );
            public static readonly QuoteProperties OrderBookRealtime = new QuoteProperties( 60, "i5", "Order Book (Realtime)" );

            public static readonly QuoteProperties PEGRatio = new QuoteProperties( 61, "r5", "P E G Ratio" );
            public static readonly QuoteProperties PERatio = new QuoteProperties( 62, "r0", "P E Ratio" );
            public static readonly QuoteProperties PERatioRealtime = new QuoteProperties( 63, "r2", "P E Ratio (Realtime)" );
            public static readonly QuoteProperties PercentChangeFromFiftydayMovingAverage = new QuoteProperties( 64, "m8", "Percent Change From Fiftyday Moving Average" );
            public static readonly QuoteProperties PercentChangeFromTwoHundreddayMovingAverage = new QuoteProperties( 65, "m6", "Percent Change From Two Hundredday Moving Average" );
            public static readonly QuoteProperties ChangeInPercentFromYearHigh = new QuoteProperties( 66, "k5", "Change In Percent From Year High" );
            public static readonly QuoteProperties PercentChangeFromYearLow = new QuoteProperties( 67, "j6", "Percent Change From Year Low" );
            public static readonly QuoteProperties PreviousClose = new QuoteProperties( 68, "p0", "Previous Close");
            public static readonly QuoteProperties PriceBook = new QuoteProperties( 69, "p6", "Price Book" );
            public static readonly QuoteProperties PriceEPSEstimateCurrentYear = new QuoteProperties( 70, "r6", "Price E P S Estimate Current Year" );

            public static readonly QuoteProperties PriceEPSEstimateNextYear = new QuoteProperties( 71, "r7", "Price E P S Estimate Next Year" );
            public static readonly QuoteProperties PricePaid = new QuoteProperties( 72, "p1", "Price Paid" );
            public static readonly QuoteProperties PriceSales = new QuoteProperties( 73, "p5", "Price Sales" );
            public static readonly QuoteProperties Revenue = new QuoteProperties( 74, "s6", "Revenue" );
            
            public static readonly QuoteProperties ShortRatio = new QuoteProperties( 77, "s7", "Short Ratio" );
            public static readonly QuoteProperties StockExchange = new QuoteProperties( 78, "x0", "Stock Exchange" );
            public static readonly QuoteProperties Symbol = new QuoteProperties( 79, "s0", "Symbol" );
            public static readonly QuoteProperties TickerTrend = new QuoteProperties( 80, "t7", "Ticker Trend" );

            public static readonly QuoteProperties TradeDate = new QuoteProperties( 81, "d2", "Trade Date" );
            
            public static readonly QuoteProperties TwoHundreddayMovingAverage = new QuoteProperties( 84, "m4", "Two Hundredday Moving Average" );
            public static readonly QuoteProperties Volume = new QuoteProperties( 85, "v0", "Volume" );
            public static readonly QuoteProperties YearHigh = new QuoteProperties( 86, "k0", "Year High"  );
            public static readonly QuoteProperties YearLow = new QuoteProperties( 87, "j0", "Year Low" );
            public static readonly QuoteProperties YearRange = new QuoteProperties( 88, "w0", "Year Range" );


            // TODO: Implement parsing/special cases
            // Inactive QuoteProperties: 
            // 38 - Shares Float
              public static readonly QuoteProperties SharesFloat = new QuoteProperties( 38, "f6", "Shares Float" ); 
             // 49 - Last Trade Size
             
             // 75 - Shares Owned
             public static readonly QuoteProperties SharesOwned = new QuoteProperties( 75, "s1", "Shares Owned" );
             // 76 - Shares Outstanding
             public static readonly QuoteProperties SharesOutstanding = new QuoteProperties( 76, "j2", "Shares Outstanding" );
             // 82 - Trade Links
             public static readonly QuoteProperties TradeLinks = new QuoteProperties( 82, "t6", "Trade Links" );
             // 83 - Trade Links Additional
             public static readonly QuoteProperties TradeLinksAdditional = new QuoteProperties( 83, "f0", "Trade Links Additional" );

            // These will be skipped
            public static HashSet<QuoteProperties> _SpecialCases = new HashSet<QuoteProperties> { SharesFloat, LastTradeSize, SharesOwned, SharesOutstanding, TradeLinks, TradeLinksAdditional };
            private QuoteProperties( int value, string name )
            {
                this.name = name;
                this.value = value;
            }

            private QuoteProperties( int value, string name, string desription )
            {
                if ( GetValueFromName == null ) GetValueFromName = new Dictionary<string, string>( );
                if ( GetNames == null ) GetNames = new HashSet<string>( );
                if ( SetOfAll == null ) SetOfAll = new HashSet<QuoteProperties>( );
                this.value = value;
                this.name = name;
                this.description = desription;
                GetValueFromName.Add( this.description, this.name );
                GetNames.Add( this.description );
                SetOfAll.Add( this );
            }

            public override string ToString()
            {
                return name;
            }

            public string GetDesription()
            {
                return description;
            }



            public static string[] GetAllNames() {
                string[] ret = GetNames.ToArray<string>();
                Array.Sort(ret);
                return ret;
            }

            public static Dictionary<string, string> GetAllNameValuePairs()
            {
                return GetValueFromName;
            }

            public static HashSet<QuoteProperties> SetOfAll { get; set; }
            static HashSet<string> GetNames { get; set; }
            static Dictionary<string, string> GetValueFromName{ get; set; }

        }

        
        // MarketProperties are used for Sectors and Industries related requests
        // - Neither of these requests offer granulated queries using theese,
        //   they're only for selecting a parameter to sort the response by.
        //   (Currently unimplemented -- these only exist to facilitate future extension/modification) 
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
    }
}
