﻿CREATE PROCEDURE [First_value].RestoreData AS
	DELETE FROM [First_value].DS_1

	INSERT INTO [First_value].DS_1 VALUES ('A', 'XX', 1993, 3, 1)
	INSERT INTO [First_value].DS_1 VALUES ('A', 'XX', 1994, 4, 9)
	INSERT INTO [First_value].DS_1 VALUES ('A', 'XX', 1995, 7, 5)
	INSERT INTO [First_value].DS_1 VALUES ('A', 'XX', 1996, 6, 8)
	INSERT INTO [First_value].DS_1 VALUES ('A', 'YY', 1993, 9, 3)
	INSERT INTO [First_value].DS_1 VALUES ('A', 'YY', 1994, 5, 4)
	INSERT INTO [First_value].DS_1 VALUES ('A', 'YY', 1995, 10, 2)
	INSERT INTO [First_value].DS_1 VALUES ('A', 'YY', 1996, 2, 7)
GO