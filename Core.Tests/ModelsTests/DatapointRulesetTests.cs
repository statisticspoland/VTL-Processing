namespace StatisticsPoland.VtlProcessing.Core.Tests.ModelsTests
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using Xunit;

    public partial class DatapointRulesetTests
    {
        [Fact]
        public void InferStructure_VariableRuleset_AssignsStructure()
        {
            DatapointRuleset ruleset = this.GetVariableRuleset();

            IDataStructure expectedStructure = ModelResolvers.DsResolver();
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.None, "var4"));
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "x"));
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "y"));
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.String, "z"));

            ruleset.InferStructure();
            IDataStructure dataStructure = ruleset.Structure;

            Assert.True(expectedStructure.EqualsObj(dataStructure));
            Assert.Equal(expectedStructure.Measures[0].ComponentName, dataStructure.Measures[0].ComponentName);
            Assert.Equal(expectedStructure.Measures[1].ComponentName, dataStructure.Measures[1].ComponentName);
            Assert.Equal(expectedStructure.Measures[2].ComponentName, dataStructure.Measures[2].ComponentName);
            Assert.Equal(expectedStructure.Measures[3].ComponentName, dataStructure.Measures[3].ComponentName);
        }

        [Fact]
        public void InferStructure_ValueDomainRuleset_AssignsStructure()
        {
            DatapointRuleset ruleset = this.GetValueDomainRuleset();

            IDataStructure expectedStructure = ModelResolvers.DsResolver();
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.None, "var4"));
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "x"));
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "y"));
            expectedStructure.Measures.Add(new StructureComponent(BasicDataType.String, "z"));

            ruleset.InferStructure();
            IDataStructure dataStructure = ruleset.Structure;

            Assert.True(expectedStructure.EqualsObj(dataStructure));
            Assert.Equal(expectedStructure.Measures[0].ComponentName, dataStructure.Measures[0].ComponentName);
            Assert.Equal(expectedStructure.Measures[1].ComponentName, dataStructure.Measures[1].ComponentName);
            Assert.Equal(expectedStructure.Measures[2].ComponentName, dataStructure.Measures[2].ComponentName);
            Assert.Equal(expectedStructure.Measures[3].ComponentName, dataStructure.Measures[3].ComponentName);
        }
    }
}
