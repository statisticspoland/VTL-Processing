﻿namespace Core.App.VtlSources
{
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.DataModel.Infrastructure.Interfaces;
    using System;

    public static class RegularModel
    {
        public static Action<IRegularModelConfiguration> ModelConfiguration => (modelConfigure) =>
        {
            modelConfigure
            .AddDataSet(
                "Regular",
                "R1",
                (ComponentType.Identifier, BasicDataType.Integer, "Id1"),
                (ComponentType.Identifier, BasicDataType.Integer, "Id2"),
                (ComponentType.Measure, BasicDataType.Integer, "Me1"),
                (ComponentType.Measure, BasicDataType.Integer, "Me2"),
                (ComponentType.NonViralAttribute, BasicDataType.String, "At1"),
                (ComponentType.ViralAttribute, BasicDataType.Integer, "At2")
                )
            .AddDataSet(
                "Regular",
                "R2",
                (ComponentType.Identifier, BasicDataType.Integer, "Id1"),
                (ComponentType.Measure, BasicDataType.String, "Me1"),
                (ComponentType.Measure, BasicDataType.Integer, "Me2")
                )
            .AddDataSet(
                "Regular",
                "R_num",
                (ComponentType.Identifier, BasicDataType.Integer, "Id1"),
                (ComponentType.Measure, BasicDataType.Number, "Me2")
                );
        };
    }
}
