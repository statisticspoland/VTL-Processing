namespace StatisticsPoland.VtlProcessing.Core.Tests.Utilities
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Utilities;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using System;

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
        /// Gets the "join" expression resolver.
        /// </summary>
        public readonly static JoinExpressionResolver JoinExprResolver;

        /// <summary>
        /// Gets the data structure resolver.
        /// </summary>
        public readonly static DataStructureResolver DsResolver;

        /// <summary>
        /// Gets the transformation schema resolver.
        /// </summary>
        public readonly static TransformationSchemaResolver SchemaResolver;

        /// <summary>
        /// Gets the operator resolver.
        /// </summary>
        public readonly static OperatorResolver OperatorResolver;

        /// <summary>
        /// Initializes resolvers.
        /// </summary>
        static ModelResolvers()
        {
            Mock<ExpressionResolver> exprResolverMock = new Mock<ExpressionResolver>();
            exprResolverMock.Setup(o => o(null)).Returns(() => { return new Expression(); });
            exprResolverMock.Setup(o => o(It.IsNotNull<IExpression>())).Returns((IExpression parentExpr) => { return new Expression(parentExpr); });

            Mock<JoinExpressionResolver> joinExprResolverMock = new Mock<JoinExpressionResolver>();
            joinExprResolverMock.Setup(o => o(It.IsNotNull<IExpression>())).Returns((IExpression expression) => { return new JoinExpression(expression); });

            Mock<DataStructureResolver> dsResolverMock = new Mock<DataStructureResolver>();
            dsResolverMock.Setup(o => o(null, null, null)).Returns(() => { return new DataStructure(); });
            dsResolverMock.Setup(o => o(It.IsNotNull<string>(), It.IsNotNull<ComponentType?>(), It.IsNotNull<BasicDataType?>()))
                .Returns((string compName, ComponentType? compType, BasicDataType? dataType) => { return new DataStructure(compName, (ComponentType)compType, (BasicDataType)dataType); });

            Mock<TransformationSchemaResolver> schemaResolverMock = new Mock<TransformationSchemaResolver>();
            schemaResolverMock.Setup(o => o()).Returns(() => { return new TransformationSchema(); });

            ModelResolvers.ExprResolver = exprResolverMock.Object;
            ModelResolvers.JoinExprResolver = joinExprResolverMock.Object;
            ModelResolvers.DsResolver = dsResolverMock.Object;
            ModelResolvers.SchemaResolver = schemaResolverMock.Object;
            ModelResolvers.OperatorResolver = ModelResolvers.InitOperatorResolver();
        }

        /// <summary>
        /// Initializes the operator resolver.
        /// </summary>
        /// <returns>The operator resolver.</returns>
        private static OperatorResolver InitOperatorResolver()
        {
            Mock<OperatorResolver> opResolverMock = new Mock<OperatorResolver>();
            IExpressionFactory exprFactory = 
                new ExpressionFactory(
                    ModelResolvers.ExprResolver,
                    ModelResolvers.JoinExprResolver,
                    opResolverMock.Object);

            JoinApplyMeasuresOperator joinApplyMeasuresOp = new JoinApplyMeasuresOperator(
                exprFactory,
                ModelResolvers.DsResolver);

            opResolverMock.Setup(o => o(It.IsAny<string>()))
                .Returns((string key) =>
                {
                    if (key.In("+", "-", "*", "/")) return new ArithmeticOperator(joinApplyMeasuresOp, key);
                    else if (key == "between") return new BetweenOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key.In("and", "or", "xor", "not")) return new BooleanOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, key);
                    else if (key == "calc") return new CalcOperator(ModelResolvers.DsResolver);
                    else if (key.In("=", "<>", "<", "<=", ">", ">=")) return new ComparisonOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, key);
                    else if (key == "comp") return new ComponentOperator(ModelResolvers.DsResolver, new ComponentTypeInference(ModelResolvers.DsResolver));
                    else if (key == "const") return new ConstantOperator(ModelResolvers.DsResolver);
                    else if (key == "get") return new GetOperator(new Mock<IDataModel>().Object); // operator tests should mock IDataModel implementation
                    else if (key == "isnull") return new IsNullOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key == "join") return new JoinOperator(ModelResolvers.DsResolver);
                    else if (key.In("keep", "drop")) return new KeepDropOperator(ModelResolvers.DsResolver, key);
                    else if (key == "#") return new MembershipOperator();
                    else if (key.In("ceil", "floor", "abs", "exp", "ln", "sqrt", "mod", "round", "power", "log", "trunc")) return new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, key);
                    else if (key == "opt") return new OptionalOperator(ModelResolvers.DsResolver);
                    else if (key == "ref") return new ReferenceOperator();
                    else if (key == "rename") return new RenameOperator(ModelResolvers.DsResolver);
                    else if (key.In("||", "trim", "rtrim", "ltrim", "upper", "lower", "substr", "replace", "instr", "length")) return new StringOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, key);
                    else if (key.In("plus", "minus")) return new UnaryArithmeticOperator(joinApplyMeasuresOp, key);
                    // ---
                    else if (key == "calcExpr") return new CalcExprOperator(ModelResolvers.DsResolver);
                    else if (key == "renameExpr") return new RenameExprOperator(ModelResolvers.DsResolver);
                    
                    throw new Exception("Operator not found");
                });

            return opResolverMock.Object;
        }
    }
}
