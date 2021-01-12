CREATE PROCEDURE [Square_root].RestoreData AS
	DELETE FROM [Square_root].DS_1

	INSERT INTO [Square_root].DS_1 VALUES (10, 'A', 16, 0.7545)
	INSERT INTO [Square_root].DS_1 VALUES (10, 'B', 81, 13.45)
	INSERT INTO [Square_root].DS_1 VALUES (11, 'A', 64, 1.87)
GO