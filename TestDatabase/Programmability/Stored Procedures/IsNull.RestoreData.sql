CREATE PROCEDURE [IsNull].RestoreData AS
	DELETE FROM [IsNull].DS_1

	INSERT INTO [IsNull].DS_1 VALUES (2012, 'B', 'Total', 'Total', 11094850)
	INSERT INTO [IsNull].DS_1 VALUES (2012, 'G', 'Total', 'Total', 11123034)
	INSERT INTO [IsNull].DS_1 VALUES (2012, 'S', 'Total', 'Total', NULL)
	INSERT INTO [IsNull].DS_1 VALUES (2012, 'M', 'Total', 'Total', 417546)
	INSERT INTO [IsNull].DS_1 VALUES (2012, 'F', 'Total', 'Total', 5401267)
	INSERT INTO [IsNull].DS_1 VALUES (2012, 'N', 'Total', 'Total', NULL)
GO