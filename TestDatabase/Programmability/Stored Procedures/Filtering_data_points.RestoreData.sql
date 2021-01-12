CREATE PROCEDURE [Filtering_Data_points].RestoreData AS
	DELETE FROM [Filtering_Data_points].DS_1

	INSERT INTO [Filtering_Data_points].DS_1 VALUES (1, 'A', 'XX', 2, 'E')
	INSERT INTO [Filtering_Data_points].DS_1 VALUES (1, 'A', 'YY', 2, 'F')
	INSERT INTO [Filtering_Data_points].DS_1 VALUES (1, 'B', 'XX', 20, 'F')
	INSERT INTO [Filtering_Data_points].DS_1 VALUES (1, 'B', 'YY', 1, 'F')
	INSERT INTO [Filtering_Data_points].DS_1 VALUES (2, 'A', 'XX', 4, 'E')
	INSERT INTO [Filtering_Data_points].DS_1 VALUES (2, 'A', 'YY', 9, 'F')
GO