﻿CREATE TABLE [Join].[DS_2]
(
	[Id_1] INT NOT NULL,
	[Id_2] VARCHAR(32) NOT NULL,
	[Me_1] VARCHAR(32),
	[Me_2] VARCHAR(32),
	CONSTRAINT [PK_Join_DS_2] PRIMARY KEY CLUSTERED ([Id_1], [Id_2])
)
