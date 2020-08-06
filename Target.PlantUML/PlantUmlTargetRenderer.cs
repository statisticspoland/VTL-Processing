namespace Target.PlantUML
{
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Target.PlantUML.Infrastructure.Interfaces;

    /// <summary>
    /// The PlantUML target renderer for the VTL 2.0 translation.
    /// </summary>
    public class PlantUmlTargetRenderer : ITargetRenderer
    {
        private readonly ITargetConfiguration conf;
        private readonly int fontSize;

        /// <summary>
        /// Initializes new instance of the <see cref="PlantUmlTargetRenderer"/> class.
        /// </summary>
        /// <param name="configuration">The target configuration.</param>
        public PlantUmlTargetRenderer(ITargetConfiguration configuration)
        {
            this.conf = configuration;
            this.fontSize = 12;
        }
        
        public string Name => "PlantUML";

        public string Render(ITransformationSchema schema)
        {
            return this.Render(this.RenderTransformationSchema(schema.GetExpressions()));
        }

        public string Render(IExpression expression)
        {
            return this.Render(this.RenderExpression(expression, "expr"));
        }

        /// <summary>
        /// Renders a completed PlantUML code based on a given partial PlantUML code.
        /// </summary>
        /// <param name="source">The partial PlantUML code.</param>
        /// <returns>The completed PlantUML code.</returns>
        private string Render(string source)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("@startuml");
            sb.Append(this.RenderSkinparam());
            sb.Append(source);
            sb.AppendLine("@enduml");

            return sb.ToString();
        }

        /// <summary>
        /// Renders the PlantUML code based on a given VTL 2.0 expressions collection.
        /// </summary>
        /// <param name="exprs">The VTL 2.0 expressions collection.</param>
        /// <returns>The PlantUML code.</returns>
        private string RenderTransformationSchema(IEnumerable<IExpression> exprs)
        {
            StringBuilder sb = new StringBuilder();
            string objectName = "expr";

            sb.Append(this.RenderSchemaObject("Expressions"));

            for (int i = 0; i < exprs.Count(); i++)
            {
                sb.Append(this.RenderExpression(exprs.ToArray()[i], $"{objectName}{i + 1}"));
            }

            for (int i = 0; i < exprs.Count(); i++)
            {
                sb.AppendLine($"shema {this.conf.Arrow} {objectName}{i + 1}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders PlantUML code based on a given VTL 2.0 expression.
        /// </summary>
        /// <param name="expr">The VTL 2.0 expression.</param>
        /// <param name="name">The name of PlantUML object to render.</param>
        /// <param name="deep">The left margin of PlantUML object in the rendered code.</param>
        /// <returns>The PlantUML code.</returns>
        private string RenderExpression(IExpression expr, string name, int deep = 0)
        {
            StringBuilder sb = new StringBuilder();
            string space = string.Empty;

            for (int i = 0; i < deep * 6; i++)
            {
                space += " ";
            }

            sb.Append(this.RenderObject(expr, name, space));

            if (expr.OperandsCollection.Count > 0)
            {
                for (int i = 0; i < expr.OperandsCollection.Count; i++)
                {
                    string childName = $"{name}{deep + 1}_{i}";
                    sb.AppendLine(this.RenderExpression(expr.OperandsCollection.ToArray()[i], childName, deep + 1));
                    sb.AppendLine($"{space}{name} {this.conf.Arrow} {childName}");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders a VTL 2.0 transformation schema object by the PlantUML code representation.
        /// </summary>
        /// <param name="objectsName">The name of rendered schema objects..</param>
        /// <returns>The PlantUML code.</returns>
        private string RenderSchemaObject(string objectsName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("object shema {");
            sb.AppendLine($"  <size:{this.fontSize}> {objectsName}");
            sb.AppendLine("}"); ;

            return sb.ToString();
        }

        /// <summary>
        /// Renders PlantUML code object based on a given VTL 2.0 expression.
        /// </summary>
        /// <param name="expr">The VTL 2.0 expression.</param>
        /// <param name="name">The name of PlantUML object to render.</param>
        /// <param name="space">The left margin of PlantUML object in the rendered code.</param>
        /// <returns>The PlantUML code.</returns>
        private string RenderObject(IExpression expr, string name, string space)
        {
            StringBuilder sb = new StringBuilder();
            
            string size = $"<size:{(int)(this.fontSize * 0.9)}>";
            string objectName = expr.ExpressionText;
            
            if (objectName == null || objectName == string.Empty)
            {
                objectName = " ";
            }

            string color = null;
            if (expr.ResultName == null || objectName != " ") color = "#PHYSICAL";
            if (objectName == " ") color = "#LightPink";

            sb.AppendLine();
            sb.AppendLine($"{space}object \"{this.ReplaceQuotationMarks(objectName)}\" as {name} {color}{"{"}");
            sb.AppendLine($"{space}  {size}ResultName = '{expr.ResultName}'");
            sb.AppendLine($"{space}  {size}OperatorSymbol = '{expr.OperatorSymbol}'");
            if(expr.OperatorDefinition?.Keyword != null) sb.AppendLine($"{space}  {size}OperatorKeyword = '{expr.OperatorDefinition.Keyword}'");
            sb.AppendLine($"{space}  {size}IsScalar = {expr.IsScalar}");
            if(this.conf.ShowNumberLine) sb.AppendLine($"{space}  {size}LineNumber = {expr.LineNumber}");
            sb.AppendLine($"{space}  {size}ParamSignature = '{expr.ParamSignature}'");
            sb.AppendLine($"{space}{"}"}");

            return sb.ToString();
        }

        /// <summary>
        /// Renders the PlantUML code with the skinparam configuration.
        /// </summary>
        /// <returns>The PlantUML code.</returns>
        private string RenderSkinparam()
        {
            StringBuilder sb = new StringBuilder();

            if (this.conf.UseHorizontalView) sb.AppendLine("left to right direction");

            sb.AppendLine("skinparam object {");
            sb.AppendLine("  FontSize 15");
            sb.AppendLine("  BackgroundColor PaleGreen");
            sb.AppendLine("  ArrowColor SlateGray");
            sb.AppendLine("  BorderColor SlateGray");
            sb.AppendLine("}");

            return sb.ToString();
        }

        /// <summary>
        /// Replaces quotation marks to an understandable form by the PlantUML environment.
        /// </summary>
        /// <param name="str">The characters string to replace quotation marks inside of it.</param>
        /// <returns>The PlantUML code.</returns>
        private string ReplaceQuotationMarks(string str)
        {
            return str.Replace("\"", "''");
        }
    }
}
