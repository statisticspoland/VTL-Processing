CREATE PROCEDURE [Set_difference].RestoreData AS
	DELETE FROM [Set_difference].DS_1A
	DELETE FROM [Set_difference].DS_1B
	DELETE FROM [Set_difference].DS_2A
	DELETE FROM [Set_difference].DS_2B

	INSERT INTO [Set_difference].DS_1A VALUES (2012, 'B', 'Total', 'Total', 10)
	INSERT INTO [Set_difference].DS_1A VALUES (2012, 'G', 'Total', 'Total', 20)
	INSERT INTO [Set_difference].DS_1A VALUES (2012, 'F', 'Total', 'Total', 30)
	INSERT INTO [Set_difference].DS_1A VALUES (2012, 'M', 'Total', 'Total', 40)
	INSERT INTO [Set_difference].DS_1A VALUES (2012, 'I', 'Total', 'Total', 50)
	INSERT INTO [Set_difference].DS_1A VALUES (2012, 'S', 'Total', 'Total', 60)

	INSERT INTO [Set_difference].DS_2A VALUES (2011, 'B', 'Total', 'Total', 10)
	INSERT INTO [Set_difference].DS_2A VALUES (2012, 'G', 'Total', 'Total', 20)
	INSERT INTO [Set_difference].DS_2A VALUES (2012, 'F', 'Total', 'Total', 30)
	INSERT INTO [Set_difference].DS_2A VALUES (2012, 'M', 'Total', 'Total', 40)
	INSERT INTO [Set_difference].DS_2A VALUES (2012, 'I', 'Total', 'Total', 50)
	INSERT INTO [Set_difference].DS_2A VALUES (2012, 'S', 'Total', 'Total', 60)

	INSERT INTO [Set_difference].DS_1B VALUES ('R', 'M', 2011, 7)
	INSERT INTO [Set_difference].DS_1B VALUES ('R', 'F', 2011, 10)
	INSERT INTO [Set_difference].DS_1B VALUES ('R', 'T', 2011, 12)

	INSERT INTO [Set_difference].DS_2B VALUES ('R', 'M', 2011, 7)
	INSERT INTO [Set_difference].DS_2B VALUES ('R', 'F', 2011, 10)
GO