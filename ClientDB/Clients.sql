CREATE TABLE [dbo].[Clients]
(
	[ClientId] INT NOT NULL PRIMARY KEY, 
    [ClientSecret] VARCHAR(50) NULL, 
    [Scopes] VARCHAR(MAX) NULL
)
GO




