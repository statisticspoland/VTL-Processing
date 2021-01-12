CREATE PROCEDURE [Custom_Check_datapoint].RestoreData AS
	DELETE FROM [Custom_Check_datapoint].DS_1

	INSERT INTO [Custom_Check_datapoint].DS_1 VALUES (1, 'Jack', 14, 178.6, 1, 'UK', 'Tall')
	INSERT INTO [Custom_Check_datapoint].DS_1 VALUES (2, 'Sophia', 16, 152.4, 2, 'Germany', 'Short')
	INSERT INTO [Custom_Check_datapoint].DS_1 VALUES (3, 'Martin', 13, 164.2, 1, 'UK', 'Short')
	INSERT INTO [Custom_Check_datapoint].DS_1 VALUES (4, 'frank', 13, 189.1, 1, 'UK', 'Short')
	INSERT INTO [Custom_Check_datapoint].DS_1 VALUES (5, 'Penny', 14, 158.3, 2, 'USA', 'Tall')
	INSERT INTO [Custom_Check_datapoint].DS_1 VALUES (6, 'Annie', 12, 170.2, 2, 'Germany', 'Tall')
GO