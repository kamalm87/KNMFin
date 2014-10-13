CREATE TABLE [Sectors].[LinksSector] (
    [SectorID]  INT NOT NULL,
    [CompanyID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([SectorID] ASC, [CompanyID] ASC),
    FOREIGN KEY ([CompanyID]) REFERENCES [Company].[InfoCompany] ([ID]),
    FOREIGN KEY ([SectorID]) REFERENCES [Sectors].[InfoSector] ([ID])
);

