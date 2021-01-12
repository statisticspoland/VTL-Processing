CREATE PROCEDURE [Equal_to].RestoreData AS
	DELETE FROM [Equal_to].DS_1

	INSERT INTO [Equal_to].DS_1 VALUES (2012, 'B', 'Total', 'Total', NULL)
	INSERT INTO [Equal_to].DS_1 VALUES (2012, 'G', 'Total', 'Total', 0.286)
	INSERT INTO [Equal_to].DS_1 VALUES (2012, 'S', 'Total', 'Total', 0.064)
	INSERT INTO [Equal_to].DS_1 VALUES (2012, 'M', 'Total', 'Total', 0.043)
	INSERT INTO [Equal_to].DS_1 VALUES (2012, 'F', 'Total', 'Total', 0.08)
	INSERT INTO [Equal_to].DS_1 VALUES (2012, 'W', 'Total', 'Total', 0.08)
GO