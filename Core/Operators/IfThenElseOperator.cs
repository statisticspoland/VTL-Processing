namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Linq;

    /// <summary>
    /// The "If-Then-Else" operator definition.
    /// </summary>
    [OperatorSymbol("if")]
    public class IfThenElseOperator : IOperatorDefinition
    {
        private readonly IJoinApplyMeasuresOperator _joinApplyMeasuresOp;
        private readonly DataStructureResolver _dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="IfThenElseOperator"/> class.
        /// </summary>
        /// <param name="joinApplyMeasuresOp">The join apply measure operator.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        public IfThenElseOperator(IJoinApplyMeasuresOperator joinApplyMeasuresOp, DataStructureResolver dsResolver)
        {
            this._joinApplyMeasuresOp = joinApplyMeasuresOp;
            this._dsResolver = dsResolver;
        }

        public string Name => "If-Then-Else";

        public string Symbol { get; set; } = "if";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            if (expression.IsApplyComponent)
            {
                expression.Operands["if"].Structure = expression.Operands["if"].Operands["ds_1"].Structure.GetCopy();
                expression.Operands["then"].Structure = expression.Operands["then"].Operands["ds_1"].Structure.GetCopy();
                expression.Operands["else"].Structure = expression.Operands["else"].Operands["ds_1"].Structure.GetCopy();
                return this._joinApplyMeasuresOp.GetMeasuresStructure(expression);
            }
            
            IDataStructure dataStructure = null;
            IExpression ifExpr = expression.Operands["if"];
            IExpression thenExpr = expression.Operands["then"];
            IExpression elseExpr = expression.Operands["else"];

            ifExpr.Structure = ifExpr.OperandsCollection.First().Structure.GetCopy();
            thenExpr.Structure = thenExpr.OperandsCollection.First().Structure.GetCopy();
            elseExpr.Structure = elseExpr.OperandsCollection.First().Structure.GetCopy();

            if (ifExpr.IsScalar && ifExpr.Structure.Components[0].ValueDomain.DataType != BasicDataType.Boolean)
                throw new VtlOperatorError(expression, this.Name, "Expected boolean value in condition expression.");
            else if (!ifExpr.IsScalar)
            {
                if (ifExpr.Structure.Measures.Count != 1)
                    throw new VtlOperatorError(expression, this.Name, "Expected single measure expression as condition.");

                if(ifExpr.Structure.Measures[0].ValueDomain.DataType != BasicDataType.Boolean)
                    throw new VtlOperatorError(expression, this.Name, "Expected boolean measure in condition expression.");
            }

            if (!thenExpr.IsScalar)
            {
                if (elseExpr.IsScalar)
                {
                    dataStructure = thenExpr.Structure.GetCopy().WithAttributesOf(ifExpr.Structure);
                    if (thenExpr.Structure.Measures.FirstOrDefault(me => !me.ValueDomain.DataType.EqualsObj(elseExpr.Structure.Components[0].ValueDomain.DataType)) != null)
                        throw new VtlOperatorError(expression, this.Name, "All measures of thenClause data structure must have elseClause's scalar data type.");

                    // Remove none and replace ints to numbers data types when possible:
                    foreach (StructureComponent measure in dataStructure.Measures)
                    {
                        if (measure.ValueDomain.DataType.In(BasicDataType.None, BasicDataType.Integer) && elseExpr.Structure.Components[0].ValueDomain.DataType != BasicDataType.None)
                            measure.ValueDomain = new ValueDomain(elseExpr.Structure.Components[0].ValueDomain.DataType);
                    }
                }

                if (!ifExpr.IsScalar && !ifExpr.Structure.IsSupersetOf(thenExpr.Structure)) throw new VtlOperatorError(expression, this.Name, "Datasets don't fit");
            }
            
            if (!elseExpr.IsScalar)
            {
                if (thenExpr.IsScalar)
                {
                    dataStructure = elseExpr.Structure.GetCopy().WithAttributesOf(ifExpr.Structure);
                    if (elseExpr.Structure.Measures.FirstOrDefault(me => !me.ValueDomain.DataType.EqualsObj(thenExpr.Structure.Components[0].ValueDomain.DataType)) != null)
                        throw new VtlOperatorError(expression, this.Name, "All measures of elseClause data structure must have thenClause's scalar data type.");

                    // Remove none and replace ints to numbers data types when possible:
                    foreach (StructureComponent measure in dataStructure.Measures)
                    {
                        if (measure.ValueDomain.DataType.In(BasicDataType.None, BasicDataType.Integer) && thenExpr.Structure.Components[0].ValueDomain.DataType != BasicDataType.None)
                            measure.ValueDomain = new ValueDomain(thenExpr.Structure.Components[0].ValueDomain.DataType);
                    }
                }

                if (!ifExpr.IsScalar && !ifExpr.Structure.IsSupersetOf(elseExpr.Structure)) throw new VtlOperatorError(expression, this.Name, "Datasets don't fit");
            }

            if (!thenExpr.IsScalar && !elseExpr.IsScalar)
            {
                bool thenStructure = thenExpr.Structure.IsSupersetOf(elseExpr.Structure, true, false, true);

                if (thenStructure)
                    dataStructure = thenExpr.Structure.GetCopy().WithAttributesOf(ifExpr.Structure).WithAttributesOf(elseExpr.Structure);
                else if (elseExpr.Structure.IsSupersetOf(thenExpr.Structure, true, false, true))
                    dataStructure = elseExpr.Structure.GetCopy().WithAttributesOf(ifExpr.Structure).WithAttributesOf(thenExpr.Structure);
                else throw new VtlOperatorError(expression, this.Name, "Datasets don't fit");

                // Remove none and replace ints to numbers data types when possible:
                for (int i = 0; i < dataStructure.Measures.Count; i++)
                {
                    StructureComponent measure = dataStructure.Measures[i];
                    if (measure.ValueDomain.DataType.In(BasicDataType.None, BasicDataType.Integer))
                    {
                        if (thenStructure && elseExpr.Structure.Measures[i].ValueDomain.DataType != BasicDataType.None)
                            measure.ValueDomain = new ValueDomain(elseExpr.Structure.Measures[i].ValueDomain.DataType);
                        else if (thenExpr.Structure.Measures[i].ValueDomain.DataType != BasicDataType.None)
                            measure.ValueDomain = new ValueDomain(thenExpr.Structure.Measures[i].ValueDomain.DataType);
                    }
                }
            }
            else if (thenExpr.IsScalar && elseExpr.IsScalar &&
                !thenExpr.Structure.Components[0].ValueDomain.DataType.EqualsObj(elseExpr.Structure.Components[0].ValueDomain.DataType))
            {
                throw new VtlOperatorError(expression, this.Name, "ThenClause and ElseClause scalar data types are not the same.");
            }

            if (dataStructure != null) return dataStructure;
            else if (!ifExpr.IsScalar)
            {
                if (thenExpr.Structure.Components[0].ValueDomain.DataType != BasicDataType.Boolean ||
                    elseExpr.Structure.Components[0].ValueDomain.DataType != BasicDataType.Boolean)
                {
                    throw new VtlOperatorError(expression, this.Name, "Expected boolean values in thenClause and elseClause.");
                }

                return ifExpr.Structure.GetCopy();
            }

            BasicDataType type = thenExpr.Structure.Components[0].ValueDomain.DataType;
            if (type.In(BasicDataType.Integer, BasicDataType.None) && elseExpr.Structure.Components[0].ValueDomain.DataType != BasicDataType.None)
                type = elseExpr.Structure.Components[0].ValueDomain.DataType;
            return this._dsResolver(type.GetVariableName(), ComponentType.Measure, type);
        }
    }
}