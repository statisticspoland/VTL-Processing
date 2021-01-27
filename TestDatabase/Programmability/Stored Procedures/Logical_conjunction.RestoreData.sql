CREATE PROCEDURE [Logical_conjunction].RestoreData AS
	DELETE FROM [Logical_conjunction].DS_1
	DELETE FROM [Logical_conjunction].DS_2

	INSERT INTO [Logical_conjunction].DS_1 VALUES ('M', 15, 'B', 2013, 1)
	INSERT INTO [Logical_conjunction].DS_1 VALUES ('M', 64, 'B', 2013, 0)
	INSERT INTO [Logical_conjunction].DS_1 VALUES ('M', 65, 'B', 2013, 1)
	INSERT INTO [Logical_conjunction].DS_1 VALUES ('F', 15, 'U', 2013, 0)
	INSERT INTO [Logical_conjunction].DS_1 VALUES ('F', 64, 'U', 2013, 0)
	INSERT INTO [Logical_conjunction].DS_1 VALUES ('F', 65, 'U', 2013, 1)

	INSERT INTO [Logical_conjunction].DS_2 VALUES ('M', 15, 'B', 2013, 0)
	INSERT INTO [Logical_conjunction].DS_2 VALUES ('M', 64, 'B', 2013, 1)
	INSERT INTO [Logical_conjunction].DS_2 VALUES ('M', 65, 'B', 2013, 1)
	INSERT INTO [Logical_conjunction].DS_2 VALUES ('F', 15, 'U', 2013, 1)
	INSERT INTO [Logical_conjunction].DS_2 VALUES ('F', 64, 'U', 2013, 0)
	INSERT INTO [Logical_conjunction].DS_2 VALUES ('F', 65, 'U', 2013, 0)
GO