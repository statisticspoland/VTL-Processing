namespace StatisticsPoland.VtlProcessing.Core.Models.Logical
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// The VTL 2.0 transformation schema representation.
    /// </summary>
    public class TransformationSchema : ITransformationSchema
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TransformationSchema"/> class.
        /// </summary>
        public TransformationSchema()
        {
            this.AssignmentObjects = new AssignmentObjectCollection();
            this.UsedOperators = new List<string>();
        }

        public AssignmentObjectCollection AssignmentObjects { get; }

        public ICollection<string> UsedOperators { get; }

        public ICollection<IExpression> GetExpressions()
        {
            List<IExpression> expressions = new List<IExpression>();

            foreach (AssignmentObject assignmentObject in this.AssignmentObjects)
            {
                expressions.Add(assignmentObject.Expression);
            }

            return expressions;
        }
    }
}
