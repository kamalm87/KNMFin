CREATE TABLE [Users].[Portfolio] (
    [UserID]    INT NOT NULL,
    [CompanyID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC, [CompanyID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID]),
    FOREIGN KEY ([UserID]) REFERENCES [Users].[InfoUsers] ([ID])
);

