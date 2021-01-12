CREATE PROCEDURE [Rounding].RestoreData AS
	DELETE FROM [Rounding].DS_1

	INSERT INTO [Rounding].DS_1 VALUES (10, 'A', 7.5, 5.9)
	INSERT INTO [Rounding].DS_1 VALUES (10, 'B', 7.1, 5.5)
	INSERT INTO [Rounding].DS_1 VALUES (11, 'A', 36.2, 17.7)
	INSERT INTO [Rounding].DS_1 VALUES (11, 'B', 44.5, 24.3)
GO