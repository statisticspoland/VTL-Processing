namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class EnvironmentMapperTests
    {
        private readonly Dictionary<string, string> _mapping;

        public EnvironmentMapperTests()
        {
            this._mapping = new Dictionary<string, string>()
            {
                { "A" , "a." },
                { "B" , "b." }
            };
        }

        [Theory]
        [InlineData(null, null, "A\\X", "a.X")]
        [InlineData(null, null, "B\\Y", "b.Y")]

        [InlineData("A", null, "X", "a.X")]
        [InlineData("B", null, "X", "b.X")]
        [InlineData("B", null, "A\\X", "a.X")]

        [InlineData(null, "c.", "A\\X", "a.X")]
        [InlineData(null, "c.", "B\\X", "b.X")]
        [InlineData(null, "c.", "X", "c.X")]

        [InlineData("A", "c.", "B\\X", "b.X")]
        [InlineData("A", "c.", "D\\X", "c.X")]
        [InlineData("A", "c.", "A\\X", "a.X")]
        public void Map_WithDefaults_ReturnMapped(string defNamespace, string defPrefix, string dsName, string tgtName)
        {
            Mock<IDataModelAggregator> aggregator = new Mock<IDataModelAggregator>();
            aggregator.SetupGet(agg => agg.DefaultNamespace).Returns(defNamespace);

            EnvironmentMapper mapper = new EnvironmentMapper(aggregator.Object);
            mapper.Mapping = this._mapping;
            mapper.DefaultTargetPrefix = defPrefix;

            Assert.Equal(tgtName, mapper.Map(dsName));
        }

        [Theory]
        [InlineData(null, null, "C\\X")]
        [InlineData("C", null, "X")]
        [InlineData("A", null, "D\\X")]
        public void Map_NotFound_RaiseError(string defNamespace, string defPrefix, string dsName)
        {
            Mock<IDataModelAggregator> aggregator = new Mock<IDataModelAggregator>();
            aggregator.SetupGet(agg => agg.DefaultNamespace).Returns(defNamespace);

            EnvironmentMapper mapper = new EnvironmentMapper(aggregator.Object);
            mapper.Mapping = this._mapping;
            mapper.DefaultTargetPrefix = defPrefix;

            Assert.Throws<KeyNotFoundException>(() => mapper.Map(dsName));
        }
    }
}
