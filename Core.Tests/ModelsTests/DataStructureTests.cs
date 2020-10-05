namespace StatisticsPoland.VtlProcessing.Core.Tests.ModelsTests
{
    using Microsoft.Extensions.Logging;
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling.Logging;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class DataStructureTests
    {
        [Theory]
        [InlineData("A", BasicDataType.Integer, ComponentType.Identifier)]
        [InlineData("B", BasicDataType.Integer, ComponentType.Identifier)]
        [InlineData("A", BasicDataType.String, ComponentType.Identifier)]
        [InlineData("B", BasicDataType.String, ComponentType.Identifier)]
        [InlineData("A", BasicDataType.Integer, ComponentType.Measure)]
        [InlineData("B", BasicDataType.Integer, ComponentType.Measure)]
        [InlineData("A", BasicDataType.String, ComponentType.Measure)]
        [InlineData("B", BasicDataType.String, ComponentType.Measure)]
        [InlineData("A", BasicDataType.Integer, ComponentType.NonViralAttribute)]
        [InlineData("B", BasicDataType.Integer, ComponentType.NonViralAttribute)]
        [InlineData("A", BasicDataType.String, ComponentType.NonViralAttribute)]
        [InlineData("B", BasicDataType.String, ComponentType.NonViralAttribute)]
        [InlineData("A", BasicDataType.Integer, ComponentType.ViralAttribute)]
        [InlineData("B", BasicDataType.Integer, ComponentType.ViralAttribute)]
        [InlineData("A", BasicDataType.String, ComponentType.ViralAttribute)]
        [InlineData("B", BasicDataType.String, ComponentType.ViralAttribute)]
        public void Constructor2_SingleComponentStructure(string name, BasicDataType dataType, ComponentType compType)
        {
            DataStructure dataStructure = new DataStructure(name, compType, dataType);

            Assert.True(dataStructure.IsSingleComponent);

            switch (compType)
            {
                case ComponentType.Identifier:
                    Assert.Equal(1, dataStructure.Identifiers.Count);
                    Assert.Equal(0, dataStructure.Measures.Count);
                    Assert.Equal(0, dataStructure.NonViralAttributes.Count);
                    Assert.Equal(0, dataStructure.ViralAttributes.Count);
                    break;
                case ComponentType.Measure:
                    Assert.Equal(0, dataStructure.Identifiers.Count);
                    Assert.Equal(1, dataStructure.Measures.Count);
                    Assert.Equal(0, dataStructure.NonViralAttributes.Count);
                    Assert.Equal(0, dataStructure.ViralAttributes.Count);
                    break;
                case ComponentType.NonViralAttribute:
                    Assert.Equal(0, dataStructure.Identifiers.Count);
                    Assert.Equal(0, dataStructure.Measures.Count);
                    Assert.Equal(1, dataStructure.NonViralAttributes.Count);
                    Assert.Equal(0, dataStructure.ViralAttributes.Count);
                    break;
                case ComponentType.ViralAttribute:
                    Assert.Equal(0, dataStructure.Identifiers.Count);
                    Assert.Equal(0, dataStructure.Measures.Count);
                    Assert.Equal(0, dataStructure.NonViralAttributes.Count);
                    Assert.Equal(1, dataStructure.ViralAttributes.Count);
                    break;
                default: throw new Exception();
            }

            Assert.Equal(name, dataStructure.Components[0].ComponentName);
            Assert.Equal(dataType, dataStructure.Components[0].ValueDomain.DataType);
        }

        [Fact]
        public void Components_Count1_IsSingleComponentTrue()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer));

            Assert.Equal(1, dataStructure.Components.Count);
            Assert.True(dataStructure.IsSingleComponent);
        }

        [Fact]
        public void Components_CountNot1_IsSingleComponentFalse()
        {
            for (int i = 0; i < 10; i++)
            {
                DataStructure dataStructure = new DataStructure();

                if (i != 1)
                {
                    for (int j = 0; j < i; j++)
                    {
                        dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, $"comp{j + 1}"));
                    }

                    Assert.Equal(i, dataStructure.Components.Count);
                    Assert.False(dataStructure.IsSingleComponent);
                }
            }
        }

        [Fact]
        public void Identifiers_SortedIdentifiers()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "B"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "C"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "A"));

            Assert.Equal("A", dataStructure.Identifiers[0].ComponentName);
            Assert.Equal("B", dataStructure.Identifiers[1].ComponentName);
            Assert.Equal("C", dataStructure.Identifiers[2].ComponentName);
        }


        [Fact]
        public void Measures_SortedMeasures()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "B"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "C"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "A"));

            Assert.Equal("A", dataStructure.Measures[0].ComponentName);
            Assert.Equal("B", dataStructure.Measures[1].ComponentName);
            Assert.Equal("C", dataStructure.Measures[2].ComponentName);
        }

        [Fact]
        public void NonViralAttributes_SortedNonViralAttributes()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "B"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "C"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "A"));

            Assert.Equal("A", dataStructure.NonViralAttributes[0].ComponentName);
            Assert.Equal("B", dataStructure.NonViralAttributes[1].ComponentName);
            Assert.Equal("C", dataStructure.NonViralAttributes[2].ComponentName);
        }

        [Fact]
        public void ViralAttributes_SortedViralAttributes()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "B"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "C"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "A"));

            Assert.Equal("A", dataStructure.ViralAttributes[0].ComponentName);
            Assert.Equal("B", dataStructure.ViralAttributes[1].ComponentName);
            Assert.Equal("C", dataStructure.ViralAttributes[2].ComponentName);
        }

        [Fact]
        public void Components_SortedComponents()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At1"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At2"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At4"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

            Assert.Equal("Id1", dataStructure.Components[0].ComponentName);
            Assert.Equal("Id2", dataStructure.Components[1].ComponentName);
            Assert.Equal("Me1", dataStructure.Components[2].ComponentName);
            Assert.Equal("Me2", dataStructure.Components[3].ComponentName);
            Assert.Equal("At1", dataStructure.Components[4].ComponentName);
            Assert.Equal("At2", dataStructure.Components[5].ComponentName);
            Assert.Equal("At3", dataStructure.Components[6].ComponentName);
            Assert.Equal("At4", dataStructure.Components[7].ComponentName);
        }

        [Fact]
        public void Identifiers_ComponentNotIdentifier_Identifier()
        {
            DataStructure dataStructure = new DataStructure();

            Assert.Equal(0, dataStructure.Identifiers.Count);

            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp", ComponentType.Measure));

            Assert.Equal(ComponentType.Identifier, dataStructure.Identifiers[0].ComponentType);
        }

        [Fact]
        public void Measures_ComponentNotMeasure_Measure()
        {
            DataStructure dataStructure = new DataStructure();

            Assert.Equal(0, dataStructure.Measures.Count);

            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "comp", ComponentType.Identifier));

            Assert.Equal(ComponentType.Measure, dataStructure.Measures[0].ComponentType);
        }

        [Fact]
        public void NonViralAttributes_ComponentNotNonViralAttribute_NonViralAttribute()
        {
            DataStructure dataStructure = new DataStructure();

            Assert.Equal(0, dataStructure.NonViralAttributes.Count);

            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "comp", ComponentType.Measure));

            Assert.Equal(ComponentType.NonViralAttribute, dataStructure.NonViralAttributes[0].ComponentType);
        }

        [Fact]
        public void ViralAttributes_ComponentNotViralAttribute_ViralAttribute()
        {
            DataStructure dataStructure = new DataStructure();

            Assert.Equal(0, dataStructure.ViralAttributes.Count);

            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "comp", ComponentType.Measure));

            Assert.Equal(ComponentType.ViralAttribute, dataStructure.ViralAttributes[0].ComponentType);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetCopy_CopyOfStructure(bool copyName)
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.DatasetName = "Dataset";
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Time, "Me2"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At1"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At2"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "At1"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At2"));

            IDataStructure copyStructure = dataStructure.GetCopy(copyName);

            if (copyName) Assert.Equal(dataStructure.DatasetName, copyStructure.DatasetName);
            for (int i = 0; i < copyStructure.Components.Count; i++)
            {
                Assert.Equal(dataStructure.Components[i].ComponentName, copyStructure.Components[i].ComponentName);
                Assert.True(copyStructure.Components[i].EqualsObj(dataStructure.Components[i]));
            }
        }

        [Fact]
        public void WithAttributesOf_Structure_StructureWithAttributesOFGivenStructure()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At1"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At3"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Boolean, "Me1"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.String, "At_1"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "At_2"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At_3"));

            IDataStructure expected = ModelResolvers.DsResolver();
            expected.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At1"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.String, "At_1"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At3"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "At_2"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At_3"));

            IDataStructure result = dataStructure.WithAttributesOf(dataStructure2);

            Assert.Equal(expected.Components.Count, result.Components.Count);
            for (int i = 0; i < expected.Components.Count; i++)
            {
                Assert.Equal(expected.Components[i].ComponentName, result.Components[i].ComponentName);
                Assert.True(expected.Components[i].EqualsObj(result.Components[i]));
            }
        }

        [Fact]
        public void WithAttributesOf_RepeatedDiffTypesNonViralAttributesStructure_StructureAndWarnings()
        {
            ErrorCollectorProvider provider = new ErrorCollectorProvider();
            ILogger<IDataStructure> logger = new Logger<IDataStructure>(new LoggerFactory(new List<ILoggerProvider>() { provider }));

            DataStructure dataStructure = new DataStructure(logger);
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At1"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At3"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At4"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Boolean, "Me1"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.String, "At1"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Date, "At2"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At3"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At4"));

            IDataStructure expected = ModelResolvers.DsResolver();
            expected.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At1"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At3"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At4"));

            IDataStructure result = dataStructure.WithAttributesOf(dataStructure2);

            Assert.Equal(expected.Components.Count, result.Components.Count);
            for (int i = 0; i < expected.Components.Count; i++)
            {
                Assert.Equal(expected.Components[i].ComponentName, result.Components[i].ComponentName);
                Assert.True(expected.Components[i].EqualsObj(result.Components[i]));
            }

            Assert.Equal(2, provider.ErrorCollectors.Sum(ec => ec.Warnings.Count));
        }

        [Fact]
        public void WithAttributesOf_RepeatedDiffTypesViralAttributesStructure_ThrowsException()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At1"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At3"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At4"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Boolean, "Me1"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At1"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "At2"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At3"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At4"));

            Assert.ThrowsAny<Exception>(() => { dataStructure.WithAttributesOf(dataStructure2); });
        }

        [Fact]
        public void IsSupersetOf_SupersetStructure_True()
        {
            for (int i = 0; i < 2; i++)
            {
                DataStructure dataStructure = new DataStructure();
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                if (i == 1) dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id3"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));

                IDataStructure dataStructure2 = ModelResolvers.DsResolver();
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Duration, "Me1"));
                dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                Assert.True(dataStructure.IsSupersetOf(dataStructure2));
            }
        }

        [Fact]
        public void IsSupersetOf_NotSupersetStructure_False()
        {
            for (int i = 0; i < 2; i++)
            {
                DataStructure dataStructure = new DataStructure();
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));

                IDataStructure dataStructure2 = ModelResolvers.DsResolver();
                if (i == 0) dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id1"));
                else
                {
                    dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                    dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                }

                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Duration, "Me1"));
                dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                Assert.False(dataStructure.IsSupersetOf(dataStructure2));
            }
        }

        [Fact]
        public void IsSupersetOf_SupersetStructureCheckMeasures_True()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

            Assert.True(dataStructure.IsSupersetOf(dataStructure2, true));
        }

        [Fact]
        public void IsSupersetOf_NotSupersetStructureCheckMeasures_False()
        {
            for (int i = 0; i < 3; i++)
            {
                DataStructure dataStructure = new DataStructure();
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                if (i == 0) dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                if (i == 2) dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me2"));
                else if (i == 3)
                {
                    dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                    dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me3"));
                }

                IDataStructure dataStructure2 = ModelResolvers.DsResolver();
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                Assert.False(dataStructure.IsSupersetOf(dataStructure2, true));
            }
        }

        [Fact]
        public void IsSupersetOf_SupersetStructureCheckAttributes_True()
        {
            for (int i = 0; i < 2; i++)
            {
                DataStructure dataStructure = new DataStructure();
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                if (i == 1) dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                IDataStructure dataStructure2 = ModelResolvers.DsResolver();
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                Assert.True(dataStructure.IsSupersetOf(dataStructure2, false, true));
            }
        }

        [Fact]
        public void IsSupersetOf_NotSupersetStructureCheckAttributes_False()
        {
            for (int i = 0; i < 3; i++)
            {
                DataStructure dataStructure = new DataStructure();
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                if (i == 0) dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                if (i == 1) dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At3"));
                else if (i == 2)
                {
                    dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));
                    dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At4"));
                }

                IDataStructure dataStructure2 = ModelResolvers.DsResolver();
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                Assert.False(dataStructure.IsSupersetOf(dataStructure2, false, true));
            }
        }

        [Fact]
        public void IsSupersetOf_SupersetStructureCheckMeasuresCheckAttributes_True()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

            Assert.True(dataStructure.IsSupersetOf(dataStructure2, true, true));
        }

        [Fact]
        public void IsSupersetOf_NotSupersetStructureCheckMeasuresCheckAttributes_False()
        {
            for (int i = 0; i < 6; i++)
            {
                DataStructure dataStructure = new DataStructure();
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                if (i == 0) dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                if (i == 2) dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me2"));
                else if (i == 3)
                {
                    dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                    dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me3"));
                }

                dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                if (i == 4) dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At3"));
                else if (i == 5)
                {
                    dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));
                    dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At4"));
                }

                IDataStructure dataStructure2 = ModelResolvers.DsResolver();
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                Assert.False(dataStructure.IsSupersetOf(dataStructure2, true, true));
            }
        }

        [Fact]
        public void IsSupersetOf_NullIdStructureAllowNulls_False()
        {
            for (int i = 0; i < 2; i++)
            {
                DataStructure dataStructure = new DataStructure();
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.None, "Id2"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));

                IDataStructure dataStructure2 = ModelResolvers.DsResolver();
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Duration, "Me1"));
                dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                Assert.False(dataStructure.IsSupersetOf(dataStructure2, false, false, true));
            }
        }

        [Fact]
        public void IsSupersetOf_NullMeasureStructureCheckMeasuresAllowNulls_True()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.None, "Me2"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

            Assert.True(dataStructure.IsSupersetOf(dataStructure2, true, false, true));
        }

        [Fact]
        public void IsSupersetOf_NullAttributeStructureCheckAttributesAllowNulls_True()
        {
            for (int i = 0; i < 2; i++)
            {
                DataStructure dataStructure = new DataStructure();
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.None, "At3"));

                IDataStructure dataStructure2 = ModelResolvers.DsResolver();
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                Assert.True(dataStructure.IsSupersetOf(dataStructure2, false, true, true));
            }
        }

        [Fact]
        public void IsSupersetOf_NullMeasureAndAttributeStructurecheckMeasuresCheckAttributesAllowNulls_True()
        {
            for (int i = 0; i < 2; i++)
            {
                DataStructure dataStructure = new DataStructure();
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure.Measures.Add(new StructureComponent(BasicDataType.None, "Me2"));
                dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.None, "At3"));

                IDataStructure dataStructure2 = ModelResolvers.DsResolver();
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "Id1"));
                dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "Me1"));
                dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Number, "Me2"));
                dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Number, "At1"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.String, "At2"));
                dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Integer, "At3"));

                Assert.True(dataStructure.IsSupersetOf(dataStructure2, true, true, true));
            }
        }

        [Fact]
        public void AddStructure_NoCompConfictsStructure_MergedStructure()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.DatasetName = "ds1";
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp5"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp7"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.DatasetName = "ds2";
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp9"));
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp10"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp11"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.String, "comp12"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp13"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp14"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp15"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp16"));

            IDataStructure expected = ModelResolvers.DsResolver();
            expected.DatasetName = string.Empty;
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp9"));
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp10"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            expected.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp11"));
            expected.Measures.Add(new StructureComponent(BasicDataType.String, "comp12"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp5"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp13"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp14"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp7"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp15"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp16"));

            dataStructure.AddStructure(dataStructure2);

            Assert.Equal(expected.DatasetName, dataStructure.DatasetName);
            Assert.Equal(expected.Components.Count, dataStructure.Components.Count);
            for (int i = 0; i < expected.Components.Count; i++)
            {
                Assert.True(expected.Components[i].EqualsObj(dataStructure.Components[i]));
                Assert.Equal(expected.Components[i].ComponentName, dataStructure.Components[i].ComponentName);
            }
        }

        [Fact]
        public void AddStructure_DuplicatesCompsStructure_MergedStructure()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.DatasetName = "ds1";
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp5"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp7"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.DatasetName = "ds2";
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp9"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp10"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp5"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp11"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp7"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp12"));

            IDataStructure expected = ModelResolvers.DsResolver();
            expected.DatasetName = string.Empty;
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp9"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            expected.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp10"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp5"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp11"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp7"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp12"));

            dataStructure.AddStructure(dataStructure2);

            Assert.Equal(expected.DatasetName, dataStructure.DatasetName);
            Assert.Equal(expected.Components.Count, dataStructure.Components.Count);
            for (int i = 0; i < expected.Components.Count; i++)
            {
                Assert.True(expected.Components[i].EqualsObj(dataStructure.Components[i]));
                Assert.Equal(expected.Components[i].ComponentName, dataStructure.Components[i].ComponentName);
            }
        }

        [Fact]
        public void AddStructure_CompConfictsStructure_MergedStructure()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.DatasetName = "ds1";
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp5"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp7"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.DatasetName = "ds2";
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "comp1"));
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "comp3"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Integer, "comp9"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "comp5"));
            dataStructure2.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));

            IDataStructure expected = ModelResolvers.DsResolver();
            expected.DatasetName = string.Empty;
            expected.Identifiers.Add(new StructureComponent(BasicDataType.String, "comp1"));
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Integer, "comp3"));
            expected.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Integer, "comp9"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "comp5"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp7"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));

            dataStructure.AddStructure(dataStructure2);

            Assert.Equal(expected.DatasetName, dataStructure.DatasetName);
            Assert.Equal(expected.Components.Count, dataStructure.Components.Count);
            for (int i = 0; i < expected.Components.Count; i++)
            {
                Assert.True(expected.Components[i].EqualsObj(dataStructure.Components[i]));
                Assert.Equal(expected.Components[i].ComponentName, dataStructure.Components[i].ComponentName);
            }
        }

        [Fact]
        public void RemoveComponentDuplicates_DuplicatesCompsStructure_Structure()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.String, "comp1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp2"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp5"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp5"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "comp5"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp7"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.String, "comp7"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));

            IDataStructure expected = ModelResolvers.DsResolver();
            expected.Identifiers.Add(new StructureComponent(BasicDataType.String, "comp1"));
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp2"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            expected.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Number, "comp5"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.String, "comp7"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));

            dataStructure.RemoveComponentDuplicates();

            Assert.Equal(expected.Components.Count, dataStructure.Components.Count);
            for (int i = 0; i < expected.Components.Count; i++)
            {
                Assert.True(expected.Components[i].EqualsObj(dataStructure.Components[i]));
                Assert.Equal(expected.Components[i].ComponentName, dataStructure.Components[i].ComponentName);
            }
        }

        [Fact]
        public void RemoveComponentDuplicates_CompConflictsStructure_Structure()
        {
            DataStructure dataStructure = new DataStructure();
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp1"));
            dataStructure.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            dataStructure.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Time, "comp5"));
            dataStructure.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.TimePeriod, "comp7"));
            dataStructure.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));

            IDataStructure dataStructure2 = ModelResolvers.DsResolver();
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.String, "comp5"));
            dataStructure2.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "comp9"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            dataStructure2.Measures.Add(new StructureComponent(BasicDataType.String, "comp1"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            dataStructure2.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp7"));

            IDataStructure expected = ModelResolvers.DsResolver();
            expected.Identifiers.Add(new StructureComponent(BasicDataType.Number, "comp2"));
            expected.Measures.Add(new StructureComponent(BasicDataType.Boolean, "comp3"));
            expected.Measures.Add(new StructureComponent(BasicDataType.String, "comp4"));
            expected.ViralAttributes.Add(new StructureComponent(BasicDataType.Date, "comp6"));
            expected.NonViralAttributes.Add(new StructureComponent(BasicDataType.Duration, "comp8"));

            dataStructure.RemoveComponentDuplicates(dataStructure2);

            Assert.Equal(expected.Components.Count, dataStructure.Components.Count);
            for (int i = 0; i < expected.Components.Count; i++)
            {
                Assert.True(expected.Components[i].EqualsObj(dataStructure.Components[i]));
                Assert.Equal(expected.Components[i].ComponentName, dataStructure.Components[i].ComponentName);
            }
        }
    }
}
