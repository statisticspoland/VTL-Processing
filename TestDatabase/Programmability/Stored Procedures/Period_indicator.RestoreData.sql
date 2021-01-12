CREATE PROCEDURE [Period_indicator].RestoreData AS
	DELETE FROM [Period_indicator].DS_1

	INSERT INTO [Period_indicator].DS_1 VALUES ('A', 1, '2010', 10)
	INSERT INTO [Period_indicator].DS_1 VALUES ('A', 1, '2013Q1', 50)
GO