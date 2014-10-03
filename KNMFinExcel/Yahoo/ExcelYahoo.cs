using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using KNMFin.Yahoo.HistoricalQuotes;

namespace KNMFinExcel.Yahoo
{
    
    public static class ExcelYahoo
    {
        public static void SaveToExcel( string fileName, IList<StockPriceResult> sprs )
        {
            System.IO.FileInfo fi = new System.IO.FileInfo( fileName );
            pck = new ExcelPackage( fi );

            foreach ( StockPriceResult spr in sprs )
            {
                CreateCompanyStockPriceResultsTab( spr );
            }
            pck.Save( );
            pck = null;
        }

        public static void SaveMarketQuotes(string fileName, Dictionary<string, Dictionary<string, string>> RawParsedData) {
            var fi = new System.IO.FileInfo(fileName);
            pck = new ExcelPackage(fi);
            var ws = pck.Workbook.Worksheets.Add("Results");
            
            var headers = RawParsedData.Values.FirstOrDefault().Keys;
            int i = 2, j = 2;
            foreach(string s in headers){
                ws.Cells[1, i++ ].Value = s;
            }
            
            i = 2;
            foreach ( KeyValuePair<string, Dictionary<string, string>> vals in RawParsedData ) {
                
                ws.Cells[j,1].Value = vals.Key;
                var data = vals.Value;
                foreach ( KeyValuePair<string, string> innerVals in data )
                {
                    ws.Cells [ j, i].Value = innerVals.Value;
                    i++;
                }
                i = 2;
                j++;
            }

            pck.Save( );
            pck = null;
        }


        static void CreateCompanyStockPriceResultsTab( StockPriceResult spr )
        {
            string ticker = spr.Ticker.ToUpper( );
            
            var ws = pck.Workbook.Worksheets.Add( ticker );
            ws.Cells [ "A1" ].Value = "Date";
            ws.Cells [ "B1" ].Value = "Open";
            ws.Cells [ "C1" ].Value = "High";
            ws.Cells [ "D1" ].Value = "Low";
            ws.Cells [ "E1" ].Value = "Close";
            ws.Cells [ "F1" ].Value = "Volume";
            ws.Cells [ "G1" ].Value = "Adj Close";

            var results = spr.StockPriceInformation.StockPriceInformation;
            int rowNumber = 2;
            foreach ( StockPriceRow row in results )
            {
                ws.Cells [ rowNumber, 1 ].Value = row.Date;
                ws.Cells [ rowNumber, 2 ].Value = row.Open;
                ws.Cells [ rowNumber, 3 ].Value = row.High;
                ws.Cells [ rowNumber, 4 ].Value = row.Low;
                ws.Cells [ rowNumber, 5 ].Value = row.Close;
                ws.Cells [ rowNumber, 6 ].Value = row.Volume;
                ws.Cells [ rowNumber, 7 ].Value = row.AdjClose;
                rowNumber++;
            }
            
        }
        static ExcelPackage pck;
    }
}
