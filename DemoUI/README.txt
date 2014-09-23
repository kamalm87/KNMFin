( LIMITED IMPLEMENTED FUNCTIONALITY )

To test a query: 
	1. Enter a stock ticker in the textbox to the right of the 'Add a Ticker' label.
	2. Hit the 'Enter' key or click the 'Add' button. -> Ticker should now be in the listbox underneath 'Tickers to Query'
	3. (Optional) Select a Date Range with DateTimePickers next to 'Beginning Date' and 'Ending Date' 
	4. Repeat 1-2 if you desire to query multiple tickers.
	5. Hit the 'Query button' on the lower left-hand corner.
	6. A successful query process will place each 'Tickers to Query' item into the 'Queried Tickers' listbox.
	7. Selecting a ticker from the 'Queried Tickers' listbox will populate the tabs on the right with associated data.
To review queried data:
	Genera:
		- 'EXCEL' button:  
			- Select a filename for an .xlsx file containing the historical price quotes for each ticker in the 'Queried Tickers' listbox, if such data exists  		
	Google Tab:
		- Company Info: Displays summary info
		- Financial Statements: 
			- B/S, I/S SocF tabs: Select a Finanical Statement to Display
			- Excel: BS: Name a .xlsx file which will contain Balance Sheet data
			- Excel: IS: Name a .xlsx file which will contain Income Statement data
			- Excel: SoCF: Name a .xlsx file which will contain Statement of Cash Flow data
			- Excel: All Statements: Will create the above 3 .xlsx files in the current users desktop. The files will be named [Ticker]_[BS/IS/SoCF]
	Yahoo Tab:
		- Historical Prices 
			- Contains a datagrid containing the queried hsitorical prices (sluggish performance if a it's a large date range)
		- Company Quotation ( NOT IMPLEMENTED )

		
