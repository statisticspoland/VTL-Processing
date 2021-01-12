CREATE PROCEDURE [Check_datapoint].RestoreData AS
	DELETE FROM [Check_datapoint].DS_1

	INSERT INTO [Check_datapoint].DS_1 VALUES (2011, 'l', 'CREDIT', 10)
	INSERT INTO [Check_datapoint].DS_1 VALUES (2011, 'l', 'DEBIT', -2)
	INSERT INTO [Check_datapoint].DS_1 VALUES (2012, 'l', 'CREDIT', 10)
	INSERT INTO [Check_datapoint].DS_1 VALUES (2012, 'l', 'DEBIT', 2)
GO