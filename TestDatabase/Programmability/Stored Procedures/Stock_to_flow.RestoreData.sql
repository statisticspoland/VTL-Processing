CREATE PROCEDURE [Stock_to_flow].RestoreData AS
	DELETE FROM [Stock_to_flow].DS_1
	DELETE FROM [Stock_to_flow].DS_2
	DELETE FROM [Stock_to_flow].DS_3
	DELETE FROM [Stock_to_flow].DS_4

	INSERT INTO [Stock_to_flow].DS_1 VALUES ('A', '2010-01/2010-12', 2)
	INSERT INTO [Stock_to_flow].DS_1 VALUES ('A', '2011-01/2011-12', 7)
	INSERT INTO [Stock_to_flow].DS_1 VALUES ('A', '2012-01/2012-12', 4)
	INSERT INTO [Stock_to_flow].DS_1 VALUES ('A', '2013-01/2013-12', 13)
	INSERT INTO [Stock_to_flow].DS_1 VALUES ('B', '2010-01/2010-12', 4)
	INSERT INTO [Stock_to_flow].DS_1 VALUES ('B', '2011-01/2011-12', -4)
	INSERT INTO [Stock_to_flow].DS_1 VALUES ('B', '2012-01/2012-12', -4)
	INSERT INTO [Stock_to_flow].DS_1 VALUES ('B', '2013-01/2013-12', 2)

	INSERT INTO [Stock_to_flow].DS_2 VALUES ('A', '2010-12-31', 2)
	INSERT INTO [Stock_to_flow].DS_2 VALUES ('A', '2011-12-31', 7)
	INSERT INTO [Stock_to_flow].DS_2 VALUES ('A', '2012-12-31', 4)
	INSERT INTO [Stock_to_flow].DS_2 VALUES ('A', '2013-12-31', 13)
	INSERT INTO [Stock_to_flow].DS_2 VALUES ('B', '2010-12-31', 4)
	INSERT INTO [Stock_to_flow].DS_2 VALUES ('B', '2011-12-31', -4)
	INSERT INTO [Stock_to_flow].DS_2 VALUES ('B', '2012-12-31', -4)
	INSERT INTO [Stock_to_flow].DS_2 VALUES ('B', '2013-12-31', 2)

	-- INSERT INTO [Stock_to_flow].DS_3 VALUES ('A', '2010Y', 2)
	-- INSERT INTO [Stock_to_flow].DS_3 VALUES ('A', '2011Y', 7)
	-- INSERT INTO [Stock_to_flow].DS_3 VALUES ('A', '2012Y', 4)
	-- INSERT INTO [Stock_to_flow].DS_3 VALUES ('A', '2013Y', 13)
	-- INSERT INTO [Stock_to_flow].DS_3 VALUES ('B', '2010Y', 4)
	-- INSERT INTO [Stock_to_flow].DS_3 VALUES ('B', '2011Y', -4)
	-- INSERT INTO [Stock_to_flow].DS_3 VALUES ('B', '2012Y', -4)
	-- INSERT INTO [Stock_to_flow].DS_3 VALUES ('B', '2013Y', 2)
	-- documentation example input error
	INSERT INTO [Stock_to_flow].DS_3 VALUES ('A', '2010A', 2)
	INSERT INTO [Stock_to_flow].DS_3 VALUES ('A', '2011A', 7)
	INSERT INTO [Stock_to_flow].DS_3 VALUES ('A', '2012A', 4)
	INSERT INTO [Stock_to_flow].DS_3 VALUES ('A', '2013A', 13)
	INSERT INTO [Stock_to_flow].DS_3 VALUES ('B', '2010A', 4)
	INSERT INTO [Stock_to_flow].DS_3 VALUES ('B', '2011A', -4)
	INSERT INTO [Stock_to_flow].DS_3 VALUES ('B', '2012A', -4)
	INSERT INTO [Stock_to_flow].DS_3 VALUES ('B', '2013A', 2)

	-- INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2010Y', 2)
	-- INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2011Y', 9)
	-- INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2012Y', 13)
	-- INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2013Y', 26)
	-- INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2010Q1', 2)
	-- INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2011Q2', -1)
	-- INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2012Q3', 6)
	-- INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2013Q4', 2)
	-- documentation example input error
	INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2010A', 2)
	INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2011A', 9)
	INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2012A', 13)
	INSERT INTO [Stock_to_flow].DS_4 VALUES ('A', '2013A', 26)
	INSERT INTO [Stock_to_flow].DS_4 VALUES ('B', '2010Q1', 2)
	INSERT INTO [Stock_to_flow].DS_4 VALUES ('B', '2011Q2', -1)
	INSERT INTO [Stock_to_flow].DS_4 VALUES ('B', '2012Q3', 6)
	INSERT INTO [Stock_to_flow].DS_4 VALUES ('B', '2013Q4', 2)
GO