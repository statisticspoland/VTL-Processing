CREATE PROCEDURE [Removal_of_components].RestoreData AS
	DELETE FROM [Removal_of_components].DS_1

	INSERT INTO [Removal_of_components].DS_1 VALUES (2010, 'A', 'XX', 20, 'E')
	INSERT INTO [Removal_of_components].DS_1 VALUES (2010, 'A', 'YY', 4, 'F')
	INSERT INTO [Removal_of_components].DS_1 VALUES (2010, 'B', 'XX', 9, 'F')
GO