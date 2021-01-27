CREATE PROCEDURE [Calculation_of_a_Component].RestoreData AS
	DELETE FROM [Calculation_of_a_Component].DS_1

	INSERT INTO [Calculation_of_a_Component].DS_1 VALUES (1, 'A', 'CA', 20)
	INSERT INTO [Calculation_of_a_Component].DS_1 VALUES (1, 'B', 'CA', 2)
	INSERT INTO [Calculation_of_a_Component].DS_1 VALUES (2, 'A', 'CA', 2)
GO