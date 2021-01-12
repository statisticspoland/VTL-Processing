CREATE PROCEDURE [Length].RestoreData AS
	DELETE FROM [Length].DS_1
	DELETE FROM [Length].DS_2

	INSERT INTO [Length].DS_1 VALUES (1, 'A', 'hello')
	INSERT INTO [Length].DS_1 VALUES (2, 'B', NULL)

	INSERT INTO [Length].DS_2 VALUES (1, 'A', 'hello', 'world')
	INSERT INTO [Length].DS_2 VALUES (2, 'B', NULL, 'hi')
GO