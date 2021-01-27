CREATE PROCEDURE [Sample_standard_deviation].RestoreData AS
	DELETE FROM [Sample_standard_deviation].DS_1

	INSERT INTO [Sample_standard_deviation].DS_1 VALUES (2011, 'A', 'XX', 3)
	INSERT INTO [Sample_standard_deviation].DS_1 VALUES (2011, 'A', 'YY', 5)
	INSERT INTO [Sample_standard_deviation].DS_1 VALUES (2011, 'B', 'YY', 7)
	INSERT INTO [Sample_standard_deviation].DS_1 VALUES (2012, 'A', 'XX', 2)
	INSERT INTO [Sample_standard_deviation].DS_1 VALUES (2012, 'B', 'YY', 4)
GO