CREATE PROCEDURE [Time_aggregation].RestoreData AS
	DELETE FROM [Time_aggregation].DS_1

	INSERT INTO [Time_aggregation].DS_1 VALUES ('2010Q1', 'A', 20)
	INSERT INTO [Time_aggregation].DS_1 VALUES ('2010Q2', 'A', 20)
	INSERT INTO [Time_aggregation].DS_1 VALUES ('2010Q3', 'A', 20)
	INSERT INTO [Time_aggregation].DS_1 VALUES ('2010Q1', 'B', 50)
	INSERT INTO [Time_aggregation].DS_1 VALUES ('2010Q2', 'B', 50)
	INSERT INTO [Time_aggregation].DS_1 VALUES ('2010Q1', 'C', 10)
	INSERT INTO [Time_aggregation].DS_1 VALUES ('2010Q2', 'C', 10)
GO