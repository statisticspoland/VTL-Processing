CREATE PROCEDURE [Time_shift].RestoreData AS
	DELETE FROM [Time_shift].DS_1
	DELETE FROM [Time_shift].DS_2
	DELETE FROM [Time_shift].DS_3
	DELETE FROM [Time_shift].DS_4

	INSERT INTO [Time_shift].DS_1 VALUES ('A', '2010-01/2010-12', 'helo world')
	INSERT INTO [Time_shift].DS_1 VALUES ('A', '2011-01/2011-12', NULL)
	INSERT INTO [Time_shift].DS_1 VALUES ('A', '2012-01/2012-12', 'say hello')
	INSERT INTO [Time_shift].DS_1 VALUES ('A', '2013-01/2013-12', 'he')
	INSERT INTO [Time_shift].DS_1 VALUES ('B', '2010-01/2010-12', 'hi, hello! ')
	INSERT INTO [Time_shift].DS_1 VALUES ('B', '2011-01/2011-12', 'hi')
	INSERT INTO [Time_shift].DS_1 VALUES ('B', '2012-01/2012-12', NULL)
	INSERT INTO [Time_shift].DS_1 VALUES ('B', '2013-01/2013-12', 'hello!')

	INSERT INTO [Time_shift].DS_2 VALUES ('A', '2010-12-31', 'helo world')
	INSERT INTO [Time_shift].DS_2 VALUES ('A', '2011-12-31', NULL)
	INSERT INTO [Time_shift].DS_2 VALUES ('A', '2012-12-31', 'say hello')
	INSERT INTO [Time_shift].DS_2 VALUES ('A', '2013-12-31', 'he')
	INSERT INTO [Time_shift].DS_2 VALUES ('B', '2010-12-31', 'hi, hello! ')
	INSERT INTO [Time_shift].DS_2 VALUES ('B', '2011-12-31', 'hi')
	INSERT INTO [Time_shift].DS_2 VALUES ('B', '2012-12-31', NULL)
	INSERT INTO [Time_shift].DS_2 VALUES ('B', '2013-12-31', 'hello!')

	-- INSERT INTO [Time_shift].DS_3 VALUES ('A', '2010Y', 'helo world')
	-- INSERT INTO [Time_shift].DS_3 VALUES ('A', '2011Y', NULL)
	-- INSERT INTO [Time_shift].DS_3 VALUES ('A', '2012Y', 'say hello')
	-- INSERT INTO [Time_shift].DS_3 VALUES ('A', '2013Y', 'he')
	-- INSERT INTO [Time_shift].DS_3 VALUES ('B', '2010Y', 'hi, hello! ')
	-- INSERT INTO [Time_shift].DS_3 VALUES ('B', '2011Y', 'hi')
	-- INSERT INTO [Time_shift].DS_3 VALUES ('B', '2012Y', NULL)
	-- INSERT INTO [Time_shift].DS_3 VALUES ('B', '2013Y', 'hello!')
	-- documentation example input error
	INSERT INTO [Time_shift].DS_3 VALUES ('A', '2010A', 'helo world')
	INSERT INTO [Time_shift].DS_3 VALUES ('A', '2011A', NULL)
	INSERT INTO [Time_shift].DS_3 VALUES ('A', '2012A', 'say hello')
	INSERT INTO [Time_shift].DS_3 VALUES ('A', '2013A', 'he')
	INSERT INTO [Time_shift].DS_3 VALUES ('B', '2010A', 'hi, hello! ')
	INSERT INTO [Time_shift].DS_3 VALUES ('B', '2011A', 'hi')
	INSERT INTO [Time_shift].DS_3 VALUES ('B', '2012A', NULL)
	INSERT INTO [Time_shift].DS_3 VALUES ('B', '2013A', 'hello!')

	-- INSERT INTO [Time_shift].DS_4 VALUES ('A', '2010Y', 'helo world') - documentation example input error
	INSERT INTO [Time_shift].DS_4 VALUES ('A', '2010A', 'helo world')
	-- INSERT INTO [Time_shift].DS_4 VALUES ('A', '2011Y', NULL) - documentation example input error
	INSERT INTO [Time_shift].DS_4 VALUES ('A', '2011A', NULL)
	-- INSERT INTO [Time_shift].DS_4 VALUES ('A', '2012Y', 'say hello') - documentation example input error
	INSERT INTO [Time_shift].DS_4 VALUES ('A', '2012A', 'say hello')
	-- INSERT INTO [Time_shift].DS_4 VALUES ('A', '2013Y', 'he') - documentation example input error
	INSERT INTO [Time_shift].DS_4 VALUES ('A', '2013A', 'he')
	INSERT INTO [Time_shift].DS_4 VALUES ('A', '2010Q1', 'hi, hello! ')
	INSERT INTO [Time_shift].DS_4 VALUES ('A', '2010Q2', 'hi')
	INSERT INTO [Time_shift].DS_4 VALUES ('A', '2010Q3', NULL)
	INSERT INTO [Time_shift].DS_4 VALUES ('A', '2010Q4', 'hello!')
GO