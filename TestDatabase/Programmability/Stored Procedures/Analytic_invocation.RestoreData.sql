CREATE PROCEDURE [Analytic_invocation].RestoreData AS
	DELETE FROM [Analytic_invocation].DS_1

	INSERT INTO [Analytic_invocation].DS_1 VALUES (2010, 'E', 'XX', 5)
	INSERT INTO [Analytic_invocation].DS_1 VALUES (2010, 'B', 'XX', -3)
	INSERT INTO [Analytic_invocation].DS_1 VALUES (2010, 'R', 'XX', 9)
	INSERT INTO [Analytic_invocation].DS_1 VALUES (2010, 'F', 'YY', 13)
	INSERT INTO [Analytic_invocation].DS_1 VALUES (2011, 'E', 'XX', 11)
	INSERT INTO [Analytic_invocation].DS_1 VALUES (2011, 'B', 'ZZ', 7)
	INSERT INTO [Analytic_invocation].DS_1 VALUES (2011, 'E', 'YY', -1)
	INSERT INTO [Analytic_invocation].DS_1 VALUES (2011, 'F', 'XX', 0)
	INSERT INTO [Analytic_invocation].DS_1 VALUES (2012, 'L', 'ZZ', -2)
	INSERT INTO [Analytic_invocation].DS_1 VALUES (2012, 'E', 'YY', 3)
GO