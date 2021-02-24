namespace StatisticsPoland.VtlProcessing.Core.Tests.MiddleEndTests
{
    using StatisticsPoland.VtlProcessing.Core.Models;
    using System.Linq;
    using Xunit;

    public partial class DeadCodeModifierTests
    {
        [Fact]
        public void Modify_ExprWithDeadCode_ExprWithoutDeadCode()
        {
            this.deadCodeModifier.Modify(this.schema);

            Assert.Equal(2, schema.AssignmentObjects.Count);
            Assert.Equal(
                new AssignmentObject[] { this.schema.AssignmentObjects["C"] },
                this.schema.AssignmentObjects["A"].Users.ToArray());
            Assert.Equal(
                new AssignmentObject[] { },
                this.schema.AssignmentObjects["C"].Users.ToArray());
        }
    }
}
