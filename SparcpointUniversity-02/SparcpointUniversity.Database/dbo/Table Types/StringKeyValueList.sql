CREATE TYPE [dbo].[StringKeyValueList] AS TABLE
(
	[Key] VARCHAR(64) NOT NULL,
	[Value] VARCHAR(MAX) NULL
)
