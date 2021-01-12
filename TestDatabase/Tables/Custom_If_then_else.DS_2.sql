CREATE TABLE [Custom_If_then_else].[DS_2]
(
	[Id_1] INT NOT NULL,
	[Id_2] VARCHAR(32) NOT NULL,
	[Id_3] VARCHAR(32) NOT NULL,
	[Me_1] INT,
	[Me_2] INT,
	[At_1] VARCHAR(32)
	CONSTRAINT [PK_Custom_If_then_else_DS_2] PRIMARY KEY CLUSTERED ([Id_1], [Id_2], [Id_3])
)
GO

EXEC sp_addextendedproperty   
	@name = N'vtl_component_role',   
	@value = 'attribute.viral',  
	@level0type = N'Schema', @level0name = 'Custom_If_then_else',  
	@level1type = N'Table',  @level1name = 'DS_2',  
	@level2type = N'Column', @level2name = 'At_1';  
GO
