CREATE PROCEDURE [Unpivoting].RestoreData AS
	DELETE FROM [Unpivoting].DS_1

	INSERT INTO [Unpivoting].DS_1 VALUES (1, 5, 2, 7)
	INSERT INTO [Unpivoting].DS_1 VALUES (2, 3, 4, 9)
GO