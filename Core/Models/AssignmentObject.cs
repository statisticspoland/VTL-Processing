namespace StatisticsPoland.VtlProcessing.Core.Models
{
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// Contains informations of a VTL 2.0 expression assignment object.
    /// </summary>
    public class AssignmentObject
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AssignmentObject"/> class.
        /// </summary>
        /// <param name="schema">The transformation schema the assignment object is assigned to.</param>
        /// <param name="expression">The expression to assign to assignment object.</param>
        /// <param name="isPersistentAssignment">The value indicating whether the assignment is persistent.</param>
        /// <param name="refsNames">The collection of assignment objects names that this instance's expression contains.</param>
        public AssignmentObject(ITransformationSchema schema, IExpression expression, bool isPersistentAssignment, ICollection<string> refsNames)
        {
            this.ContainingSchema = schema;
            this.Name = expression.ResultName;
            this.Expression = expression;
            this.IsPersistentAssignment = isPersistentAssignment;
            this.Users = new List<AssignmentObject>();

            foreach (string userName in refsNames)
            {
                this.ContainingSchema.AssignmentObjects[userName].Users.Add(this);
            }
        }

        /// <summary>
        /// Gets the name of the assignment object.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value indicating whether the assignment is persistent.
        /// </summary>
        public bool IsPersistentAssignment { get; }

        /// <summary>
        /// Gets or sets the expression assigned to the instance.
        /// </summary>
        public IExpression Expression { get; set; }

        /// <summary>
        /// Gets or sets the transformation schema containing this assignment object.
        /// </summary>
        public ITransformationSchema ContainingSchema { get; set; }

        /// <summary>
        /// Gets the collection of assignment objects that uses this instance.
        /// </summary>
        public ICollection<AssignmentObject> Users { get; }
    }
}
