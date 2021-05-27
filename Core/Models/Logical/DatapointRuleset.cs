namespace StatisticsPoland.VtlProcessing.Core.Models.Logical
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The VTL 2.0 datapoint ruleset representation.
    /// </summary>
    public class DatapointRuleset : IRuleset
    {
        private readonly DataStructureResolver _dsResolver;
        private string rulesetText;

        /// <summary>
        /// Initialises a new instance of the <see cref="DatapointRuleset"/> class.
        /// </summary>
        /// <param name="name">The name of the ruleset.</param>
        /// <param name="rulesetText">The text of the ruleset.</param>
        /// <param name="dsResolver">The data structure resolver.</param>
        public DatapointRuleset(string name, string rulesetText, DataStructureResolver dsResolver)
        {
            this._dsResolver = dsResolver;
            this.Name = name;
            this.RulesetText = rulesetText;
            this.Variables = new Dictionary<string, string>();
            this.ValueDomains = new Dictionary<string, ValueDomain>();
            this.Rules = new Dictionary<string, IRuleExpression>();
        }

        public string Name { get; set; }

        public string RulesetText 
        {
            get
            {
                return this.rulesetText;
            }
            set
            {
                this.rulesetText = value;
                this.rulesetText = this.rulesetText?.Replace(Environment.NewLine, " ");
                while (this.rulesetText?.StartsWith(' ') == true)
                {
                    this.rulesetText = this.rulesetText.Remove(0, 1);
                }
            }
        }

        public Dictionary<string, string> Variables { get; set; }

        public Dictionary<string, ValueDomain> ValueDomains { get; set; }

        public Dictionary<string, IRuleExpression> Rules { get; set; }

        public ICollection<IRuleExpression> RulesCollection
        {
            get
            {
                return this.Rules.Values;
            }

            set
            {
                this.Rules.Clear();

                foreach (IRuleExpression expr in value)
                {
                    if (this.Rules.ContainsKey(expr.ResultName)) throw new ArgumentException("Trying to add a rule which name exists in the rules collection.");
                    this.Rules.Add(expr.ResultName, expr);
                }
            }
        }

        public IDataStructure Structure { get; set; }

        public void InferStructure()
        {
            this.Structure = this._dsResolver();
            if (this.Variables.Count > 0)
            {
                foreach (string variableName in this.Variables.Keys)
                {
                    IEnumerable<StructureComponent> comps = this.RulesCollection.SelectMany(rule => this.GetComponentsFromDescendantExprs(rule, variableName));
                    BasicDataType dataType = comps.FirstOrDefault(comp => comp.ValueDomain.DataType != BasicDataType.None)?.ValueDomain.DataType ?? BasicDataType.None;
                    foreach (StructureComponent comp in comps)
                    {
                        if (comp.ValueDomain.DataType != dataType)
                        {
                            if (dataType == BasicDataType.Integer && comp.ValueDomain.DataType == BasicDataType.Number) comp.ValueDomain = new ValueDomain(dataType);
                            else if (comp.ValueDomain.DataType == BasicDataType.None) comp.ValueDomain = new ValueDomain(dataType);
                            else if (dataType == BasicDataType.None) dataType = comp.ValueDomain.DataType;
                            else throw new InvalidOperationException($"Something went wrong during a types inference of ruleset {this.Name}.");
                        }
                    }

                    this.Structure.Measures.Add(comps.First());
                }
            }
            else
            {
                foreach (string valueDomainName in this.ValueDomains.Keys)
                {
                    IEnumerable<StructureComponent> comps = this.RulesCollection.SelectMany(rule => this.GetComponentsFromDescendantExprs(rule, valueDomainName));
                    this.Structure.Measures.Add(comps.First());
                }
            }
        }

        /// <summary>
        /// Gets the collection of components from a given expression's descendant expressions.
        /// </summary>
        /// <param name="expr">The expression.</param>
        /// <param name="compName">The component name.</param>
        /// <returns>The collection of components.</returns>
        private ICollection<StructureComponent> GetComponentsFromDescendantExprs(IExpression expr, string compName)
        {
            List<StructureComponent> components = new List<StructureComponent>();

            foreach (IExpression operand in expr.OperandsCollection)
            {
                components.AddRange(this.GetComponentsFromDescendantExprs(operand, compName));
            }

            if (expr.OperatorSymbol == "comp" && expr.ExpressionText == compName)
                components.Add(expr.Structure.Components[0]);

            return components;
        }
    }
}
