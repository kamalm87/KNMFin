* ExcelGoogle
	- SaveBalanceSheets
		* parameters: filename, KNMFin.Google.CompanyInfo
		* returns: (void) saves .xlsx file containing queried balance sheet data associated with the companyinfo							
	- SaveIncomeStatements
		* parameters: filename, KNMFin.Google.CompanyInfo
		* returns: (void) saves .xlsx file containing queried income statement data associated with the companyinfo
	- SaveCashFlowStatement
		* parameters: filename, KNMFin.Google.CompanyInfo
		* returns: (void) saves .xlsx file containing queried cash flow statement data associated with the companyinfo

* ExcelYahoo
	- SaveToExcel
		* parameters: filename, list of stockpriceresults
		* returns: (void) saves .xlsx file with associated historical returns for each ticker
