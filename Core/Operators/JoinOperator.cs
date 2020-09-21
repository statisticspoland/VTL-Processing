namespace StatisticsPoland.VtlProcessing.Core.Operators
{
    using StatisticsPoland.VtlProcessing.Core.ErrorHandling;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.Attributes;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.DependencyInjection;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System;
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Precesses a "join" expression's keeping/dropping operations at a given data structure.
        /// </summary>
        /// <param name="expression">The "join" expression.</param>
        /// <param name="mergedStructure">The data structure.</param>
        private IDataStructure ProcessKeepDropBranch(IJoinExpression expression, IDataStructure mergedStructure)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Precesses a "join" expression's renaming operations at a given data structure.
        /// </summary>
        /// <param name="expression">The "join" expression.</param>
        /// <param name="mergedStructure">The data structure.</param>
        private IDataStructure ProcessRenameBranch(IJoinExpression expression, IDataStructure mergedStructure)
        {
            throw new NotImplementedException();
        }
    }
}

