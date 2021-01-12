CREATE PROCEDURE [Unary_minus].RestoreData AS
	DELETE FROM [Unary_minus].DS_1

	INSERT INTO [Unary_minus].DS_1 VALUES (10, 'A', 1, 5.0)
	INSERT INTO [Unary_minus].DS_1 VALUES (10, 'B', 2, 10.0)
	INSERT INTO [Unary_minus].DS_1 VALUES (11, 'A', 3, 12.0)
GO