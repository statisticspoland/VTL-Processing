CREATE PROCEDURE [Minimum_value].RestoreData AS
	DELETE FROM [Minimum_value].DS_1

	INSERT INTO [Minimum_value].DS_1 VALUES (2011, 'A', 'XX', 3)
	INSERT INTO [Minimum_value].DS_1 VALUES (2011, 'A', 'YY', 5)
	INSERT INTO [Minimum_value].DS_1 VALUES (2011, 'B', 'YY', 7)
	INSERT INTO [Minimum_value].DS_1 VALUES (2012, 'A', 'XX', 2)
	INSERT INTO [Minimum_value].DS_1 VALUES (2012, 'B', 'YY', 4)
GO