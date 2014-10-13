CREATE TABLE [StockReturn].[LinksStockReturn] (
    [ReturnDate] DATE NOT NULL,
    [CompanyID]  INT  NOT NULL,
    [ReturnID]   INT  NOT NULL,
    PRIMARY KEY CLUSTERED ([CompanyID] ASC, [ReturnDate] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID]),
    FOREIGN KEY ([ReturnID]) REFERENCES [StockReturn].[InfoStockReturn] ([ID])
);

