CREATE PROCEDURE [Union].RestoreData AS
	DELETE FROM [Union].DS_1
	DELETE FROM [Union].DS_2A
	DELETE FROM [Union].DS_2B

	INSERT INTO [Union].DS_1 VALUES (2012, 'B', 'Total', 'Total', 5)
	INSERT INTO [Union].DS_1 VALUES (2012, 'G', 'Total', 'Total', 2)
	INSERT INTO [Union].DS_1 VALUES (2012, 'F', 'Total', 'Total', 3)

	INSERT INTO [Union].DS_2A VALUES (2012, 'N', 'Total', 'Total', 23)
	INSERT INTO [Union].DS_2A VALUES (2012, 'S', 'Total', 'Total', 5)

	INSERT INTO [Union].DS_2B VALUES (2012, 'B', 'Total', 'Total', 23)
	INSERT INTO [Union].DS_2B VALUES (2012, 'S', 'Total', 'Total', 5)
GO