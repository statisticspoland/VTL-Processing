CREATE PROCEDURE [Custom_Join].RestoreData AS
	DELETE FROM [Custom_Join].DS_1
	DELETE FROM [Custom_Join].DS_2

	INSERT INTO [Custom_Join].DS_1 VALUES (1, 'A', 'A', 'B', 'X', 1)
	INSERT INTO [Custom_Join].DS_1 VALUES (1, 'B', 'C', 'D', 'Y', 4)
	INSERT INTO [Custom_Join].DS_1 VALUES (2, 'A', 'E', 'F', 'X', 7)

	INSERT INTO [Custom_Join].DS_2 VALUES (1, 'A', 'B', 'Q', 'Y', 4)
	INSERT INTO [Custom_Join].DS_2 VALUES (1, 'B', 'S', 'T', 'Y', 2)
	INSERT INTO [Custom_Join].DS_2 VALUES (3, 'A', 'Z', 'M', 'X', 5)
GO