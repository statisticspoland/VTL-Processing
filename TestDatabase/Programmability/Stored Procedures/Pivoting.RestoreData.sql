CREATE PROCEDURE [Pivoting].RestoreData AS
	DELETE FROM [Pivoting].DS_1

	INSERT INTO [Pivoting].DS_1 VALUES (1, 'A', 5, 'E')
	INSERT INTO [Pivoting].DS_1 VALUES (1, 'B', 2, 'F')
	INSERT INTO [Pivoting].DS_1 VALUES (1, 'C', 7, 'F')
	INSERT INTO [Pivoting].DS_1 VALUES (2, 'A', 3, 'E')
	INSERT INTO [Pivoting].DS_1 VALUES (2, 'B', 4, 'E')
	INSERT INTO [Pivoting].DS_1 VALUES (2, 'C', 9, 'F')
GO