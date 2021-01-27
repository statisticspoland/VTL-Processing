CREATE PROCEDURE [Fill_time_series].RestoreData AS
	DELETE FROM [Fill_time_series].DS_1
	DELETE FROM [Fill_time_series].DS_2
	DELETE FROM [Fill_time_series].DS_3
	DELETE FROM [Fill_time_series].DS_4

	INSERT INTO [Fill_time_series].DS_1 VALUES ('A', '2010-01/2010-12', 'hello world')
	INSERT INTO [Fill_time_series].DS_1 VALUES ('A', '2012-01/2012-12', 'say hello')
	INSERT INTO [Fill_time_series].DS_1 VALUES ('A', '2013-01/2013-12', 'he')
	INSERT INTO [Fill_time_series].DS_1 VALUES ('B', '2011-01/2011-12', 'hi, hello! ')
	INSERT INTO [Fill_time_series].DS_1 VALUES ('B', '2012-01/2012-12', 'hi')
	INSERT INTO [Fill_time_series].DS_1 VALUES ('B', '2014-01/2014-12', 'hello!')
														   
	INSERT INTO [Fill_time_series].DS_2 VALUES ('A', '2010-12-31', 'hello world')
	INSERT INTO [Fill_time_series].DS_2 VALUES ('A', '2012-12-31', 'say hello')
	INSERT INTO [Fill_time_series].DS_2 VALUES ('A', '2013-12-31', 'he')
	INSERT INTO [Fill_time_series].DS_2 VALUES ('B', '2011-12-31', 'hi, hello! ')
	INSERT INTO [Fill_time_series].DS_2 VALUES ('B', '2012-12-31', 'hi')
	INSERT INTO [Fill_time_series].DS_2 VALUES ('B', '2014-12-31', 'hello!')

	INSERT INTO [Fill_time_series].DS_3 VALUES ('A', '2010Y', 'hello world')
	INSERT INTO [Fill_time_series].DS_3 VALUES ('A', '2012Y', 'say hello')
	INSERT INTO [Fill_time_series].DS_3 VALUES ('A', '2013Y', 'he')
	INSERT INTO [Fill_time_series].DS_3 VALUES ('B', '2011Y', 'hi, hello! ')
	INSERT INTO [Fill_time_series].DS_3 VALUES ('B', '2012Y', 'hi')
	INSERT INTO [Fill_time_series].DS_3 VALUES ('B', '2014Y', 'hello!')

	INSERT INTO [Fill_time_series].DS_4 VALUES ('A', '2010Y', 'hello world')
	INSERT INTO [Fill_time_series].DS_4 VALUES ('A', '2012Y', 'say hello')
	INSERT INTO [Fill_time_series].DS_4 VALUES ('A', '2010Q1', 'he')
	INSERT INTO [Fill_time_series].DS_4 VALUES ('B', '2010Q2', 'hi, hello! ')
	INSERT INTO [Fill_time_series].DS_4 VALUES ('B', '2010Q4', 'hi')
	INSERT INTO [Fill_time_series].DS_4 VALUES ('B', '2011Q2', 'hello!')
GO