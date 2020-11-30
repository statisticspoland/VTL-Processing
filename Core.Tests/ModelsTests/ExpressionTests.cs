namespace StatisticsPoland.VtlProcessing.Core.Tests.ModelsTests
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ExpressionTests
    {
        [Fact]
        public void Constructor_HasCorrectParentExpression()
        {
            IExpression parentExpr = ModelResolvers.ExprResolver();
            Expression expr = new Expression(parentExpr);

            Assert.Equal(parentExpr, expr.ParentExpression);
        }

        [Fact]
        public void ExpressionText_TextStartingWithSpace_TransformedText()
        {
            Expression expr = new Expression();
            expr.ExpressionText = "   space";

            Assert.Equal("space", expr.ExpressionText);
        }

        [Fact]
        public void ExpressionText_TextWithNewLine_TransformedText()
        {
            Expression expr = new Expression();
            expr.ExpressionText = "The new\r\nline";

            Assert.Equal("The new line", expr.ExpressionText);
        }

        [Theory]
        [InlineData("+")]
        [InlineData("ref")]
        [InlineData("sqrt")]
        public void OperatorDefinition_Set_OperatorSymbolSet(string opSymbol)
        {
            Expression expr = new Expression();
            expr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

            Assert.Equal(opSymbol, expr.OperatorSymbol);
        }

        [Fact]
        public void ResultName_AliasAndApplyAncestor_IsApplyComponentAndIsScalarTrue()
        {
            Expression applyExpr = new Expression();
            applyExpr.ResultName = "Apply";

            Expression expr = new Expression();
            expr.ResultName = "Alias";

            applyExpr.AddOperand("ds_1", expr);

            Assert.True(expr.IsApplyComponent);
            Assert.True(expr.IsScalar);
        }

        [Fact]
        public void ResultName_ApplyAndAliasDescendant_IsApplyComponentAndIsScalarTrue()
        {
            Expression aliasExpr = new Expression();
            aliasExpr.ResultName = "Alias";

            Expression expr = new Expression();
            expr.ResultName = "Apply";
            expr.AddOperand("ds_1", aliasExpr);

            Assert.True(expr.IsApplyComponent);
            Assert.True(expr.IsScalar);
        }

        [Fact]
        public void Expression_IsApplyComponentAndIsScalarFalse()
        {
            Expression expr = new Expression();

            Assert.False(expr.IsApplyComponent);
            Assert.False(expr.IsScalar);
        }

        [Fact]
        public void Structure_IsSingleComponentTrue_IsScalarTrue()
        {
            Expression expr = new Expression();
            expr.Structure = ModelResolvers.DsResolver("comp", ComponentType.Measure, BasicDataType.Integer);

            Assert.True(expr.IsScalar);
        }

        [Fact]
        public void Structure_IsSingleComponentFalse_IsScalarFalse()
        {
            Expression expr = new Expression();
            expr.Structure = ModelResolvers.DsResolver("comp1", ComponentType.Measure, BasicDataType.Integer);
            expr.Structure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp2"));

            Assert.False(expr.IsScalar);
        }

        [Fact]
        public void CurrentJoinExpr_JoinExprAncestor_CorrectExpression()
        {
            IJoinExpression joinExpr = ModelResolvers.JoinExprResolver(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            Expression expr = new Expression(joinExpr);

            Assert.Equal(joinExpr, expr.CurrentJoinExpr);
        }

        [Fact]
        public void CurrentJoinExpr_NotJoinExprAncestor_Null()
        {
            Expression parentExpr = new Expression();
            Expression expr = new Expression(parentExpr);

            Assert.Null(expr.CurrentJoinExpr);
        }

        [Fact]
        public void OperandsCollection_Set_OperandsSetToo()
        {
            List<IExpression> exprCollection = new List<IExpression>() 
            {
                ModelResolvers.ExprResolver(),
                ModelResolvers.ExprResolver(),
                ModelResolvers.ExprResolver()
            };

            for (int i = 0; i < exprCollection.Count; i++)
            {
                exprCollection[i].ParamSignature = $"ds_{i}";
            }
            
            Expression expr = new Expression();
            expr.OperandsCollection = exprCollection;

            Assert.Equal(expr.OperandsCollection.Count, expr.Operands.Count);
            for (int i = 0; i < expr.OperandsCollection.Count; i++)
            {
                Assert.Equal(expr.OperandsCollection.ToArray()[i], expr.Operands.Values.ToArray()[i]);
            }
        }

        [Fact]
        public void AddOperand_Expression_AddsExpression()
        {
            Expression operandExpr = new Expression();
            Expression expr = new Expression();

            Assert.Equal(0, expr.OperandsCollection.Count);
            Assert.Equal(0, expr.Operands.Count);

            expr.AddOperand("ds_1", operandExpr);

            Assert.Equal(1, expr.OperandsCollection.Count);
            Assert.Equal(1, expr.Operands.Count);
            Assert.Equal(operandExpr, expr.OperandsCollection.First());
            Assert.Equal(operandExpr, expr.Operands.First().Value);
        }

        [Fact]
        public void AddOperand_ExpressionWithExistingSignature_OverridesOperand()
        {
            Expression operandExpr = new Expression();
            Expression expr = new Expression();
            expr.AddOperand("ds_1", ModelResolvers.ExprResolver());
            expr.AddOperand("ds_1", operandExpr);

            Assert.Equal(1, expr.OperandsCollection.Count);
            Assert.Equal(1, expr.Operands.Count);
            Assert.Equal(operandExpr, expr.OperandsCollection.First());
            Assert.Equal(operandExpr, expr.Operands.First().Value);
        }

        [Fact]
        public void AddOperand_Null_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => { new Expression().AddOperand("ds_1", null); });
        }

        [Fact]
        public void SetContainingSchema_Schema_SetsSchema()
        {
            ITransformationSchema schema = ModelResolvers.SchemaResolver();
            Expression expr = new Expression();
            expr.AddOperand("ds_1", ModelResolvers.ExprResolver());

            Assert.Null(expr.ContainingSchema);
            Assert.Null(expr.Operands["ds_1"].ContainingSchema);

            expr.SetContainingSchema(schema);

            Assert.Equal(expr.ContainingSchema, schema);
            Assert.Equal(expr.Operands["ds_1"].ContainingSchema, schema);
        }

        [Fact]
        public void GetFirstAncestorExpr_FirstAncestorExpr()
        {
            IExpression grandGrandExpr = ModelResolvers.ExprResolver();
            IExpression grandExpr = ModelResolvers.ExprResolver(grandGrandExpr);
            IExpression fatherExpr = ModelResolvers.ExprResolver(grandExpr);
            Expression expr = new Expression(fatherExpr);

            Assert.Equal(grandGrandExpr, expr.GetFirstAncestorExpr());
        }

        [Fact]
        public void GetFirstAncestorExpr_Name_FirstAncestorExprWithGivenName()
        {
            IExpression grandGrandExpr = ModelResolvers.ExprResolver();
            IExpression grandExpr = ModelResolvers.ExprResolver(grandGrandExpr);
            IExpression fatherExpr = ModelResolvers.ExprResolver(grandExpr);
            Expression expr = new Expression(fatherExpr);

            grandExpr.ResultName = "GrandExpr";

            Assert.Equal(grandExpr, expr.GetFirstAncestorExpr("GrandExpr"));
        }

        [Fact]
        public void GetDescendantExprs_Name_DescendantExprsWithGivenName()
        {
            Expression expr = new Expression();
            IExpression childExpr1 = ModelResolvers.ExprResolver();
            IExpression childExpr2 = ModelResolvers.ExprResolver();
            IExpression grandChildExpr1_1 = ModelResolvers.ExprResolver();
            IExpression grandChildExpr1_2 = ModelResolvers.ExprResolver();
            IExpression grandChildExpr2_1 = ModelResolvers.ExprResolver();
            IExpression grandChildExpr2_2 = ModelResolvers.ExprResolver();

            childExpr1.ResultName = "childExpr1";
            childExpr2.ResultName = "childExpr2";
            grandChildExpr1_1.ResultName = "childExpr1";
            grandChildExpr1_2.ResultName = "childExpr2";
            grandChildExpr2_1.ResultName = "childExpr1";
            grandChildExpr2_2.ResultName = "childExpr2";

            expr.AddOperand("ds_1", childExpr1);
            expr.AddOperand("ds_2", childExpr2);
            childExpr1.AddOperand("ds_1", grandChildExpr1_1);
            childExpr1.AddOperand("ds_2", grandChildExpr1_2);
            childExpr2.AddOperand("ds_1", grandChildExpr2_1);
            childExpr2.AddOperand("ds_2", grandChildExpr2_2);

            List<IExpression> descendantExprs = new List<IExpression>()
            {
                childExpr1,
                grandChildExpr1_1,
                grandChildExpr2_1
            };

            ICollection<IExpression> result = expr.GetDescendantExprs("childExpr1");

            Assert.NotNull(result);
            Assert.Equal(descendantExprs.Count, result.Count);
            
            foreach (IExpression childExpr in result)
            {
                Assert.Contains(childExpr, descendantExprs);
            }
        }
    }
}
