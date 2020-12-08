namespace StatisticsPoland.VtlProcessing.Core.Tests.OperatorsTests
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using Xunit;

    public class TimeOperatorTests
    {
        [Theory]
        [InlineData("fill_time_series", BasicDataType.Integer)]
        [InlineData("fill_time_series", BasicDataType.Number)]
        [InlineData("fill_time_series", BasicDataType.String)]
        [InlineData("fill_time_series", BasicDataType.Boolean)]
        [InlineData("flow_to_stock", BasicDataType.Integer)]
        [InlineData("flow_to_stock", BasicDataType.Number)]
        [InlineData("flow_to_stock", BasicDataType.String)]
        [InlineData("flow_to_stock", BasicDataType.Boolean)]
        [InlineData("stock_to_flow", BasicDataType.Integer)]
        [InlineData("stock_to_flow", BasicDataType.Number)]
        [InlineData("stock_to_flow", BasicDataType.String)]
        [InlineData("stock_to_flow", BasicDataType.Boolean)]
        [InlineData("timeshift", BasicDataType.Integer)]
        [InlineData("timeshift", BasicDataType.Number)]
        [InlineData("timeshift", BasicDataType.String)]
        [InlineData("timeshift", BasicDataType.Boolean)]
        public void GetOutputStructure_NoTimeIdentifiersExpr_ThrowsException(string symbol, BasicDataType type)
        {
            IExpression timeExpr = ModelResolvers.ExprResolver();
            timeExpr.OperatorDefinition = ModelResolvers.OperatorResolver(symbol);

            IExpression timeDsExpr = ModelResolvers.ExprResolver();
            timeDsExpr.Structure = ModelResolvers.DsResolver("Id1", ComponentType.Identifier, type);
            timeDsExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));

            timeExpr.AddOperand("ds_1", timeDsExpr);

            Assert.ThrowsAny<VtlOperatorError>(() => { timeExpr.OperatorDefinition.GetOutputStructure(timeExpr); });
        }

        [Theory]
        [InlineData("fill_time_series", BasicDataType.Time)]
        [InlineData("fill_time_series", BasicDataType.Date)]
        [InlineData("fill_time_series", BasicDataType.TimePeriod)]
        [InlineData("flow_to_stock", BasicDataType.Time)]
        [InlineData("flow_to_stock", BasicDataType.Date)]
        [InlineData("flow_to_stock", BasicDataType.TimePeriod)]
        [InlineData("stock_to_flow", BasicDataType.Time)]
        [InlineData("stock_to_flow", BasicDataType.Date)]
        [InlineData("stock_to_flow", BasicDataType.TimePeriod)]
        [InlineData("timeshift", BasicDataType.Time)]
        [InlineData("timeshift", BasicDataType.Date)]
        [InlineData("timeshift", BasicDataType.TimePeriod)]
        public void GetOutputStructure_1TimeIdentifierExpr_DataStructure(string symbol, BasicDataType timeType)
        {
            IExpression timeExpr = ModelResolvers.ExprResolver();
            timeExpr.OperatorDefinition = ModelResolvers.OperatorResolver(symbol);

            IExpression timeDsExpr = ModelResolvers.ExprResolver();
            timeDsExpr.Structure = ModelResolvers.DsResolver("Id1", ComponentType.Identifier, timeType);
            timeDsExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, "Id2"));

            timeExpr.AddOperand("ds_1", timeDsExpr);

            IDataStructure dataStructure = timeExpr.OperatorDefinition.GetOutputStructure(timeExpr);

            Assert.True(timeDsExpr.Structure.EqualsObj(dataStructure));
        }

        [Theory]
        [InlineData("fill_time_series", BasicDataType.Time, BasicDataType.Time)]
        [InlineData("fill_time_series", BasicDataType.Time, BasicDataType.Date)]
        [InlineData("fill_time_series", BasicDataType.Time, BasicDataType.TimePeriod)]
        [InlineData("fill_time_series", BasicDataType.Date, BasicDataType.Time)]
        [InlineData("fill_time_series", BasicDataType.Date, BasicDataType.Date)]
        [InlineData("fill_time_series", BasicDataType.Date, BasicDataType.TimePeriod)]
        [InlineData("fill_time_series", BasicDataType.TimePeriod, BasicDataType.Time)]
        [InlineData("fill_time_series", BasicDataType.TimePeriod, BasicDataType.Date)]
        [InlineData("fill_time_series", BasicDataType.TimePeriod, BasicDataType.TimePeriod)]
        [InlineData("flow_to_stock", BasicDataType.Time, BasicDataType.Time)]
        [InlineData("flow_to_stock", BasicDataType.Time, BasicDataType.Date)]
        [InlineData("flow_to_stock", BasicDataType.Time, BasicDataType.TimePeriod)]
        [InlineData("flow_to_stock", BasicDataType.Date, BasicDataType.Time)]
        [InlineData("flow_to_stock", BasicDataType.Date, BasicDataType.Date)]
        [InlineData("flow_to_stock", BasicDataType.Date, BasicDataType.TimePeriod)]
        [InlineData("flow_to_stock", BasicDataType.TimePeriod, BasicDataType.Time)]
        [InlineData("flow_to_stock", BasicDataType.TimePeriod, BasicDataType.Date)]
        [InlineData("flow_to_stock", BasicDataType.TimePeriod, BasicDataType.TimePeriod)]
        [InlineData("stock_to_flow", BasicDataType.Time, BasicDataType.Time)]
        [InlineData("stock_to_flow", BasicDataType.Time, BasicDataType.Date)]
        [InlineData("stock_to_flow", BasicDataType.Time, BasicDataType.TimePeriod)]
        [InlineData("stock_to_flow", BasicDataType.Date, BasicDataType.Time)]
        [InlineData("stock_to_flow", BasicDataType.Date, BasicDataType.Date)]
        [InlineData("stock_to_flow", BasicDataType.Date, BasicDataType.TimePeriod)]
        [InlineData("stock_to_flow", BasicDataType.TimePeriod, BasicDataType.Time)]
        [InlineData("stock_to_flow", BasicDataType.TimePeriod, BasicDataType.Date)]
        [InlineData("stock_to_flow", BasicDataType.TimePeriod, BasicDataType.TimePeriod)]
        [InlineData("timeshift", BasicDataType.Time, BasicDataType.Time)]
        [InlineData("timeshift", BasicDataType.Time, BasicDataType.Date)]
        [InlineData("timeshift", BasicDataType.Time, BasicDataType.TimePeriod)]
        [InlineData("timeshift", BasicDataType.Date, BasicDataType.Time)]
        [InlineData("timeshift", BasicDataType.Date, BasicDataType.Date)]
        [InlineData("timeshift", BasicDataType.Date, BasicDataType.TimePeriod)]
        [InlineData("timeshift", BasicDataType.TimePeriod, BasicDataType.Time)]
        [InlineData("timeshift", BasicDataType.TimePeriod, BasicDataType.Date)]
        [InlineData("timeshift", BasicDataType.TimePeriod, BasicDataType.TimePeriod)]
        public void GetOutputStructure_MoreThan1TimeIdentifierExpr_ThrowsException(string symbol, params BasicDataType[] timeTypes)
        {
            IExpression timeExpr = ModelResolvers.ExprResolver();
            timeExpr.OperatorDefinition = ModelResolvers.OperatorResolver(symbol);

            IExpression timeDsExpr = ModelResolvers.ExprResolver();
            timeDsExpr.Structure = ModelResolvers.DsResolver();

            for (int i = 0; i < timeTypes.Length; i++)
            {
                timeDsExpr.Structure.Identifiers.Add(new StructureComponent(timeTypes[i], $"Id{i + 1}"));
            }

            timeDsExpr.Structure.Identifiers.Add(new StructureComponent(BasicDataType.Integer, $"Id{timeTypes.Length}"));
            timeExpr.AddOperand("ds_1", timeDsExpr);

            Assert.ThrowsAny<VtlOperatorError>(() => { timeExpr.OperatorDefinition.GetOutputStructure(timeExpr); });
        }
    }
}
