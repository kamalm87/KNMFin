CREATE TABLE [FS].[CompanyCashFlowStatement] (
    [CompanyID]   INT NOT NULL,
    [StatementID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([CompanyID] ASC, [StatementID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID]),
    FOREIGN KEY ([StatementID]) REFERENCES [FS].[StatementOfCashFlow] ([ID])
);

