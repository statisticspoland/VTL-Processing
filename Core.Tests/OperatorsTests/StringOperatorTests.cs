namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using StatisticsPoland.VtlProcessing.Core.Operators.Auxiliary.ComponentManagement;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public partial class StringOperatorTests
    {
        [Theory]
        // OneArg
        [InlineData("trim", TestExprType.String)]
        [InlineData("trim", TestExprType.None)]
        [InlineData("rtrim", TestExprType.String)]
        [InlineData("rtrim", TestExprType.None)]
        [InlineData("ltrim", TestExprType.String)]
        [InlineData("ltrim", TestExprType.None)]
        [InlineData("upper", TestExprType.String)]
        [InlineData("upper", TestExprType.None)]
        [InlineData("lower", TestExprType.String)]
        [InlineData("lower", TestExprType.None)]
        [InlineData("substr", TestExprType.String)]
        [InlineData("substr", TestExprType.None)]
        // TwoArgs
        [InlineData("||", TestExprType.String, TestExprType.String)]
        [InlineData("||", TestExprType.String, TestExprType.None)]
        [InlineData("||", TestExprType.None, TestExprType.None)]
        [InlineData("||", TestExprType.None, TestExprType.String)]
        [InlineData("replace", TestExprType.String, TestExprType.String)]
        [InlineData("replace", TestExprType.String, TestExprType.None)]
        [InlineData("replace", TestExprType.None, TestExprType.None)]
        [InlineData("replace", TestExprType.None, TestExprType.String)]
        [InlineData("substr", TestExprType.String, TestExprType.Integer)]
        [InlineData("substr", TestExprType.String, TestExprType.None)]
        [InlineData("substr", TestExprType.None, TestExprType.None)]
        [InlineData("substr", TestExprType.None, TestExprType.Integer)]
        // ThreeArgs
        [InlineData("substr", TestExprType.String, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("substr", TestExprType.String, TestExprType.Integer, TestExprType.None)]
        [InlineData("substr", TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("substr", TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("substr", TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("substr", TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData("substr", TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("substr", TestExprType.None, TestExprType.None, TestExprType.None)]
        [InlineData("replace", TestExprType.String, TestExprType.String, TestExprType.String)]
        [InlineData("replace", TestExprType.String, TestExprType.String, TestExprType.None)]
        [InlineData("replace", TestExprType.String, TestExprType.None, TestExprType.String)]
        [InlineData("replace", TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("replace", TestExprType.None, TestExprType.String, TestExprType.String)]
        [InlineData("replace", TestExprType.None, TestExprType.String, TestExprType.None)]
        [InlineData("replace", TestExprType.None, TestExprType.None, TestExprType.String)]
        [InlineData("replace", TestExprType.None, TestExprType.None, TestExprType.None)]
        public void GetOutputStructure_CorrectScalarsExpr_StringScalarStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression stringExpr = TestExprFactory.GetExpression(types);
            stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

            IDataStructure dataStructure = stringExpr.OperatorDefinition.GetOutputStructure(stringExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.String).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        // OneArg
        [InlineData("length", TestExprType.String)]
        [InlineData("length", TestExprType.None)]
        // TwoArgs           
        [InlineData("instr", TestExprType.String, TestExprType.String)]
        [InlineData("instr", TestExprType.String, TestExprType.None)]
        [InlineData("instr", TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.None, TestExprType.String)]
        // ThreeArgs         
        [InlineData("instr", TestExprType.String, TestExprType.String, TestExprType.Integer)]
        [InlineData("instr", TestExprType.String, TestExprType.String, TestExprType.None)]
        [InlineData("instr", TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.None, TestExprType.String, TestExprType.Integer)]
        [InlineData("instr", TestExprType.None, TestExprType.String, TestExprType.None)]
        [InlineData("instr", TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.None, TestExprType.None, TestExprType.None)]
        // FourArgs          
        [InlineData("instr", TestExprType.String, TestExprType.String, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.String, TestExprType.String, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.String, TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.String, TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.String, TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.String, TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.String, TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.String, TestExprType.None, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.None, TestExprType.String, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.None, TestExprType.String, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.None, TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.None, TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.None, TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.None, TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.None, TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.None, TestExprType.None, TestExprType.None, TestExprType.None)]
        public void GetOutputStructure_CorrectScalarsExpr_IntScalarStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression stringExpr = TestExprFactory.GetExpression(types);
            stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

            IDataStructure dataStructure = stringExpr.OperatorDefinition.GetOutputStructure(stringExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.Integer).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        // OneArg
        [InlineData("trim", TestExprType.StringsDataset)]
        [InlineData("trim", TestExprType.NonesDataset)]
        [InlineData("trim", TestExprType.MixedNoneStrDataset)]
        [InlineData("rtrim", TestExprType.StringsDataset)]
        [InlineData("rtrim", TestExprType.NonesDataset)]
        [InlineData("rtrim", TestExprType.MixedNoneStrDataset)]
        [InlineData("ltrim", TestExprType.StringsDataset)]
        [InlineData("ltrim", TestExprType.NonesDataset)]
        [InlineData("ltrim", TestExprType.MixedNoneStrDataset)]
        [InlineData("upper", TestExprType.StringsDataset)]
        [InlineData("upper", TestExprType.NonesDataset)]
        [InlineData("upper", TestExprType.MixedNoneStrDataset)]
        [InlineData("lower", TestExprType.StringsDataset)]
        [InlineData("lower", TestExprType.NonesDataset)]
        [InlineData("lower", TestExprType.MixedNoneStrDataset)]
        [InlineData("substr", TestExprType.StringsDataset)]
        [InlineData("substr", TestExprType.NonesDataset)]
        [InlineData("substr", TestExprType.MixedNoneStrDataset)]
        // TwoArgs
        [InlineData("||", TestExprType.StringsDataset, TestExprType.String)]
        [InlineData("||", TestExprType.StringsDataset, TestExprType.None)]
        [InlineData("||", TestExprType.NonesDataset, TestExprType.String)]
        [InlineData("||", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("||", TestExprType.MixedNoneStrDataset, TestExprType.String)]
        [InlineData("||", TestExprType.MixedNoneStrDataset, TestExprType.None)]
        [InlineData("replace", TestExprType.StringsDataset, TestExprType.String)]
        [InlineData("replace", TestExprType.StringsDataset, TestExprType.None)]
        [InlineData("replace", TestExprType.NonesDataset, TestExprType.String)]
        [InlineData("replace", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("replace", TestExprType.MixedNoneStrDataset, TestExprType.String)]
        [InlineData("replace", TestExprType.MixedNoneStrDataset, TestExprType.None)]
        [InlineData("substr", TestExprType.StringsDataset, TestExprType.Integer)]
        [InlineData("substr", TestExprType.StringsDataset, TestExprType.None)]
        [InlineData("substr", TestExprType.NonesDataset, TestExprType.Integer)]
        [InlineData("substr", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("substr", TestExprType.MixedNoneStrDataset, TestExprType.Integer)]
        [InlineData("substr", TestExprType.MixedNoneStrDataset, TestExprType.None)]
        // ThreeArgs
        [InlineData("substr", TestExprType.StringsDataset, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("substr", TestExprType.StringsDataset, TestExprType.Integer, TestExprType.None)]
        [InlineData("substr", TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData("substr", TestExprType.StringsDataset, TestExprType.None, TestExprType.None)]
        [InlineData("substr", TestExprType.NonesDataset, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("substr", TestExprType.NonesDataset, TestExprType.Integer, TestExprType.None)]
        [InlineData("substr", TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData("substr", TestExprType.NonesDataset, TestExprType.None, TestExprType.None)]
        [InlineData("substr", TestExprType.MixedNoneStrDataset, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("substr", TestExprType.MixedNoneStrDataset, TestExprType.Integer, TestExprType.None)]
        [InlineData("substr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData("substr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None)]
        [InlineData("replace", TestExprType.StringsDataset, TestExprType.String, TestExprType.String)]
        [InlineData("replace", TestExprType.StringsDataset, TestExprType.String, TestExprType.None)]
        [InlineData("replace", TestExprType.StringsDataset, TestExprType.None, TestExprType.String)]
        [InlineData("replace", TestExprType.StringsDataset, TestExprType.None, TestExprType.None)]
        [InlineData("replace", TestExprType.NonesDataset, TestExprType.String, TestExprType.String)]
        [InlineData("replace", TestExprType.NonesDataset, TestExprType.String, TestExprType.None)]
        [InlineData("replace", TestExprType.NonesDataset, TestExprType.None, TestExprType.String)]
        [InlineData("replace", TestExprType.NonesDataset, TestExprType.None, TestExprType.None)]
        [InlineData("replace", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.String)]
        [InlineData("replace", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None)]
        [InlineData("replace", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.String)]
        [InlineData("replace", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None)]
        public void GetOutputStructure_CorrectDatasetNScalarsExpr_StringsDatasetStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression stringExpr = TestExprFactory.GetExpression(types);
            stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

            IDataStructure dataStructure = stringExpr.OperatorDefinition.GetOutputStructure(stringExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.StringsDataset).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData("||", TestExprType.String, TestExprType.StringsDataset)]
        [InlineData("||", TestExprType.String, TestExprType.NonesDataset)]
        [InlineData("||", TestExprType.String, TestExprType.MixedNoneStrDataset)]
        [InlineData("||", TestExprType.None, TestExprType.StringsDataset)]
        [InlineData("||", TestExprType.None, TestExprType.NonesDataset)]
        [InlineData("||", TestExprType.None, TestExprType.MixedNoneStrDataset)]
        public void GetOutputStructure_ScalarDatasetExpr_StringsDatasetStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression stringExpr = TestExprFactory.GetExpression(types);
            stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

            IDataStructure dataStructure = stringExpr.OperatorDefinition.GetOutputStructure(stringExpr);

            Assert.True(TestExprFactory.GetExpression(TestExprType.StringsDataset).Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData("||", TestExprType.StringsDataset, TestExprType.StringsDataset)]
        [InlineData("||", TestExprType.StringsDataset, TestExprType.NonesDataset)]
        [InlineData("||", TestExprType.StringsDataset, TestExprType.MixedNoneStrDataset)]
        [InlineData("||", TestExprType.NonesDataset, TestExprType.StringsDataset)]
        [InlineData("||", TestExprType.NonesDataset, TestExprType.NonesDataset)]
        [InlineData("||", TestExprType.NonesDataset, TestExprType.MixedNoneStrDataset)]
        [InlineData("||", TestExprType.MixedNoneStrDataset, TestExprType.StringsDataset)]
        [InlineData("||", TestExprType.MixedNoneStrDataset, TestExprType.NonesDataset)]
        [InlineData("||", TestExprType.MixedNoneStrDataset, TestExprType.MixedNoneStrDataset)]
        public void GetOutputStructure_2DatasetsExpr_StringsSupersetStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression stringExpr = TestExprFactory.GetExpression(types);
            stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

            IDataStructure expected = stringExpr.Operands["ds_1"].Structure;
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
            expected.Measures[0].ValueDomain = new ValueDomain(BasicDataType.String);
            expected.Measures[1].ValueDomain = new ValueDomain(BasicDataType.String);

            IDataStructure dataStructure = stringExpr.OperatorDefinition.GetOutputStructure(stringExpr);

            Assert.True(expected.EqualsObj(dataStructure));
        }

        [Theory]
        // OneArg
        [InlineData("length", TestExprType.StringsDataset)]
        [InlineData("length", TestExprType.NonesDataset)]
        [InlineData("length", TestExprType.MixedNoneStrDataset)]
        // TwoArgs
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None)]
        // ThreeArgs
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.None)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None)]
        // FourArgs
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None, TestExprType.None)]
        public void GetOutputStructure_OneMeasureDatasetNScalarsExpr_OneMeasureIntStructure(string opSymbol, params TestExprType[] types)
        {
            IExpression stringExpr = TestExprFactory.GetExpression(types);
            stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);
            stringExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

            IDataStructure expected = stringExpr.Operands["ds_1"].Structure.GetCopy();
            expected.Measures.Clear();
            expected.Measures.Add(new StructureComponent(BasicDataType.Integer, "int_var"));

            IDataStructure dataStructure = stringExpr.OperatorDefinition.GetOutputStructure(stringExpr);

            Assert.True(expected.EqualsObj(dataStructure));
        }

        [Theory]
        // OneArg
        [InlineData("length", TestExprType.StringsDataset)]
        [InlineData("length", TestExprType.NonesDataset)]
        [InlineData("length", TestExprType.MixedNoneStrDataset)]
        // TwoArgs
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None)]
        // ThreeArgs
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.None)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None)]
        // FourArgs
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.StringsDataset, TestExprType.None, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.NonesDataset, TestExprType.None, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer, TestExprType.None)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None, TestExprType.Integer)]
        [InlineData("instr", TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None, TestExprType.None)]
        public void GetOutputStructure_MultiMeasuresDatasetNScalarsExpr_ThrowsException(string opSymbol, params TestExprType[] types)
        {
            IExpression stringExpr = TestExprFactory.GetExpression(types);
            stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);

            Assert.ThrowsAny<VtlOperatorError>(() => { stringExpr.OperatorDefinition.GetOutputStructure(stringExpr); });
        }

        [Theory]
        [InlineData("trim")]
        [InlineData("rtrim")]
        [InlineData("ltrim")]
        [InlineData("upper")]
        [InlineData("lower")]
        [InlineData("substr")]
        [InlineData("length")]
        public void GetOutputStructure_WrongArgExpr1Arg_ThrowsException(string opSymbol)
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.String },
                new TestExprType[] { TestExprType.None },
                new TestExprType[] { TestExprType.StringsDataset },
                new TestExprType[] { TestExprType.NonesDataset },
                new TestExprType[] { TestExprType.MixedNoneStrDataset }
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(1);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs.ToArray());

            foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18)) // No mixed datasets
            {
                IExpression stringExpr = TestExprFactory.GetExpression(new TestExprType[] { wrongComb[0] });
                stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);
                if (opSymbol.In("length", "instr") && stringExpr.Operands["ds_1"].Structure.Measures.Count > 1)
                    stringExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                // Debug condition example: wrongComb[0] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { stringExpr.OperatorDefinition.GetOutputStructure(stringExpr); });
            }
        }

        [Theory]
        [InlineData("||")]
        [InlineData("replace")]
        [InlineData("substr")]
        [InlineData("instr")]
        public void GetOutputStructure_WrongArgsExpr2Args_ThrowsException(string opSymbol)
        {
            List<TestExprType[]> correctCombs = new List<TestExprType[]>();

            if (opSymbol == "||")
            {
                correctCombs.AddRange(new TestExprType[][] {
                    new TestExprType[] { TestExprType.String, TestExprType.String },
                    new TestExprType[] { TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.String, TestExprType.StringsDataset },
                    new TestExprType[] { TestExprType.String, TestExprType.NonesDataset },
                    new TestExprType[] { TestExprType.String, TestExprType.MixedNoneStrDataset },
                    new TestExprType[] { TestExprType.None, TestExprType.String },
                    new TestExprType[] { TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.StringsDataset },
                    new TestExprType[] { TestExprType.None, TestExprType.NonesDataset },
                    new TestExprType[] { TestExprType.None, TestExprType.MixedNoneStrDataset },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.String },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.StringsDataset },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.NonesDataset },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.MixedNoneStrDataset },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.String },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.StringsDataset },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.NonesDataset },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.MixedNoneStrDataset },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.StringsDataset },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.NonesDataset },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.MixedNoneStrDataset }
                });
            }

            if (opSymbol == "substr")
            {
                correctCombs.AddRange(new TestExprType[][] {
                    new TestExprType[] { TestExprType.String, TestExprType.Integer },
                    new TestExprType[] { TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.Integer },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.Integer },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.Integer },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None }
                });
            }
            else
            {
                correctCombs.AddRange(new TestExprType[][] {
                    new TestExprType[] { TestExprType.String, TestExprType.String },
                    new TestExprType[] { TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.String },
                    new TestExprType[] { TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.String },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.String },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None }
                });
            }

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs.ToArray());

            foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18 && (int)wrongComb[1] < 18)) // No mixed datasets
            {
                IExpression stringExpr = TestExprFactory.GetExpression(wrongComb);
                stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);
                if (opSymbol.In("length", "instr") && stringExpr.Operands["ds_1"].Structure.Measures.Count > 1)
                    stringExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { stringExpr.OperatorDefinition.GetOutputStructure(stringExpr); });
            }
        }

        [Theory]
        [InlineData("replace")]
        [InlineData("substr")]
        [InlineData("instr")]
        public void GetOutputStructure_WrongArgsExpr3Args_ThrowsException(string opSymbol)
        {
            List<TestExprType[]> correctCombs = new List<TestExprType[]>();

            if (opSymbol == "substr")
            {
                correctCombs.AddRange(new TestExprType[][] {
                    new TestExprType[] { TestExprType.String, TestExprType.Integer, TestExprType.Integer },
                    new TestExprType[] { TestExprType.String, TestExprType.Integer, TestExprType.None },
                    new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.Integer, TestExprType.Integer },
                    new TestExprType[] { TestExprType.None, TestExprType.Integer, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.Integer, TestExprType.Integer },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.Integer, TestExprType.None },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.Integer, TestExprType.Integer },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.Integer, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.Integer, TestExprType.Integer },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.Integer, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None }
                });
            }
            else if (opSymbol == "instr")
            {
                correctCombs.AddRange(new TestExprType[][] {
                    new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.Integer },
                    new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.Integer },
                    new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.Integer },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.Integer },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.Integer },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None }
                });
            }
            else
            {
                correctCombs.AddRange(new TestExprType[][] {
                    new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.String },
                    new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.String },
                    new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.String },
                    new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.String },
                    new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.String },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.String },
                    new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.String },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.String },
                    new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.String },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.String },
                    new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None }
                });
            }

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(3);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs.ToArray());

            foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18 && (int)wrongComb[1] < 18 && (int)wrongComb[2] < 18)) // No mixed datasets
            {
                IExpression stringExpr = TestExprFactory.GetExpression(wrongComb);
                stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);
                if (opSymbol.In("length", "instr") && stringExpr.Operands["ds_1"].Structure.Measures.Count > 1)
                    stringExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer && wrongComb[2] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { stringExpr.OperatorDefinition.GetOutputStructure(stringExpr); });
            }
        }

        [Theory]
        [InlineData("instr")]
        public void GetOutputStructure_WrongArgsExpr4Args_ThrowsException(string opSymbol)
        {
            TestExprType[][] correctCombs = new TestExprType[][] {
                new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.String, TestExprType.String, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.String, TestExprType.None, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.String, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.None, TestExprType.None, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.String, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.StringsDataset, TestExprType.None, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.String, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.NonesDataset, TestExprType.None, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer, TestExprType.Integer },
                new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.String, TestExprType.None, TestExprType.None },
                new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.Integer, TestExprType.None },
                new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None, TestExprType.Integer },
                new TestExprType[] { TestExprType.MixedNoneStrDataset, TestExprType.None, TestExprType.None, TestExprType.None },
            };

            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(4);
            TestExprType[][] wrongCombs = combinations.Without(correctCombs.ToArray());

            foreach (TestExprType[] wrongComb in wrongCombs.Where(wrongComb => (int)wrongComb[0] < 18 && (int)wrongComb[1] < 18 && (int)wrongComb[2] < 18 && (int)wrongComb[3] < 18)) // No mixed datasets)
            {
                IExpression stringExpr = TestExprFactory.GetExpression(wrongComb);
                stringExpr.OperatorDefinition = ModelResolvers.OperatorResolver(opSymbol);
                if (opSymbol.In("length", "instr") && stringExpr.Operands["ds_1"].Structure.Measures.Count > 1)
                    stringExpr.Operands["ds_1"].Structure.Measures.RemoveAt(1);

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer && wrongComb[2] == TestExprType.Integer && wrongComb[3] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { stringExpr.OperatorDefinition.GetOutputStructure(stringExpr); });
            }
        }
    }
}
