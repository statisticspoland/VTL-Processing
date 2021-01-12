CREATE TABLE [Stock_to_flow].[DS_1]
(
	[Id_1] VARCHAR(32) NOT NULL,
	[Id_2] VARCHAR(32) NOT NULL,
	[Me_1] INT
	CONSTRAINT [PK_Stock_to_flow_DS_1] PRIMARY KEY CLUSTERED ([Id_1], [Id_2])
)
GO

EXEC sp_addextendedproperty   
	@name = N'vtl_time_type',   
	@value = 'time',  
	@level0type = N'Schema', @level0name = 'Stock_to_flow',  
	@level1type = N'Table',  @level1name = 'DS_1',  
	@level2type = N'Column', @level2name = 'Id_2';  
GO
