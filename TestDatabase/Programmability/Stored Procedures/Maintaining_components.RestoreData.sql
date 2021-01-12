CREATE PROCEDURE [Maintaining_components].RestoreData AS
	DELETE FROM [Maintaining_components].DS_1

	INSERT INTO [Maintaining_components].DS_1 VALUES (2010, 'A', 'XX', 20, 36, 'E')
	INSERT INTO [Maintaining_components].DS_1 VALUES (2010, 'A', 'YY', 4, 9, 'F')
	INSERT INTO [Maintaining_components].DS_1 VALUES (2010, 'B', 'XX', 9, 10, 'F')
GO