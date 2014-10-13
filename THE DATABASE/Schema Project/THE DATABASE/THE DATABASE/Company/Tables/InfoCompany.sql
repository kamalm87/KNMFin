CREATE TABLE [Company].[InfoCompany] (
    [ID]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (150) NOT NULL,
    [Ticker] VARCHAR (15)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

