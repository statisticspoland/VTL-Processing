namespace StatisticsPoland.VtlProcessing.Core.Tests.ModelsTests
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Collections.Generic;

    public partial class DatapointRulesetTests
    {
        private DatapointRuleset GetVariableRuleset()
        {
            DatapointRuleset ruleset = new DatapointRuleset("ruleset", string.Empty, ModelResolvers.DsResolver);
            ruleset.Variables.Add("x", "var1");
            ruleset.Variables.Add("y", "var2");
            ruleset.Variables.Add("z", "var3");
            ruleset.Variables.Add("var4", "var4");
            ruleset.RulesCollection = this.GetRuleExpressions(ruleset);

            return ruleset;
        }

        private DatapointRuleset GetValueDomainRuleset()
        {
            DatapointRuleset ruleset = new DatapointRuleset("ruleset", string.Empty, ModelResolvers.DsResolver);
            ruleset.ValueDomains.Add("x", new ValueDomain(BasicDataType.Integer));
            ruleset.ValueDomains.Add("y", new ValueDomain(BasicDataType.Integer));
            ruleset.ValueDomains.Add("z", new ValueDomain(BasicDataType.String));
            ruleset.ValueDomains.Add("var4", new ValueDomain(BasicDataType.None));
            ruleset.RulesCollection = this.GetRuleExpressions(ruleset);

            return ruleset;
        }

        private ICollection<IRuleExpression> GetRuleExpressions(IRuleset ruleset)
        {
            IRuleExpression expr1_1 = ModelResolvers.RuleExprResolver(TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol), ruleset);
            IRuleExpression expr1_2 = ModelResolvers.RuleExprResolver(TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol), ruleset);
            IRuleExpression expr2_1 = ModelResolvers.RuleExprResolver(TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol), ruleset);
            IRuleExpression expr2_2 = ModelResolvers.RuleExprResolver(TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol), ruleset);
            IRuleExpression expr3_1 = ModelResolvers.RuleExprResolver(TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol), ruleset);
            IRuleExpression expr3_2 = ModelResolvers.RuleExprResolver(TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol), ruleset);
            IRuleExpression expr4_1 = ModelResolvers.RuleExprResolver(TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol), ruleset);
            IRuleExpression expr4_2 = ModelResolvers.RuleExprResolver(TestExprFactory.GetExpression("comp", ExpressionFactoryNameTarget.OperatorSymbol), ruleset);

            expr1_1.Structure = ModelResolvers.DsResolver("x", ComponentType.Measure, BasicDataType.Integer);
            expr1_2.Structure = ModelResolvers.DsResolver("x", ComponentType.Measure, BasicDataType.Integer);
            expr2_1.Structure = ModelResolvers.DsResolver("y", ComponentType.Measure, BasicDataType.Integer);
            expr2_2.Structure = ModelResolvers.DsResolver("y", ComponentType.Measure, BasicDataType.Number);
            expr3_1.Structure = ModelResolvers.DsResolver("z", ComponentType.Measure, BasicDataType.String);
            expr3_2.Structure = ModelResolvers.DsResolver("z", ComponentType.Measure, BasicDataType.None);
            expr4_2.Structure = ModelResolvers.DsResolver("var4", ComponentType.Measure, BasicDataType.None);
            expr4_1.Structure = ModelResolvers.DsResolver("var4", ComponentType.Measure, BasicDataType.None);

            expr1_1.ExpressionText = "x";
            expr1_2.ExpressionText = "x";
            expr2_1.ExpressionText = "y";
            expr2_2.ExpressionText = "y";
            expr3_1.ExpressionText = "z";
            expr3_2.ExpressionText = "z";
            expr4_2.ExpressionText = "var4";
            expr4_1.ExpressionText = "var4";

            expr1_1.ResultName += "1_1";
            expr1_2.ResultName += "1_2";
            expr2_1.ResultName += "2_1";
            expr2_2.ResultName += "2_2";
            expr3_1.ResultName += "3_1";
            expr3_2.ResultName += "3_2";
            expr4_2.ResultName += "4_1";
            expr4_1.ResultName += "4_2";

            List<IRuleExpression> ruleExprs = new List<IRuleExpression>();
            ruleExprs.Add(expr1_1);
            ruleExprs.Add(expr1_2);
            ruleExprs.Add(expr2_1);
            ruleExprs.Add(expr2_2);
            ruleExprs.Add(expr3_1);
            ruleExprs.Add(expr3_2);
            ruleExprs.Add(expr4_1);
            ruleExprs.Add(expr4_2);

            return ruleExprs;
        }
    }
}
