CREATE PROCEDURE [Non_Persistent_assignment].RestoreData AS
	DELETE FROM [Non_Persistent_assignment].DS_1

	INSERT INTO [Non_Persistent_assignment].DS_1 VALUES (2013, 'Belgium', 5, 5)
	INSERT INTO [Non_Persistent_assignment].DS_1 VALUES (2013, 'Denmark', 2, 10)
	INSERT INTO [Non_Persistent_assignment].DS_1 VALUES (2013, 'France', 3, 12)
	INSERT INTO [Non_Persistent_assignment].DS_1 VALUES (2013, 'Spain', 4, 20)
GO