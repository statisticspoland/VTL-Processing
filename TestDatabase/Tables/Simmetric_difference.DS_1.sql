﻿CREATE TABLE [Simmetric_difference].[DS_1]
(
	[Id_1] INT NOT NULL,
	[Id_2] VARCHAR(32) NOT NULL,
	[Id_3] VARCHAR(32) NOT NULL,
	[Id_4] VARCHAR(32) NOT NULL,
	[Me_1] INT
	CONSTRAINT [PK_Simmetric_difference_DS_1] PRIMARY KEY CLUSTERED ([Id_1], [Id_2], [Id_3], [Id_4])
)
