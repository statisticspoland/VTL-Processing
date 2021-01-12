CREATE PROCEDURE [String_pattern_location].RestoreData AS
	DELETE FROM [String_pattern_location].DS_1
	DELETE FROM [String_pattern_location].DS_2
		
	INSERT INTO [String_pattern_location].DS_1 VALUES (1, 'A', 'hello world')
	INSERT INTO [String_pattern_location].DS_1 VALUES (2, 'A', 'say hello')
	INSERT INTO [String_pattern_location].DS_1 VALUES (3, 'A', 'he')
	INSERT INTO [String_pattern_location].DS_1 VALUES (4, 'A', 'hi, hello!')

	INSERT INTO [String_pattern_location].DS_2 VALUES (1, 'A', 'hello', 'world')
	INSERT INTO [String_pattern_location].DS_2 VALUES (2, 'B', NULL, 'hi')
GO