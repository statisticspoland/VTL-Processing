namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests
{
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class EnvironmentMapperTests
    {
        private EnvironmentMapper mapper;

        public EnvironmentMapperTests()
        {
            this.mapper = new EnvironmentMapper(new Dictionary<string, string>()
            {
                { "A" , "a." },
                { "B" , "b." }
            });
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
            this.mapper.DefaultNamespace = defNamespace;
            this.mapper.DefaultTargetPrefix = defPrefix;
            Assert.Equal(tgtName, this.mapper.Map(dsName));
        }

        [Theory]
        [InlineData(null, null, "C\\X")]
        [InlineData("C", null, "X")]
        [InlineData("A", null, "D\\X")]
        public void Map_NotFound_RaiseError(string defNamespace, string defPrefix, string dsName)
        {
            this.mapper.DefaultNamespace = defNamespace;
            this.mapper.DefaultTargetPrefix = defPrefix;
            Assert.Throws<ArgumentOutOfRangeException>(() => this.mapper.Map(dsName));
        }
    }
}
