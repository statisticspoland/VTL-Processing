CREATE PROCEDURE [Absolute_value].RestoreData AS
	DELETE FROM [Absolute_value].DS_1

	INSERT INTO [Absolute_value].DS_1 VALUES (10, 'A', 0.484183, 0.7545)
	INSERT INTO [Absolute_value].DS_1 VALUES (10, 'B', -0.515817, -13.45)
	INSERT INTO [Absolute_value].DS_1 VALUES (11, 'A', -1.000000, 187.0)
GO