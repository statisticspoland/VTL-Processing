CREATE PROCEDURE [Element_of].RestoreData AS
	DELETE FROM [Element_of].DS_1

	INSERT INTO [Element_of].DS_1 VALUES (2012, 'BS', 0)
	INSERT INTO [Element_of].DS_1 VALUES (2012, 'GZ', 4)
	INSERT INTO [Element_of].DS_1 VALUES (2012, 'SQ', 9)
	INSERT INTO [Element_of].DS_1 VALUES (2012, 'MO', 6)
	INSERT INTO [Element_of].DS_1 VALUES (2012, 'FJ', 7)
	INSERT INTO [Element_of].DS_1 VALUES (2012, 'CQ', 2)
GO