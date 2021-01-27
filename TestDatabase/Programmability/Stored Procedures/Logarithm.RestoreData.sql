CREATE PROCEDURE [Logarithm].RestoreData AS
	DELETE FROM [Logarithm].DS_1

	INSERT INTO [Logarithm].DS_1 VALUES (10, 'A', 1024, 0.7545)
	INSERT INTO [Logarithm].DS_1 VALUES (10, 'B', 64, 13.45)
	INSERT INTO [Logarithm].DS_1 VALUES (11, 'A', 32, 1.87)
GO