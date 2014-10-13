CREATE TABLE [Lists].[LinksList] (
    [CompanyID] INT NOT NULL,
    [ListID]    INT NOT NULL,
    PRIMARY KEY CLUSTERED ([CompanyID] ASC, [ListID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID]),
    FOREIGN KEY ([ListID]) REFERENCES [Lists].[InfoList] ([ID])
);

