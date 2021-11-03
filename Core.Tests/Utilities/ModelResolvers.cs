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
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
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
        /// Gets the rule expression resolver.
        /// </summary>
        public readonly static RuleExpressionResolver RuleExprResolver;

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

            Mock<RuleExpressionResolver> ruleExprResolverMock = new Mock<RuleExpressionResolver>();
            ruleExprResolverMock.Setup(o => o(It.IsNotNull<IExpression>(), It.IsAny<IRuleset>(), It.IsAny<string>(), It.IsAny<int?>()))
                .Returns((IExpression expression, IRuleset containingRuleset, string errorCode, int? errorLevel) => { return new RuleExpression(expression, containingRuleset, errorCode, errorLevel); });

            Mock<JoinExpressionResolver> joinExprResolverMock = new Mock<JoinExpressionResolver>();
            joinExprResolverMock.Setup(o => o(It.IsNotNull<IExpression>())).Returns((IExpression expression) => { return new JoinExpression(expression); });

            Mock<DataStructureResolver> dsResolverMock = new Mock<DataStructureResolver>();
            dsResolverMock.Setup(o => o(null, null, null)).Returns(() => { return new DataStructure(); });
            dsResolverMock.Setup(o => o(It.IsNotNull<string>(), It.IsNotNull<ComponentType?>(), It.IsNotNull<BasicDataType?>()))
                .Returns((string compName, ComponentType? compType, BasicDataType? dataType) => { return new DataStructure(compName, (ComponentType)compType, (BasicDataType)dataType); });

            Mock<TransformationSchemaResolver> schemaResolverMock = new Mock<TransformationSchemaResolver>();
            schemaResolverMock.Setup(o => o()).Returns(() => { return new TransformationSchema(); });

            ModelResolvers.ExprResolver = exprResolverMock.Object;
            ModelResolvers.RuleExprResolver = ruleExprResolverMock.Object;
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
                    ModelResolvers.RuleExprResolver,
                    ModelResolvers.JoinExprResolver,
                    opResolverMock.Object);

            JoinApplyMeasuresOperator joinApplyMeasuresOp = new JoinApplyMeasuresOperator(
                exprFactory,
                ModelResolvers.DsResolver);

            opResolverMock.Setup(o => o(It.IsAny<string>()))
                .Returns((string key) =>
                {
                    IOperatorDefinition op;

                    if (key.In("count", "min", "max", "median", "sum", "avg", "stddev_pop", "stddev_samp", "var_pop", "var_samp")) op = new AggrFunctionOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key == "aggr") op = new AggrOperator();
                    else if (key.In("first_value", "last_value", "lag", "rank", "ratio_to_report", "lead")) op = new AnalyticFunctionOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key.In("+", "-", "*", "/")) op = new ArithmeticOperator(joinApplyMeasuresOp);
                    else if (key == "between") op = new BetweenOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key.In("and", "or", "xor", "not")) op = new BooleanOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key == "calc") op = new CalcOperator(ModelResolvers.DsResolver);
                    else if (key == "check_datapoint") op = new CheckDatapointOperator(ModelResolvers.DsResolver, exprFactory);
                    else if (key.In("=", "<>", "<", "<=", ">", ">=")) op = new ComparisonOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key == "comp") op = new ComponentOperator(ModelResolvers.DsResolver, new ComponentTypeInference(ModelResolvers.DsResolver));
                    else if (key == "const") op = new ConstantOperator(ModelResolvers.DsResolver);
                    else if (key == "current_date") op = new CurrentDateOperator(ModelResolvers.DsResolver);
                    else if (key == "exists_in") op = new ExistsInOperator(ModelResolvers.DsResolver);
                    else if (key == "get") op = new GetOperator(new Mock<IDataModelProvider>().Object); // operator tests should mock IDataModel implementation
                    else if (key == "if") op = new IfThenElseOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key.In("in", "not_in")) op = new InOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key == "isnull") op = new IsNullOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key == "join") op = new JoinOperator(ModelResolvers.DsResolver);
                    else if (key.In("keep", "drop")) op = new KeepDropOperator(ModelResolvers.DsResolver);
                    else if (key == "match_characters") op = new MatchCharactersOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key == "#") op = new MembershipOperator();
                    else if (key.In("ceil", "floor", "abs", "exp", "ln", "sqrt", "mod", "round", "power", "log", "trunc")) op = new NumericOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key == "nvl") op = new NvlOperator(joinApplyMeasuresOp);
                    else if (key == "opt") op = new OptionalOperator(ModelResolvers.DsResolver);
                    else if (key == "period_indicator") op = new PeriodIndicatorOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver, exprFactory);
                    else if (key == "pivot") op = new PivotOperator(ModelResolvers.DsResolver);
                    else if (key == "ref") op = new ReferenceOperator();
                    else if (key == "rename") op = new RenameOperator(ModelResolvers.DsResolver);
                    else if (key.In("union", "intersect", "setdiff", "symdiff")) op = new SetOperator();
                    else if (key.In("||", "trim", "rtrim", "ltrim", "upper", "lower", "substr", "replace", "instr", "length")) op = new StringOperator(joinApplyMeasuresOp, ModelResolvers.DsResolver);
                    else if (key == "sub") op = new SubspaceOperator(ModelResolvers.DsResolver);
                    else if (key.In("fill_time_series", "flow_to_stock", "stock_to_flow", "timeshift", "time_agg")) op = new TimeOperator();
                    else if (key.In("plus", "minus")) op = new UnaryArithmeticOperator(joinApplyMeasuresOp);
                    else if (key == "unpivot") op = new UnpivotOperator(ModelResolvers.DsResolver);
                    // ---
                    else if (key == "calcExpr") op = new CalcExprOperator(ModelResolvers.DsResolver);
                    else if (key == "collection") op = new CollectionOperator(ModelResolvers.DsResolver);
                    else if (key == "datasetClause") op = new DatasetClauseOperator();
                    else if (key == "group") op = new GroupOperator(ModelResolvers.DsResolver);
                    else if (key == "order") op = new OrderOperator(ModelResolvers.DsResolver);
                    else if (key == "partition") op = new PartitionOperator(ModelResolvers.DsResolver);
                    else if (key == "renameExpr") op = new RenameExprOperator(ModelResolvers.DsResolver);
                    else if (key == "subExpr") op = new SubspaceExprOperator();
                    else if (key == "when") op = new WhenOperator();
                    else throw new InvalidOperationException("Operator not found");

                    op.Symbol = key;
                    return op;
                });

            return opResolverMock.Object;
        }
    }
}
