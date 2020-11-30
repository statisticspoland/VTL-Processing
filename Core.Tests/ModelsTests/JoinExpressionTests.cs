namespace StatisticsPoland.VtlProcessing.Core.Tests.ModelsTests
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class JoinExpressionTests
    {
        [Fact]
        public void Constructor_JoinOperatorExpr_JoinExpr()
        {
            IExpression expr = TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression refExpr = ModelResolvers.ExprResolver();
            ITransformationSchema schema = ModelResolvers.SchemaResolver();

            refExpr.AddOperand("ds", expr);
            expr.ContainingSchema = schema;
            expr.ReferenceExpression = refExpr;

            JoinExpression joinExpr = new JoinExpression(expr);

            Assert.Equal(expr.ParentExpression, joinExpr.ParentExpression);
            Assert.Equal(expr.ContainingSchema, joinExpr.ContainingSchema);
            Assert.Equal(expr.ExpressionText, joinExpr.ExpressionText);
            Assert.Equal(expr.LineNumber, joinExpr.LineNumber);
            Assert.Equal(expr.OperandsCollection, joinExpr.OperandsCollection);
            Assert.Equal(expr.OperatorDefinition, joinExpr.OperatorDefinition);
            Assert.Equal(expr.ParamSignature, joinExpr.ParamSignature);
            Assert.Equal(expr.ReferenceExpression, joinExpr.ReferenceExpression);
            Assert.Equal(expr.ResultName, joinExpr.ResultName);
            Assert.Equal(expr.Structure, joinExpr.Structure);
            Assert.Null(joinExpr.BasicStructure);
        }

        [Fact]
        public void Constructor_JoinExpr_JoinExpr()
        {
            IExpression expr = TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol);
            IExpression refExpr = ModelResolvers.ExprResolver();
            ITransformationSchema schema = ModelResolvers.SchemaResolver();

            refExpr.AddOperand("ds", expr);
            expr.ContainingSchema = schema;
            expr.ReferenceExpression = refExpr;

            IJoinExpression joinExpr2 = ModelResolvers.JoinExprResolver(expr);
            joinExpr2.BasicStructure = ModelResolvers.DsResolver();

            JoinExpression joinExpr = new JoinExpression(joinExpr2);

            Assert.Equal(expr.ParentExpression, joinExpr.ParentExpression);
            Assert.Equal(expr.ContainingSchema, joinExpr.ContainingSchema);
            Assert.Equal(expr.ExpressionText, joinExpr.ExpressionText);
            Assert.Equal(expr.LineNumber, joinExpr.LineNumber);
            Assert.Equal(expr.OperandsCollection, joinExpr.OperandsCollection);
            Assert.Equal(expr.OperatorDefinition, joinExpr.OperatorDefinition);
            Assert.Equal(expr.ParamSignature, joinExpr.ParamSignature);
            Assert.Equal(expr.ReferenceExpression, joinExpr.ReferenceExpression);
            Assert.Equal(expr.ResultName, joinExpr.ResultName);
            Assert.Equal(expr.Structure, joinExpr.Structure);
            Assert.Equal(joinExpr2.BasicStructure, joinExpr.BasicStructure);
        }

        [Fact]
        public void Constructor_NotJoinOperatorExpr_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => { new JoinExpression(ModelResolvers.ExprResolver()); });
        }

        [Fact]
        public void GetSubsetAlias_Exprs_SubsetExpr()
        {
            for (int i = 0; i < 2; i++)
            {
                List<IExpression> exprs = new List<IExpression>();

                IExpression expr1 = ModelResolvers.ExprResolver();
                expr1.Structure = ModelResolvers.DsResolver();
                expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

                IExpression expr2 = ModelResolvers.ExprResolver();
                expr2.Structure = ModelResolvers.DsResolver();
                expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

                exprs.Add(expr1);
                exprs.Add(expr2);

                IExpression expr3 = ModelResolvers.ExprResolver();
                if (i == 1)
                {
                    expr3.Structure = ModelResolvers.DsResolver();
                    expr3.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                    exprs.Add(expr3);
                }

                IExpression result = JoinExpression.GetSubsetAlias(exprs);

                if (i == 0) Assert.Equal(expr1, result);
                else Assert.Equal(expr3, result);
            }
        }

        [Fact]
        public void GetSubsetAlias_ExprsNoSubsetExpr_Null()
        {
            List<IExpression> exprs = new List<IExpression>();

            IExpression expr1 = ModelResolvers.ExprResolver();
            expr1.Structure = ModelResolvers.DsResolver();
            expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

            IExpression expr2 = ModelResolvers.ExprResolver();
            expr2.Structure = ModelResolvers.DsResolver();
            expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id3"));

            exprs.Add(expr1);
            exprs.Add(expr2);

            Assert.Null(JoinExpression.GetSubsetAlias(exprs));
        }

        [Fact]
        public void GetSupersetAlias_Exprs_SupersetExpr()
        {
            for (int i = 0; i < 2; i++)
            {
                List<IExpression> exprs = new List<IExpression>();

                IExpression expr1 = ModelResolvers.ExprResolver();
                expr1.Structure = ModelResolvers.DsResolver();
                expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

                IExpression expr2 = ModelResolvers.ExprResolver();
                expr2.Structure = ModelResolvers.DsResolver();
                expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

                exprs.Add(expr1);
                exprs.Add(expr2);

                IExpression expr3 = ModelResolvers.ExprResolver();
                if (i == 1)
                {
                    expr3.Structure = ModelResolvers.DsResolver();
                    expr3.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                    expr3.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));
                    expr3.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Number, "Id3"));
                    exprs.Add(expr3);
                }

                IExpression result = JoinExpression.GetSupersetAlias(exprs);

                if (i == 0) Assert.Equal(expr1, result);
                else Assert.Equal(expr3, result);
            }
        }

        [Fact]
        public void GetSupersetAlias_ExprsNoSupersetExpr_Null()
        {
            List<IExpression> exprs = new List<IExpression>();

            IExpression expr1 = ModelResolvers.ExprResolver();
            expr1.Structure = ModelResolvers.DsResolver();
            expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

            IExpression expr2 = ModelResolvers.ExprResolver();
            expr2.Structure = ModelResolvers.DsResolver();
            expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id3"));

            exprs.Add(expr1);
            exprs.Add(expr2);

            Assert.Null(JoinExpression.GetSupersetAlias(exprs));
        }

        [Fact]
        public void GetSubsetAliasStructure_SubsetStructure()
        {
            for (int i = 0; i < 2; i++)
            {
                JoinExpression joinExpr = new JoinExpression(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
                joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());

                IExpression expr1 = ModelResolvers.ExprResolver();
                expr1.Structure = ModelResolvers.DsResolver();
                expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

                IExpression expr2 = ModelResolvers.ExprResolver();
                expr2.Structure = ModelResolvers.DsResolver();
                expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

                joinExpr.Operands["ds"].AddOperand("ds_1", expr1);
                joinExpr.Operands["ds"].AddOperand("ds_2", expr2);

                IExpression expr3 = ModelResolvers.ExprResolver();
                if (i == 1)
                {
                    expr3.Structure = ModelResolvers.DsResolver();
                    expr3.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                    joinExpr.Operands["ds"].AddOperand("ds_3", expr3);
                }

                IDataStructure result = joinExpr.GetSubsetAliasStructure();

                if (i == 0) Assert.True(expr1.Structure.EqualsObj(result));
                else Assert.True(expr3.Structure.EqualsObj(result));
            }
        }

        [Fact]
        public void GetSubsetAliasStructure_NoDsBranch_ThrowsException()
        {
            JoinExpression joinExpr = new JoinExpression(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinExpr.AddOperand("ds_1", ModelResolvers.ExprResolver());
            joinExpr.AddOperand("ds_2", ModelResolvers.ExprResolver());

            Assert.ThrowsAny<Exception>(() => { joinExpr.GetSubsetAliasStructure(); });
        }

        [Fact]
        public void GetSupersetAliasStructure_SupersetStructure()
        {
            for (int i = 0; i < 2; i++)
            {
                JoinExpression joinExpr = new JoinExpression(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
                joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());

                IExpression expr1 = ModelResolvers.ExprResolver();
                expr1.Structure = ModelResolvers.DsResolver();
                expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

                IExpression expr2 = ModelResolvers.ExprResolver();
                expr2.Structure = ModelResolvers.DsResolver();
                expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));

                joinExpr.Operands["ds"].AddOperand("ds_1", expr1);
                joinExpr.Operands["ds"].AddOperand("ds_2", expr2);

                IExpression expr3 = ModelResolvers.ExprResolver();
                if (i == 1)
                {
                    expr3.Structure = ModelResolvers.DsResolver();
                    expr3.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                    expr3.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));
                    expr3.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Number, "Id3"));
                    joinExpr.Operands["ds"].AddOperand("ds_3", expr3);
                }

                IDataStructure result = joinExpr.GetSupersetAliasStructure();

                if (i == 0) Assert.True(expr1.Structure.EqualsObj(result));
                else Assert.True(expr3.Structure.EqualsObj(result));
            }
        }

        [Fact]
        public void GetSupersetAliasStructure_NoDsBranch_ThrowsException()
        {
            JoinExpression joinExpr = new JoinExpression(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinExpr.AddOperand("ds_1", ModelResolvers.ExprResolver());
            joinExpr.AddOperand("ds_2", ModelResolvers.ExprResolver());

            Assert.ThrowsAny<Exception>(() => { joinExpr.GetSupersetAliasStructure(); });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("comp1")]
        [InlineData("comp2")]
        [InlineData("comp4")]
        public void GetAliasesSignatures_CompName_Signatures(string compName)
        {
            JoinExpression joinExpr = new JoinExpression(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());

            IExpression expr1 = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
            expr1.Structure = ModelResolvers.DsResolver();
            expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            expr1.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "comp2"));

            IExpression expr2 = TestExprFactory.GetExpression("ref", ExpressionFactoryNameTarget.OperatorSymbol);
            expr2.Structure = ModelResolvers.DsResolver();
            expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            expr2.Structure.Identifiers.Add(new StructureComponent(BasicDataType.String, "comp3"));

            joinExpr.Operands["ds"].AddOperand("ds_1", expr1);
            joinExpr.Operands["ds"].AddOperand("ds_2", expr2);

            List<string> expected = new List<string>();
            if (compName != "comp4") expected.Add("ds_1");
            if (!compName.In("comp2", "comp4")) expected.Add("ds_2");
            
            string[] result = joinExpr.GetAliasesSignatures(compName);

            Assert.Equal(expected.Count, result.Length);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], result[i]);
            }
        }

        [Fact]
        public void GetAliasesSignatures_NoDsBranch_ThrowsException()
        {
            JoinExpression joinExpr = new JoinExpression(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinExpr.AddOperand("ds_1", ModelResolvers.ExprResolver());
            joinExpr.AddOperand("ds_2", ModelResolvers.ExprResolver());

            Assert.ThrowsAny<Exception>(() => { joinExpr.GetAliasesSignatures(); });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("ds_1")]
        [InlineData("ds_2")]
        [InlineData("ds_3")]
        public void GetAliasExpression_CompName_Signatures(string name)
        {
            IExpression expr1 = ModelResolvers.ExprResolver();
            IExpression expr2 = ModelResolvers.ExprResolver();
            
            JoinExpression joinExpr = new JoinExpression(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinExpr.AddOperand("ds", ModelResolvers.ExprResolver());
            joinExpr.Operands["ds"].AddOperand("ds_1", expr1);
            joinExpr.Operands["ds"].AddOperand("ds_2", expr2);

            IExpression expected = name.In(null, "ds_3") ? null : joinExpr.Operands["ds"].Operands[name];

            Assert.Equal(expected, joinExpr.GetAliasExpression(name));
        }

        [Fact]
        public void GetAliasExpression_NoDsBranch_ThrowsException()
        {
            JoinExpression joinExpr = new JoinExpression(TestExprFactory.GetExpression("join", ExpressionFactoryNameTarget.OperatorSymbol));
            joinExpr.AddOperand("ds_1", ModelResolvers.ExprResolver());
            joinExpr.AddOperand("ds_2", ModelResolvers.ExprResolver());

            Assert.ThrowsAny<Exception>(() => { joinExpr.GetAliasExpression("ds_1"); });
        }
    }
}
