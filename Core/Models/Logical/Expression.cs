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
        private string expressionText;

        /// <summary>
        /// Initialises a new instance of the <see cref="Expression"/> class.
        /// </summary>
        /// <param name="parentExpr">The parent expression.</param>
        public Expression(IExpression parentExpr = null)
        {
            this.ParentExpression = parentExpr;
            this.ContainingSchema = parentExpr?.ContainingSchema;
            this.Operands = new Dictionary<string, IExpression>();
        }

        public string ExpressionText 
        {   
            get
            {
                return this.expressionText;
            }
            set
            {
                this.expressionText = value;
                this.expressionText = this.expressionText?.Replace(Environment.NewLine, " ");
                while (this.expressionText?.StartsWith(' ') == true)
                {
                    this.expressionText = this.expressionText.Remove(0, 1);
                }
            }
        }

        public string ResultName { get; set; }

        public string ResultMappedName { get; set; }

        public string OperatorSymbol => this.OperatorDefinition?.Symbol;

        public string ParamSignature { get; set; }

        public bool IsScalar => this.Structure?.IsSingleComponent == true || this.IsApplyComponent;

        public bool IsApplyComponent =>
            (this.ResultName == "Apply" || this.GetFirstAncestorExpr("Apply") != null) &&
            (this.ResultName == "Alias" || this.GetDescendantExprs("Alias").Count != 0);

        public int LineNumber { get; set; }

        public ITransformationSchema ContainingSchema { get; set; }

        public IExpression ParentExpression { get; set; }

        public IJoinExpression CurrentJoinExpr => this.GetCurrentJoinExpr(this);

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
                    foreach (IExpression ex in value)
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
                throw new ArgumentNullException("expression", "Try to add non-existent (null) operand");
            }

            (expression as Expression).ParentExpression = this;
            expression.ParamSignature = signature;
            expression.ContainingSchema = this.ContainingSchema;
            if (!this.Operands.ContainsKey(signature)) this.Operands.Add(signature, expression);
            else this.Operands[signature] = expression;
        }

        public void SetContainingSchema(ITransformationSchema schema)
        {
            this.ContainingSchema = schema;
            foreach (IExpression operand in this.OperandsCollection)
            {
                operand.SetContainingSchema(schema);
            }
        }

        public IExpression GetFirstAncestorExpr()
        {
            IExpression ancestorExpr = this.ParentExpression;
            while (ancestorExpr?.ParentExpression != null)
            {
                ancestorExpr = ancestorExpr.ParentExpression;
            }

            return ancestorExpr;
        }

        public IExpression GetFirstAncestorExpr(string resultName)
        {
            IExpression ancestorExpr = this.ParentExpression;
            while (ancestorExpr != null && ancestorExpr.ResultName != resultName)
            {
                ancestorExpr = ancestorExpr.ParentExpression;
            }

            return ancestorExpr;
        }

        public ICollection<IExpression> GetDescendantExprs(string resultName)
        {
            List<IExpression> descendantExprs = new List<IExpression>();
            foreach (IExpression expr in this.OperandsCollection)
            {
                if (expr.ResultName == resultName) descendantExprs.Add(expr);
                descendantExprs.AddRange(expr.GetDescendantExprs(resultName));
            }

            return descendantExprs;
        }

        /// <summary>
        /// Gets the "join" expression that a given expression is inside it.
        /// </summary>
        /// <param name="expr">The expression.</param>
        /// <returns>The "join" expression.</returns>
        private IJoinExpression GetCurrentJoinExpr(IExpression expr)
        {
            if (expr == null) return null;
            if (expr.ParentExpression?.OperatorSymbol == "join") return (IJoinExpression)expr.ParentExpression;
            else return this.GetCurrentJoinExpr(expr.ParentExpression);
        }
    }
}
