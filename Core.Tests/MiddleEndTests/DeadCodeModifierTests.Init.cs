namespace StatisticsPoland.VtlProcessing.Core.Tests.MiddleEndTests
{
    using StatisticsPoland.VtlProcessing.Core.MiddleEnd.Modifiers;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using StatisticsPoland.VtlProcessing.Core.Tests.Utilities;
    using System.Collections.Generic;

    public partial class DeadCodeModifierTests
    {
        private readonly DeadCodeModifier deadCodeModifier;
        private readonly ITransformationSchema schema;

        public DeadCodeModifierTests()
        {
            this.deadCodeModifier = new DeadCodeModifier();

            this.InitSchema(ref this.schema);
        }

        private void InitSchema(ref ITransformationSchema schema)
        {
            IDataStructure structureX = ModelResolvers.DsResolver();
            structureX.DatasetName = "X";

            IDataStructure structureY = ModelResolvers.DsResolver();
            structureY.DatasetName = "Y";

            IDataStructure constStructure = ModelResolvers.DsResolver();
            constStructure.DatasetName = "const";

            IExpression expr1_1 = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
            expr1_1.ParamSignature = "ds";
            expr1_1.ExpressionText = "X";
            expr1_1.Structure = structureX;
            IExpression expr1_2 = TestExprFactory.GetExpression("get", ExpressionFactoryNameTarget.OperatorSymbol);
            expr1_2.ParamSignature = "ds";
            expr1_2.ExpressionText = "Y";
            expr1_2.Structure = structureY;

            IExpression expr2_1 = expr1_1; // X
            IExpression expr2_2 = TestExprFactory.GetExpression("ref", ExpressionFactoryNameTarget.OperatorSymbol); // A
            expr2_2.ParamSignature = "ds";
            expr2_2.ExpressionText = "A";
            expr2_2.Structure = structureY; // struktura Y

            IExpression expr3_1 = expr2_2; // A
            IExpression expr3_2 = TestExprFactory.GetExpression("const", ExpressionFactoryNameTarget.OperatorSymbol); // 2
            expr3_2.ParamSignature = "ds";
            expr3_2.ExpressionText = "2";
            expr3_2.Structure = constStructure; // struktura const

            IExpression expr4_1 = expr1_2; // Y
            IExpression expr4_2 = TestExprFactory.GetExpression("ref", ExpressionFactoryNameTarget.OperatorSymbol); // B
            expr4_2.ParamSignature = "ds";
            expr4_2.ExpressionText = "B";
            expr4_2.Structure = structureX; // struktura X

            IExpression expr1 = TestExprFactory.GetExpression("+", ExpressionFactoryNameTarget.OperatorSymbol); // A := X + Y
            expr1.ResultName = "A";
            expr1.ExpressionText = "X + Y";
            expr1.ParamSignature = "<root>";
            expr1.OperandsCollection = new List<IExpression>() { expr1_1, expr1_2 };
            expr1.Structure = structureY; // struktura Y

            IExpression expr2 = TestExprFactory.GetExpression("-", ExpressionFactoryNameTarget.OperatorSymbol); // B := X - 2
            expr2.ResultName = "B";
            expr2.ExpressionText = "X - A";
            expr2.ParamSignature = "<root>";
            expr2.OperandsCollection = new List<IExpression>() { expr2_1, expr2_2 };
            expr2.Structure = structureX; // struktura X

            IExpression expr3 = TestExprFactory.GetExpression("*", ExpressionFactoryNameTarget.OperatorSymbol); // C <- A * 2
            expr3.ResultName = "C";
            expr3.ExpressionText = "A - 2";
            expr3.ParamSignature = "<root>";
            expr3.OperandsCollection = new List<IExpression>() { expr3_1, expr3_2 };
            expr3.Structure = structureY; // strktura Y

            IExpression expr4 = TestExprFactory.GetExpression("+", ExpressionFactoryNameTarget.OperatorSymbol); // D := Y + B
            expr4.ResultName = "D";
            expr4.ExpressionText = "Y + B";
            expr4.ParamSignature = "<root>";
            expr4.OperandsCollection = new List<IExpression>() { expr4_1, expr4_2 };
            expr4.Structure = structureY; // struktura Y

            schema = ModelResolvers.SchemaResolver();
            schema.AssignmentObjects.Add(new AssignmentObject(schema, expr1, false, new string[0])); // A := X + Y
            schema.AssignmentObjects.Add(new AssignmentObject(schema, expr2, false, new string[] { "A" })); // B := X - 2
            schema.AssignmentObjects.Add(new AssignmentObject(schema, expr3, true, new string[] { "A" })); // C <- A * 2
            schema.AssignmentObjects.Add(new AssignmentObject(schema, expr4, false, new string[] { "B" })); // D := Y + B
        }
    }
}
