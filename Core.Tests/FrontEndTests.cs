//namespace StatisticsPoland.VtlProcessing.Core.Tests
//{
//    using Microsoft.Extensions.DependencyInjection;
//    using StatisticsPoland.VtlProcessing.Core.FrontEnd.Interfaces;
//    using StatisticsPoland.VtlProcessing.Core.Infrastructure;
//    using StatisticsPoland.VtlProcessing.Core.Model;
//    using System;
//    using Xunit;

//    /// <summary>
//    /// Klasa testów jednostkowych front-endu.
//    /// </summary>
//    public class FrontEndTests
//    {
//        private ITreeGenerator frontEnd;

//        /// <summary>
//        /// Inicjalizuje now¹ instancjê klasy <see cref="FrontEndTests"/>.
//        /// </summary>
//        public FrontEndTests()
//        {
//            IServiceCollection services = new ServiceCollection().AddVtlProcessing();
//            IServiceProvider provider = services.BuildServiceProvider();
//            this.frontEnd = provider.GetService<ITreeGenerator>();
//        }

//        [Fact]
//        public void BuildTransformationSchema_ConstAssignment_SingleNodeTransformationSchema()
//        {
//            TransformationSchema schema = this.frontEnd.BuildTransformationSchema("A := 1");
//            Assert.Empty(schema.Errors);
//            Assert.True(schema["A"].OperatorSymbol == "const");
//        }

//        [Fact]
//        public void BuildTransformationSchema_AmbiguousMinus_NoErrors()
//        {
//            TransformationSchema schema = this.frontEnd.BuildTransformationSchema("A := 1-1");
//            Assert.Empty(schema.Errors);
//        }

//        [Fact]
//        public void BuildTransformationSchema_Namespace_NoErrors()
//        {
//            TransformationSchema schema = this.frontEnd.BuildTransformationSchema(@"A := NamespaceName\B");
//            Assert.Empty(schema.Errors);
//        }

//        [Fact]
//        public void BuildTransformationSchema_Parentheses_NoErrors()
//        {
//            TransformationSchema schema = this.frontEnd.BuildTransformationSchema("A := (X * 5) + (A - 2);");
//            Assert.Empty(schema.Errors);
//        }

//        [Fact]
//        public void BuildTransformationSchema_Expressions_MatchTree()
//        {
//            string vtlSource =
//                @"A := X + 2;
//                  B <- A * 3 - 4;";
//            TransformationSchema schema = this.frontEnd.BuildTransformationSchema(vtlSource);

//            Assert.Empty(schema.Errors);
//            Assert.True(schema.AssignmentObjects.Count == 2);

//            Assert.True(schema["A"].OperatorSymbol == "+"); // X + 2
//            Assert.True(schema["A"].Operands["ds_1"].OperatorSymbol == "get"); // X
//            Assert.True(schema["A"].Operands["ds_2"].OperatorSymbol == "const"); // 2

//            Assert.True(schema["B"].OperatorSymbol == "-"); // A * 3 - 4
//            Assert.True(schema["B"].Operands["ds_1"].OperatorSymbol == "*"); // A * 3
//            Assert.True(schema["B"].Operands["ds_1"].Operands["ds_1"].OperatorSymbol == "ref"); // A
//            Assert.True(schema["B"].Operands["ds_1"].Operands["ds_2"].OperatorSymbol == "const"); // 3
//            Assert.True(schema["B"].Operands["ds_2"].OperatorSymbol == "const"); // 4
//        }
//    }
//}
