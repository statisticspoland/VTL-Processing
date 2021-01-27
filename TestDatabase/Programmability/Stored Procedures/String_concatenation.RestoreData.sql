CREATE PROCEDURE [String_concatenation].RestoreData AS
	DELETE FROM [String_concatenation].DS_1
	DELETE FROM [String_concatenation].DS_2
		
	INSERT INTO [String_concatenation].DS_1 VALUES (1, 'A', 'hello')
	INSERT INTO [String_concatenation].DS_1 VALUES (2, 'B', 'hi')
				
	INSERT INTO [String_concatenation].DS_2 VALUES (1, 'A', 'world')
	INSERT INTO [String_concatenation].DS_2 VALUES (2, 'B', 'there')
GO