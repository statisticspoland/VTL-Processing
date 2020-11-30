namespace Target.TSQL.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Auxiliary methods for "join" operator expressions.
    /// </summary>
    public static class JoinExpressionExtensions
    {
        /// <summary>
        /// Gets an expression with a given id name from an aliases collection from a "ds" branch.
        /// </summary>
        /// <param name="joinExpr">The "join" operator expression.</param>
        /// <param name="idName">The name of the identifier.</param>
        /// <returns>Operand containing a given id name.</returns>
        public static IExpression GetAliasExprWithId(this IJoinExpression joinExpr, string idName)
        {
            List<IExpression> aliases = joinExpr.Operands["ds"].OperandsCollection.Where(d => d.Structure.Identifiers.FirstOrDefault(id =>
                id.ComponentName.GetNameWithoutAlias() == idName.GetNameWithoutAlias()) != null).ToList();
            if (aliases.Count > 1 && idName.Split('#').Length > 1)
                return aliases.First(alias => alias.ParamSignature == idName.Split('#')[0]);
            return aliases.FirstOrDefault();
        }

        /// <summary>
        /// Gets an expression with a given measure name from an aliases collection from a "ds" branch.
        /// </summary>
        /// <param name="joinExpr">The "join" operator expression.</param>
        /// <param name="measureName">The name of the measure.</param>
        /// <returns>Operand containing a given measure name.</returns>
        public static IExpression GetAliasExprWithMeasure(this IJoinExpression joinExpr, string measureName)
        {
            List<IExpression> aliases = joinExpr.Operands["ds"].OperandsCollection.Where(d => d.Structure.Measures.FirstOrDefault(me => 
                me.ComponentName.GetNameWithoutAlias() == measureName.GetNameWithoutAlias()) != null).ToList();
            if (aliases.Count == 0)
            {
                if (joinExpr.Operands.ContainsKey("apply") && joinExpr.Operands["apply"].OperatorSymbol.In("#", "period_indicator"))
                {
                    aliases = joinExpr.Operands["ds"].OperandsCollection.Where(d => d.Structure.Components.FirstOrDefault(me =>
                        me.ComponentName.GetNameWithoutAlias() == measureName.GetNameWithoutAlias()) != null).ToList();
                }
            }
            if (aliases.Count > 1)
            {
                if (measureName.Split('#').Length > 1) return aliases.First(alias => alias.ParamSignature == measureName.Split('#')[0]);
            }

            return aliases.FirstOrDefault();
        }

        /// <summary>
        /// Gets an expression with a given viral attribute from an aliases collection from a "ds" branch.
        /// </summary>
        /// <param name="joinExpr">the "join" operator expression.</param>
        /// <param name="attributeName">The name of the viral attribute.</param>
        /// <returns>Operand containing a given viral attribute.</returns>
        public static IExpression GettAliasExprWithViralAttribute(this IJoinExpression joinExpr, string attributeName)
        {
            List<IExpression> aliases = joinExpr.Operands["ds"].OperandsCollection.Where(d => d.Structure.ViralAttributes.FirstOrDefault(at =>
                 at.ComponentName.GetNameWithoutAlias() == attributeName.GetNameWithoutAlias()) != null).ToList();
            if (aliases.Count > 1)
            {
                if (attributeName.Split('#').Length > 1) return aliases.First(alias => alias.ParamSignature == attributeName.Split('#')[0]);
                return null;
            }

            return aliases.FirstOrDefault();
        }
    }
}
