namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using Xunit;

    public class ComponentOperatorTests
    {
        [Theory]
        [InlineData(ComponentType.Identifier)]
        [InlineData(ComponentType.Measure)]
        [InlineData(ComponentType.ViralAttribute)]
        [InlineData(ComponentType.NonViralAttribute)]
        public void GetOutputStructure_MembershipComponentExpr_DataStructure(ComponentType compType)
        {
            string compName = "component";
            IExpression membershipExpr = ModelResolvers.ExprResolver();
            IExpression dsExpr = ModelResolvers.ExprResolver();
            IExpression compExpr = ModelResolvers.ExprResolver();

            dsExpr.Structure = ModelResolvers.DsResolver(compName, compType, BasicDataType.Integer);
            compExpr.ExpressionText = compName;
            compExpr.OperatorDefinition = ModelResolvers.OperatorResolver("comp");

            membershipExpr.OperatorDefinition = ModelResolvers.OperatorResolver("#");
            membershipExpr.AddOperand("ds_1", dsExpr);
            membershipExpr.AddOperand("ds_2", compExpr);

            IDataStructure dataStructure = compExpr.OperatorDefinition.GetOutputStructure(compExpr);

            Assert.True(dataStructure.IsSingleComponent);
            Assert.True(dsExpr.Structure.Components[0].EqualsObj(dataStructure.Components[0]));
            Assert.Equal(dsExpr.Structure.Components[0].ComponentName, dataStructure.Components[0].ComponentName);
        }

        [Theory]
        [InlineData("identifier")]
        [InlineData("measure")]
        [InlineData("viral attribute")]
        [InlineData("attribute")]
        public void GetOutputStructure_CalcComponentExpr_DataStructure(string keyword)
        {
            IExpression calcExpr = ModelResolvers.ExprResolver();
            IExpression compExpr = ModelResolvers.ExprResolver();

            compExpr.ExpressionText = "component";
            compExpr.OperatorDefinition = ModelResolvers.OperatorResolver("comp");
            calcExpr.OperatorDefinition = ModelResolvers.OperatorResolver("calcExpr");
            calcExpr.OperatorDefinition.Keyword = keyword;
            calcExpr.AddOperand("ds_1", compExpr);

            ComponentType compType;
            switch (keyword)
            {
                case "identifier": compType = ComponentType.Identifier; break;
                case "measure": compType = ComponentType.Measure; break;
                case "attribute": compType = ComponentType.NonViralAttribute; break;
                case "viral attribute": compType = ComponentType.ViralAttribute; break;
                default: throw new ArgumentOutOfRangeException("keyword");
            }

            IDataStructure dataStructure = compExpr.OperatorDefinition.GetOutputStructure(compExpr);

            Assert.True(dataStructure.IsSingleComponent);
            Assert.Equal(compType, dataStructure.Components[0].ComponentType);
        }

        [Theory]
        [InlineData(ComponentType.Identifier)]
        [InlineData(ComponentType.Measure)]
        [InlineData(ComponentType.ViralAttribute)]
        [InlineData(ComponentType.NonViralAttribute)]
        public void GetOutputStructure_RenameComponentExpr_DataStructure(ComponentType compType)
        {
            IExpression renameExpr = ModelResolvers.ExprResolver();
            IExpression compExpr = ModelResolvers.ExprResolver();
            IExpression oldCompExpr = ModelResolvers.ExprResolver();

            compExpr.ExpressionText = "component";
            compExpr.OperatorDefinition = ModelResolvers.OperatorResolver("comp");
            oldCompExpr.Structure = ModelResolvers.DsResolver("oldComponent", compType, BasicDataType.Integer);
            renameExpr.OperatorDefinition = ModelResolvers.OperatorResolver("renameExpr");
            renameExpr.AddOperand("ds_1", oldCompExpr);
            renameExpr.AddOperand("ds_2", compExpr);

            IDataStructure dataStructure = compExpr.OperatorDefinition.GetOutputStructure(compExpr);

            Assert.True(dataStructure.IsSingleComponent);
            Assert.True(oldCompExpr.Structure.Components[0].EqualsObj(dataStructure.Components[0]));
        }
    }
}
