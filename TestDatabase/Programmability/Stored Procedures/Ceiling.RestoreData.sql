CREATE PROCEDURE [Ceiling].RestoreData AS
	DELETE FROM [Ceiling].DS_1

	INSERT INTO [Ceiling].DS_1 VALUES (10, 'A', 7.0, 5.9)
	INSERT INTO [Ceiling].DS_1 VALUES (10, 'B', 0.1, -5.0)
	INSERT INTO [Ceiling].DS_1 VALUES (11, 'A', -32.2, 17.7)
	INSERT INTO [Ceiling].DS_1 VALUES (11, 'B', 44.5, -0.3)
GO