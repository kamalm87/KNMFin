CREATE TABLE [FS].[CompanyBalanceSheet] (
    [CompanyID]   INT NOT NULL,
    [StatementID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([CompanyID] ASC, [StatementID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID]),
    FOREIGN KEY ([StatementID]) REFERENCES [FS].[BalanceSheet] ([ID])
);

