namespace ExampleApp.VtlSources
{
    using StatisticsPoland.VtlProcessing.Core.DataModelProviders.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;

    public static class Model
    {
        public static Action<IDictionaryModelConfiguration> ModelConfiguration => (modelConfigure) =>
        {
            modelConfigure
            .AddDataSet(
                "R1",
                (ComponentType.Identifier, BasicDataType.Integer, "Id1"),
                (ComponentType.Identifier, BasicDataType.Integer, "Id2"),
                (ComponentType.Measure, BasicDataType.Integer, "Me1"),
                (ComponentType.Measure, BasicDataType.Integer, "Me2"),
                (ComponentType.NonViralAttribute, BasicDataType.String, "At1"),
                (ComponentType.ViralAttribute, BasicDataType.Integer, "At2")
                )
            .AddDataSet(
                "R2",
                (ComponentType.Identifier, BasicDataType.Integer, "Id1"),
                (ComponentType.Measure, BasicDataType.String, "Me1"),
                (ComponentType.Measure, BasicDataType.Integer, "Me2")
                )
            .AddDataSet(
                "R_num",
                (ComponentType.Identifier, BasicDataType.Integer, "Id1"),
                (ComponentType.Measure, BasicDataType.Number, "Me2")
                );
        };
    }
}
