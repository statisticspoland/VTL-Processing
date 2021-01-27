CREATE PROCEDURE [Natural_logarithm].RestoreData AS
	DELETE FROM [Natural_logarithm].DS_1

	INSERT INTO [Natural_logarithm].DS_1 VALUES (10, 'A', 148.413, 0.7545)
	INSERT INTO [Natural_logarithm].DS_1 VALUES (10, 'B', 2980.95, 13.45)
	INSERT INTO [Natural_logarithm].DS_1 VALUES (11, 'A', 7.38905, 1.87)
GO