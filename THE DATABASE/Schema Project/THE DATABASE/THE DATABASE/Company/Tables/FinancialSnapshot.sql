CREATE TABLE [Company].[FinancialSnapshot] (
    [QueryDate]              DATE            NOT NULL,
    [CompanyID]              INT             NOT NULL,
    [Beta]                   DECIMAL (5, 2)  NULL,
    [ClosePrice]             DECIMAL (14, 6) NULL,
    [OpenPrice]              DECIMAL (14, 6) NULL,
    [Dividend]               DECIMAL (14, 6) NULL,
    [DividendYield]          DECIMAL (14, 6) NULL,
    [EarningsPerShare]       DECIMAL (14, 6) NULL,
    [FiftyTwoWeekLow]        DECIMAL (14, 6) NULL,
    [FiftyTwoWeekHigh]       DECIMAL (14, 6) NULL,
    [InstitutionalOwnership] DECIMAL (14, 6) NULL,
    [MarketCap]              DECIMAL (23, 6) NULL,
    [PriceEarnings]          DECIMAL (14, 6) NULL,
    [RangeLow]               DECIMAL (14, 6) NULL,
    [RangeHigh]              DECIMAL (14, 6) NULL,
    [Shares]                 DECIMAL (23, 6) NULL,
    [VolumeAverage]          DECIMAL (14, 6) NULL,
    [VolumeDaily]            DECIMAL (14, 6) NULL,
    PRIMARY KEY CLUSTERED ([QueryDate] ASC, [CompanyID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID])
);

