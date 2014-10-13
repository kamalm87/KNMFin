CREATE TABLE [StockReturn].[Limits] (
    [ID]          INT  NOT NULL,
    [FirstReturn] DATE NULL,
    [LastReturn]  DATE NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([ID]) REFERENCES [Company].[InfoCompany] ([ID])
);

