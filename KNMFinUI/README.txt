Contains UI static classes, which create WPF controls displaying queried data, for Google Finance and Yahoo Finance.

	* YahooFinanceUI
		- IndividualHistoricalPriceResult(): 
			- paramters associated WPF window,
			KNMFin.Yahoo.HistoricalQuotes.STockPriceResult
			- returns a WPF control with a datagrid that displays
			the stock price results
	* GoogleFinanceUI
		
		- BalanceSheetCanvas(): given a KNFMFIn.Google.BalanceSheet
		object, returns a canvas displaying the object's values
		- IncomeStatementCanvas(): given a KNFMFIn.Google.BalanceSheet
		object, returns a canvas displaying the object's values
		- StatementOfCashFlowCanvas(): given a KNFMFIn.Google.BalanceSheet
		object, returns a canvas displaying the object's values		
			
