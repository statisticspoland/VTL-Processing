namespace StatisticsPoland.VtlProcessing.Target.PlantUML
{
    using Infrastructure;
    using Infrastructure.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.BackEnd;
    using StatisticsPoland.VtlProcessing.Core.Models;
    using StatisticsPoland.VtlProcessing.Core.Models.Interfaces;
    using StatisticsPoland.VtlProcessing.Core.Models.Types;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The PlantUML target renderer for the VTL 2.0 translation.
    /// </summary>
    public class PlantUmlTargetRenderer : ITargetRenderer
    {
        private readonly ITargetConfiguration _conf;
        private readonly int _fontSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantUmlTargetRenderer"/> class.
        /// </summary>
        /// <param name="configuration">The target configuration.</param>
        public PlantUmlTargetRenderer(ITargetConfiguration configuration)
        {
            this._conf = configuration;
            this._fontSize = 12;
        }
        
        public string Name => "PlantUML";

        public string Render(ITransformationSchema schema)
        {
            if (this._conf.ExprType == ExpressionsType.Standard)
                return this.Render(this.RenderTransformationSchema(schema.GetExpressions()));
            return this.Render(this.RenderTransformationSchema(schema.Rulesets));
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
                sb.AppendLine($"shema {this._conf.Arrow} {objectName}{i + 1}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders the PlantUML code based on a given VTL 2.0 rulesets collection.
        /// </summary>
        /// <param name="rulesets">The VTL 2.0 rulesets collection.</param>
        /// <returns>The PlantUML code.</returns>
        private string RenderTransformationSchema(IEnumerable<IRuleset> rulesets)
        {
            StringBuilder sb = new StringBuilder();
            string objectName = "ruleset";

            sb.Append(this.RenderSchemaObject("Rulesets"));

            for (int i = 0; i < rulesets.Count(); i++)
            {
                sb.Append(this.RenderRuleset(rulesets.ToArray()[i], $"{objectName}{i + 1}"));
            }

            for (int i = 0; i < rulesets.Count(); i++)
            {
                sb.AppendLine($"shema {this._conf.Arrow} {objectName}{i + 1}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders the PlantUML code based on a given VTL 2.0 ruleset.
        /// </summary>
        /// <param name="ruleset">The VTL 2.0 ruleset.</param>
        /// <param name="name">The name of PlantUML object to render.</param>
        /// <returns>The PlantUML code.</returns>
        private string RenderRuleset(IRuleset ruleset, string name)
        {
            StringBuilder sb = new StringBuilder();
            
            string size = $"<size:{(int)(this._fontSize * 0.9)}>";
            string objectName = ruleset.RulesetText;

            sb.AppendLine();
            sb.AppendLine($"object \"{this.ReplaceQuotationMarks(objectName)}\" as {name} #Bisque{"{"}");
            sb.AppendLine($"  {size}Name = '{ruleset.Name}'");
            sb.AppendLine("}");

            IEnumerable<IRuleExpression> exprs = ruleset.RulesCollection;
            string ruleName = "rule";

            for (int i = 0; i < exprs.Count(); i++)
            {
                sb.Append(this.RenderExpression(exprs.ToArray()[i], $"{name}_{ruleName}{i + 1}"));
            }

            for (int i = 0; i < exprs.Count(); i++)
            {
                sb.AppendLine($"{name} {this._conf.Arrow} {name}_{ruleName}{i + 1}");
            }

            if (ruleset.Structure != null && this._conf.ShowDataStructure)
            {
                sb.AppendLine();
                sb.Append(RenderDataStructureObject(ruleset.Structure, $"{name}_ds", name, string.Empty));
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
                    sb.AppendLine($"{space}{name} {this._conf.Arrow} {childName}");
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
            sb.AppendLine($"  <size:{this._fontSize}> {objectsName}");
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
            IRuleExpression ruleExpr = (this._conf.ExprType == ExpressionsType.Rulesets && expr.ParentExpression == null) ? (IRuleExpression)expr : null;
            
            string size = $"<size:{(int)(this._fontSize * 0.9)}>";
            string objectName = expr.ExpressionText;
            
            if (objectName == null || objectName == string.Empty)
            {
                objectName = " ";
            }

            string color = null;
            if (expr.OperatorSymbol == "join") color = "#Moccasin";
            else if (expr.ParentExpression?.OperatorSymbol == "join") color = "#PapayaWhip";
            else if (expr.ResultName == null || objectName != " ") color = "#PHYSICAL";
            
            if (ruleExpr != null) color = "#BlanchedAlmond";
            if (objectName == " ") color = "#LightPink";

            sb.AppendLine();
            sb.AppendLine($"{space}object \"{this.ReplaceQuotationMarks(objectName)}\" as {name} {color}{"{"}");
            sb.AppendLine($"{space}  {size}ResultName = '{expr.ResultName}'");
            sb.AppendLine($"{space}  {size}OperatorSymbol = '{expr.OperatorSymbol}'");
            if(expr.OperatorDefinition?.Keyword != null) sb.AppendLine($"{space}  {size}OperatorKeyword = '{expr.OperatorDefinition.Keyword}'");
            sb.AppendLine($"{space}  {size}IsScalar = {expr.IsScalar}");
            if(this._conf.ShowNumberLine) sb.AppendLine($"{space}  {size}LineNumber = {expr.LineNumber}");
            sb.AppendLine($"{space}  {size}ParamSignature = '{expr.ParamSignature}'");

            if (ruleExpr != null)
            {
                if (ruleExpr.ErrorCode != null) sb.AppendLine($"{space}  {size}errorCode = '{ruleExpr.ErrorCode}'");
                if (ruleExpr.ErrorLevel != null) sb.AppendLine($"{space}  {size}errorLevel = {ruleExpr.ErrorLevel}");
            }

            sb.AppendLine($"{space}{"}"}");

            if (expr.Structure != null && this._conf.ShowDataStructure)
            {
                sb.AppendLine();
                sb.Append(RenderDataStructureObject(expr.Structure, $"{name}_ds", name, space));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders PlantUML code based on a given VTL 2.0 data structure.
        /// </summary>
        /// <param name="structure">The VTL 2.0 data structure.</param>
        /// <param name="name">The name of PlantUML object to render.</param>
        /// <param name="parentName">The name of PlantUML object's parent to render.</param>
        /// <param name="space">The left margin of PlantUML object in the rendered code.</param>
        /// <returns>The PlantUML code.</returns>
        private string RenderDataStructureObject(IDataStructure structure, string name, string parentName, string space)
        {
            StringBuilder sb = new StringBuilder();
            
            string color = structure.Identifiers.Count == 0 ? "#PowderBlue" : "#LightCyan";
            color = structure.IsSingleComponent ? "#PaleTurquoise" : color;

            if (structure.DatasetType == DatasetType.Pivoted) color = structure.IsSingleComponent ? "#Thistle" : "#Lavender";

            sb.AppendLine($"{space}object \"Data Structure {structure.DatasetName}\" as {name} {color}{"{"}");
            if (structure.Identifiers.Count != 0)
            {
                sb.AppendLine($"{space}  <size:{(int)(this._fontSize * 0.9)}>Identifiers:");
                sb.Append(this.RenderDataStructureElements(structure.Identifiers, space));
            }

            if (structure.Measures.Count != 0)
            {
                sb.AppendLine($"{space}  <size:{(int)(this._fontSize * 0.9)}>Measures:");
                sb.Append(this.RenderDataStructureElements(structure.Measures, space));
            }

            if (structure.NonViralAttributes.Count != 0)
            {
                sb.AppendLine($"{space}  <size:{(int)(this._fontSize * 0.9)}>NonViralAttributes:");
                sb.Append(this.RenderDataStructureElements(structure.NonViralAttributes, space));
            }

            if (structure.ViralAttributes.Count != 0)
            {
                sb.AppendLine($"{space}  <size:{(int)(this._fontSize * 0.9)}>ViralAttributes:");
                sb.Append(this.RenderDataStructureElements(structure.ViralAttributes, space));
            }

            sb.AppendLine($"{space}{"}"}");

            sb.AppendLine();
            sb.AppendLine($"{space}{parentName} {this._conf.Arrow} {name}");

            return sb.ToString();
        }

        /// <summary>
        /// Renders PlantUML code based on a given VTL 2.0 structure component collection.
        /// </summary>
        /// <param name="components">The VTL 2.0 structure components collectione.</param>
        /// <param name="space">The left margin of PlantUML object in the rendered code.</param>
        /// <returns>The PlantUML code.</returns>
        private string RenderDataStructureElements(IEnumerable<StructureComponent> components, string space)
        {
            StringBuilder sb = new StringBuilder();

            foreach (StructureComponent component in components)
            {
                string dataType = Enum.GetName(component.ValueDomain.DataType.GetType(), component.ValueDomain.DataType);
                string baseName = string.Empty;
                
                if (component.BaseComponentName != component.ComponentName) baseName = $" ({component.BaseComponentName})";
                sb.AppendLine($"{space}    **{dataType}** {component.ComponentName}{baseName}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Renders the PlantUML code with the skinparam configuration.
        /// </summary>
        /// <returns>The PlantUML code.</returns>
        private string RenderSkinparam()
        {
            StringBuilder sb = new StringBuilder();

            if (this._conf.UseHorizontalView) sb.AppendLine("left to right direction");

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
