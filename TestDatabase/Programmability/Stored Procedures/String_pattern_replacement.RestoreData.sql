CREATE PROCEDURE [String_pattern_replacement].RestoreData AS
	DELETE FROM [String_pattern_replacement].DS_1
		
	INSERT INTO [String_pattern_replacement].DS_1 VALUES (1, 'A', 'hello world')
	INSERT INTO [String_pattern_replacement].DS_1 VALUES (2, 'A', 'say hello')
	INSERT INTO [String_pattern_replacement].DS_1 VALUES (3, 'A', 'he')
	INSERT INTO [String_pattern_replacement].DS_1 VALUES (4, 'A', 'hello!')
GO