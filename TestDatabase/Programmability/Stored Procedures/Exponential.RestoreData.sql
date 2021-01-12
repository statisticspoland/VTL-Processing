CREATE PROCEDURE [Exponential].RestoreData AS
	DELETE FROM [Exponential].DS_1

	INSERT INTO [Exponential].DS_1 VALUES (10, 'A', 5, 0.7545)
	INSERT INTO [Exponential].DS_1 VALUES (10, 'B', 8, 13.45)
	INSERT INTO [Exponential].DS_1 VALUES (11, 'A', 2, 1.87)
GO