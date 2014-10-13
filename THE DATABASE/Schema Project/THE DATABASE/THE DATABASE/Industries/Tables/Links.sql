CREATE TABLE [Industries].[Links] (
    [IndustryID] INT NOT NULL,
    [CompanyID]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IndustryID] ASC, [CompanyID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID]),
    FOREIGN KEY ([IndustryID]) REFERENCES [Industries].[InfoIndustry] ([ID])
);

