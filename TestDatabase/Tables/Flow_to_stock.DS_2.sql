CREATE TABLE [Flow_to_stock].[DS_2]
(
	[Id_1] VARCHAR(32) NOT NULL,
	[Id_2] VARCHAR(32) NOT NULL,
	[Me_1] INT
	CONSTRAINT [PK_Flow_to_stock_DS_2] PRIMARY KEY CLUSTERED ([Id_1], [Id_2])
)
GO

EXEC sp_addextendedproperty   
	@name = N'vtl_time_type',   
	@value = 'date',  
	@level0type = N'Schema', @level0name = 'Flow_to_stock',  
	@level1type = N'Table',  @level1name = 'DS_2',  
	@level2type = N'Column', @level2name = 'Id_2';  
GO
