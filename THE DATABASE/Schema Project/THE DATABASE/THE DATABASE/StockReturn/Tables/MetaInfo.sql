CREATE TABLE [StockReturn].[MetaInfo] (
    [ID]       INT  NOT NULL,
    [RangeBeg] DATE NULL,
    [RangeEnd] DATE NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([ID]) REFERENCES [Company].[InfoCompany] ([ID])
);

