namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Linq;
    using Xunit;

    public class PeriodIndicatorOperatorTests
    {
        [Fact]
        public void GetOutputStructure_CorrectNoParamDatasetClauseExpr_DurationScalarStructure()
        {
            foreach (string name in DatasetClauseOperator.ClauseNames)
            {
                IExpression datasetClauseExpr = ModelResolvers.ExprResolver();
                IExpression datasetExpr = ModelResolvers.ExprResolver();
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);

                datasetClauseExpr.AddOperand("ds_1", datasetExpr);
                datasetClauseExpr.AddOperand("ds_2", clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);

                datasetExpr.Structure = ModelResolvers.DsResolver();
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
                datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

                IDataStructure expectedStructure = ModelResolvers.DsResolver("duration_var", ComponentType.Measure, BasicDataType.Duration);

                IDataStructure dataStructure = periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Fact]
        public void GetOutputStructure_DoubleNoParamDatasetClauseExpr_ThrowsException()
        {
            foreach (string name in DatasetClauseOperator.ClauseNames)
            {
                IExpression datasetClauseExpr = ModelResolvers.ExprResolver();
                IExpression datasetExpr = ModelResolvers.ExprResolver();
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);

                datasetClauseExpr.AddOperand("ds_1", datasetExpr);
                datasetClauseExpr.AddOperand("ds_2", clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);

                datasetExpr.Structure = ModelResolvers.DsResolver();
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id1"));
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
                datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

                Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
            }
        }

        [Theory]
        [InlineData(BasicDataType.Integer)]
        [InlineData(BasicDataType.Number)]
        [InlineData(BasicDataType.String)]
        [InlineData(BasicDataType.Boolean)]
        [InlineData(BasicDataType.Time)]
        [InlineData(BasicDataType.Date)]
        [InlineData(BasicDataType.Duration)]
        public void GetOutputStructure_WrongNoParamDatasetClauseExpr_ThrowsException(BasicDataType type)
        {
            foreach (string name in DatasetClauseOperator.ClauseNames)
            {
                IExpression datasetClauseExpr = ModelResolvers.ExprResolver();
                IExpression datasetExpr = ModelResolvers.ExprResolver();
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);

                datasetClauseExpr.AddOperand("ds_1", datasetExpr);
                datasetClauseExpr.AddOperand("ds_2", clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);

                datasetExpr.Structure = ModelResolvers.DsResolver();
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(type, "Id2"));
                datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

                Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
            }
        }

        [Fact]
        public void GetOutputStructure_CorrectNoParamOneDatasetJoinExpr_DurationScalarStructure()
        {
            foreach (string name in DatasetClauseOperator.ClauseNames.Where(name => !name.In("Pivot", "Unpivot", "Subspace")))
            {
                IExpression joinExpr = ModelResolvers.JoinExprResolver(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
                IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);

                joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());
                joinExpr.Operands["ds"].AddOperand("ds_1", datasetExpr);
                joinExpr.AddOperand(name, clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);

                datasetExpr.Structure = ModelResolvers.DsResolver();
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
                datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

                IDataStructure expectedStructure = ModelResolvers.DsResolver("duration_var", ComponentType.Measure, BasicDataType.Duration);

                IDataStructure dataStructure = periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Fact]
        public void GetOutputStructure_DoubleNoParamOneDatasetJoinExpr_ThrowsException()
        {
            foreach (string name in DatasetClauseOperator.ClauseNames.Where(name => !name.In("Pivot", "Unpivot", "Subspace")))
            {
                IExpression joinExpr = ModelResolvers.JoinExprResolver(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
                IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);

                joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());
                joinExpr.Operands["ds"].AddOperand("ds_1", datasetExpr);
                joinExpr.AddOperand(name, clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);

                datasetExpr.Structure = ModelResolvers.DsResolver();
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id1"));
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
                datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

                Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
            }
        }

        [Theory]
        [InlineData(BasicDataType.Integer)]
        [InlineData(BasicDataType.Number)]
        [InlineData(BasicDataType.String)]
        [InlineData(BasicDataType.Boolean)]
        [InlineData(BasicDataType.Time)]
        [InlineData(BasicDataType.Date)]
        [InlineData(BasicDataType.Duration)]
        public void GetOutputStructure_WrongNoParamOneDatasetJoinExpr_ThrowsException(BasicDataType type)
        {
            foreach (string name in DatasetClauseOperator.ClauseNames.Where(name => !name.In("Pivot", "Unpivot", "Subspace")))
            {
                IExpression joinExpr = ModelResolvers.JoinExprResolver(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
                IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);

                joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());
                joinExpr.Operands["ds"].AddOperand("ds_1", datasetExpr);
                joinExpr.AddOperand(name, clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);

                datasetExpr.Structure = ModelResolvers.DsResolver();
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(type, "Id2"));
                datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

                Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
            }
        }

        [Fact]
        public void GetOutputStructure_CorrectScalarExpr_DurationScalarStructure()
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression scalarExpr = TestExprFactory.GetExpression(TestExprType.TimePeriod);
            scalarExpr.OperatorDefinition = ModelResolvers.OperatorResolver("const");

            periodIndicatorExpr.AddOperand("ds_1", scalarExpr);

            IDataStructure dataStructure = periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr);

            Assert.True(dataStructure.IsSingleComponent);
            Assert.True(dataStructure.Measures.Count == 1);
            Assert.Equal(BasicDataType.Duration, dataStructure.Measures[0].ValueDomain.DataType);
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.String)]
        [InlineData(TestExprType.Boolean)]
        [InlineData(TestExprType.Time)]
        [InlineData(TestExprType.Date)]
        [InlineData(TestExprType.Duration)]
        public void GetOutputStructure_WrongScalarExpr_ThrowsException(TestExprType type)
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression scalarExpr = TestExprFactory.GetExpression(type);
            scalarExpr.OperatorDefinition = ModelResolvers.OperatorResolver("const");

            periodIndicatorExpr.AddOperand("ds_1", scalarExpr);

            Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
        }

        [Fact]
        public void GetOutputStructure_CorrectComponentOneDatasetJoinExpr_DurationScalarStructure()
        {
            foreach (string name in DatasetClauseOperator.ClauseNames.Where(name => !name.In("Pivot", "Unpivot", "Subspace")))
            {
                IExpression joinExpr = ModelResolvers.JoinExprResolver(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
                IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression paramExpr = TestExprFactory.GetExpression(TestExprType.TimePeriod);

                joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());
                joinExpr.Operands["ds"].AddOperand("ds_1", datasetExpr);
                joinExpr.AddOperand(name, clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);
                periodIndicatorExpr.AddOperand("ds_1", paramExpr);

                datasetExpr.Structure = ModelResolvers.DsResolver();
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
                datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

                IDataStructure expectedStructure = ModelResolvers.DsResolver("duration_var", ComponentType.Measure, BasicDataType.Duration);

                IDataStructure dataStructure = periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.String)]
        [InlineData(TestExprType.Boolean)]
        [InlineData(TestExprType.Time)]
        [InlineData(TestExprType.Date)]
        [InlineData(TestExprType.Duration)]
        public void GetOutputStructure_WrongComponentOneDatasetJoinExpr_DurationScalarStructure(TestExprType type)
        {
            foreach (string name in DatasetClauseOperator.ClauseNames.Where(name => !name.In("Pivot", "Unpivot", "Subspace")))
            {
                IExpression joinExpr = ModelResolvers.JoinExprResolver(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
                IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression paramExpr = TestExprFactory.GetExpression(type);

                joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());
                joinExpr.Operands["ds"].AddOperand("ds_1", datasetExpr);
                joinExpr.AddOperand(name, clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);
                periodIndicatorExpr.AddOperand("ds_1", paramExpr);

                datasetExpr.Structure = ModelResolvers.DsResolver();
                datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                datasetExpr.Structure.Identifiers.Add(new StructureComponent((BasicDataType)type, "Id2"));
                datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

                Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
            }
        }

        [Fact]
        public void GetOutputStructure_CorrectComponentTwoDatasetsJoinExpr_DurationScalarStructure()
        {
            foreach (string name in DatasetClauseOperator.ClauseNames.Where(name => !name.In("Pivot", "Unpivot", "Subspace")))
            {
                IExpression joinExpr = ModelResolvers.JoinExprResolver(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
                IExpression ds1Expr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression ds2Expr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression paramExpr = TestExprFactory.GetExpression(TestExprType.TimePeriod);
                paramExpr.OperatorDefinition = ModelResolvers.OperatorResolver("#");

                joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());
                joinExpr.Operands["ds"].AddOperand("ds_1", ds1Expr);
                joinExpr.Operands["ds"].AddOperand("ds_2", ds2Expr);
                joinExpr.AddOperand(name, clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);
                periodIndicatorExpr.AddOperand("ds_1", paramExpr);

                ds1Expr.Structure = ModelResolvers.DsResolver();
                ds1Expr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                ds1Expr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
                ds1Expr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

                IDataStructure expectedStructure = ModelResolvers.DsResolver("duration_var", ComponentType.Measure, BasicDataType.Duration);

                IDataStructure dataStructure = periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Theory]
        [InlineData(false, TestExprType.TimePeriod)]
        [InlineData(false, TestExprType.Integer)]
        [InlineData(false, TestExprType.Number)]
        [InlineData(false, TestExprType.String)]
        [InlineData(false, TestExprType.Boolean)]
        [InlineData(false, TestExprType.Time)]
        [InlineData(false, TestExprType.Date)]
        [InlineData(false, TestExprType.Duration)]
        [InlineData(true, TestExprType.Integer)]
        [InlineData(true, TestExprType.Number)]
        [InlineData(true, TestExprType.String)]
        [InlineData(true, TestExprType.Boolean)]
        [InlineData(true, TestExprType.Time)]
        [InlineData(true, TestExprType.Date)]
        [InlineData(true, TestExprType.Duration)]
        public void GetOutputStructure_WrongComponentTwoDatasetsJoinExpr_DurationScalarStructure(bool isMemership, TestExprType type)
        {
            foreach (string name in DatasetClauseOperator.ClauseNames.Where(name => !name.In("Pivot", "Unpivot", "Subspace")))
            {
                IExpression joinExpr = ModelResolvers.JoinExprResolver(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
                IExpression ds1Expr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression ds2Expr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression clauseExpr = TestExprFactory.GetExpression(name, ExpressionFactoryNameTarget.ResultName);
                IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
                IExpression paramExpr = TestExprFactory.GetExpression(type);
                if (isMemership) paramExpr.OperatorDefinition = ModelResolvers.OperatorResolver("#");

                joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());
                joinExpr.Operands["ds"].AddOperand("ds_1", ds1Expr);
                joinExpr.Operands["ds"].AddOperand("ds_2", ds2Expr);
                joinExpr.AddOperand(name, clauseExpr);
                clauseExpr.AddOperand("ds_1", periodIndicatorExpr);
                periodIndicatorExpr.AddOperand("ds_1", paramExpr);

                ds1Expr.Structure = ModelResolvers.DsResolver();
                ds1Expr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                ds1Expr.Structure.Identifiers.Add(new StructureComponent((BasicDataType)type, "Id2"));
                ds1Expr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));
                ds2Expr.Structure = ds1Expr.Structure.GetCopy();

                Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
            }
        }

        [Fact]
        public void GetOutputStructure_CorrectDatasetParamExpr_DurationScalarStructure()
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);

            periodIndicatorExpr.AddOperand("ds_1", datasetExpr);

            datasetExpr.Structure = ModelResolvers.DsResolver();
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
            datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

            IDataStructure expectedStructure = datasetExpr.Structure.GetCopy();
            expectedStructure.Measures[0] = new StructureComponent(BasicDataType.Duration, "duration_var");

            IDataStructure dataStructure = periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr);

            Assert.True(expectedStructure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_DoubleCompDatasetParamExpr_ThrowsException()
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);

            periodIndicatorExpr.AddOperand("ds_1", datasetExpr);

            datasetExpr.Structure = ModelResolvers.DsResolver();
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id1"));
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.TimePeriod, "Id2"));
            datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

            Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
        }

        [Theory]
        [InlineData(TestExprType.Integer)]
        [InlineData(TestExprType.Number)]
        [InlineData(TestExprType.String)]
        [InlineData(TestExprType.Boolean)]
        [InlineData(TestExprType.Time)]
        [InlineData(TestExprType.Date)]
        [InlineData(TestExprType.Duration)]
        public void GetOutputStructure_WrongDatasetParamExpr_ThrowsException(TestExprType type)
        {
            IExpression periodIndicatorExpr = TestExprFactory.GetExpression("period_indicator", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression datasetExpr = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);

            periodIndicatorExpr.AddOperand("ds_1", datasetExpr);

            datasetExpr.Structure = ModelResolvers.DsResolver();
            datasetExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            datasetExpr.Structure.Identifiers.Add(new StructureComponent((BasicDataType)type, "Id2"));
            datasetExpr.Structure.Measures.Add(new StructureComponent(BasicDataType.String, "Me1"));

            Assert.ThrowsAny<VtlOperatorError>(() => { periodIndicatorExpr.OperatorDefinition.GetOutputStructure(periodIndicatorExpr); });
        }
    }
}
