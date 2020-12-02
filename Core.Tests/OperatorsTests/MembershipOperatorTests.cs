namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using Xunit;

    public class MembershipOperatorTests
    {
        [Theory]
        [InlineData("Id1", TestExprType.Integer)]
        [InlineData("Id1", TestExprType.Number)]
        [InlineData("Id1", TestExprType.String)]
        [InlineData("Id1", TestExprType.Boolean)]
        [InlineData("Id1", TestExprType.Time)]
        [InlineData("Id1", TestExprType.Date)]
        [InlineData("Id1", TestExprType.TimePeriod)]
        [InlineData("Me1", TestExprType.Integer)]
        [InlineData("Me1", TestExprType.Number)]
        [InlineData("Me1", TestExprType.String)]
        [InlineData("Me1", TestExprType.Boolean)]
        [InlineData("Me1", TestExprType.Time)]
        [InlineData("Me1", TestExprType.Date)]
        [InlineData("Me1", TestExprType.TimePeriod)]
        [InlineData("NV_At1", TestExprType.Integer)]
        [InlineData("NV_At1", TestExprType.Number)]
        [InlineData("NV_At1", TestExprType.String)]
        [InlineData("NV_At1", TestExprType.Boolean)]
        [InlineData("NV_At1", TestExprType.Time)]
        [InlineData("NV_At1", TestExprType.Date)]
        [InlineData("NV_At1", TestExprType.TimePeriod)]
        [InlineData("V_At1", TestExprType.Integer)]
        [InlineData("V_At1", TestExprType.Number)]
        [InlineData("V_At1", TestExprType.String)]
        [InlineData("V_At1", TestExprType.Boolean)]
        [InlineData("V_At1", TestExprType.Time)]
        [InlineData("V_At1", TestExprType.Date)]
        [InlineData("V_At1", TestExprType.TimePeriod)]
        public void GetOutputStructure_CorrectExprNonJoin_CorrectStructure(string compName, TestExprType compType)
        {
            string name = string.Empty;
            TestExprType? dsType = null;

            switch (compType)
            {
                case TestExprType.Integer:
                    name = "int_var";
                    dsType = TestExprType.IntsDataset;
                    break;
                case TestExprType.Number:
                    name = "num_var";
                    dsType = TestExprType.NumbersDataset;
                    break;
                case TestExprType.String:
                    name = "string_var";
                    dsType = TestExprType.StringsDataset;
                    break;
                case TestExprType.Boolean:
                    name = "bool_var";
                    dsType = TestExprType.BoolsDataset;
                    break;
                case TestExprType.Time:
                    name = "time_var";
                    dsType = TestExprType.TimesDataset;
                    break;
                case TestExprType.Date:
                    name = "date_var";
                    dsType = TestExprType.DatesDataset;
                    break;
                case TestExprType.TimePeriod:
                    name = "period_var";
                    dsType = TestExprType.TimePeriodsDataset;
                    break;
                case TestExprType.Duration:
                    name = "duration_var";
                    dsType = TestExprType.DurationsDataset;
                    break;
                case TestExprType.None:
                    name = "null";
                    dsType = TestExprType.NonesDataset;
                    break;
                default: throw new Exception();
            }

            IExpression membershipExpr = TestExprFactory.GetExpression((TestExprType)dsType, compType);
            membershipExpr.Operands["ds_2"].ExpressionText = compName;
            membershipExpr.Operands["ds_2"].Structure.Components[0].ComponentName = compName;

            IDataStructure expectedStructure = TestExprFactory.GetExpression((TestExprType)dsType).Structure;
            expectedStructure.Measures.Clear();
            if (compName == "Me1") expectedStructure.Measures.Add(new StructureComponent((BasicDataType)compType, compName));
            else
            {
                if (compName != "Id1") expectedStructure.Measures.Add(new StructureComponent((BasicDataType)compType, name));
                else
                {
                    membershipExpr.Operands["ds_2"].Structure = ModelResolvers.DsResolver(compName, ComponentType.Identifier, (BasicDataType)compType);
                    expectedStructure.Measures.Add(new StructureComponent((BasicDataType)compType, compType.GetVariableName()));
                }
            }

            if (compName == "NV_At1")
            {
                membershipExpr.Operands["ds_1"].Structure.NonViralAttributes.Add(new StructureComponent((BasicDataType)compType, compName));
                membershipExpr.Operands["ds_2"].Structure = ModelResolvers.DsResolver(compName, ComponentType.NonViralAttribute, (BasicDataType)compType);
                expectedStructure.NonViralAttributes.Add(new StructureComponent((BasicDataType)compType, compName));
            }
            else if (compName == "V_At1")
            {
                membershipExpr.Operands["ds_1"].Structure.ViralAttributes.Add(new StructureComponent((BasicDataType)compType, compName));
                membershipExpr.Operands["ds_2"].Structure = ModelResolvers.DsResolver(compName, ComponentType.ViralAttribute, (BasicDataType)compType);
                expectedStructure.ViralAttributes.Add(new StructureComponent((BasicDataType)compType, compName));
            }

            membershipExpr.OperatorDefinition = ModelResolvers.OperatorResolver("#");

            IDataStructure dataStructure = membershipExpr.OperatorDefinition.GetOutputStructure(membershipExpr);

            Assert.True(expectedStructure.EqualsObj(dataStructure));
            Assert.Equal(expectedStructure.Measures[0].ComponentName, dataStructure.Measures[0].ComponentName);
        }

        [Theory]
        [InlineData("Id1")]
        [InlineData("Me1")]
        [InlineData("NV_At1")]
        [InlineData("V_At1")]
        public void GetOutputStructure_CorrectExprJoin_CorrectScalarStructure(string compName)
        {
            IExpression membershipExpr = TestExprFactory.GetExpression(TestExprType.IntsDataset, TestExprType.Integer);

            IDataStructure expectedStructure = null;
            switch (compName)
            {
                case "Id1":
                    membershipExpr.Operands["ds_2"].Structure = ModelResolvers.DsResolver(compName, ComponentType.Identifier, BasicDataType.Integer);
                    expectedStructure = ModelResolvers.DsResolver(compName, ComponentType.Identifier, BasicDataType.Integer);
                    break;
                case "Me1":
                    membershipExpr.Operands["ds_2"].Structure = ModelResolvers.DsResolver(compName, ComponentType.Measure, BasicDataType.Integer);
                    expectedStructure = ModelResolvers.DsResolver(compName, ComponentType.Measure, BasicDataType.Integer);
                    break;
                case "NV_At1":
                    membershipExpr.Operands["ds_1"].Structure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "NV_At1"));
                    membershipExpr.Operands["ds_2"].Structure = ModelResolvers.DsResolver(compName, ComponentType.NonViralAttribute, BasicDataType.Integer);
                    expectedStructure = ModelResolvers.DsResolver(compName, ComponentType.NonViralAttribute, BasicDataType.Integer); 
                    break;
                case "V_At1":
                    membershipExpr.Operands["ds_1"].Structure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "V_At1"));
                    membershipExpr.Operands["ds_2"].Structure = ModelResolvers.DsResolver(compName, ComponentType.ViralAttribute, BasicDataType.Integer); 
                    expectedStructure = ModelResolvers.DsResolver(compName, ComponentType.ViralAttribute, BasicDataType.Integer);
                    break;
            }

            membershipExpr.OperatorDefinition = ModelResolvers.OperatorResolver("#");
            membershipExpr.OperatorDefinition.Keyword = "Component";

            IDataStructure dataStructure = membershipExpr.OperatorDefinition.GetOutputStructure(membershipExpr);

            Assert.True(expectedStructure.EqualsObj(dataStructure));
        }

        [Fact]
        public void GetOutputStructure_WrongExpr_ThrowsException()
        {
            IExpression membershipExpr = TestExprFactory.GetExpression(TestExprType.IntsDataset, TestExprType.Integer);
            membershipExpr.Operands["ds_2"].ExpressionText = "Component";
            membershipExpr.Operands["ds_2"].Structure.Components[0].ComponentName = "Component";

            membershipExpr.OperatorDefinition = ModelResolvers.OperatorResolver("#");
            membershipExpr.OperatorDefinition.Keyword = "Standard";

            Assert.ThrowsAny<VtlOperatorError>(() => { membershipExpr.OperatorDefinition.GetOutputStructure(membershipExpr); });
        }
    }
}
