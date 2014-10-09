using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNMFin.Yahoo
{
    namespace Aggregates
    {
        public enum ResultType
        {
            Text,
            Number,
            Percentage,
            Range,
            RangePercentage,
            TruncuatedCurrency,
            Date,
            Pair,
            Currency
        }
        
        // To classify the AggregateResult type, if distinction is necessary
        public enum AggregateType
        {
            Company,
            Industry,
            Sector
        }
        
        // The various columns that an Aggregate query row may contain
        public sealed class AggregateProperty
        {
            public ResultType Type;
            public readonly String Description, Name, Value;
            public static HashSet<AggregateProperty> All;

            public static readonly AggregateProperty OneDayPriceChange = new AggregateProperty( "1 Day Price Change (%)", "OneDayPriceChange", "1-Day Price Chg %", ResultType.Percentage );
            public static readonly AggregateProperty ReturnOnEquity = new AggregateProperty( "Return on Equity", "ReturnOnEquity", "ROE %", ResultType.Percentage );
            public static readonly AggregateProperty DividendYield = new AggregateProperty( "Dividend Yield", "DividendYield", "Div. Yield %", ResultType.Percentage );
            public static readonly AggregateProperty NetProfit = new AggregateProperty( "Net Profit (mrq)", "NetProfit", "Net Profit Margin (mrq)", ResultType.Currency );
            public static readonly AggregateProperty PriceOverEarnings = new AggregateProperty( "P/E", "PriceOverEarnings", "P/E", ResultType.Number );
            public static readonly AggregateProperty DebtToEquity = new AggregateProperty( "Debt to Equity", "DebtToEquity", "Debt to Equity", ResultType.Number );
            public static readonly AggregateProperty PriceToBook = new AggregateProperty( "Price to Book", "PriceToBook", "Price to Book", ResultType.Number );
            public static readonly AggregateProperty PriceToFreeCashFlow = new AggregateProperty( "Price to Free Cash Flow (mrq)", "Price to Free Cash Flow (mrq)", "Price To Free Cash Flow (mrq)", ResultType.Number );
            public static readonly AggregateProperty MarketCap = new AggregateProperty( "Market Cap", "MarketCap", "Market Cap", ResultType.Currency );
            private AggregateProperty( string d, string n, string v, ResultType type )
            {
                if ( All == null ) All = new HashSet<AggregateProperty>( );
                Description = d;
                Name = n;
                Value = v;
                Type = type;
                All.Add( this );
            }
        }
        
        // Contains all the publically queryable Industry options. 
        // In addition, contains HashSets of industries belonging to a Sector:
        // Sectors: Basic Materials, Conglomerates, Consumer Goods, Financials, 
        //          Healthcare, Industrial Goods, Services, Technology, Utilities
        public sealed class Industry
        {
            public int Value;
            public string Description;

            private Industry( string des, int val )
            {
                if ( All == null ) All = new HashSet<Industry>( );
                if ( BasicMaterials == null ) BasicMaterials = new HashSet<Industry>( );
                // Single industry sector  if ( Conglomerates == null ) ConsumerGoods = new HashSet<Industry>( );
                if ( ConsumerGoods == null ) ConsumerGoods = new HashSet<Industry>( );
                if ( Finanical == null ) Finanical = new HashSet<Industry>( );
                if ( Healthcare == null ) Healthcare = new HashSet<Industry>( );
                if ( IndustrialGoods == null ) IndustrialGoods = new HashSet<Industry>( );
                if ( Services == null ) Services = new HashSet<Industry>( );
                if ( Technology == null ) Technology = new HashSet<Industry>( );
                if ( Utilities == null ) Utilities = new HashSet<Industry>( );
                this.Description = des;
                this.Value = val;
                All.Add( this );




                if ( 100 <= Value && Value < 200 )
                    BasicMaterials.Add( this );

                /* Single industry Sector
                if ( 200 <= Value && Value < 300 )
                    Conglomerates.Add( this );
                */

                if ( 300 <= Value && Value < 400 )
                    ConsumerGoods.Add( this );

                if ( 400 <= Value && Value < 500 )
                    Finanical.Add( this );

                if ( 500 <= Value && Value < 600 )
                    Healthcare.Add( this );

                if ( 600 <= Value && Value < 700 )
                    IndustrialGoods.Add( this );

                if ( 700 <= Value && Value < 800 )
                    Services.Add( this );

                if ( 800 <= Value && Value < 900 )
                    Technology.Add( this );

                if ( 900 <= Value && Value < 1000 )
                    Utilities.Add( this );
            }
            private Industry() { }

            public static HashSet<Industry> All;

            // Basic Materials Industries
            public static HashSet<Industry> BasicMaterials;
            static public readonly Industry ChemicalsMajorDiversified = new Industry( "Chemicals Major Diversified", 110 );
            static public readonly Industry Synthetics = new Industry( "Synthetics", 111 );
            static public readonly Industry ArgiculturalChemicals = new Industry( "Argicultural Chemicals", 112 );
            static public readonly Industry SpecialtyChemicals = new Industry( "Specialty Chemicals", 113 );
            static public readonly Industry MajorIntegratedOilAndGas = new Industry( "Major Integrated Oil and Gas", 120 );
            static public readonly Industry IndpendentOilAndGas = new Industry( "Indpendent Oil and Gas", 121 );
            static public readonly Industry OilAndGasRefiningAndMarketing = new Industry( "Oil and Gas: Refining and Marketing", 122 );
            static public readonly Industry OilAndGasDrillingAndExploration = new Industry( "Oil and Gas: Drilling and Exploration", 123 );
            static public readonly Industry OilAndGasEquipmentAndServices = new Industry( "Oil and Gas: Equipment and Services", 124 );
            static public readonly Industry OilAndGasPipelines = new Industry( "Oil and Gas: Pipelines", 125 );
            static public readonly Industry SteelAndIron = new Industry( "Steel and Iron", 130 );
            static public readonly Industry Copper = new Industry( "Copper", 131 );
            static public readonly Industry Aluminum = new Industry( "Aluminum", 132 );
            static public readonly Industry IndustrialMetalsAndMinerals = new Industry( "Industrial: Metals and Minerals", 133 );
            static public readonly Industry Gold = new Industry( "Gold", 134 );
            static public readonly Industry Silver = new Industry( "Silver", 135 );
            static public readonly Industry NonMetallicMineralMining = new Industry( "Non-Metallic Mineral Mining", 136 );

            // Consumer Goods Industries
            // Note: this is a single industry sector, so there is no hashset for industries
            static public readonly Industry Conglomerates = new Industry( "Conglomerates", 210 );

            // Consumer Goods Industries
            public static HashSet<Industry> ConsumerGoods;
            static public readonly Industry Appliances = new Industry( "Appliances", 310 );
            static public readonly Industry HomeFurnishingsAndFixtures = new Industry( "Home Furnishings and Fixtures", 311 );
            static public readonly Industry HousewaresAndAccessories = new Industry( "Housewares and Accessories", 312 );
            static public readonly Industry BusinessEquipment = new Industry( "Business Equipment", 313 );
            static public readonly Industry ElectronicEquipment = new Industry( "Electronic Equipment", 314 );
            static public readonly Industry ToysAndGames = new Industry( "Toys and Games", 315 );
            static public readonly Industry SportingGoods = new Industry( "SportingGoods", 316 );
            static public readonly Industry RecreationalGoodsOther = new Industry( "Recreational Goods, Other", 317 );
            static public readonly Industry PhotographicEquipmentAndSupplies = new Industry( "Photographic Equipment and Supplies", 318 );
            static public readonly Industry TextileApparelClothing = new Industry( "Textile: Apparel, Clothing", 320 );
            static public readonly Industry TextileApparelFootwearAndAccessories = new Industry( "Textile: Apparel,Footwear, and Accessories", 321 );
            static public readonly Industry RubberAndPlastics = new Industry( "Rubber and Plastics", 322 );
            static public readonly Industry PersonalProducts = new Industry( "Personal Products", 323 );
            static public readonly Industry PaperAndPaperProducts = new Industry( "Paper and Paper Products", 324 );
            static public readonly Industry PackagingAndContainers = new Industry( "Packaging and Containers", 325 );
            static public readonly Industry CleaningProducts = new Industry( "Cleaning Products", 326 );
            static public readonly Industry OfficeSupplies = new Industry( "Office Supplies", 327 );
            static public readonly Industry AutoManufacturersMajor = new Industry( "Auto Manufacturers: Major", 330 );
            static public readonly Industry TrucksAndOtherVehicles = new Industry( "Trucks and OtherVehicles", 331 );
            static public readonly Industry RecreationalVehicles = new Industry( "Recreational Vehicles", 332 );
            static public readonly Industry AutoParts = new Industry( "Auto Parts", 333 );
            static public readonly Industry FoodMajorDiversified = new Industry( "Food: Major, Diversified", 340 );
            static public readonly Industry FarmProducts = new Industry( "Farm Products", 341 );
            static public readonly Industry ProcessedAndPackagedGoods = new Industry( "Processed and Packaged Goods", 342 );
            static public readonly Industry MeatProducts = new Industry( "Meat Products", 343 );
            static public readonly Industry DairyProducts = new Industry( "Dairy Products", 344 );
            static public readonly Industry Confectioners = new Industry( "Confectioners", 345 );
            static public readonly Industry BeveragesBrewers = new Industry( "Beverages: Brewers", 346 );
            static public readonly Industry BeveragesWineriesAndDistillers = new Industry( "Beverages: Wineries and Distillers", 347 );
            static public readonly Industry BeveragesSoftDrinks = new Industry( "Beverages: Soft Drinks", 348 );
            static public readonly Industry Cigarettes = new Industry( "Cigarettes", 350 );
            static public readonly Industry TobaccoProductsOther = new Industry( "Tobacco Products: Other", 351 );

            // Finanical Industries
            public static HashSet<Industry> Finanical;
            static public readonly Industry MoneyCenterBanks = new Industry( "Money Center Banks", 410 );
            static public readonly Industry RegionalNortheastBanks = new Industry( "Regional: Northeast Banks", 411 );
            static public readonly Industry RegionalMidAtlanticBanks = new Industry( "Regional: Mid-Atlantic Banks", 412 );
            static public readonly Industry RegionalSoutheastBanks = new Industry( "Regional: Southeast Banks", 413 );
            static public readonly Industry RegionalMidwestBanks = new Industry( "Regional:  Midwest Banks", 414 );
            static public readonly Industry RegionalSouthwestBanks = new Industry( "Regional: Southwest Banks", 415 );
            static public readonly Industry RegionalPacificBanks = new Industry( "Regional: Pacific Banks", 416 );
            static public readonly Industry ForeignMoneyCenterBanks = new Industry( "Foreign Money Center Banks", 417 );
            static public readonly Industry ForeignRegionalBanks = new Industry( "Foreign Regional Banks", 418 );
            static public readonly Industry SavingsAndLoans = new Industry( "Savings and Loans", 419 );
            static public readonly Industry InvestmentBrokerageNational = new Industry( "Investment Brokerage: National", 420 );
            static public readonly Industry InvestmentBrokerageRegional = new Industry( "Investment Brokerage: Regional", 421 );
            static public readonly Industry AssetManagement = new Industry( "Asset Management", 422 );
            static public readonly Industry DiversifiedInvestments = new Industry( "Diversified Investments", 423 );
            static public readonly Industry CreditServices = new Industry( "Credit Services", 424 );
            static public readonly Industry ClosedEndFundDebt = new Industry( "Closed-End Fund: Debt", 425 );
            static public readonly Industry ClosedEndFundEquity = new Industry( "Closed-End Fund: Equity", 426 );
            static public readonly Industry ClosedEndFundForeign = new Industry( "Closed-End Fund: Foreign", 437 );
            static public readonly Industry LifeInsurance = new Industry( "Life Insurance", 430 );
            static public readonly Industry AccidentAndHealthInsurance = new Industry( "Accident and Health Insurance", 431 );
            static public readonly Industry PropertyAndCasualtyInsurance = new Industry( "Property and Casualty Insurance", 432 );
            static public readonly Industry SuretyAndTitleInsurance = new Industry( "Surety and Title Insurance", 433 );
            static public readonly Industry InsuranceBrokers = new Industry( "Insurance Brokers", 434 );
            static public readonly Industry REITDiversiied = new Industry( "REIT: Diversified", 440 );
            static public readonly Industry REITOffice = new Industry( "REIT: Office", 441 );
            static public readonly Industry REITHealthcareFacilities = new Industry( "REIT: Healthcare Facilities", 442 );
            static public readonly Industry REITHotelMotel = new Industry( "REIT: Hotel/Motel", 443 );
            static public readonly Industry REITIndustrial = new Industry( "REIT: Industrial", 444 );
            static public readonly Industry REITResidential = new Industry( "REIT: Residential", 445 );
            static public readonly Industry REITRetail = new Industry( "REIT: Retail", 446 );
            static public readonly Industry MortgateInvestment = new Industry( "Mortgage Investment", 447 );
            static public readonly Industry PropertyManagement = new Industry( "Property Management", 448 );
            static public readonly Industry RealEstateDevelopment = new Industry( "Real Estate Development", 449 );

            // Healthcare Industries
            public static HashSet<Industry> Healthcare;
            static public readonly Industry DrugManufacturersMajor = new Industry( "Drug Manufacturers: Major", 510 );
            static public readonly Industry DrugManufacturersOther = new Industry( "Drug Manufacturers: Other", 511 );
            static public readonly Industry DrugsGeneric = new Industry( "Drugs: Generic", 512 );
            static public readonly Industry DrugDelivery = new Industry( "Drug Delivery", 513 );
            static public readonly Industry DrugRelatedProducts = new Industry( "Drug Related Products", 514 );
            static public readonly Industry Biotechnology = new Industry( "Biotechnology", 515 );
            static public readonly Industry DiagnosticSubstances = new Industry( "Diagnostic Substances", 516 );
            static public readonly Industry MedicalInstrumentsAndSupplies = new Industry( "Medical Instruments and Supplies", 520 );
            static public readonly Industry MedicalAppliancesAndEquipment = new Industry( "Medical Appliances and Equipment", 521 );
            static public readonly Industry HealthcarePlans = new Industry( "Healthcare Plans", 522 );
            static public readonly Industry LongTermCareFacilities = new Industry( "Long-Term Care Facilities", 523 );
            static public readonly Industry Hospitals = new Industry( "Hospitals", 524 );
            static public readonly Industry MedicalLaboratoriesAndResearch = new Industry( "Medical Laboratories and Research", 525 );
            static public readonly Industry HomeHealthcare = new Industry( "Home Healthcare", 526 );
            static public readonly Industry MedicalPractitioners = new Industry( "Medical Practitioners", 527 );
            static public readonly Industry SpecializedHealthServices = new Industry( "Specialized Health Services", 528 );

            // Industrial Goods Industries
            public static HashSet<Industry> IndustrialGoods;
            static public readonly Industry AerospaceDefenseMajorDiversified = new Industry( "Aerospace Defense: Major, Diversified", 610 );
            static public readonly Industry AerospaceDefenseProductsAndServices = new Industry( "Aerospace Defense: Products and Services", 611 );
            static public readonly Industry FarmAndConstructionMachinery = new Industry( "Farm and Construction Machinery", 620 );
            static public readonly Industry IndustrialEquipmentAndComponents = new Industry( "Industrial Equipment and Components", 621 );
            static public readonly Industry DiversifiedMachinery = new Industry( "Diversified Machinery", 622 );
            static public readonly Industry PollutionAndTreatmentControls = new Industry( "Pollution and Treatment Controls", 623 );
            static public readonly Industry MachineToolsAndAccessories = new Industry( "Machine Tools and Accessories", 624 );
            static public readonly Industry SmallToolsAndAccessories = new Industry( "Small Tools and Accessories", 625 );
            static public readonly Industry MetalFabrication = new Industry( "Metal Fabrication", 626 );
            static public readonly Industry IndustrialElectricalEquipment = new Industry( "Industrial Electrical Equipment", 627 );
            static public readonly Industry TextileIndustrial = new Industry( "Textile: Industrial", 628 );
            static public readonly Industry ResidentialConstruction = new Industry( "Residential Construction", 630 );
            static public readonly Industry ManufacturedHousing = new Industry( "Manufactured Housing", 631 );
            static public readonly Industry LumberWoodProduction = new Industry( "Lumber/Wood Production", 632 );
            static public readonly Industry Cement = new Industry( "Cement", 633 );
            static public readonly Industry GeneralBuildingMaterials = new Industry( "General Building Materials", 634 );
            static public readonly Industry HeavyConstruction = new Industry( "Heavy Construction", 635 );
            static public readonly Industry GeneralContractors = new Industry( "General Contractors", 636 );
            static public readonly Industry WasteManagement = new Industry( "Waste Management", 637 );

            // Services Industries
            public static HashSet<Industry> Services;
            static public readonly Industry Lodging = new Industry( "Lodging", 710 );
            static public readonly Industry ResortsAndCasinos = new Industry( "Resorts and Casinos", 711 );
            static public readonly Industry Restaurants = new Industry( "Restaurants", 712 );
            static public readonly Industry SpecialtyEateries = new Industry( "Specialty Eateries", 713 );
            static public readonly Industry GamingActivities = new Industry( "Gaming Activities", 714 );
            static public readonly Industry SportingActivities = new Industry( "Sporting Activities", 715 );
            static public readonly Industry GeneralEntertainment = new Industry( "General Entertainment", 716 );
            static public readonly Industry AdvertisingAgencies = new Industry( "Advertising Agencies", 720 );
            static public readonly Industry MarketingServices = new Industry( "Marketing Services ", 721 );
            static public readonly Industry EntertainmentDiversified = new Industry( "Entertainment: Diversified", 722 );
            static public readonly Industry BroadcastingTV = new Industry( "Broadcasting: TV", 723 );
            static public readonly Industry BroadcastingRadio = new Industry( "Broadcasting: Radio", 724 );
            static public readonly Industry CATVSystems = new Industry( "CATV Systems", 725 );
            static public readonly Industry MovieProductionTheaters = new Industry( "Movie Production Theaters", 726 );
            static public readonly Industry PublishingNewspapers = new Industry( "Publishing: Newspapers", 727 );
            static public readonly Industry PublishingPeriodicals = new Industry( "Publishing: Periodicals", 728 );
            static public readonly Industry PublishingBooks = new Industry( "Publishing: Books", 729 );
            static public readonly Industry ApparelStores = new Industry( "Apparel Stores", 730 );
            static public readonly Industry DepartmentStores = new Industry( "Department Stores", 731 );
            static public readonly Industry DiscountVarietyStores = new Industry( "Discount Variety Stores", 732 );
            static public readonly Industry DrugStores = new Industry( "Drug Stores", 733 );
            static public readonly Industry GroceryStores = new Industry( "Grocery Stores", 734 );
            static public readonly Industry ElectronicsStores = new Industry( "Electronics Stores", 735 );
            static public readonly Industry HomeImprovementStores = new Industry( "Home Improvement Stores", 736 );
            static public readonly Industry HomeFurnishingStores = new Industry( "Home Furnishing Stores", 737 );
            static public readonly Industry AutoPartsStores = new Industry( "Auto Parts Stores", 738 );
            static public readonly Industry CatalogAndMailOrderHouses = new Industry( "Catalog and Mail Order Houses", 739 );
            static public readonly Industry SportingGoodsStores = new Industry( "Sporting Goods Stores", 740 );
            static public readonly Industry ToyAndHobbyStores = new Industry( "Toy and Hobby Stores", 741 );
            static public readonly Industry JewelryStores = new Industry( "Jewelry Stores", 742 );
            static public readonly Industry MusicAndVideoStores = new Industry( "Music and Video Stores", 743 );
            static public readonly Industry AutoDealerships = new Industry( "Auto Dealerships", 744 );
            static public readonly Industry SpecialtyRetailOther = new Industry( "Specialty Retail Other", 745 );
            static public readonly Industry AutoPartsWholesale = new Industry( "Auto Parts Wholesale", 750 );
            static public readonly Industry BuildingMaterialsWholesale = new Industry( "Building Materials Wholesale", 751 );
            static public readonly Industry IndustrialEquipmentWholesale = new Industry( "Industrial Equipment Wholesale", 752 );
            static public readonly Industry ElectronicsWholesale = new Industry( "Electronics Wholesale", 753 );
            static public readonly Industry MedicalEquipmentWholesale = new Industry( "Medical Equipment Wholesale", 754 );
            static public readonly Industry ComputersWholesale = new Industry( "Computers Wholesale", 755 );
            static public readonly Industry DrugsWholesale = new Industry( "Drugs Wholesale", 756 );
            static public readonly Industry FoodWholesale = new Industry( "Food Wholesale", 757 );
            static public readonly Industry BasicMaterialsWholesale = new Industry( "Basic Materials Wholesale", 758 );
            static public readonly Industry WholesaleOther = new Industry( "Wholesale Other", 759 );
            static public readonly Industry BusinessServices = new Industry( "Business Services", 760 );
            static public readonly Industry RentalAndLeasingServices = new Industry( "Rental and Leasing Services", 761 );
            static public readonly Industry PersonalServices = new Industry( "Personal Services", 762 );
            static public readonly Industry ConsumerServices = new Industry( "Consumer Services", 763 );
            static public readonly Industry StaffingAndOutsourcingServices = new Industry( "Staffing and Outsourcing Services", 764 );
            static public readonly Industry SecurityAndProtectionServices = new Industry( "Security and Protection Services", 765 );
            static public readonly Industry EducationAndTrainingServices = new Industry( "Education and Training Services", 766 );
            static public readonly Industry TechnicalServices = new Industry( "Technical Services", 767 );
            static public readonly Industry ResearchServices = new Industry( "Research Services", 768 );
            static public readonly Industry ManagementServices = new Industry( "Management Services", 769 );
            static public readonly Industry MajorAirlines = new Industry( "Major Airlines", 770 );
            static public readonly Industry RegionalAirlines = new Industry( "Regional Airlines", 771 );
            static public readonly Industry AirServicesOther = new Industry( "Air Services Other", 772 );
            static public readonly Industry AirDeliveryAndFreightServices = new Industry( "Air Delivery and Freight Services", 773 );
            static public readonly Industry Trucking = new Industry( "Trucking", 774 );
            static public readonly Industry Shipping = new Industry( "Shipping", 775 );
            static public readonly Industry Railroads = new Industry( "Railroads", 776 );

            // Technology Sector Industries
            public static HashSet<Industry> Technology;
            static public readonly Industry DiversifiedComputerSystems = new Industry( "Diversified Computer Systems", 810 );
            static public readonly Industry PersonalComputers = new Industry( "Personal Computers", 811 );
            static public readonly Industry ComputerBasedSystems = new Industry( "Computer Based Systems", 812 );
            static public readonly Industry DataStorageDevices = new Industry( "Data Storage Devices", 813 );
            static public readonly Industry NetworkingAndCommunicationsDevices = new Industry( "Networking and Communications Devices", 814 );
            static public readonly Industry ComputerPeripherals = new Industry( "Computer Peripherals", 815 );
            static public readonly Industry MultimediaAndGraphicsSoftware = new Industry( "Multimedia and Graphics Software", 820 );
            static public readonly Industry ApplicationSoftware = new Industry( "Application Software", 821 );
            static public readonly Industry TechnicalAndSystemSoftware = new Industry( "Technical and System Software", 822 );
            static public readonly Industry SecuritySoftwareAndServices = new Industry( "Security Software and Services", 823 );
            static public readonly Industry InformationTechnologyServices = new Industry( "Information Technology Services", 824 );
            static public readonly Industry HealthcareInformationServices = new Industry( "Healthcare Information Services", 825 );
            static public readonly Industry BusinessSoftwareAndServices = new Industry( "Business Software and Services", 826 );
            static public readonly Industry InformationAndDeliveryServices = new Industry( "Information and Delivery Services", 827 );
            static public readonly Industry SemiconductorBroadLine = new Industry( "Semiconductor: Broad Line", 830 );
            static public readonly Industry SemiconductorMemoryChips = new Industry( "Semiconductor: Memory Chips", 831 );
            static public readonly Industry SemiconductorSpecialized = new Industry( "Semiconductor: Specialized", 832 );
            static public readonly Industry SemiconductorIntegratedCircuits = new Industry( "Semiconductor: Integrated Circuits", 833 );
            static public readonly Industry SemiconductorEquipmentAndMaterials = new Industry( "Semiconductor: Equipment and Materials", 834 );
            static public readonly Industry PrintedCircuitBoards = new Industry( "Printed Circuit Boards", 835 );
            static public readonly Industry DiversifiedElectronics = new Industry( "Diversified Electronics", 836 );
            static public readonly Industry ScientificAndTechnicalInstruments = new Industry( "Scientific and Technical Instruments", 837 );
            static public readonly Industry WirelessCommunications = new Industry( "Wireless Communications", 840 );
            static public readonly Industry CommunicationEquipment = new Industry( "Communication Equipment", 841 );
            static public readonly Industry ProcessingSystemsAndProducts = new Industry( "Processing Systems and Products", 842 );
            static public readonly Industry LongDistanceCarriers = new Industry( "Long Distance Carriers", 843 );
            static public readonly Industry TelecomServicesDomestic = new Industry( "Telecom Services: Domestic", 844 );
            static public readonly Industry TelecomServicesForeign = new Industry( "Telecom Services: Foreign", 845 );
            static public readonly Industry DiversifiedCommunicationServices = new Industry( "Diversified Communication Services", 846 );
            static public readonly Industry InternetServiceProviders = new Industry( "Internet Service Providers_", 850 );
            static public readonly Industry InternetInformationProviders = new Industry( "Internet Information Providers ", 851 );
            static public readonly Industry InternetSoftwareandServices = new Industry( "Internet Software and Services", 852 );

            // Utility Sector Industries
            public static HashSet<Industry> Utilities;
            static public readonly Industry ForeignUtilities = new Industry( "Foreign/Utilities", 910 );
            static public readonly Industry ElectricUtilities = new Industry( "Electric: Utilities", 911 );
            static public readonly Industry GasUtilities = new Industry( "Gas/Utilities", 912 );
            static public readonly Industry DiversifiedUtilities = new Industry( "Diversified Utilities", 913 );
            static public readonly Industry WaterUtilities = new Industry( "Water/Utilities", 914 );
        }
        
        // struct is represent an AggregateResult to a query, 
        // where Companies contains the speciic query result rows
        public struct AggregateResult
        {
            public Result Sector, Industry;
            public Industry IndustryInfo;
            public HashSet<Result> Companies;
            public HashSet<String> CompanyNames;
            public AggregateResult( Dictionary<string, Dictionary<String, double?>> results, Industry industry = null )
            {
                Sector = new Result( );
                Industry = new Result( );
                Companies = new HashSet<Result>( );
                CompanyNames = new HashSet<string>( );
                IndustryInfo = industry;
                int i = 0;
                foreach ( KeyValuePair<string, Dictionary<string, double?>> kvp in results )
                {
                    if ( i == 0 )
                    {
                        Sector = new Result( kvp.Key, kvp.Value, AggregateType.Sector );
                        i++;
                        if ( kvp.Key == "Conglomerates" ) // special case sector with no industry
                        {
                            Industry = Sector;
                            i++;
                        }
                    }
                    else if ( i == 1 )
                    {
                        Industry = new Result( kvp.Key, kvp.Value, AggregateType.Industry );
                        i++;
                    }
                    else
                    {
                        var r = new Result( kvp.Key, kvp.Value, AggregateType.Company );
                        Companies.Add( r );
                        CompanyNames.Add( r.Name );
                    }
                }
            }
        }
    
        // Represents a result row in an aggregate query
        // Values contains the results, other variables represent row information
        public struct Result
        {
            public String Name;
            public AggregateType Type;
            public Dictionary<AggregateProperty, double?> Values;

            public Result( string name, Dictionary<string, double?> results, AggregateType type )
            {
                Values = new Dictionary<AggregateProperty, double?>( );
                Name = name;
                Type = type;
                foreach ( KeyValuePair<string, double?> kvp in results )
                {
                    var ip = AggregateProperty.All.Where( i => i.Value == kvp.Key ).FirstOrDefault( );
                    Values.Add( ip, kvp.Value );
                }
            }
        }
        
        // Class to make an aggregate query with
        // Can query: 
        //          1) All of the sectors for sector-specific aggregate infromation
        //          2) A specific industry, for company-specific aggregate information
        //          3) A collection of industries, returning a list of industry specific
        //             aggregates, each containing company-specific aggregate infromation 
        public static class Aggregates
        {

            public static List<AggregateResult> Industries( List<Industry> industries )
            {
                var results = new List<AggregateResult>( );
                foreach ( Industry i in industries )
                {
                    results.Add( Industry( i ) );
                }
                return results;
            }


            public static AggregateResult Sectors()
            {
                string requestURL = baseURL + "s_conameu" + endURL;

                string qResult = string.Empty;
                try
                {
                    qResult = client.DownloadString( requestURL );
                }
                catch
                {
                    return new AggregateResult();
                }

                string [] stringSeparator = new string [] { "\n" };
                var resultLines = qResult.Split( stringSeparator, StringSplitOptions.RemoveEmptyEntries );


                string [] columns = resultLines [ 0 ].Split( ',' );

                string [] cols = new string [ columns.Length - 1 ];
                for ( int i = 1; i < columns.Length; i++ )
                    cols [ i - 1 ] = columns [ i ].Replace( "\\", "" ).Replace( "\"", "" );

                Dictionary<string, Dictionary<string, double?>> results = new Dictionary<string, Dictionary<string, double?>>( );

                for ( int i = 1; i < resultLines.Length - 1; i++ )
                {

                    var rows = resultLines [ i ].Split( ',' );
                    Dictionary<string, double?> industryData = new Dictionary<string, double?>( );

                    // % formatted cases:
                    //                   * 1-Day Price Chg %   
                    //                   * ROE %
                    //                   * Div. Yield %
                    //                   * Net Profit Margin (mrq)
                    industryData.Add( cols [ 0 ], Convert.ToDouble( rows [ 1 ] ) / 100 );
                    industryData.Add( cols [ 3 ], Convert.ToDouble( rows [ 4 ] ) / 100 );
                    industryData.Add( cols [ 4 ], Convert.ToDouble( rows [ 5 ] ) / 100 );
                    industryData.Add( cols [ 7 ], Convert.ToDouble( rows [ 8 ] ) / 100 );

                    // standard numeric cases:
                    //                        * P/E
                    //                        * Debt to Equity
                    //                        * Price to Book
                    //                        * Price To Free Cash Flow (mrq)
                    industryData.Add( cols [ 2 ], Convert.ToDouble( rows [ 3 ] ) );
                    industryData.Add( cols [ 5 ], Convert.ToDouble( rows [ 6 ] ) );
                    industryData.Add( cols [ 6 ], Convert.ToDouble( rows [ 7 ] ) );
                    industryData.Add( cols [ 8 ], Convert.ToDouble( rows [ 9 ] ) );

                    // special numeric case:
                    //                      * Market Cap
                    //                          - contains 'B" to represent a billion
                    string tempValue = rows [ 2 ].Substring( 0, rows [ 2 ].Length - 1 );
                    industryData.Add( cols [ 1 ], Convert.ToDouble( tempValue ) * billion );

                    results.Add( rows [ 0 ], industryData );
                }
                var res =  new AggregateResult( results );
                foreach ( Result r in res.Companies )
                {
                    var t = r;
                    t.Type = AggregateType.Sector;
                }
                return res;
            }


            public static AggregateResult Industry( Industry industry )
            {
                string requestURL = baseURL + industry.Value.ToString( ) + "coname" + 'u' + endURL;
                string qResult = string.Empty;

                try
                { // attempt a request... 
                    qResult = client.DownloadString( requestURL );
                }
                catch
                { // if the request returns nothing, then the function will terminate here, returning an empty container
                    return new AggregateResult( );
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

                    var rows = resultLines [ i ].Replace( "\"", "" ).Replace( "\\", "" ).Split( ',' );

                    // offset is used to 'offset' company names that corrupt the expected format of the csv response
                    // , by setting this value to the number of csv 'columns' that a given iteration's
                    // result exceeds the expected amount
                    if ( rows.Length > 10 ) offset = rows.Length - 10;
                    else
                        offset = 0;

                    Dictionary<string, double?> industryData = new Dictionary<string, double?>( );

                    // % formatted cases: (1) 1-Day Price Chg % (2) ROE % (3) Div. Yield % (4) Net Profit Margin (mrq)
                    industryData.Add( cols [ 0 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 1 + offset ] != "NA" ? Convert.ToDouble( rows [ 1 + offset ] ) / 100 : default( double? ) );
                    industryData.Add( cols [ 3 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 4 + offset ] != "NA" ? Convert.ToDouble( rows [ 4 + offset ] ) / 100 : default( double? ) );
                    industryData.Add( cols [ 4 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 5 + offset ] != "NA" ? Convert.ToDouble( rows [ 5 + offset ] ) / 100 : default( double? ) );
                    industryData.Add( cols [ 7 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 8 + offset ] != "NA" ? Convert.ToDouble( rows [ 8 + offset ] ) / 100 : default( double? ) );
                    // standard numeric cases: (1) P/E (2) Debt to Equity (3) Price to Book (4) Price To Free Cash Flow (mrq)
                    industryData.Add( cols [ 2 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 3 + offset ] != "NA" ? Convert.ToDouble( rows [ 3 + offset ] ) : default( double? ) );
                    industryData.Add( cols [ 5 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 6 + offset ] != "NA" ? Convert.ToDouble( rows [ 6 + offset ] ) : default( double? ) );
                    industryData.Add( cols [ 6 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 7 + offset ] != "NA" ? Convert.ToDouble( rows [ 7 + offset ] ) : default( double? ) );
                    industryData.Add( cols [ 8 ].Replace( "\"", "" ).Replace( "\\", "" ), rows [ 9 + offset ] != "NA" ? Convert.ToDouble( rows [ 9 + offset ] ) : default( double? ) );
                    // special numeric case: * Market Cap -- contains 'B" to represent a billion
                    if ( rows [ 2 ] != "NA" )
                    {
                        string tempValue = rows [ 2 + offset ].Substring( 0, rows [ 2 + offset ].Length - 1 );
                        if ( tempValue != "N" )
                            industryData.Add( cols [ 1 ], rows [ 2 ].Contains( "B" ) ? Convert.ToDouble( tempValue ) * billion : Convert.ToDouble( tempValue ) * million );
                    }
                    else
                    {
                        industryData.Add( cols [ 1 ], default( double? ) );
                    }

                    if ( offset == 0 )
                    {
                        if ( !results.ContainsKey( rows [ 0 ] ) )
                        {
                            results.Add( rows [ 0 ], industryData );
                        }
                    }
                    else
                    {
                        StringBuilder mysteryCompanyName = new StringBuilder( );
                        for ( int j = 0; j < offset; j++ )
                            mysteryCompanyName.Append( rows [ j ] );
                        if ( !results.ContainsKey( mysteryCompanyName.ToString( ) ) )
                            results.Add( mysteryCompanyName.ToString( ), industryData );
                    }
                }
                return new AggregateResult( results, industry );
            }

            // Internal request components
            private static readonly string baseURL = @"http://biz.yahoo.com/p/csv/";
            private static readonly string endURL = @".csv";
            private static System.Net.WebClient client = new System.Net.WebClient( );
            private static readonly double? million = 1000000;
            private static readonly double? billion = 1000000000;
            private static readonly double? trillion = 1000000000000;
        }
    }
}
