CREATE PROCEDURE [Whitespace_removal].RestoreData AS
	DELETE FROM [Whitespace_removal].DS_1
		
	INSERT INTO [Whitespace_removal].DS_1 VALUES (1, 'A', 'hello   ')
	INSERT INTO [Whitespace_removal].DS_1 VALUES (2, 'B', 'hi    ')
GO