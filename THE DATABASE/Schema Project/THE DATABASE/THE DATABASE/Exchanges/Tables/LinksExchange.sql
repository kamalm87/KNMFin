CREATE TABLE [Exchanges].[LinksExchange] (
    [ExchangeID] INT NOT NULL,
    [CompanyID]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ExchangeID] ASC, [CompanyID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID]),
    FOREIGN KEY ([ExchangeID]) REFERENCES [Exchanges].[InfoExchange] ([ID])
);

