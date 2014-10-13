CREATE TABLE [FS].[CompanyIncomeStatement] (
    [CompanyID]   INT NOT NULL,
    [StatementID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([CompanyID] ASC, [StatementID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID]),
    FOREIGN KEY ([StatementID]) REFERENCES [FS].[IncomeStatement] ([ID])
);

