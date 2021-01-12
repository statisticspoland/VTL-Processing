CREATE TABLE [Aggregate_invocation].[DS_1B]
(
	[Id_1] INT NOT NULL,
	[Id_2] VARCHAR(32) NOT NULL,
	[Id_3] VARCHAR(32) NOT NULL,
	[Me_1] INT,
	[At_1] VARCHAR(32)
	CONSTRAINT [PK_Aggregate_invocation_DS_1B] PRIMARY KEY CLUSTERED ([Id_1], [Id_2], [Id_3])
)
GO

EXEC sp_addextendedproperty   
	@name = N'vtl_component_role',   
	@value = 'attribute.viral',  
	@level0type = N'Schema', @level0name = 'Aggregate_invocation',  
	@level1type = N'Table',  @level1name = 'DS_1B',  
	@level2type = N'Column', @level2name = 'At_1';  
GO
