﻿CREATE TABLE [Multiplication].[DS_2]
(
	[Id_1] INT NOT NULL,
	[Id_2] VARCHAR(32) NOT NULL,
	[Me_1] INT,
	[Me_2] DECIMAL(28,9),
	CONSTRAINT [PK_Multiplication_DS_2] PRIMARY KEY CLUSTERED ([Id_1], [Id_2])
)