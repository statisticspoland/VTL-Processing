CREATE PROCEDURE [Intersection].RestoreData AS
	DELETE FROM [Intersection].DS_1
	DELETE FROM [Intersection].DS_2

	INSERT INTO [Intersection].DS_1 VALUES (2012, 'B', 'Total', 'Total', 1)
	INSERT INTO [Intersection].DS_1 VALUES (2012, 'G', 'Total', 'Total', 2)
	INSERT INTO [Intersection].DS_1 VALUES (2012, 'F', 'Total', 'Total', 3)

	INSERT INTO [Intersection].DS_2 VALUES (2011, 'B', 'Total', 'Total', 10)
	INSERT INTO [Intersection].DS_2 VALUES (2012, 'G', 'Total', 'Total', 2)
	INSERT INTO [Intersection].DS_2 VALUES (2011, 'M', 'Total', 'Total', 40)
GO