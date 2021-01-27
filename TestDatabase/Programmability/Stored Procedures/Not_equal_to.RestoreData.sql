CREATE PROCEDURE [Not_equal_to].RestoreData AS
	DELETE FROM [Not_equal_to].DS_1
	DELETE FROM [Not_equal_to].DS_2

	INSERT INTO [Not_equal_to].DS_1 VALUES ('G', 'Total', 'Percentage', 'Total', 7.1)
	INSERT INTO [Not_equal_to].DS_1 VALUES ('R', 'Total', 'Percentage', 'Total', NULL)

	INSERT INTO [Not_equal_to].DS_2 VALUES ('G', 'Total', 'Percentage', 'Total', 7.5)
	INSERT INTO [Not_equal_to].DS_2 VALUES ('R', 'Total', 'Percentage', 'Total', 3)
GO