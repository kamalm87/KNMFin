CREATE TABLE [StockReturn].[InfoStockReturn] (
    [ID]         INT             IDENTITY (1, 1) NOT NULL,
    [StockClose] DECIMAL (14, 6) NULL,
    [Volume]     DECIMAL (14, 6) NULL,
    [AdjClose]   DECIMAL (14, 6) NULL,
    [Low]        DECIMAL (14, 6) NULL,
    [High]       DECIMAL (14, 6) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

