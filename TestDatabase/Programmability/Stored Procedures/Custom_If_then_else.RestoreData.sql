CREATE PROCEDURE [Custom_If_then_else].RestoreData AS
	DELETE FROM [Custom_If_then_else].DS_1
	DELETE FROM [Custom_If_then_else].DS_2
	DELETE FROM [Custom_If_then_else].DS_3
	DELETE FROM [Custom_If_then_else].DS_4

	INSERT INTO [Custom_If_then_else].DS_1 VALUES (1, 'A', 'Custom', 0)
	INSERT INTO [Custom_If_then_else].DS_1 VALUES (1, 'B', 'Custom', 0)
	INSERT INTO [Custom_If_then_else].DS_1 VALUES (2, 'A', 'Custom', 0)
	INSERT INTO [Custom_If_then_else].DS_1 VALUES (2, 'B', 'Custom', 1)
	INSERT INTO [Custom_If_then_else].DS_1 VALUES (3, 'A', 'Custom', 1)
	INSERT INTO [Custom_If_then_else].DS_1 VALUES (3, 'B', 'Custom', 0)
	INSERT INTO [Custom_If_then_else].DS_1 VALUES (4, 'A', 'Custom', 1)
	INSERT INTO [Custom_If_then_else].DS_1 VALUES (4, 'B', 'Custom', 1)

	INSERT INTO [Custom_If_then_else].DS_2 VALUES (1, 'A', 'Custom', 2, 3, 'First')
	INSERT INTO [Custom_If_then_else].DS_2 VALUES (1, 'B', 'Custom', 4, 5, 'Second')
	INSERT INTO [Custom_If_then_else].DS_2 VALUES (2, 'A', 'Custom', 6, 7, 'Third')
	INSERT INTO [Custom_If_then_else].DS_2 VALUES (2, 'B', 'Custom', 8, 9, 'Fourth')
	INSERT INTO [Custom_If_then_else].DS_2 VALUES (3, 'A', 'Custom', 10, 11, 'Fifth')
	INSERT INTO [Custom_If_then_else].DS_2 VALUES (3, 'B', 'Custom', 12, 13, 'Sixth')
	INSERT INTO [Custom_If_then_else].DS_2 VALUES (4, 'A', 'Custom', 14, 15, 'Seventh')
	INSERT INTO [Custom_If_then_else].DS_2 VALUES (4, 'B', 'Custom', 16, 17, 'Eighth')

	INSERT INTO [Custom_If_then_else].DS_3 VALUES (1, 'A', 'Custom', 2, 'DS_3', 'First')
	INSERT INTO [Custom_If_then_else].DS_3 VALUES (1, 'B', 'Custom', 4, 'DS_3', 'Second')
	INSERT INTO [Custom_If_then_else].DS_3 VALUES (2, 'A', 'Custom', 6, 'DS_3', 'Third')
	INSERT INTO [Custom_If_then_else].DS_3 VALUES (2, 'B', 'Custom', 8, 'DS_3', 'Fourth')
	INSERT INTO [Custom_If_then_else].DS_3 VALUES (3, 'A', 'Custom', 10, 'DS_3', 'Fifth')
	INSERT INTO [Custom_If_then_else].DS_3 VALUES (3, 'B', 'Custom', 12, 'DS_3', 'Sixth')
	INSERT INTO [Custom_If_then_else].DS_3 VALUES (4, 'A', 'Custom', 14, 'DS_3', 'Seventh')
	INSERT INTO [Custom_If_then_else].DS_3 VALUES (4, 'B', 'Custom', 16, 'DS_3', 'Eighth')

	INSERT INTO [Custom_If_then_else].DS_4 VALUES (1, 'A', 'Custom', 3, 'DS_4', '1st')
	INSERT INTO [Custom_If_then_else].DS_4 VALUES (1, 'B', 'Custom', 5, 'DS_4', '2nd')
	INSERT INTO [Custom_If_then_else].DS_4 VALUES (2, 'A', 'Custom', 7, 'DS_4', '3rd')
	INSERT INTO [Custom_If_then_else].DS_4 VALUES (2, 'B', 'Custom', 9, 'DS_4', '4th')
	INSERT INTO [Custom_If_then_else].DS_4 VALUES (3, 'A', 'Custom', 11, 'DS_4', '5th')
	INSERT INTO [Custom_If_then_else].DS_4 VALUES (3, 'B', 'Custom', 13, 'DS_4', '6th')
	INSERT INTO [Custom_If_then_else].DS_4 VALUES (4, 'A', 'Custom', 15, 'DS_4', '7th')
	INSERT INTO [Custom_If_then_else].DS_4 VALUES (4, 'B', 'Custom', 17, 'DS_4', '8th')
GO