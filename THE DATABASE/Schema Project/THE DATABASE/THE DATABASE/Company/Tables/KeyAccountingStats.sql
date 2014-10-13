CREATE TABLE [Company].[KeyAccountingStats] (
    [CompanyID]             INT             NOT NULL,
    [Period]                DATE            NOT NULL,
    [Annual]                BIT             NOT NULL,
    [NetProfitMargin]       DECIMAL (20, 6) NULL,
    [OperatingMargin]       DECIMAL (20, 6) NULL,
    [EBITDMargin]           DECIMAL (20, 6) NULL,
    [ReturnOnAverageAssets] DECIMAL (20, 6) NULL,
    [ReturnOnAverageEquity] DECIMAL (20, 6) NULL,
    [Employees]             INT             NULL,
    [CDPScoreN]             INT             NULL,
    [CDPScoreL]             VARCHAR (30)    NULL,
    PRIMARY KEY CLUSTERED ([CompanyID] ASC, [Period] ASC, [Annual] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID])
);

