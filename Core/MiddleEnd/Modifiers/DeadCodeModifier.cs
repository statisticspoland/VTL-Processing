namespace StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers
{
    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Modifier removing a dead code.
    /// </summary>
    public class DeadCodeModifier : ISchemaModifier
    {
        private ITransformationSchema schema;

        /// <summary>
        /// Performs removing a dead code for a transformation schema.
        /// </summary>
        /// <param name="schema">The transformation schema to modify.</param>
        public void Modify(ITransformationSchema schema)
        {
            this.schema = schema;
            List<string> usedObjects = new List<string>(); // object list used in the schema

            for (byte stage = 1; stage <= 3; stage++)
            {
                if (stage == 2) // stage of removing unused non persistent objects (refs) from the schema
                    this.schema.AssignmentObjects.RemoveAll(ao => !ao.IsPersistentAssignment && !ao.Name.In(usedObjects.ToArray()));
                else
                {
                    foreach (AssignmentObject assignmentObject in this.schema.AssignmentObjects)
                    {
                        if (stage == 1 && assignmentObject.IsPersistentAssignment) // stage of searching used objects by the current object - if the persistent assignment object
                        {
                            usedObjects.Add(assignmentObject.Name); // persistent assignment object addition
                            usedObjects.AddRange(this.SearchRefs(assignmentObject.Expression)); // addition of used object by the current persistent assignment object
                        }
                        else if (stage == 3) // stage of removing unused non persistent objects (refs) from assignment objects users
                        {
                            if (assignmentObject.IsPersistentAssignment) (assignmentObject.Users as List<AssignmentObject>).Clear(); // persistent assignment object doesn't need specified users - it's not a reference
                            else (assignmentObject.Users as List<AssignmentObject>).RemoveAll(user => !user.Name.In(usedObjects.ToArray())); // removement of unused non persistent objects (refs)
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Searches for objects that are used by an expression.
        /// </summary>
        /// <param name="expression">The operand that using objects.</param>
        /// <returns>The list of used objects names.</returns>
        private List<string> SearchRefs(IExpression expression)
        {
            List<string> usedObjects = new List<string>();

            if (expression.OperatorSymbol == "ref" && this.schema.AssignmentObjects.FirstOrDefault(ao => !ao.IsPersistentAssignment && ao.Name == expression.ExpressionText) != null)
            {
                // if a reference object and it calls a persistent assignment object
                usedObjects.Add(expression.ExpressionText); // non persistent assignment object (ref) addition
                usedObjects.AddRange(this.SearchRefs(this.schema.AssignmentObjects[expression.ExpressionText].Expression)); // addition of used object by the current non persistent assignment object (ref)
            }

            foreach (IExpression op in expression.OperandsCollection)
            {
                usedObjects.AddRange(this.SearchRefs(op));
            }

            return usedObjects;
        }
    }
}