﻿namespace StatisticsPoland.VtlProcessing.Core.Models.Logical
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class JoinExpression : Expression, IJoinExpression
    {
        public JoinExpression(IExpression expression) : base(expression.ParentExpression)
        {
            this.ContainingSchema = expression.ContainingSchema;
            this.ExpressionText = expression.ExpressionText;
            this.LineNumber = expression.LineNumber;
            this.OperandsCollection = expression.OperandsCollection;
            this.OperatorDefinition = expression.OperatorDefinition;
            this.ParamSignature = expression.ParamSignature;
            this.ReferenceExpression = expression.ReferenceExpression;
            this.ResultName = expression.ResultName;
            this.Structure = expression.Structure;
        }

        /// <summary>
        /// Gets the alias expression, whose structure is a subset of others.
        /// </summary>
        /// <param name="aliasExprs">The collection of alias expressions.</param>
        /// <returns>The alias expression, whose structure is a subset of others.</returns>
        public static IExpression GetSubsetAlias(ICollection<IExpression> aliasExprs)
        {
            List<IExpression> aliases = aliasExprs?.ToList();
            if (aliases?.Count > 0)
            {
                while (aliases.Count > 1)
                {
                    for (int i = 0; i < aliases.Count; i++)
                    {
                        for (int j = 0; j < aliases.Count; j++)
                        {
                            if (i != j)
                            {
                                if (aliases[j].IsScalar || aliases[j].Structure.IsSupersetOf(aliases[i].Structure))
                                    aliases.Remove(aliases[j]);
                                else if (aliases[i].IsScalar || aliases[i].Structure.IsSupersetOf(aliases[j].Structure))
                                    aliases.Remove(aliases[i]);
                                else if (!aliases[j].Structure.IsSupersetOf(aliases[i].Structure) && !aliases[i].Structure.IsSupersetOf(aliases[j].Structure))
                                {
                                    aliases.Remove(aliases[j]);
                                    aliases.Remove(aliases[i]);
                                }

                                break;
                            }
                        }
                    }
                }
            }

            return aliases?.FirstOrDefault();
        }

        /// <summary>
        /// Gets the alias expression, whose structure is a superset of others.
        /// </summary>
        /// <param name="aliasExprs">The collection of alias expressions.</param>
        /// <returns>The alias expression, whose structure is a superset of others.</returns>
        public static IExpression GetSupersetAlias(ICollection<IExpression> aliasExprs)
        {
            List<IExpression> aliases = aliasExprs?.ToList();
            if (aliases?.Count > 0)
            {
                while (aliases.Count > 1)
                {
                    for (int i = 0; i < aliases.Count; i++)
                    {
                        for (int j = 0; j < aliases.Count; j++)
                        {
                            if (i != j)
                            {
                                if (aliases[j].IsScalar || aliases[i].Structure.IsSupersetOf(aliases[j].Structure))
                                    aliases.Remove(aliases[j]);
                                else if (aliases[i].IsScalar || aliases[j].Structure.IsSupersetOf(aliases[i].Structure))
                                    aliases.Remove(aliases[i]);
                                else if (!aliases[j].Structure.IsSupersetOf(aliases[i].Structure) && !aliases[i].Structure.IsSupersetOf(aliases[j].Structure))
                                {
                                    aliases.Remove(aliases[j]);
                                    aliases.Remove(aliases[i]);
                                }

                                break;
                            }
                        }
                    }
                }
            }

            return aliases?.FirstOrDefault();
        }

        public IDataStructure GetSubsetAliasStructure()
        {
            if (!this.Operands.ContainsKey("ds")) throw new Exception("Join expression must contain \"ds\" branch.");
            return JoinExpression.GetSubsetAlias(this.Operands["ds"].OperandsCollection.ToList()).Structure.GetCopy();
        }

        public IDataStructure GetSupersetAliasStructure()
        {
            if (!this.Operands.ContainsKey("ds")) throw new Exception("Join expression must contain \"ds\" branch.");
            return JoinExpression.GetSupersetAlias(this.Operands["ds"].OperandsCollection.ToList()).Structure.GetCopy();
        }

        public string[] GetAliasesSignatures(string compName = null)
        {
            if (!this.Operands.ContainsKey("ds")) throw new Exception("Join expression must contain \"ds\" branch.");
            return this.GetAliasesSignatures(this, compName);
        }

        public IExpression GetAliasExpression(string name)
        {
            if (!this.Operands.ContainsKey("ds")) throw new Exception("Join expression must contain \"ds\" branch.");
            return this.GetAliasExpression(this.Operands["ds"], name);
        }

        /// <summary>
        /// Gets an array of alias signatures that component with a given name of a given expression can be from.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="compName">The component name.</param>
        /// <returns>The array of aliases.</returns>
        private string[] GetAliasesSignatures(IExpression expression, string compName = null)
        {
            List<string> signatures = new List<string>();
            if (expression.OperatorSymbol == "join") expression = expression.Operands["ds"];
            foreach (IExpression expr in expression.OperandsCollection)
            {
                if (expr.OperatorSymbol.In("get", "ref", "join"))
                {
                    if (compName == null || expr.Structure.Components.FirstOrDefault(comp => compName.In(comp.ComponentName, comp.ComponentName.GetNameWithoutAlias())) != null)
                        signatures.Add(expr.ParamSignature);
                }
                else signatures.AddRange(this.GetAliasesSignatures(expr, compName));
            }

            return signatures.ToArray();
        }

        /// <summary>
        /// Gets the alias expression with a given name from a given expression.
        /// </summary>
        /// <param name="expression">The epxression.</param>
        /// <param name="name">The name of the alias.</param>
        /// <returns>The alias expression.</returns>
        private IExpression GetAliasExpression(IExpression expression, string name)
        {
            if (expression.ParamSignature == name) return expression;

            foreach (IExpression expr in expression.OperandsCollection)
            {
                IExpression resultExpr = this.GetAliasExpression(expr, name);
                if (resultExpr != null) return resultExpr;
            }

            return null;
        }
    }
}
