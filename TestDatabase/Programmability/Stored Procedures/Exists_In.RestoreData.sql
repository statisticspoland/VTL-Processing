CREATE PROCEDURE [Exists_In].RestoreData AS
	DELETE FROM [Exists_In].DS_1
	DELETE FROM [Exists_In].DS_2

	INSERT INTO [Exists_In].DS_1 VALUES (2012, 'B', 'Total', 'Total', 11094850)
	INSERT INTO [Exists_In].DS_1 VALUES (2012, 'G', 'Total', 'Total', 11123034)
	INSERT INTO [Exists_In].DS_1 VALUES (2012, 'S', 'Total', 'Total', 46818219)
	INSERT INTO [Exists_In].DS_1 VALUES (2012, 'M', 'Total', 'Total', 417546)
	INSERT INTO [Exists_In].DS_1 VALUES (2012, 'F', 'Total', 'Total', 5401267)
	INSERT INTO [Exists_In].DS_1 VALUES (2012, 'W', 'Total', 'Total', 7954662)

	INSERT INTO [Exists_In].DS_2 VALUES (2012, 'B', 'Total', 'Total', 0.023)
	INSERT INTO [Exists_In].DS_2 VALUES (2012, 'G', 'Total', 'M', 0.286)
	INSERT INTO [Exists_In].DS_2 VALUES (2012, 'S', 'Total', 'Total', 0.064)
	INSERT INTO [Exists_In].DS_2 VALUES (2012, 'M', 'Total', 'M', 0.043)
	INSERT INTO [Exists_In].DS_2 VALUES (2012, 'F', 'Total', 'Total', NULL)
	INSERT INTO [Exists_In].DS_2 VALUES (2012, 'W', 'Total', 'Total', 0.08)
GO