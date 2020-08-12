namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;
    using System.Globalization;

    /// <summary>
    /// The "Constant" operator definition.
    /// </summary>
    [OperatorSymbol("const")]
    public class ConstantOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="ConstantOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public ConstantOperator(DataStructureResolver dsResolver)
        {
            this.dsResolver = dsResolver;
        }

        public string Name => "Constant";

        public string Symbol => "const";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            string exprText = expression.ExpressionText;
            string constName;
            BasicDataType dataType;
            NumberFormatInfo decimalSeparator = new NumberFormatInfo { NumberDecimalSeparator = "." };

            if (exprText.StartsWith("t") && exprText.EndsWith("\""))
            {
                exprText = exprText.Replace("t", string.Empty).Replace("\"", string.Empty);

                string[] split = exprText.Split('/');
                if (split.Length == 2)
                {
                    if (!DateTime.TryParse(split[0], out _) && !DateTime.TryParse(split[1], out _))
                        throw new VtlOperatorError(expression, this.Name, $"Could not convert expression '{exprText}' to data type time.");
                    dataType = BasicDataType.Time;
                    constName = "time_var";
                }
                else
                {
                    if (DateTime.TryParse(exprText, out _))
                    {
                        dataType = BasicDataType.Date;
                        constName = "date_var";
                    }
                    else if (exprText.In("D", "W", "M", "Q", "S", "A"))
                    {
                        dataType = BasicDataType.Duration;
                        constName = "duration_var";
                    }
                    else if (exprText.Length >= 4)
                    {
                        if (exprText.Length > 5 &&
                            (exprText.Length > 8 &&
                            !int.TryParse(exprText.Substring(5, exprText.Length - 5), out _)))
                                throw new VtlOperatorError(expression, this.Name, $"Could not convert expression '{exprText}' to data type time_period.");
                        dataType = BasicDataType.TimePeriod;
                        constName = "time_period_var";
                    }
                    else throw new VtlOperatorError(expression, this.Name, $"Could not convert expression '{exprText}' to data type time_period.");
                }
            }
            else if (exprText.StartsWith("\"") && exprText.EndsWith("\""))
            {
                dataType = BasicDataType.String;
                constName = "string_var";
            }
            else if (long.TryParse(exprText, out long l))
            {
                dataType = BasicDataType.Integer;
                constName = "int_var";
            }
            else if (decimal.TryParse(exprText, NumberStyles.Float, decimalSeparator, out decimal d))
            {
                dataType = BasicDataType.Number;
                constName = "num_var";
            }
            else if (exprText.Equals("true") || exprText.Equals("false"))
            {
                dataType = BasicDataType.Boolean;
                constName = "bool_var";
            }
            else if (exprText == "null")
            {
                dataType = BasicDataType.None;
                constName = "NULL";
            }
            else
            {
                throw new VtlOperatorError(expression, this.Name, "Could not infer data type.");
            }

            return this.dsResolver(constName, ComponentType.Measure, dataType);
        }
    }
}