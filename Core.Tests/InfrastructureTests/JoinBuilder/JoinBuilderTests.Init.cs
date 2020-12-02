namespace StatisticsPoland.VtlProcessing.Core.Tests.InfrastructureTests.JoinBuilder
{
    using Moq;
    using StatisticsPoland.VtlProcessing.Core.Infrastructure.JoinBuilder.JoinBranches.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Collections.Generic;

    public partial class JoinBuilderTests
    {
        public IEnumerable<IJoinBranch> joinBranches;

        public JoinBuilderTests()
        {
            Mock<IJoinBranch> joinBranch1Mock = new Mock<IJoinBranch>();
            Mock<IJoinBranch> joinBranch2Mock = new Mock<IJoinBranch>();

            joinBranch1Mock.SetupGet(jb => jb.Signature).Returns("branch1");
            joinBranch2Mock.SetupGet(jb => jb.Signature).Returns("branch2");

            joinBranch1Mock.Setup(jb => jb.Build(It.IsAny<IExpression>())).
                Returns((IExpression datasetExpr) => { return ModelResolvers.ExprResolver(); });

            joinBranch1Mock.Setup(jb => jb.Build(It.IsAny<IExpression>())).
                Returns((IExpression datasetExpr) => { return ModelResolvers.ExprResolver(); });

            this.joinBranches = new List<IJoinBranch>()
            {
                joinBranch1Mock.Object,
                joinBranch2Mock.Object
            };
        }
    }
}
