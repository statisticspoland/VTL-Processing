CREATE PROCEDURE [Ratio_to_report].RestoreData AS
	DELETE FROM [Ratio_to_report].DS_1

	INSERT INTO [Ratio_to_report].DS_1 VALUES ('A', 'XX', 2000, 3, 1)
	INSERT INTO [Ratio_to_report].DS_1 VALUES ('A', 'XX', 2001, 4, 3)
	INSERT INTO [Ratio_to_report].DS_1 VALUES ('A', 'XX', 2002, 7, 5)
	INSERT INTO [Ratio_to_report].DS_1 VALUES ('A', 'XX', 2003, 6, 1)
	INSERT INTO [Ratio_to_report].DS_1 VALUES ('A', 'YY', 2000, 12, 0)
	INSERT INTO [Ratio_to_report].DS_1 VALUES ('A', 'YY', 2001, 8, 8)
	INSERT INTO [Ratio_to_report].DS_1 VALUES ('A', 'YY', 2002, 6, 5)
	INSERT INTO [Ratio_to_report].DS_1 VALUES ('A', 'YY', 2003, 14, -3)
GO