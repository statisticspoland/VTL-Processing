namespace StatisticsPoland.VtlProcessing.Core.Tests.Utilities
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;

    /// <summary>
    /// The model resolvers container.
    /// </summary>
    public static class ModelResolvers
    {
        /// <summary>
        /// Gets the expression resolver.
        /// </summary>
        public readonly static ExpressionResolver ExprResolver;

        /// <summary>
        /// Gets the data structure resolver.
        /// </summary>
        public readonly static DataStructureResolver DsResolver;

        /// <summary>
        /// Gets the transformation schema resolver.
        /// </summary>
        public readonly static TransformationSchemaResolver SchemaResolver;

        /// <summary>
        /// Initializes resolvers.
        /// </summary>
        static ModelResolvers()
        {
            Mock<ExpressionResolver> exprResolverMock = new Mock<ExpressionResolver>();
            exprResolverMock.Setup(o => o(null)).Returns(() => { return new Expression(); });
            exprResolverMock.Setup(o => o(It.IsNotNull<IExpression>())).Returns((IExpression parentExpr) => { return new Expression(parentExpr); });

            Mock<DataStructureResolver> dsResolverMock = new Mock<DataStructureResolver>();
            dsResolverMock.Setup(o => o(null, null, null)).Returns(() => { return new DataStructure(); });
            dsResolverMock.Setup(o => o(It.IsNotNull<string>(), It.IsNotNull<ComponentType?>(), It.IsNotNull<BasicDataType?>()))
                .Returns((string compName, ComponentType? compType, BasicDataType? dataType) => { return new DataStructure(compName, (ComponentType)compType, (BasicDataType)dataType); });

            Mock<TransformationSchemaResolver> schemaResolverMock = new Mock<TransformationSchemaResolver>();
            schemaResolverMock.Setup(o => o()).Returns(() => { return new TransformationSchema(); });

            ModelResolvers.ExprResolver = exprResolverMock.Object;
            ModelResolvers.DsResolver = dsResolverMock.Object;
            ModelResolvers.SchemaResolver = schemaResolverMock.Object;
        }
    }
}
