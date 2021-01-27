CREATE TABLE [Period_indicator].[DS_1]
(
	[Id_1] VARCHAR(32) NOT NULL,
	[Id_2] INT NOT NULL,
	[Id_3] VARCHAR(32) NOT NULL,
	[Me_1] INT
	CONSTRAINT [PK_Period_indicator_DS_1] PRIMARY KEY CLUSTERED ([Id_1], [Id_2], [Id_3])
)
GO

EXEC sp_addextendedproperty   
	@name = N'vtl_time_type',   
	@value = 'time_period',  
	@level0type = N'Schema', @level0name = 'Period_indicator',  
	@level1type = N'Table',  @level1name = 'DS_1',  
	@level2type = N'Column', @level2name = 'Id_3';  
GO
