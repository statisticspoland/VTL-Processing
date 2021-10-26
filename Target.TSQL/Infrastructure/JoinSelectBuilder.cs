namespace StatisticsPoland.VtlProcessing.Target.TSQL.Infrastructure
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Interfaces;

    /// <summary>
    /// Visits an "apply" branch of a "join" operator expression and renders measures for the TSQL "join select" query.
    /// </summary>
    /// <param name="applyBranch">The "apply" branch of a "join" operator which parameters shall be used to render.</param>
    /// <param name="measure">The selected measure with the old name to assign in the translated code.</param>
    /// <param name="renamedMeasure">The selected measure with the new name to assign in the translated code.</param>
    /// <returns>The TSQL translated code with measures.</returns>
    public delegate string JoinMeasuresRenderer(IExpression applyBranch, StructureComponent measure, StructureComponent renamedMeasure);

    /// <summary>
    /// The TSQL "join select" query builder.
    /// </summary>
    internal sealed class JoinSelectBuilder : IJoinSelectBuilder
    {
        private readonly OperatorRendererResolver _opRendererResolver;
        private readonly IEnvironmentMapper _envMapper;
        private readonly IAttributePropagationAlgorithm _propagationAlgorithm;
        private readonly Dictionary<string, string> parts;
        private IJoinExpression joinExpr;
        private bool ifThenElse;

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinSelectBuilder"/> class.
        /// </summary>
        /// <param name="opRendererResolver">The operator renderer resolver.</param>
        /// <param name="envMapper">The environment names mapper.</param>
        /// <param name="propagationAlgorithm">The attribute propagation algorithm.</param>
        public JoinSelectBuilder(OperatorRendererResolver opRendererResolver, IEnvironmentMapper envMapper, IAttributePropagationAlgorithm propagationAlgorithm)
        {
            this._opRendererResolver = opRendererResolver;
            this._envMapper = envMapper;
            this._propagationAlgorithm = propagationAlgorithm;

            this.parts = new Dictionary<string, string>();
            this.parts.Add("identifiers", string.Empty);
            this.parts.Add("measures", string.Empty);
            this.parts.Add("attributes", string.Empty);
            this.parts.Add("calc", string.Empty);
            this.parts.Add("source", string.Empty);
            this.parts.Add("filters", string.Empty);
            this.parts.Add("group", string.Empty);
            this.parts.Add("having", string.Empty);
            this.parts.Add("over", string.Empty);
        }

        public string Build()
        {
            StringBuilder sb = new StringBuilder();

            if (this.ifThenElse) sb.AppendLine("SELECT * FROM (");
            
            sb.AppendLine("SELECT");
            sb.Append(this.parts["identifiers"]);
            sb.Append(this.parts["measures"]);
            sb.Append(this.parts["attributes"]);
            sb.Append("FROM ");
            sb.Append(this.parts["source"]);
            sb.Append(this.parts["filters"]);
            sb.Append(this.parts["group"]);
            sb.Append(this.parts["having"]);
            
            if (this.ifThenElse) sb.AppendLine($") AS t WHERE {this.joinExpr.Operands["ds"].OperandsCollection.First().Structure.Identifiers[0].ComponentName} IS NOT NULL");

            return sb.ToString();
        }

        public IJoinSelectBuilder AddJoinExpr(IJoinExpression joinExpr)
        {
            this.joinExpr = joinExpr;
            this.ifThenElse = this.joinExpr.Operands.ContainsKey("apply") && this.joinExpr.Operands["apply"].OperatorSymbol == "if";

            return this;
        }

        public IJoinSelectBuilder AddIdentifiers()
        {
            StringBuilder sb = new StringBuilder();
            StructureComponent[] identifiers = this.joinExpr.Structure.Identifiers.ToArray();

            for (int i = 0; i < identifiers.Length; i++)
            {
                bool isCalc = false;
                if (this.joinExpr.Operands.ContainsKey("calc"))
                {
                    IExpression calcExpr = this.joinExpr.Operands["calc"].GetDescendantExprs("Calc expression").FirstOrDefault(expr => expr.Structure.Components.FirstOrDefault(comp => comp.ComponentName == identifiers[i].ComponentName) != null);
                    if (calcExpr != null)
                    {
                        sb.Append($"{this._opRendererResolver(calcExpr.OperatorSymbol).Render(calcExpr)}");
                        isCalc = true;
                    }
                }

                if (!isCalc)
                {
                    bool skipId = false;
                    string baseName = this.joinExpr.BasicStructure.Identifiers.FirstOrDefault(me => me.BaseComponentName == identifiers[i].BaseComponentName.GetNameWithoutAlias())?.ComponentName;
                    string source = this.joinExpr.GetAliasExprWithId(identifiers[i].BaseComponentName)?.ParamSignature;
                    if (source == null && identifiers[i].BaseComponentName.Split('#').Length == 2) source = identifiers[i].BaseComponentName.Split('#')[0];
                    if (source != null) source += ".";

                    if (this.joinExpr.Operands.ContainsKey("group") && this.joinExpr.Operands["group"].Structure.Components.FirstOrDefault(comp => comp.ComponentName.Replace('#', '.') == $"{source}{identifiers[i].BaseComponentName}") == null)
                    {
                        string src = this.joinExpr.Operands["group"].Structure.Components.FirstOrDefault(comp => comp.BaseComponentName == identifiers[i].BaseComponentName)?.ComponentName;
                        if (src == null)
                        {
                            if (this.joinExpr.Operands["group"].OperatorDefinition.Keyword != "except") skipId = true;
                        }
                        else
                        {
                            if (this.joinExpr.Operands["group"].OperatorDefinition.Keyword == "except") skipId = true;
                            else if (src.Contains("#")) source = $"{src.Split("#")[0]}.";
                        }
                    }

                    if (!skipId)
                    {
                        if (i == 0 && this.ifThenElse) sb.AppendLine(this.RenderIfThenElseIdentifier(identifiers[i]));
                        else
                        {
                            if (this.joinExpr.OperatorDefinition.Keyword != "full")
                            {
                                if (baseName != identifiers[i].ComponentName.GetNameWithoutAlias()) sb.AppendLine($"{source}{baseName} AS {identifiers[i].ComponentName},");
                                else sb.AppendLine($"{source}{baseName},");
                            }
                            else
                            {
                                string[] aliases = this.joinExpr.GetAliasesSignatures(baseName);
                                sb.AppendLine($"CASE WHEN NOT {aliases[0]}.{baseName} IS NULL THEN {aliases[0]}.{baseName} ELSE {aliases[1]}.{baseName} END AS {identifiers[i].ComponentName},");
                            }
                        }
                    }
                }
            }

            this.parts["identifiers"] = sb.ToString();
            return this;
        }

        public IJoinSelectBuilder AddMeasures(JoinMeasuresRenderer applyMeasuresRenderer)
        {
            StringBuilder sb = new StringBuilder();
            StructureComponent[] measures = this.joinExpr.Structure.Measures.ToArray();

            for (int i = 0; i < measures.Length; i++)
            {
                bool isCalc = false;
                if (this.joinExpr.Operands.ContainsKey("calc"))
                {
                    IExpression calcExpr = this.joinExpr.Operands["calc"].GetDescendantExprs("Calc expression").FirstOrDefault(expr => expr.Structure.Components.FirstOrDefault(comp => comp.ComponentName == measures[i].ComponentName) != null);
                    if (calcExpr != null)
                    {
                        sb.Append($"{this._opRendererResolver(calcExpr.OperatorSymbol).Render(calcExpr)}");
                        isCalc = true;
                    }
                }

                if (!isCalc)
                {
                    string baseName = this.joinExpr.BasicStructure.Measures.FirstOrDefault(me => me.BaseComponentName.In(measures[i].BaseComponentName, measures[i].BaseComponentName.GetNameWithoutAlias()))?.ComponentName;
                    string source = this.joinExpr.GetAliasExprWithMeasure(measures[i].BaseComponentName)?.ParamSignature;
                    if (source == null && measures[i].BaseComponentName.Split('#').Length == 2) source = measures[i].BaseComponentName.Split('#')[0];
                    if (source != null) source += ".";

                    if (this.joinExpr.Operands.ContainsKey("apply"))
                        sb.AppendLine(applyMeasuresRenderer(this.joinExpr.Operands["apply"], measures[i], this.joinExpr.Structure.Measures[i]));
                    else
                    {
                        if (baseName.GetNameWithoutAlias() != measures[i].ComponentName.GetNameWithoutAlias())
                            sb.AppendLine($"{source}{baseName.GetNameWithoutAlias()} AS {measures[i].ComponentName.GetNameWithoutAlias()},");
                        else sb.AppendLine($"{source}{baseName.GetNameWithoutAlias()},");
                    }
                }
            }

            this.parts["measures"] = sb.ToString();
            return this;
        }
        
        public IJoinSelectBuilder AddViralAttributes()
        {
            StringBuilder sb = new StringBuilder();

            if (this.joinExpr.Structure.ViralAttributes.Count != 0)
            {
                StructureComponent[] attributes = this.joinExpr.Structure.ViralAttributes.ToArray();
                for (int i = 0; i < attributes.Length; i++)
                {
                    bool isCalc = false;
                    if (this.joinExpr.Operands.ContainsKey("calc"))
                    {
                        IExpression calcExpr = this.joinExpr.Operands["calc"].GetDescendantExprs("Calc expression").FirstOrDefault(expr => expr.Structure.Components.FirstOrDefault(comp => comp.ComponentName == attributes[i].ComponentName) != null);
                        if (calcExpr != null)
                        {
                            sb.Append($"{this._opRendererResolver(calcExpr.OperatorSymbol).Render(calcExpr)}");
                            isCalc = true;
                        }
                    }

                    if (!isCalc)
                    {
                        bool propagate = false;
                        string baseName = this.joinExpr.BasicStructure.ViralAttributes.FirstOrDefault(me => me.BaseComponentName.In(attributes[i].BaseComponentName, attributes[i].BaseComponentName.GetNameWithoutAlias()))?.ComponentName;
                        string source = null;
                        List<string> aliases = this.joinExpr.GetAliasesSignatures(baseName.GetNameWithoutAlias()).ToList();
                        
                        if (this.ifThenElse && this.joinExpr.Operands["apply"].GetDescendantExprs("Membership").Count > 0)
                        {
                            foreach (IExpression membershipExpr in this.joinExpr.Operands["apply"].GetDescendantExprs("Membership"))
                            {
                                aliases.Remove(membershipExpr.OperandsCollection.ToArray()[0].ParamSignature);
                            }

                            if (aliases.Count == 1) source = $"{aliases[0]}.";
                        }

                        if (attributes[i].ComponentName.Contains('#') || aliases.Count == 1)
                        {
                            if (aliases.Count == 1)
                            {
                                if (source == null)
                                {
                                    source = this.joinExpr.GettAliasExprWithViralAttribute(attributes[i].BaseComponentName)?.ParamSignature;
                                    if (source == null && attributes[i].BaseComponentName.Split('#').Length == 2) source = attributes[i].BaseComponentName.Split('#')[0];
                                    if (source != null) source += ".";
                                }

                                if (baseName.GetNameWithoutAlias() != attributes[i].ComponentName.GetNameWithoutAlias())
                                    sb.AppendLine($"{source}{baseName.GetNameWithoutAlias()} AS {attributes[i].ComponentName.GetNameWithoutAlias()},");
                                else sb.AppendLine($"{source}{baseName.GetNameWithoutAlias()},");
                            }
                            else
                            {
                                if (this.joinExpr.Operands.ContainsKey("drop"))
                                    aliases.RemoveAll(alias => alias.In(this.joinExpr.Operands["drop"].Structure.Components.Where(
                                        w => w.ComponentName.GetNameWithoutAlias() == baseName.GetNameWithoutAlias()).Select(s => s.ComponentName.Split("#")[0]).ToArray()));
                                else if (this.joinExpr.Operands.ContainsKey("keep"))
                                    aliases.RemoveAll(alias => !alias.In(this.joinExpr.Operands["keep"].Structure.Components.Where(
                                        w => w.ComponentName.GetNameWithoutAlias() == baseName.GetNameWithoutAlias()).Select(s => s.ComponentName.Split("#")[0]).ToArray()));
                                
                                if (aliases.Count > 1) propagate = true;
                                else if (aliases.Count != 0)
                                {
                                    if (baseName.GetNameWithoutAlias() != attributes[i].ComponentName.GetNameWithoutAlias())
                                        sb.AppendLine($"{aliases[0]}.{baseName.GetNameWithoutAlias()} AS {attributes[i].ComponentName.GetNameWithoutAlias()},");
                                    else sb.AppendLine($"{aliases[0]}.{baseName.GetNameWithoutAlias()},");
                                }
                            }
                        }
                        else propagate = true;

                        if (propagate) sb.AppendLine($"{this._propagationAlgorithm.Propagate(attributes[i], aliases)} AS {attributes[i].ComponentName.GetNameWithoutAlias()},");
                        if (this.joinExpr.Operands.ContainsKey("group"))
                        {
                            // TODO: Support for aggregation with attributes propagation
                            string attributesText = sb.ToString();
                            string[] split = attributesText.Split(",\r\n");
                            string lastAttribute = split[split.Length - 2];

                            int asIndex = lastAttribute.LastIndexOf(" AS");
                            if (asIndex != -1)
                            {
                                lastAttribute = lastAttribute.Remove(asIndex).Replace(",\r\n", string.Empty);
                            }

                            lastAttribute = $"MIN({lastAttribute}) AS {baseName}";
                            split[split.Length - 2] = lastAttribute;

                            for (int j = 0; j < split.Length - 1; j++)
                            {
                                split[j] += ",\r\n";
                            }

                            attributesText = string.Concat(split);
                            sb = new StringBuilder(attributesText);
                        }
                    }
                }

                if (sb.ToString() != string.Empty)
                {
                    sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)); // removement of ",\n" 
                    sb.AppendLine();
                }
            }
            else if (this.parts["measures"] != string.Empty)
            {
                // If there are not viral attributes and measures exist
                this.parts["measures"] = this.parts["measures"].ToString().Remove(this.parts["measures"].Length - 3); // removement of ",\n" from measures"
                sb.AppendLine();
            }
            
            this.parts["attributes"] = sb.ToString();
            return this;
        }

        public IJoinSelectBuilder AddSource()
        {
            StringBuilder sb = new StringBuilder();

            foreach (IExpression alias in this.joinExpr.Operands["ds"].OperandsCollection)
            {
                sb.Append(this.RenderAliasedSource(alias));
                sb.Append(this.RenderSourcesLinker(alias));

                if (alias.ParamSignature != this.joinExpr.Operands["ds"].OperandsCollection.Last().ParamSignature)
                {
                    if (this.ifThenElse && this.joinExpr.Operands["ds"].OperandsCollection.ToList().IndexOf(alias) == this.joinExpr.Operands["ds"].OperandsCollection.Count - 2) 
                        sb.Append("LEFT JOIN ");
                    else if (this.joinExpr.Operands.ContainsKey("apply") &&
                        this.joinExpr.Operands["apply"].OperatorSymbol == "exists_in")
                    {
                        if (this.joinExpr.Operands["apply"].OperatorDefinition.Keyword == "true") sb.Append("INNER JOIN ");
                        else sb.Append("LEFT JOIN ");
                    }
                    else sb.Append($"{this.joinExpr.OperatorDefinition.Keyword.ToUpper()} JOIN ");
                }
            }

            this.parts["source"] = sb.ToString();
            return this;
        }

        public IJoinSelectBuilder AddFilters()
        {
            StringBuilder sb = new StringBuilder();

            if (this.joinExpr.Operands.ContainsKey("filter"))
            {
                sb.AppendLine("WHERE");
                sb.Append(this._opRendererResolver(this.joinExpr.Operands["filter"].OperatorSymbol).Render(this.joinExpr.Operands["filter"]));
            }
            else if (this.joinExpr.Operands.ContainsKey("subspace"))
            {
                sb.AppendLine("WHERE");
                List<IExpression> subFilters = this.joinExpr.Operands["subspace"].OperandsCollection.ToList();
                for (int i = 0; i < subFilters.Count; i++)
                {
                    sb.Append(this._opRendererResolver(subFilters[i].OperatorSymbol).Render(subFilters[i]));
                    if (i != subFilters.Count - 1) sb.Append(" AND ");
                }
            }

            this.parts["filters"] = sb.ToString();
            return this;
        }

        public IJoinSelectBuilder AddGroupingClause()
        {
            StringBuilder sb = new StringBuilder();

            if (this.joinExpr.Operands.ContainsKey("group"))
            {
                sb.AppendLine("GROUP BY");
                if (this.joinExpr.Operands["group"].OperatorDefinition.Keyword != "except")
                {
                    foreach (IExpression identifierExpr in this.joinExpr.Operands["group"].OperandsCollection)
                    {
                        sb.AppendLine($"{this._opRendererResolver(identifierExpr.OperatorSymbol).Render(identifierExpr)},");
                    }
                }
                else
                {
                    foreach (StructureComponent identifier in this.joinExpr.Structure.Identifiers)
                    {
                        StructureComponent idComp = this.joinExpr.Operands["group"].Structure.Identifiers.FirstOrDefault(id => id.BaseComponentName == identifier.BaseComponentName);
                        if (idComp != null)
                        {
                            if (idComp.ComponentName.Contains('#'))
                            {
                                string[] aliases = this.joinExpr.GetAliasesSignatures(idComp.BaseComponentName);
                                string dsName = aliases.FirstOrDefault(alias => alias != idComp.ComponentName.Split('#')[0]);

                                if (aliases.Length > 1) throw new VtlTargetError(this.joinExpr.Operands["group"], $"There are more than 1 alternative data structures with id {idComp.BaseComponentName}");
                                if (dsName != null) sb.AppendLine($"{dsName}.{identifier.BaseComponentName},");
                            }
                        }
                        else sb.AppendLine($"{identifier.BaseComponentName},");
                    }
                }

                sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 3)); // removement of ",\n"
                sb.AppendLine();
            }

            this.parts["group"] = sb.ToString();
            return this;
        }

        public IJoinSelectBuilder AddHavingClause()
        {
            StringBuilder sb = new StringBuilder();

            if (this.joinExpr.Operands.ContainsKey("having"))
            {
                IExpression havingExpr = this.joinExpr.Operands["having"];
                sb.AppendLine("HAVING");
                sb.Append(this._opRendererResolver(havingExpr.OperatorSymbol).Render(havingExpr));
            }

            this.parts["having"] = sb.ToString();
            return this;
        }

        /// <summary>
        /// Renders a TSQL translated code for an aliased source.
        /// </summary>
        /// <param name="alias">The aliased source.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderAliasedSource(IExpression alias)
        {
            StringBuilder sb = new StringBuilder();
            if (alias.OperatorSymbol == "ref")
            {
                // if "ref" operator
                sb.AppendLine($"{this._envMapper.Map(alias.ResultMappedName)} AS {alias.ParamSignature} ");
            }
            else if (alias.OperatorSymbol.In("join", "#"))
            {
                // if "join" operator
                sb.AppendLine($"(");
                sb.Append(this._opRendererResolver(alias.OperatorSymbol).Render(alias));
                sb.AppendLine($") AS {alias.ParamSignature} ");
            }
            else
            {
                // other operators
                sb.AppendLine($"{this._envMapper.Map(this.GetExprSource(alias).ExpressionText)} AS {alias.ParamSignature} ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders a TSQL translated code for a sources linker.
        /// </summary>
        /// <param name="alias">The aliased source.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderSourcesLinker(IExpression alias)
        {
            StringBuilder sb = new StringBuilder();

            if (this.joinExpr.Operands.ContainsKey("using") && alias.ParamSignature != this.joinExpr.Operands["ds"].OperandsCollection.First().ParamSignature)
            {
                // if not the first alias
                sb.AppendLine("ON");
                foreach (IExpression usingExpr in this.joinExpr.Operands["using"].OperandsCollection)
                {
                    IExpression[] matchingDs = this.joinExpr.Operands["ds"].OperandsCollection.Where(op => op.Structure.Identifiers.FirstOrDefault(id => id.BaseComponentName == usingExpr.ParamSignature) != null).ToArray();
                    foreach (IExpression match in matchingDs)
                    {
                        if (alias.ParamSignature == match.ParamSignature)
                        {
                            // if the expression which alias references to
                            break;
                        }

                        if (alias.Structure.Identifiers.FirstOrDefault(id => id.BaseComponentName == usingExpr.ParamSignature) != null)
                        {
                            // if the common using identifier of all alias expressions
                            sb.AppendLine($"{match.ParamSignature}.{usingExpr.ParamSignature} = {alias.ParamSignature}.{usingExpr.ParamSignature} AND"); // matching aliases by id
                            break;
                        }
                    }
                }

                sb = new StringBuilder(sb.ToString().Remove(sb.ToString().Length - 5)); // removement of "AND\n"
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders a TSQL translated code for an "if-then-else" operator identifier.
        /// </summary>
        /// <param name="identifier">The idenfitier.</param>
        /// <returns>The TSQL translated code.</returns>
        private string RenderIfThenElseIdentifier(StructureComponent identifier)
        {
            // Get aliases of every "if-then-else" branch:
            IExpression[] ifExprAliases = this.joinExpr.Operands["ds"].OperandsCollection
                .Where(alias => alias.ParamSignature.In(this.joinExpr.Operands["apply"].Operands["if"].GetDescendantExprs("Alias").Select(a => a.ExpressionText).ToArray())).ToArray();
            IExpression[] thenExprAliases = this.joinExpr.Operands["ds"].OperandsCollection
                .Where(alias => alias.ParamSignature.In(this.joinExpr.Operands["apply"].Operands["then"].GetDescendantExprs("Alias").Select(a => a.ExpressionText).ToArray())).ToArray();
            IExpression[] elseExprAliases = this.joinExpr.Operands["ds"].OperandsCollection
                .Where(alias => alias.ParamSignature.In(this.joinExpr.Operands["apply"].Operands["else"].GetDescendantExprs("Alias").Select(a => a.ExpressionText).ToArray())).ToArray();

            // Get subset alias of every "if-then-else" branch:
            IExpression ifExprAlias = JoinExpression.GetSubsetAlias(ifExprAliases);
            IExpression thenExprAlias = JoinExpression.GetSubsetAlias(thenExprAliases);
            IExpression elseExprAlias = JoinExpression.GetSubsetAlias(elseExprAliases);

            if (thenExprAlias != null && elseExprAlias != null)
            {
                IExpression ifSubExpr = this.joinExpr.Operands["apply"].Operands["if"].Operands["ds_1"];
                string suffix = ifSubExpr.OperatorSymbol.In("ref", "const") ? " = 1" : string.Empty;

                return
                    $"IIF({this._opRendererResolver(ifSubExpr.OperatorSymbol).Render(ifSubExpr, ifExprAlias?.Structure.Measures[0] ?? this.joinExpr.Operands["apply"].Operands["then"].Structure.Measures[0])}{suffix}, " +
                    $"{thenExprAlias.ParamSignature}.{identifier.ComponentName}, " +
                    $"{elseExprAlias.ParamSignature}.{identifier.ComponentName}) AS {identifier.ComponentName},";
            }
            else if (thenExprAlias != null) return $"{thenExprAlias.ParamSignature}.{identifier.ComponentName},";
            else if (elseExprAlias != null) return $"{elseExprAlias.ParamSignature}.{identifier.ComponentName},";
            else return  $"{ifExprAlias.ParamSignature}.{identifier.ComponentName},";
        }

        /// <summary>
        /// Gets a data source expression (get operator) of a given expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>The data source expression.</returns>
        private IExpression GetExprSource(IExpression expression)
        {
            if (expression.OperatorSymbol == "get") return expression;
            foreach (IExpression expr in expression.OperandsCollection)
            {
                IExpression result = this.GetExprSource(expr);
                if (result != null) return result;
            }

            return null;
        }
    }
}
