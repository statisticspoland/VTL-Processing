CREATE PROCEDURE [Between].RestoreData AS
	DELETE FROM [Between].DS_1

	INSERT INTO [Between].DS_1 VALUES ('G', 'Total', 'Percentage', 'Total', 6)
	INSERT INTO [Between].DS_1 VALUES ('R', 'Total', 'Percentage', 'Total', -2)
GO