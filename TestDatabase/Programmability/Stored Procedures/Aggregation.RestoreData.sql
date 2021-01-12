CREATE PROCEDURE [Aggregation].RestoreData AS
	DELETE FROM [Aggregation].DS_1

	INSERT INTO [Aggregation].DS_1 VALUES (1, 'A', 'XX', 0)
	INSERT INTO [Aggregation].DS_1 VALUES (1, 'A', 'YY', 2)
	INSERT INTO [Aggregation].DS_1 VALUES (1, 'B', 'XX', 3)
	INSERT INTO [Aggregation].DS_1 VALUES (1, 'B', 'YY', 5)
	INSERT INTO [Aggregation].DS_1 VALUES (2, 'A', 'XX', 7)
	INSERT INTO [Aggregation].DS_1 VALUES (2, 'A', 'YY', 2)
GO