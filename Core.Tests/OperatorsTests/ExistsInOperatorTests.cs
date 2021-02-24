namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Linq;
    using Xunit;

    public class ExistsInOperatorTests
    {
        [Fact]
        public void GetOutputStructure_Correct2EqualIdsDatasetsExpr_OneMeasureBoolStructure()
        {
            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] datasetCombs = combinations.Where(wrongComb => (int)wrongComb[0] > 8 && (int)wrongComb[1] > 8).ToArray();

            foreach (TestExprType[] datasetComb in datasetCombs)
            {
                IExpression existsInExpr = TestExprFactory.GetExpression(datasetComb);
                existsInExpr.OperatorDefinition = ModelResolvers.OperatorResolver("exists_in");

                IDataStructure expectedStructure = existsInExpr.Operands["ds_1"].Structure.GetCopy();
                expectedStructure.Measures.Clear();
                expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));

                IDataStructure dataStructure = existsInExpr.OperatorDefinition.GetOutputStructure(existsInExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Fact]
        public void GetOutputStructure_Correct1Subset1SupersetExpr_OneMeasureBoolStructure()
        {
            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] datasetCombs = combinations.Where(wrongComb => (int)wrongComb[0] > 8 && (int)wrongComb[1] > 8).ToArray();

            foreach (TestExprType[] datasetComb in datasetCombs)
            {
                IExpression existsInExpr = TestExprFactory.GetExpression(datasetComb);
                existsInExpr.OperatorDefinition = ModelResolvers.OperatorResolver("exists_in");
                existsInExpr.Operands["ds_2"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));

                IDataStructure expectedStructure = existsInExpr.Operands["ds_1"].Structure.GetCopy();
                expectedStructure.Measures.Clear();
                expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "bool_var"));

                IDataStructure dataStructure = existsInExpr.OperatorDefinition.GetOutputStructure(existsInExpr);

                Assert.True(expectedStructure.EqualsObj(dataStructure));
            }
        }

        [Fact]
        public void GetOutputStructure_WrongArgsExpr_ThrowsException()
        {
            TestExprType[][] combinations = Enum.GetValues(typeof(TestExprType)).Cast<TestExprType>().GetCombinations(2);
            TestExprType[][] wrongCombs = combinations;

            foreach (TestExprType[] wrongComb in wrongCombs)
            {
                IExpression existsInExpr = TestExprFactory.GetExpression(wrongComb);
                existsInExpr.OperatorDefinition = ModelResolvers.OperatorResolver("exists_in");

                if (!existsInExpr.Operands["ds_1"].IsScalar) 
                    existsInExpr.Operands["ds_1"].Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));

                // Debug condition example: wrongComb[0] == TestExprType.Integer && wrongComb[1] == TestExprType.Integer
                Assert.ThrowsAny<VtlOperatorError>(() => { existsInExpr.OperatorDefinition.GetOutputStructure(existsInExpr); });
            }
        }
    }
}
