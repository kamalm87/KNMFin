CREATE TABLE [Users].[InfoUsers] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (15) NOT NULL,
    [Password] NVARCHAR (15) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

