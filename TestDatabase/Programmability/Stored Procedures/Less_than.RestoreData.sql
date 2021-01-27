CREATE PROCEDURE [Less_than].RestoreData AS
	DELETE FROM [Less_than].DS_1

	INSERT INTO [Less_than].DS_1 VALUES (2012, 'B', 'Total', 'Total', 11094850)
	INSERT INTO [Less_than].DS_1 VALUES (2012, 'G', 'Total', 'Total', 11123034)
	INSERT INTO [Less_than].DS_1 VALUES (2012, 'S', 'Total', 'Total', 46818219)
	INSERT INTO [Less_than].DS_1 VALUES (2012, 'M', 'Total', 'Total', NULL)
	INSERT INTO [Less_than].DS_1 VALUES (2012, 'F', 'Total', 'Total', 5401267)
	INSERT INTO [Less_than].DS_1 VALUES (2012, 'W', 'Total', 'Total', 7954662)
GO