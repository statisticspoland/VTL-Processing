CREATE PROCEDURE [Sub_string_extraction].RestoreData AS
	DELETE FROM [Sub_string_extraction].DS_1
		
	INSERT INTO [Sub_string_extraction].DS_1 VALUES (1, 'A', 'hello world', 'medium size text')
	INSERT INTO [Sub_string_extraction].DS_1 VALUES (1, 'B', 'abcdefghilmno', 'short text')
	INSERT INTO [Sub_string_extraction].DS_1 VALUES (2, 'A', 'pqrstuvwxyz', 'this is a long description')
GO