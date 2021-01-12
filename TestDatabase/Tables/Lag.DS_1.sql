﻿CREATE TABLE [Lag].[DS_1]
(
	[Id_1] VARCHAR(32) NOT NULL,
	[Id_2] VARCHAR(32) NOT NULL,
	[Id_3] INT NOT NULL,
	[Me_1] INT,
	[Me_2] INT
	CONSTRAINT [PK_Lag_DS_1] PRIMARY KEY CLUSTERED ([Id_1], [Id_2], [Id_3])
)
