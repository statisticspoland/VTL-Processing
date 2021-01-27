CREATE PROCEDURE [Multiplication].RestoreData AS
	DELETE FROM [Multiplication].DS_1
	DELETE FROM [Multiplication].DS_2

	INSERT INTO [Multiplication].DS_1 VALUES (10, 'A', 100, 7.6)
	INSERT INTO [Multiplication].DS_1 VALUES (10, 'B', 10, 12.3)
	INSERT INTO [Multiplication].DS_1 VALUES (11, 'A', 20, 25.0)
	INSERT INTO [Multiplication].DS_1 VALUES (11, 'B', 2, 20.0)

	INSERT INTO [Multiplication].DS_2 VALUES (10, 'A', 1, 2.0)
	INSERT INTO [Multiplication].DS_2 VALUES (10, 'C', 5, 3.0)
	INSERT INTO [Multiplication].DS_2 VALUES (11, 'B', 2, 1.0)
GO