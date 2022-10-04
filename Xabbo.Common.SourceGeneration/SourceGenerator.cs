using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;

using Scriban;

namespace Xabbo.Common.SourceGeneration
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        const int MaxParams = 20;

        private static string GetTemplate(string resourceName)
        {
            resourceName = $"Xabbo.Common.SourceGeneration.{resourceName}";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (StreamReader sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }

        private static string RenderTemplate(string resourceName, object model)
            => Template.Parse(GetTemplate(resourceName)).Render(model);

        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            var model = new { MaxParams };

            context.AddSource("PacketExtensions.g.cs", RenderTemplate("PacketExtensions.sbncs", model));
            context.AddSource("ConnectionBase.g.cs", RenderTemplate("ConnectionBase.sbncs", model));
            context.AddSource("ConnectionExtensions.g.cs", RenderTemplate("ConnectionExtensions.sbncs", model));
        }
    }
}
