CREATE PROCEDURE [Unary_plus].RestoreData AS
	DELETE FROM [Unary_plus].DS_1

	INSERT INTO [Unary_plus].DS_1 VALUES (10, 'A', 1.0, 5)
	INSERT INTO [Unary_plus].DS_1 VALUES (10, 'B', 2.3, 10)
	INSERT INTO [Unary_plus].DS_1 VALUES (11, 'A', 3.2, 12)
GO