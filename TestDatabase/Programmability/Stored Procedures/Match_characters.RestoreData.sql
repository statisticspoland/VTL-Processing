CREATE PROCEDURE [Match_characters].RestoreData AS
	DELETE FROM [Match_characters].DS_1

	INSERT INTO [Match_characters].DS_1 VALUES ('G', 'Total', 'Percentage', 'Total', 'AX123')
	INSERT INTO [Match_characters].DS_1 VALUES ('R', 'Total', 'Percentage', 'Total', 'AX2J5')
GO