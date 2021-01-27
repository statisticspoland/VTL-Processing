CREATE PROCEDURE [Hierarchical_roll_up].RestoreData AS
	DELETE FROM [Hierarchical_roll_up].DS_1

	INSERT INTO [Hierarchical_roll_up].DS_1 VALUES (2010, 'M', 2, 'Dx')
	INSERT INTO [Hierarchical_roll_up].DS_1 VALUES (2010, 'N', 5, 'Pz')
	INSERT INTO [Hierarchical_roll_up].DS_1 VALUES (2010, 'O', 4, 'Pz')
	INSERT INTO [Hierarchical_roll_up].DS_1 VALUES (2010, 'P', 7, 'Pz')
	INSERT INTO [Hierarchical_roll_up].DS_1 VALUES (2010, 'Q', -7, 'Pz')
	INSERT INTO [Hierarchical_roll_up].DS_1 VALUES (2010, 'S', 3, 'Ay')
	INSERT INTO [Hierarchical_roll_up].DS_1 VALUES (2010, 'T', 9, 'Bq')
	INSERT INTO [Hierarchical_roll_up].DS_1 VALUES (2010, 'U', NULL, 'Nj')
GO