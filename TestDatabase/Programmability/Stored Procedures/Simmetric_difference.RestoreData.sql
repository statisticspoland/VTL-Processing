CREATE PROCEDURE [Simmetric_difference].RestoreData AS
	DELETE FROM [Simmetric_difference].DS_1
	DELETE FROM [Simmetric_difference].DS_2

	INSERT INTO [Simmetric_difference].DS_1 VALUES (2012, 'B', 'Total', 'Total', 1)
	INSERT INTO [Simmetric_difference].DS_1 VALUES (2012, 'G', 'Total', 'Total', 2)
	INSERT INTO [Simmetric_difference].DS_1 VALUES (2012, 'F', 'Total', 'Total', 3)
	INSERT INTO [Simmetric_difference].DS_1 VALUES (2012, 'M', 'Total', 'Total', 4)
	INSERT INTO [Simmetric_difference].DS_1 VALUES (2012, 'I', 'Total', 'Total', 5)
	INSERT INTO [Simmetric_difference].DS_1 VALUES (2012, 'S', 'Total', 'Total', 6)

	INSERT INTO [Simmetric_difference].DS_2 VALUES (2011, 'B', 'Total', 'Total', 1)
	INSERT INTO [Simmetric_difference].DS_2 VALUES (2012, 'G', 'Total', 'Total', 2)
	INSERT INTO [Simmetric_difference].DS_2 VALUES (2012, 'F', 'Total', 'Total', 3)
	INSERT INTO [Simmetric_difference].DS_2 VALUES (2012, 'M', 'Total', 'Total', 4)
	INSERT INTO [Simmetric_difference].DS_2 VALUES (2012, 'I', 'Total', 'Total', 5)
	INSERT INTO [Simmetric_difference].DS_2 VALUES (2012, 'S', 'Total', 'Total', 6)
GO