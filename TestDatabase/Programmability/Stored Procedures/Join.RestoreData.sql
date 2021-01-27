CREATE PROCEDURE [Join].RestoreData AS
	DELETE FROM [Join].DS_1
	DELETE FROM [Join].DS_2A
	DELETE FROM [Join].DS_2B

	INSERT INTO [Join].DS_1 VALUES (1, 'A', 'A', 'B')
	INSERT INTO [Join].DS_1 VALUES (1, 'B', 'C', 'D')
	INSERT INTO [Join].DS_1 VALUES (2, 'A', 'E', 'F')

	INSERT INTO [Join].DS_2A VALUES (1, 'A', 'B', 'Q')
	INSERT INTO [Join].DS_2A VALUES (1, 'B', 'S', 'T')
	INSERT INTO [Join].DS_2A VALUES (3, 'A', 'Z', 'M')

	INSERT INTO [Join].DS_2B VALUES (1, 'A', 'B', 'Q')
	INSERT INTO [Join].DS_2B VALUES (1, 'B', 'S', 'T')
	INSERT INTO [Join].DS_2B VALUES (3, 'A', 'Z', 'M')
GO