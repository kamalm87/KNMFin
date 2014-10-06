using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNMFin.Yahoo
{
    namespace Industries
    {
        // These are used for Industry financial information requests
        // - The names represent user friendly names for queries
        // - The values are used in the url request
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

        // Class used to request financial information on a list of companies associated with a specified Industry
        public static class Quote
        {
            // Takes an Industry from the enum Industry and returns a mapping of 
            // ( Wraps the three parameter version with default response sorting parameters, which have no contextual meaning)
            public static Dictionary<string, Dictionary<string, double?>> IndustryQuery(KNMFin.Yahoo.Industries.Industry Industry)
            {
                return IndustryQuery( Industry, KNMFin.Yahoo.Quotes.MarketQuoteProperties.Name, Sectors.Sort.Up );
            }

            public static Dictionary<string, Dictionary<string, Dictionary<string, double?>>> IndustryQuery( IList<KNMFin.Yahoo.Industries.Industry> Industries )
            {
                var results = new Dictionary<string, Dictionary<string, Dictionary<string, double?>>>( );
                foreach ( Industry ind in Industries ){
                   results.Add( System.Enum.GetName( typeof(Industry), ind ), IndustryQuery( ind ) );
                }
                return results;
            }
            
            // (It is recommended to use the single parameter version -- 2nd and 3rd parameters are contextually meaningless)
            public static Dictionary<string, Dictionary<string, double?>> IndustryQuery( KNMFin.Yahoo.Industries.Industry Industry, KNMFin.Yahoo.Quotes.MarketQuoteProperties SortBy, KNMFin.Yahoo.Sectors.Sort SortDirection )
            {
                string requestURL = baseURL + (int)Industry + SortBy.ToString( ) + (char)SortDirection + endURL;
                string qResult = string.Empty;

                try{ // attempt a request... 
                    qResult = client.DownloadString( requestURL );
                }catch { // if the request returns nothing, then the function will terminate here, returning an empty container
                    return new Dictionary<string, Dictionary<string, double?>>( );
                }
                
                // split the raw response string into an array of strings, which were expected to be delimited by '\n'
                string [] stringSeparator = new string [] { "\n" };
                var resultLines = qResult.Split( stringSeparator, StringSplitOptions.RemoveEmptyEntries );
                
                // parse/sanitize the header columns
                string [] columns = resultLines [ 0 ].Split( ',' );
                string [] cols = new string [ columns.Length - 1 ];
                for ( int i = 1; i < columns.Length; i++ )
                    cols [ i - 1 ] = columns [ i ].Replace( "\\", "" ).Replace( "\"", "" );

                
                Dictionary<string, Dictionary<string, double?>> results = new Dictionary<string, Dictionary<string, double?>>( );
                int offset = 0;

                // Map each row, identified by the company name, which is expected to be either in row[0] or in row[0], row[1], ... row[n]
                // and each column with its respective identifying column header
                // - for the column -> data mappings, 
                for ( int i = 1; i < resultLines.Length - 1; i++ )
                {

                    var rows = resultLines [ i ].Replace("\"", "").Replace("\\", "").Split( ',' );

                    // offset is used to 'offset' company names that corrupt the expected format of the csv response
                    // , by setting this value to the number of csv 'columns' that a given iteration's
                    // result exceeds the expected amount
                    if ( rows.Length > 10 ) offset = rows.Length - 10;
                    else
                        offset = 0;
                    
                    Dictionary<string, double?> industryData = new Dictionary<string, double?>( );

                    // % formatted cases: (1) 1-Day Price Chg % (2) ROE % (3) Div. Yield % (4) Net Profit Margin (mrq)
                    industryData.Add( cols [ 0 ].Replace("\"","").Replace("\\", ""), rows [ 1 + offset ] != "NA" ? Convert.ToDouble( rows [ 1 + offset ] ) / 100 : default( double? ) );
                    industryData.Add( cols [ 3 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 4 + offset ] != "NA" ? Convert.ToDouble( rows [ 4 + offset ] ) / 100 : default( double? ) );
                    industryData.Add( cols [ 4 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 5 + offset ] != "NA" ? Convert.ToDouble( rows [ 5 + offset ] ) / 100 : default( double? ) );
                    industryData.Add( cols [ 7 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 8 + offset ] != "NA" ? Convert.ToDouble( rows [ 8 + offset ] ) / 100 : default( double? ) );
                    // standard numeric cases: (1) P/E (2) Debt to Equity (3) Price to Book (4) Price To Free Cash Flow (mrq)
                    industryData.Add( cols [ 2 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 3 + offset ] != "NA" ? Convert.ToDouble( rows [ 3 + offset ] ) : default( double? ) );
                    industryData.Add( cols [ 5 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 6 + offset ] != "NA" ? Convert.ToDouble( rows [ 6 + offset ] ) : default( double? ) );
                    industryData.Add( cols [ 6 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 7 + offset ] != "NA" ? Convert.ToDouble( rows [ 7 + offset ] ) : default( double? ) );
                    industryData.Add( cols [ 8 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 9 + offset ] != "NA" ? Convert.ToDouble( rows [ 9 + offset ] ) : default( double? ) );
                    // special numeric case: * Market Cap -- contains 'B" to represent a billion
                    if ( rows [ 2 ] != "NA" ){
                        string tempValue = rows [ 2 + offset ].Substring( 0, rows [ 2 + offset ].Length - 1 );
                        industryData.Add( cols [ 1 ], rows[2].Contains("B") ? Convert.ToDouble( tempValue ) * billion : Convert.ToDouble(tempValue) * million);
                    } else{
                        industryData.Add( cols [ 1 ], default(double?) );
                    }

                    if ( offset == 0 ){
                        if ( !results.ContainsKey( rows [ 0 ] ) ){
                            results.Add( rows [ 0 ], industryData );
                        }
                    } else {
                        StringBuilder mysteryCompanyName = new StringBuilder();
                        for(int j = 0; j < offset; j++)
                            mysteryCompanyName.Append(rows[j]);
                        if(!results.ContainsKey(mysteryCompanyName.ToString()))
                            results.Add( mysteryCompanyName.ToString(), industryData );
                    }
                }

                return results;
            }

            // Internal request components
            private static readonly string baseURL = @"http://biz.yahoo.com/p/csv/";
            private static readonly string endURL = @".csv";
            private static System.Net.WebClient client = new System.Net.WebClient( );
            private static readonly double? million = 1000000;
            private static readonly double? billion = 1000000000;
        }
    }
}
