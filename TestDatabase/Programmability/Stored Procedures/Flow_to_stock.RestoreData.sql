CREATE PROCEDURE [Flow_to_stock].RestoreData AS
	DELETE FROM [Flow_to_stock].DS_1
	DELETE FROM [Flow_to_stock].DS_2
	DELETE FROM [Flow_to_stock].DS_3
	DELETE FROM [Flow_to_stock].DS_4

	INSERT INTO [Flow_to_stock].DS_1 VALUES ('A', '2010-01/2010-12', 2)
	INSERT INTO [Flow_to_stock].DS_1 VALUES ('A', '2011-01/2011-12', 5)
	INSERT INTO [Flow_to_stock].DS_1 VALUES ('A', '2012-01/2012-12', -3)
	INSERT INTO [Flow_to_stock].DS_1 VALUES ('A', '2013-01/2013-12', 9)
	INSERT INTO [Flow_to_stock].DS_1 VALUES ('B', '2010-01/2010-12', 4)
	INSERT INTO [Flow_to_stock].DS_1 VALUES ('B', '2011-01/2011-12', -8)
	INSERT INTO [Flow_to_stock].DS_1 VALUES ('B', '2012-01/2012-12', 0)
	INSERT INTO [Flow_to_stock].DS_1 VALUES ('B', '2013-01/2013-12', 6)

	INSERT INTO [Flow_to_stock].DS_2 VALUES ('A', '2010-12-31', 2)
	INSERT INTO [Flow_to_stock].DS_2 VALUES ('A', '2011-12-31', 5)
	INSERT INTO [Flow_to_stock].DS_2 VALUES ('A', '2012-12-31', -3)
	INSERT INTO [Flow_to_stock].DS_2 VALUES ('A', '2013-12-31', 9)
	INSERT INTO [Flow_to_stock].DS_2 VALUES ('B', '2010-12-31', 4)
	INSERT INTO [Flow_to_stock].DS_2 VALUES ('B', '2011-12-31', -8)
	INSERT INTO [Flow_to_stock].DS_2 VALUES ('B', '2012-12-31', 0)
	INSERT INTO [Flow_to_stock].DS_2 VALUES ('B', '2013-12-31', 6)

	-- INSERT INTO [Flow_to_stock].DS_3 VALUES ('A', '2010Y', 2)
	-- INSERT INTO [Flow_to_stock].DS_3 VALUES ('A', '2011Y', 5)
	-- INSERT INTO [Flow_to_stock].DS_3 VALUES ('A', '2012Y', -3)
	-- INSERT INTO [Flow_to_stock].DS_3 VALUES ('A', '2013Y', 9)
	-- INSERT INTO [Flow_to_stock].DS_3 VALUES ('B', '2010Y', 4)
	-- INSERT INTO [Flow_to_stock].DS_3 VALUES ('B', '2011Y', -8)
	-- INSERT INTO [Flow_to_stock].DS_3 VALUES ('B', '2012Y', 0)
	-- INSERT INTO [Flow_to_stock].DS_3 VALUES ('B', '2013Y', 6)
	-- documentation example input error
	INSERT INTO [Flow_to_stock].DS_3 VALUES ('A', '2010A', 2)
	INSERT INTO [Flow_to_stock].DS_3 VALUES ('A', '2011A', 5)
	INSERT INTO [Flow_to_stock].DS_3 VALUES ('A', '2012A', -3)
	INSERT INTO [Flow_to_stock].DS_3 VALUES ('A', '2013A', 9)
	INSERT INTO [Flow_to_stock].DS_3 VALUES ('B', '2010A', 4)
	INSERT INTO [Flow_to_stock].DS_3 VALUES ('B', '2011A', -8)
	INSERT INTO [Flow_to_stock].DS_3 VALUES ('B', '2012A', 0)
	INSERT INTO [Flow_to_stock].DS_3 VALUES ('B', '2013A', 6)

	-- INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2010Y', 2)
	-- INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2011Y', 5)
	-- INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2012Y', -3)
	-- INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2013Y', 9)
	-- INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2010Q1', 4)
	-- INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2011Q2', -8)
	-- INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2012Q3', 0)
	-- INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2013Q4', 6)
	-- documentation example input error
	INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2010A', 2)
	INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2011A', 5)
	INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2012A', -3)
	INSERT INTO [Flow_to_stock].DS_4 VALUES ('A', '2013A', 9)
	INSERT INTO [Flow_to_stock].DS_4 VALUES ('B', '2010Q1', 4)
	INSERT INTO [Flow_to_stock].DS_4 VALUES ('B', '2011Q2', -8)
	INSERT INTO [Flow_to_stock].DS_4 VALUES ('B', '2012Q3', 0)
	INSERT INTO [Flow_to_stock].DS_4 VALUES ('B', '2013Q4', 6)
GO