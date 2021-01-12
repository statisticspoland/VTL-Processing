CREATE PROCEDURE [Aggregate_invocation].RestoreData AS
	DELETE FROM [Aggregate_invocation].DS_1A
	DELETE FROM [Aggregate_invocation].DS_1B

	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2010, 'E', 'XX', 20, '')
	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2010, 'B', 'XX', 1, 'H')
	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2010, 'R', 'XX', 1, 'A')
	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2010, 'F', 'YY', 23, '')
	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2011, 'E', 'XX', 20, 'P')
	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2011, 'B', 'ZZ', 1, 'N')
	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2011, 'R', 'YY', -1, 'P')
	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2011, 'F', 'XX', 20, 'Z')
	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2012, 'L', 'ZZ', 40, 'P')
	INSERT INTO [Aggregate_invocation].DS_1A VALUES (2012, 'E', 'YY', 30, 'P')

	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2010, 'E', 'XX', 20, '')
	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2010, 'B', 'XX', 1, 'H')
	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2010, 'R', 'XX', 1, 'A')
	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2010, 'F', 'YY', 23, '')
	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2011, 'E', 'XX', 20, 'P')
	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2011, 'B', 'ZZ', 1, 'N')
	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2011, 'R', 'YY', -1, 'P')
	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2011, 'F', 'XX', 20, 'Z')
	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2012, 'L', 'ZZ', 40, 'P')
	INSERT INTO [Aggregate_invocation].DS_1B VALUES (2012, 'E', 'YY', 30, 'P')
GO