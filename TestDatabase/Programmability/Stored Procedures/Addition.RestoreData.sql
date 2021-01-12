CREATE PROCEDURE [Addition].RestoreData AS
	DELETE FROM [Addition].DS_1
	DELETE FROM [Addition].DS_2

	INSERT INTO [Addition].DS_1 VALUES (10, 'A', 5, 5.0)
	INSERT INTO [Addition].DS_1 VALUES (10, 'B', 2, 10.5)
	INSERT INTO [Addition].DS_1 VALUES (11, 'A', 3, 12.2)
	INSERT INTO [Addition].DS_1 VALUES (11, 'B', 4, 20.3)

	INSERT INTO [Addition].DS_2 VALUES (10, 'A', 10, 3.0)
	INSERT INTO [Addition].DS_2 VALUES (10, 'C', 11, 6.2)
	INSERT INTO [Addition].DS_2 VALUES (11, 'B', 6, 7.0)
GO