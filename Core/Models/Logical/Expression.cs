namespace StatisticsPoland.VtlProcessing.Core.Models.Logical
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The single VTL 2.0 expression representation.
    /// </summary>
    public class Expression : IExpression
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Expression"/> class.
        /// </summary>
        /// <param name="parentExpr">The parent expression.</param>
        public Expression(IExpression parentExpr = null)
        {
            this.ParentExpression = parentExpr;
            this.Operands = new Dictionary<string, IExpression>();
        }

        public string ExpressionText { get; set; }

        public string ResultName { get; set; }

        public string ResultMappedName { get; set; }

        public string OperatorSymbol => this.OperatorDefinition?.Symbol;

        public string ParamSignature { get; set; }

        public bool IsScalar => this.Structure?.IsSingleComponent == true;

        public int LineNumber { get; set; }

        public ITransformationSchema ContainingSchema { get; set; }

        public IExpression ParentExpression { get; private set; }

        public IExpression ReferenceExpression { get; set; }

        public IDataStructure Structure { get; set; }

        public IOperatorDefinition OperatorDefinition { get; set; }

        public ICollection<IExpression> OperandsCollection
        {
            get
            {
                return this.Operands.Values;
            }

            set
            {
                this.Operands.Clear();

                Dictionary<string, int> names = new Dictionary<string, int>();
                for (byte q = 0; q < 2; q++)
                {
                    foreach (Expression ex in value)
                    {
                        if (q == 0)
                        {
                            if (!names.ContainsKey(ex.ParamSignature))
                            {
                                names.Add(ex.ParamSignature, 1);
                            }
                            else
                            {
                                names[ex.ParamSignature]++;
                            }
                        }
                        else
                        {
                            ex.ParentExpression = this;
                            if (ex.ParamSignature != "<root>")
                            {
                                if (names[ex.ParamSignature] > 1)
                                {
                                    for (int i = 1; i <= names[ex.ParamSignature]; i++)
                                    {
                                        if (!this.Operands.ContainsKey($"{ex.ParamSignature}_{i}"))
                                        {
                                            ex.ParamSignature += $"_{i}";
                                            break;
                                        }
                                    }
                                }

                                this.Operands.Add(ex.ParamSignature, ex);
                            }
                            else
                            {
                                this.Operands.Add(ex.ResultName, ex);
                            }
                        }
                    }
                }
            }
        }

        public IDictionary<string, IExpression> Operands { get; private set; }

        public void AddOperand(string signature, IExpression expression)
        {
            if (expression == null)
            {
                throw new Exception("Try to add non-existent (null) operand");
            }

            (expression as Expression).ParentExpression = this;
            if (!this.Operands.ContainsKey(signature))
            {
                expression.ParamSignature = signature;
                this.Operands[signature] = expression;
            }
        }

        public void SetContainingSchema(ITransformationSchema schema)
        {
            this.ContainingSchema = schema;
            foreach (IExpression operand in this.OperandsCollection)
            {
                operand.SetContainingSchema(schema);
            }
        }
    }
}
