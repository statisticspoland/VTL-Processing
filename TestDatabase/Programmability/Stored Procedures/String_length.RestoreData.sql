CREATE PROCEDURE [String_length].RestoreData AS
	DELETE FROM [String_length].DS_1
	DELETE FROM [String_length].DS_2
		
	INSERT INTO [String_length].DS_1 VALUES (1, 'A', 'hello')
	INSERT INTO [String_length].DS_1 VALUES (2, 'B', NULL)

	INSERT INTO [String_length].DS_2 VALUES (1, 'A', 'hello', 'world')
	INSERT INTO [String_length].DS_2 VALUES (2, 'B', NULL, 'hi')
GO