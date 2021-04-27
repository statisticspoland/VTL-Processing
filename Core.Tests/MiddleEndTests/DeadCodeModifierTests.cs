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
            this._deadCodeModifier.Modify(this._schema);

            Assert.Equal(2, _schema.AssignmentObjects.Count);
            Assert.Equal(
                new AssignmentObject[] { this._schema.AssignmentObjects["C"] },
                this._schema.AssignmentObjects["A"].Users.ToArray());
            Assert.Equal(
                new AssignmentObject[] { },
                this._schema.AssignmentObjects["C"].Users.ToArray());
        }
    }
}
