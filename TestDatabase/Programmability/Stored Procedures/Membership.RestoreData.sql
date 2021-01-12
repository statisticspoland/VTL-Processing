CREATE PROCEDURE [Membership].RestoreData AS
	DELETE FROM [Membership].DS_1

	INSERT INTO [Membership].DS_1 VALUES (1, 'A', 1, 5, NULL)
	INSERT INTO [Membership].DS_1 VALUES (1, 'B', 2, 10, 'P')
	INSERT INTO [Membership].DS_1 VALUES (2, 'A', 3, 12, NULL)
GO