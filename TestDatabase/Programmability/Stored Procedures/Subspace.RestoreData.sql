CREATE PROCEDURE [Subspace].RestoreData AS
	DELETE FROM [Subspace].DS_1

	INSERT INTO [Subspace].DS_1 VALUES (1, 'A', 'XX', 20, 'F')
	INSERT INTO [Subspace].DS_1 VALUES (1, 'A', 'YY', 1, 'F')
	INSERT INTO [Subspace].DS_1 VALUES (1, 'B', 'XX', 4, 'E')
	INSERT INTO [Subspace].DS_1 VALUES (1, 'B', 'YY', 9, 'F')
	INSERT INTO [Subspace].DS_1 VALUES (2, 'A', 'XX', 7, 'F')
	INSERT INTO [Subspace].DS_1 VALUES (2, 'A', 'YY', 5, 'E')
	INSERT INTO [Subspace].DS_1 VALUES (2, 'B', 'XX', 12, 'F')
	INSERT INTO [Subspace].DS_1 VALUES (2, 'B', 'YY', 15, 'F')
GO