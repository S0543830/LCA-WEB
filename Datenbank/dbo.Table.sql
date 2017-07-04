CREATE TABLE [dbo].[Products] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NULL,
    [Durability]     FLOAT (53)     NOT NULL,
    [TotalWeight]    FLOAT (53)     NOT NULL,
    [DateOfCreation] DATETIME       NOT NULL,
    [DateOfChanging] DATETIME       NOT NULL,
    [CreatedBy]      NVARCHAR (MAX) NULL,
    [ChangeBy]       NVARCHAR (MAX) NULL,
    [Typ_Id]         INT            NULL,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Products_dbo.ProductTypes_Typ_Id] FOREIGN KEY ([Typ_Id]) REFERENCES [dbo].[ProductTypes] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Typ_Id]
    ON [dbo].[Products]([Typ_Id] ASC);

