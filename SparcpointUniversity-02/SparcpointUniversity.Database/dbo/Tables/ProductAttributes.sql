CREATE TABLE [dbo].[ProductAttributes]
(
	[ProductId] INT NOT NULL,
	[Key] VARCHAR(64) NOT NULL,
	[Value] VARCHAR(MAX) NULL, 

    CONSTRAINT [PK_ProductAttributes] PRIMARY KEY ([ProductId], [Key]), 
    CONSTRAINT [FK_ProductAttributes_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products]([ProductId])
)
