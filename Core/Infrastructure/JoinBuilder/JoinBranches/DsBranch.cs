namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The "ds" branch of the "join" operator expression builded from a dataset expression.
    /// </summary>
    public sealed class DsBranch : IJoinBranch
    {
        private readonly IExpressionFactory exprFactory;
        private readonly IExpressionTextGenerator exprTextGen;

        /// <summary>
        /// Inittializes a new instance of the <see cref="DsBranch"/> class.
        /// </summary>
        /// <param name="exprFactory">The expression factory.</param>
        /// <param name="exprTextGen">The epression text generator.</param>
        public DsBranch(IExpressionFactory exprFactory, IExpressionTextGenerator exprTextGen)
        {
            this.exprFactory = exprFactory;
            this.exprTextGen = exprTextGen;
        }

        public string Signature => "ds";

        public IExpression Build(IExpression datasetExpr)
        {
            IExpression dsBranch = this.exprFactory.ExprResolver();

            dsBranch.ResultName = "Alias";
            dsBranch.ParamSignature = this.Signature;
            dsBranch.ExpressionText = string.Empty;
            foreach (IExpression op in datasetExpr.OperandsCollection)
            {
                this.MoveNamesToAliases(op, dsBranch); // przeniesienie operandów z operatorami get, ref i join do gałęzi ds i zastąpienie ich sygnaturami
            }

            foreach (IExpression alias in dsBranch.OperandsCollection)
            {
                dsBranch.ExpressionText += $"{alias.ExpressionText} as {alias.ParamSignature}, ";
            }

            dsBranch.ExpressionText = dsBranch.ExpressionText.Remove(dsBranch.ExpressionText.Length - 2); // usunięcie ", "

            return dsBranch;
        }

        /// <summary>
        /// Moves expression with get, reference or "join" operator and replaces it with alias expression.
        /// </summary>
        /// <param name="expr">Expression to move.</param>
        /// <param name="ds">Ds branch.</param>
        private void MoveNamesToAliases(IExpression expr, IExpression ds)
        {
            if (!expr.IsScalar)
            {
                if (!expr.OperatorSymbol.In("get", "ref", "#", "join"))
                {
                    if (expr.OperatorSymbol != "#")
                    {
                        foreach (IExpression op in expr.OperandsCollection)
                        {
                            this.MoveNamesToAliases(op, ds);
                        }
                    }
                }
                else
                {
                    IExpression dsExpr = ds.OperandsCollection.FirstOrDefault(dsOp => dsOp.ExpressionText == expr.ExpressionText);
                    string signature = dsExpr?.ParamSignature;

                    if (dsExpr == null)
                    {
                        // Jeżeli gałąź ds nie zawiera danego operandu
                        signature = $"ds{ds.Operands.Count + 1}";
                        if (expr as IJoinExpression != null) dsExpr = this.exprFactory.JoinExprResolver(expr);
                        else
                        {
                            dsExpr = this.exprFactory.ExprResolver();
                            dsExpr.ResultName = expr.ResultName;
                            dsExpr.ExpressionText = expr.ExpressionText;
                            dsExpr.LineNumber = expr.LineNumber;
                            dsExpr.OperandsCollection = expr.OperandsCollection;
                            dsExpr.OperatorDefinition = expr.OperatorDefinition;
                            dsExpr.Structure = expr.Structure.GetCopy();
                            dsExpr.ReferenceExpression = expr.ReferenceExpression;
                        }

                        ds.AddOperand(signature, dsExpr); // dodanie operandu do gałęzi ds
                    }

                    // Podmiana operandu na sygnaturę:
                    expr.ResultName = "Alias";
                    expr.ExpressionText = signature;
                    expr.ParamSignature = signature;
                    expr.OperandsCollection = new List<IExpression>();
                    expr.OperatorDefinition = this.exprFactory.GetExpression("ref", ExpressionFactoryNameTarget.OperatorSymbol).OperatorDefinition;
                    expr.ReferenceExpression = dsExpr;
                }

                if (expr.OperatorDefinition != null)
                {
                    expr.Structure = expr.OperatorDefinition.GetOutputStructure(expr);
                    if (expr.ResultName == "Alias")
                    {
                        expr.Structure.DatasetName = null;

                        if (expr.ParentExpression.OperatorSymbol != "exists_in") expr.Structure.Identifiers.Clear();
                        else if (expr.ParentExpression.OperatorSymbol == "exists_in") expr.Structure.Measures.Clear();
                        expr.Structure.ViralAttributes.Clear();
                        expr.Structure.NonViralAttributes.Clear();
                    }
                }

                this.exprTextGen.Generate(expr);
            }
        }
    }
}
