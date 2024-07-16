CREATE TABLE [dbo].[Fruit] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX)  NULL,
    [type]        NVARCHAR (MAX)  NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [Price]       DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Fruit] PRIMARY KEY CLUSTERED ([Id] ASC)
);

