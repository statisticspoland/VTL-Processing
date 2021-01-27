CREATE TABLE [Flow_to_stock].[DS_4]
(
	[Id_1] VARCHAR(32) NOT NULL,
	[Id_2] VARCHAR(32) NOT NULL,
	[Me_1] INT
	CONSTRAINT [PK_Flow_to_stock_DS_4] PRIMARY KEY CLUSTERED ([Id_1], [Id_2])
)
GO

EXEC sp_addextendedproperty   
	@name = N'vtl_time_type',   
	@value = 'time_period',  
	@level0type = N'Schema', @level0name = 'Flow_to_stock',  
	@level1type = N'Table',  @level1name = 'DS_4',  
	@level2type = N'Column', @level2name = 'Id_2';  
GO
