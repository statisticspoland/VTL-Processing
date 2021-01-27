CREATE PROCEDURE [Character_case_conversion].RestoreData AS
	DELETE FROM [Character_case_conversion].DS_1
		
	INSERT INTO [Character_case_conversion].DS_1 VALUES (1, 'A', 'hello')
	INSERT INTO [Character_case_conversion].DS_1 VALUES (2, 'B', 'hi')
GO