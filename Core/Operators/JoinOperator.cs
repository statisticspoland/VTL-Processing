namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.ComponentTools;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The "Join" operator definition.
    /// </summary>
    [OperatorSymbol("join")]
    public class JoinOperator : IOperatorDefinition
    {
        private readonly DataStructureResolver dsResolver;

        /// <summary>
        /// Initialises a new instance of the <see cref="JoinOperator"/> class.
        /// </summary>
        /// <param name="dsResolver">The data structure resolver.</param>
        public JoinOperator(DataStructureResolver dsResolver)
        {
            this.dsResolver = dsResolver;
        }

        public string Name => "Join";

        public string Symbol => "join";

        public string Keyword { get; set; }

        public IDataStructure GetOutputStructure(IExpression expression)
        {
            IJoinExpression joinExpr = (IJoinExpression)expression;
            IDataStructure mergedStructure = this.dsResolver();

            if (!joinExpr.Operands["ds"].Operands.All(ds => ds.Value.Structure.IsSingleComponent == false)) throw new VtlOperatorError(joinExpr, this.Name, "Scalar joinExprs cannot be joined.");

            this.ProcessDsBranch(joinExpr, mergedStructure);
            if (joinExpr.Operands.ContainsKey("apply")) mergedStructure = this.ProcessApplyBranch(joinExpr, mergedStructure);

            if (joinExpr.Operands.ContainsKey("keep") ||
                joinExpr.Operands.ContainsKey("drop")) mergedStructure = this.ProcessKeepDropBranch(joinExpr, mergedStructure);
            if (joinExpr.Operands.ContainsKey("calc")) mergedStructure = this.ProcessCalcBranch(joinExpr, mergedStructure);
            if (joinExpr.Operands.ContainsKey("rename")) mergedStructure = this.ProcessRenameBranch(joinExpr, mergedStructure);
            if (joinExpr.Operands.ContainsKey("filter") &&
                (!joinExpr.Operands["filter"].Structure.IsSingleComponent
                || joinExpr.Operands["filter"].Structure.Components[0].ValueDomain.DataType != BasicDataType.Boolean))
            {
                throw new VtlOperatorError(joinExpr, this.Name, "Expected boolean single component expression as filter branch.");
            }

            return mergedStructure;
        }

        /// <summary>
        /// Precesses a "join" expression's applying operations at a given data structure.
        /// </summary>
        /// <param name="expression">The "join" expression.</param>
        /// <param name="mergedStructure">The data structure.</param>
        private IDataStructure ProcessApplyBranch(IJoinExpression expression, IDataStructure mergedStructure)
        {
            mergedStructure.Measures = expression.Operands["apply"].Structure.GetCopy().Measures;
            return mergedStructure;
        }

        /// <summary>
        /// Precesses a "join" expression's datasets structures merging into a given data structure.
        /// </summary>
        /// <param name="expression">The "join" expression.</param>
        /// <param name="mergedStructure">The data structure.</param>
        private IDataStructure ProcessDsBranch(IJoinExpression expression, IDataStructure mergedStructure)
        {
            IExpression[] aliases = expression.Operands["ds"].OperandsCollection.ToArray();
            for (int i = 0; i < aliases.Length; i++)
            {
                if (i + 1 != aliases.Length)
                {
                    if (!aliases[i].Structure.IsSupersetOf(aliases[i + 1].Structure) && !aliases[i + 1].Structure.IsSupersetOf(aliases[i].Structure))
                        throw new VtlOperatorError(expression, this.Symbol, "Datasets doesn't fit");
                }

                mergedStructure.AddStructure(aliases[i].Structure.GetCopy());
            }

            return mergedStructure;
        }

        /// <summary>
        /// Precesses a "join" expression's component calculating operations at a given data structure.
        /// </summary>
        /// <param name="expression">The "join" expression.</param>
        /// <param name="mergedStructure">The data structure.</param>
        private IDataStructure ProcessCalcBranch(IJoinExpression expression, IDataStructure mergedStructure)
        {
            mergedStructure.AddStructure(expression.Operands["calc"].Structure.GetCopy());
            return mergedStructure;
        }

        /// <summary>
        /// Precesses a "join" expression's keeping/dropping operations at a given data structure.
        /// </summary>
        /// <param name="expression">The "join" expression.</param>
        /// <param name="mergedStructure">The data structure.</param>
        private IDataStructure ProcessKeepDropBranch(IJoinExpression expression, IDataStructure mergedStructure)
        {
            bool isKeep = expression.Operands.ContainsKey("keep");
            IExpression removerExpr = isKeep ? expression.Operands["keep"] : expression.Operands["drop"];
            string symbol = removerExpr.OperatorSymbol;
            IDataStructure removerStructure = removerExpr.Structure.GetCopy();

            if (symbol == "drop")
            {
                List<string> dropped = new List<string>();
                while (removerStructure.Components.Count > 0)
                {
                    StructureComponent removerComp = removerStructure.Components[0];
                    StructureComponent component = mergedStructure.Components.FirstOrDefault(comp => comp.BaseComponentName.GetNameWithoutAlias() == removerComp.BaseComponentName.GetNameWithoutAlias());

                    if (component == null) throw new VtlOperatorError(expression, this.Name, $"Could not {symbol} component {removerComp.ComponentName}");

                    component.BaseComponentName = removerComp.BaseComponentName;
                    component.ComponentName = removerComp.ComponentName;

                    dropped.Add(component.ComponentName);

                    string[] freeAliases = expression.GetAliasesSignatures(component.BaseComponentName.GetNameWithoutAlias()).Where(
                            signature => dropped.FirstOrDefault(d => d.Contains(component.BaseComponentName.GetNameWithoutAlias()) &&
                                (d.Contains($"{signature}#") || !d.Contains('#'))) == null).ToArray();
                    string[] newNames = null;
                    if (freeAliases != null)
                    {
                        newNames = new string[freeAliases.Length];
                        for (int i = 0; i < freeAliases.Length; i++)
                        {
                            if (component.BaseComponentName.Split('#').Length == 1) newNames[i] = $"{freeAliases[i]}#{component.BaseComponentName}";
                            else newNames[i] = $"{freeAliases[i]}#{component.BaseComponentName.Split('#')[1]}";
                        }
                    }

                    switch (component.ComponentType)
                    {
                        case ComponentType.Identifier: throw new VtlOperatorError(expression, this.Name, $"Unexpected Identifier component.");
                        case ComponentType.Measure:
                            mergedStructure.Measures.Remove(component);
                            if (newNames != null)
                            {
                                foreach (string newName in newNames)
                                {
                                    mergedStructure.Measures.Add(new StructureComponent(component.ValueDomain.DataType, newName));
                                }
                            }

                            break;
                        case ComponentType.ViralAttribute:
                            mergedStructure.ViralAttributes.Remove(component);
                            if (newNames != null)
                            {
                                foreach (string newName in newNames)
                                {
                                    mergedStructure.ViralAttributes.Add(new StructureComponent(component.ValueDomain.DataType, newName));
                                }
                            }

                            break;
                        case ComponentType.NonViralAttribute:
                            mergedStructure.NonViralAttributes.Remove(component);
                            if (newNames != null)
                            {
                                foreach (string newName in newNames)
                                {
                                    mergedStructure.NonViralAttributes.Add(new StructureComponent(component.ValueDomain.DataType, newName));
                                }
                            }

                            break;
                        default: throw new VtlOperatorError(expression, this.Name, $"Unknown component type: {component.ComponentType}.");
                    }

                    switch (removerComp.ComponentType)
                    {
                        case ComponentType.Identifier: throw new VtlOperatorError(expression, this.Name, $"Unexpected identifier component.");
                        case ComponentType.Measure: removerStructure.Measures.Remove(removerComp); break;
                        case ComponentType.ViralAttribute: removerStructure.ViralAttributes.Remove(removerComp); break;
                        case ComponentType.NonViralAttribute: removerStructure.NonViralAttributes.Remove(removerComp); break;
                        default: throw new VtlOperatorError(expression, this.Name, $"Unknown component type: {removerComp.ComponentType}.");
                    }
                }
            }
            else
            {
                mergedStructure.Measures = removerStructure.Measures;
                mergedStructure.ViralAttributes = removerStructure.ViralAttributes;
                mergedStructure.NonViralAttributes = removerStructure.NonViralAttributes;
            }

            return mergedStructure;
        }

        /// <summary>
        /// Precesses a "join" expression's renaming operations at a given data structure.
        /// </summary>
        /// <param name="expression">The "join" expression.</param>
        /// <param name="mergedStructure">The data structure.</param>
        private IDataStructure ProcessRenameBranch(IJoinExpression expression, IDataStructure mergedStructure)
        {
            IDataStructure renameStructure = expression.Operands["rename"].Structure.GetCopy();
            while (renameStructure.Components.Count > 0)
            {
                bool renamed = false;
                foreach (StructureComponent component in mergedStructure.Components)
                {
                    bool divideComp = false;
                    string baseName = component.BaseComponentName;
                    StructureComponent renameComp = renameStructure.Components.FirstOrDefault(comp => comp.BaseComponentName == baseName);

                    if (renameComp == null)
                    {
                        divideComp = true;
                        baseName = baseName.GetNameWithoutAlias();
                        string[] aliases = expression.GetAliasesSignatures(baseName);

                        foreach (string alias in aliases)
                        {
                            string name = $"{alias}#{baseName}";
                            renameComp = renameStructure.Components.FirstOrDefault(comp => comp.BaseComponentName == name);
                            if (renameComp != null)
                            {
                                baseName = name;
                                break;
                            }
                        }
                    }

                    if (renameComp != null)
                    {
                        string[] freeAliases = null;
                        string[] newNames = null;

                        if (divideComp)
                        {
                            freeAliases = expression.GetAliasesSignatures(baseName.GetNameWithoutAlias())
                                .Where(alias => alias != baseName.Split('#')[0]).ToArray();
                            if (freeAliases != null)
                            {
                                newNames = new string[freeAliases.Length];
                                for (int i = 0; i < freeAliases.Length; i++)
                                {
                                    newNames[i] = $"{freeAliases[i]}#{component.BaseComponentName.GetNameWithoutAlias()}";
                                }
                            }
                        }

                        switch (renameComp.ComponentType)
                        {
                            case ComponentType.Identifier:
                                mergedStructure.Identifiers.Remove(mergedStructure.Identifiers.FirstOrDefault(id => id.BaseComponentName == renameComp.BaseComponentName));
                                renameStructure.Identifiers.Remove(renameComp);
                                if (newNames != null)
                                {
                                    foreach (string newName in newNames)
                                    {
                                        mergedStructure.Identifiers.Add(new StructureComponent(component.ValueDomain.DataType, newName));
                                    }
                                }

                                break;
                            case ComponentType.Measure:
                                mergedStructure.Measures.Remove(mergedStructure.Measures.FirstOrDefault(me => me.BaseComponentName == renameComp.BaseComponentName));
                                renameStructure.Measures.Remove(renameComp);
                                if (newNames != null)
                                {
                                    foreach (string newName in newNames)
                                    {
                                        mergedStructure.Measures.Add(new StructureComponent(component.ValueDomain.DataType, newName));
                                    }
                                }

                                break;
                            case ComponentType.ViralAttribute:
                                mergedStructure.ViralAttributes.Remove(mergedStructure.ViralAttributes.FirstOrDefault(at => at.BaseComponentName == renameComp.BaseComponentName));
                                renameStructure.ViralAttributes.Remove(renameComp);
                                if (newNames != null)
                                {
                                    foreach (string newName in newNames)
                                    {
                                        mergedStructure.ViralAttributes.Add(new StructureComponent(component.ValueDomain.DataType, newName));
                                    }
                                }

                                break;
                            case ComponentType.NonViralAttribute:
                                mergedStructure.NonViralAttributes.Remove(mergedStructure.NonViralAttributes.FirstOrDefault(at => at.BaseComponentName == renameComp.BaseComponentName));
                                renameStructure.NonViralAttributes.Remove(renameComp);
                                if (newNames != null)
                                {
                                    foreach (string newName in newNames)
                                    {
                                        mergedStructure.NonViralAttributes.Add(new StructureComponent(component.ValueDomain.DataType, newName));
                                    }
                                }

                                break;
                            default: throw new VtlOperatorError(expression, this.Name, $"Unknown component type: {component.ComponentType}.");
                        }

                        component.BaseComponentName = renameComp.BaseComponentName;
                        component.ComponentName = renameComp.ComponentName;

                        renamed = true;
                        break;
                    }
                }

                if (!renamed)
                    throw new VtlOperatorError(expression, this.Name, $"Could not rename one of given components. Have any of them been dropped?");
            }

            return mergedStructure;
        }
    }
}

