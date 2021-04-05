using System;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Xabbo.Common.Generators
{
    [Generator]
    public class MessagesGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            AdditionalText messagesFile = context.AdditionalFiles.FirstOrDefault(
                x => x.Path.EndsWith("messages.ini", StringComparison.OrdinalIgnoreCase)
            );
            
            if (messagesFile is null) return;

            Ini ini = Ini.Load(new StringReader(messagesFile.GetText().ToString()));

            GenerateMessagesClass(context, ini, true);
            GenerateMessagesClass(context, ini, false);
        }

        private static void GenerateMessagesClass(GeneratorExecutionContext context, Ini ini, bool isOutgoing)
        {
            StringBuilder sb = new();

            string direction = isOutgoing ? "Outgoing" : "Incoming";
            string destination = isOutgoing ? "Server" : "Client";

            sb.Append(@$"
            using System;

            namespace Xabbo.Messages
            {{
                public class {direction} : HeaderDictionary
                {{
                    public {direction}()
                        : base(Destination.{destination})
                    {{ }}
            ");

            foreach (var (key, _) in ini[direction])
            {
                sb.AppendLine($"public Header {key} {{ get; private set; }}");
            }

            sb.Append(@"
                }
            }
            ");

            context.AddSource(
                $"{direction}.cs",
                SourceText.From(sb.ToString(), Encoding.UTF8)
            );
        }
    }
}
