namespace StatisticsPoland.VtlProcessing.Core.Tests.ModelsTests
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Logical;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class TransformationSchemaTests
    {
        [Fact]
        public void GetExpressions_Expressions()
        {
            TransformationSchema schema = new TransformationSchema();
            
            IExpression expr1 = ModelResolvers.ExprResolver();
            IExpression expr2 = ModelResolvers.ExprResolver();

            AssignmentObject assignmentObject1 = new AssignmentObject(schema, expr1, true, new List<string>());
            AssignmentObject assignmentObject2 = new AssignmentObject(schema, expr2, true, new List<string>());
            
            schema.AssignmentObjects.Add(assignmentObject1);
            schema.AssignmentObjects.Add(assignmentObject2);

            List<IExpression> expected = new List<IExpression>() { expr1, expr2 };

            ICollection<IExpression> result = schema.GetExpressions();

            Assert.Equal(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], result.ToArray()[i]);
            }
        }
    }
}
