CREATE PROCEDURE [Greater_than].RestoreData AS
	DELETE FROM [Greater_than].DS_1A
	DELETE FROM [Greater_than].DS_1B
	DELETE FROM [Greater_than].DS_2

	INSERT INTO [Greater_than].DS_1A VALUES (2, 'G', 2011, 'Total', 'Percentage', NULL)
	INSERT INTO [Greater_than].DS_1A VALUES (2, 'R', 2011, 'Total', 'Percentage', 12.2)
	INSERT INTO [Greater_than].DS_1A VALUES (2, 'F', 2011, 'Total', 'Percentage', 29.5)

	INSERT INTO [Greater_than].DS_1B VALUES ('G', 'Total', 'Percentage', 'Total', 7.1)
	INSERT INTO [Greater_than].DS_1B VALUES ('R', 'Total', 'Percentage', 'Total', 42.5)

	INSERT INTO [Greater_than].DS_2 VALUES ('G', 'Total', 'Percentage', 'Total', 7.5)
	INSERT INTO [Greater_than].DS_2 VALUES ('R', 'Total', 'Percentage', 'Total', 33.7)
GO