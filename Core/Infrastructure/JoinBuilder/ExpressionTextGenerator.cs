namespace StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Operators;
    using System.Linq;

    /// <summary>
    /// The expression text generator for expressions rebuilded in a join builder.
    /// </summary>
    public class ExpressionTextGenerator : IExpressionTextGenerator
    {
        public void Generate(IExpression expr)
        {
            if (expr.OperatorSymbol.In("+", "-", "*", "/", "||", "=", "<>", "<", "<=", ">", ">=", "and", "or", "xor", "in", "not_in"))
                expr.ExpressionText = $"{expr.OperandsCollection.ToArray()[0].ExpressionText} {expr.OperatorSymbol} {expr.OperandsCollection.ToArray()[1].ExpressionText}";
            else if (expr.OperatorSymbol == "#") expr.ExpressionText = $"{expr.OperandsCollection.ToArray()[0].ExpressionText}{expr.OperatorSymbol}{expr.OperandsCollection.ToArray()[1].ExpressionText}";
            else if (expr.OperatorSymbol == "not") expr.ExpressionText = $"{expr.OperatorSymbol} {expr.OperandsCollection.ToArray()[0].ExpressionText}";
            else if (expr.OperatorSymbol.In("minus", "plus"))
            {
                string symbol = expr.OperatorSymbol == "plus" ? "+" : "-";
                expr.ExpressionText = $"{symbol}{expr.OperandsCollection.ToArray()[0].ExpressionText}";
            }
            else if (expr.OperatorSymbol.In("calc", "keep", "drop", "rename"))
            {
                expr.ExpressionText = expr.OperatorSymbol;
                foreach (IExpression clauseExpr in expr.OperandsCollection)
                {
                    expr.ExpressionText += $" {clauseExpr.ExpressionText},";
                }

                expr.ExpressionText = expr.ExpressionText.Remove(expr.ExpressionText.Length - 1); // usunięcie ","
            }
            else if (expr.OperatorSymbol == "calcExpr") expr.ExpressionText = $"{expr.Operands["ds_1"].ExpressionText} := {expr.Operands["ds_2"].ExpressionText}";
            else if (expr.OperatorSymbol == "renameExpr") expr.ExpressionText = $"{expr.Operands["ds_1"].ExpressionText} to {expr.Operands["ds_2"].ExpressionText}";
            else if (expr.OperatorSymbol == "if")
            {
                string elseExpr = expr.Operands.ContainsKey("else") ? $" {expr.Operands["else"].ExpressionText}" : string.Empty;
                expr.ExpressionText = $"{expr.Operands["if"].ExpressionText} {expr.Operands["then"].ExpressionText}{elseExpr}";
            }
            else if (expr.OperatorSymbol == null)
            {
                if (expr.ResultName == "If") expr.ExpressionText = $"if {expr.Operands["ds_1"].ExpressionText}";
                else if (expr.ResultName == "Then") expr.ExpressionText = $"then {expr.Operands["ds_1"].ExpressionText}";
                else if (expr.ResultName == "Else") expr.ExpressionText = $"else {expr.Operands["ds_1"].ExpressionText}";
            }
            else if (!expr.OperatorSymbol.In("get", "ref", "const", "comp", "join", "collection", "datasetClause"))
            {
                expr.ExpressionText = $"{expr.OperatorSymbol}(";
                foreach (IExpression op in expr.OperandsCollection)
                {
                    expr.ExpressionText += $"{op.ExpressionText}, ";
                }

                expr.ExpressionText = expr.ExpressionText.Remove(expr.ExpressionText.Length - 2); // usunięcie ", "
                expr.ExpressionText += ")";
            }
        }

        public void GenerateRecursively(IExpression expr)
        {
            foreach (IExpression operand in expr.OperandsCollection)
            {
                this.GenerateRecursively(operand);
            }

            this.Generate(expr);
        }
    }
}
