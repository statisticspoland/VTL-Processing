﻿CREATE TABLE [Exclusive_disjunction].[DS_1]
(
	[Id_1] VARCHAR(32) NOT NULL,
	[Id_2] INT NOT NULL,
	[Id_3] VARCHAR(32) NOT NULL,
	[Id_4] INT NOT NULL,
	[Me_1] BIT,
	CONSTRAINT [PK_Exclusive_disjunction_DS_1] PRIMARY KEY CLUSTERED ([Id_1], [Id_2], [Id_3], [Id_4])
)
