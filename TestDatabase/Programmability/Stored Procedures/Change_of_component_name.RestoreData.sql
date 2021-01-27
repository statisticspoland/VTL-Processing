CREATE PROCEDURE [Change_of_component_name].RestoreData AS
	DELETE FROM [Change_of_component_name].DS_1

	INSERT INTO [Change_of_component_name].DS_1 VALUES (1, 'B', 'XX', 20, 'F')
	INSERT INTO [Change_of_component_name].DS_1 VALUES (1, 'B', 'YY', 1, 'F')
	INSERT INTO [Change_of_component_name].DS_1 VALUES (2, 'A', 'XX', 4, 'E')
	INSERT INTO [Change_of_component_name].DS_1 VALUES (2, 'A', 'YY', 9, 'F')
GO