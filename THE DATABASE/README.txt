Notes: Backup database is ~100MB and too large for github, need to link it externally 	
Folder information:
* DATABASE: Raw MSSQL backup file with inserted values extracted from Google Finance ~(10,0000 unique companies) 
	- todo: ensure deployability of the database and sanitize duplicate entries
* Schema Project: Visual Studio project/raw sql files detailing schema/table definitions
* Web Scrape to Database Project: Contains a (rough) script read from a hard-coded input file to populate databse values
	- NoteS: HARDCODED VALUES
	- TODO: Make portable, modularize, magic.