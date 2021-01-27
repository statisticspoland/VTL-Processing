CREATE PROCEDURE [If_then_else].RestoreData AS
	DELETE FROM [If_then_else].DS_cond
	DELETE FROM [If_then_else].DS_1
	DELETE FROM [If_then_else].DS_2

	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'B', 'Total', 'M', 5451780)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'B', 'Total', 'F', 5643070)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'G', 'Total', 'M', 5449803)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'G', 'Total', 'F', 5673231)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'S', 'Total', 'M', 23099012)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'S', 'Total', 'F', 23719207)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'F', 'Total', 'M', 31616281)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'F', 'Total', 'F', 33671580)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'I', 'Total', 'M', 28726599)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'I', 'Total', 'F', 30667608)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'A', 'Total', 'M', NULL)
	INSERT INTO [If_then_else].DS_cond VALUES (2012, 'A', 'Total', 'F', NULL)

	INSERT INTO [If_then_else].DS_1 VALUES (2012, 'S', 'Total', 'F', 25.8)
	INSERT INTO [If_then_else].DS_1 VALUES (2012, 'F', 'Total', 'F', NULL)
	INSERT INTO [If_then_else].DS_1 VALUES (2012, 'I', 'Total', 'F', 20.9)
	INSERT INTO [If_then_else].DS_1 VALUES (2012, 'A', 'Total', 'M', 6.3)

	INSERT INTO [If_then_else].DS_2 VALUES (2012, 'B', 'Total', 'M', 0.12)
	INSERT INTO [If_then_else].DS_2 VALUES (2012, 'G', 'Total', 'M', 22.5)
	INSERT INTO [If_then_else].DS_2 VALUES (2012, 'S', 'Total', 'M', 23.7)
	INSERT INTO [If_then_else].DS_2 VALUES (2012, 'A', 'Total', 'F', NULL)
GO