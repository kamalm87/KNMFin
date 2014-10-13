CREATE TABLE [Company].[Address] (
    [CompanyID]  INT           NOT NULL,
    [StreetName] NVARCHAR (40) NULL,
    [ZipCode]    VARCHAR (15)  NULL,
    [City]       NVARCHAR (40) NULL,
    [State]      NVARCHAR (5)  NULL,
    [Country]    NVARCHAR (40) NULL,
    [Phone]      VARCHAR (15)  NULL,
    [Fax]        VARCHAR (15)  NULL,
    PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID])
);

