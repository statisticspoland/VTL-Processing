namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class SubspaceExprOperatorTests
    {
        [Fact]
        public void GetOutputStructure_Correct()
        {
            IExpression expr = ModelResolvers.ExprResolver();
            expr.OperatorDefinition = ModelResolvers.OperatorResolver("subExpr");

            IExpression compExpr = TestExprFactory.GetDatasetExpr(new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.String));
            IExpression constExpr = TestExprFactory.GetDatasetExpr(new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.String));
            expr.AddOperand("ds_1", compExpr);
            expr.AddOperand("ds_2", constExpr);

            IDataStructure dataStructure = expr.OperatorDefinition.GetOutputStructure(expr);

            Assert.True(dataStructure.Identifiers.Count() == 1);
            Assert.True(dataStructure.Components.Count() == 1);
            Assert.True(dataStructure.Identifiers.First().ComponentName == compExpr.Structure.Identifiers.First().ComponentName);
            Assert.True(dataStructure.Identifiers.First().ValueDomain.DataType == constExpr.Structure.Measures.First().ValueDomain.DataType);
        }

        [Fact]
        public void GetOutputStructure_Wrong()
        {
            IExpression expr = ModelResolvers.ExprResolver();
            expr.OperatorDefinition = ModelResolvers.OperatorResolver("subExpr");

            IExpression compExpr = TestExprFactory.GetDatasetExpr(new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Identifier, BasicDataType.String));
            IExpression constExpr = TestExprFactory.GetDatasetExpr(new KeyValuePair<ComponentType, BasicDataType>(ComponentType.Measure, BasicDataType.Integer));
            expr.AddOperand("ds_1", compExpr);
            expr.AddOperand("ds_2", constExpr);

            Assert.ThrowsAny<Exception>(() => { expr.OperatorDefinition.GetOutputStructure(expr); });

        }
    }
}