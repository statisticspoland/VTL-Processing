CREATE PROCEDURE [Counting_the_number_of_data_points].RestoreData AS
	DELETE FROM [Counting_the_number_of_data_points].DS_1

	INSERT INTO [Counting_the_number_of_data_points].DS_1 VALUES (2011, 'A', 'XX', 'iii')
	INSERT INTO [Counting_the_number_of_data_points].DS_1 VALUES (2011, 'A', 'YY', 'jjj')
	INSERT INTO [Counting_the_number_of_data_points].DS_1 VALUES (2011, 'B', 'YY', 'iii')
	INSERT INTO [Counting_the_number_of_data_points].DS_1 VALUES (2012, 'A', 'XX', 'kkk')
	INSERT INTO [Counting_the_number_of_data_points].DS_1 VALUES (2012, 'B', 'YY', 'iii')
GO