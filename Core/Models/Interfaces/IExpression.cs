namespace StatisticsPoland.VtlProcessing.Core.Models.Interfaces
{
    using StatisticsPoland.VtlProcessing.Core.Operators.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// The single VTL 2.0 expression representation interface.
    /// </summary>
    public interface IExpression
    {
        /// <summary>
        /// Gets or sets the expression defining text.
        /// </summary>
        string ExpressionText { get; set; }

        /// <summary>
        /// Gets or sets the name of the intermediate result of operand.
        /// </summary>
        string ResultName { get; set; }

        /// <summary>
        /// Gets or sets the name on a storage of the result mapped to.
        /// </summary>
        string ResultMappedName { get; set; }

        /// <summary>
        /// Gets or sets the operator symbol.
        /// </summary>
        string OperatorSymbol { get; }

        /// <summary>
        /// Gets or sets the parameter signature.
        /// </summary>
        string ParamSignature { get; set; }

        /// <summary>
        /// Gets the value indicating whether this instance is scalar.
        /// </summary>
        bool IsScalar { get; }

        /// <summary>
        /// Gets the value indicating whether this instance is a component of the "apply" branch of a "join" operator.
        /// </summary>
        bool IsApplyComponent { get; }

        /// <summary>
        /// Gets or sets the expression line number.
        /// </summary>
        int LineNumber { get; set; }

        /// <summary>
        /// Gets or sets the transformation schema containing this expression.
        /// </summary>
        ITransformationSchema ContainingSchema { get; set; }

        /// <summary>
        /// Gets the parent expression of this expression.
        /// </summary>
        IExpression ParentExpression { get; }

        /// <summary>
        /// Gets the expression that this expression is reference to.
        /// </summary>
        IExpression ReferenceExpression { get; set; }

        /// <summary>
        /// Gets or sets the structure.
        /// </summary>
        IDataStructure Structure { get; set; }

        /// <summary>
        /// Gets the operator definition.
        /// </summary>
        IOperatorDefinition OperatorDefinition { get; set; }

        /// <summary>
        /// Gets or sets the operands collection. <br />
        /// The field is assignable, but methods changing the collection instance shall not work.
        /// </summary>
        ICollection<IExpression> OperandsCollection { get; set; }

        /// <summary>
        /// Gets the operands dictionary.
        /// </summary>
        IDictionary<string, IExpression> Operands { get; }

        /// <summary>
        /// Adds a new expression to this instance's operands dictionary.
        /// </summary>
        /// <param name="signature">The signature of expression in the dictionary.</param>
        /// <param name="expression">The expression.</param>
        void AddOperand(string signature, IExpression expression);

        /// <summary>
        /// Sets the containing schema of this expression and its operands.
        /// </summary>
        /// <param name="schema">The schema instance.</param>
        void SetContainingSchema(ITransformationSchema schema);

        /// <summary>
        /// Gets the first ancestor expression.
        /// </summary>
        /// <returns>The first ancestor expression.</returns>
        IExpression GetFirstAncestorExpr();

        /// <summary>
        /// Gets the first ancestor expression with a given result name.
        /// </summary>
        /// <param name="resultName">The result name of the ancestor expression to get.</param>
        /// <returns>The first ancestor expression with a given result name.</returns>
        IExpression GetFirstAncestorExpr(string resultName);

        /// <summary>
        /// Gets the collection of descendant expressions with a given result name.
        /// </summary>
        /// <param name="resultName">The result name of descendant expressions to get.</param>
        /// <returns>The collection of descendant expressions with a given result name.</returns>
        ICollection<IExpression> GetDescendantExprs(string resultName);
    }
}
