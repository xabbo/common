using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using Scriban;

namespace Xabbo.Common.SourceGeneration;

[Generator]
public class SourceGenerator : ISourceGenerator
{
    const int MaxParams = 10;

    class MessageInfo
    {
        public bool IsFlash { get; set; }
        public bool IsUnity { get; set; }
        public bool IsBoth => IsFlash && IsUnity;
        public string FlashName { get; set; }
        public string UnityName { get; set; }
        public string Name => IsUnity ? UnityName : FlashName;
        public string Client => (IsUnity ? (IsFlash ? "Unity/Flash" : "Unity") : "Flash");
        public string Description { get; set; }
    }

    private static Stream GetResourceStream(string resourceName)
    {
        resourceName = "Xabbo.Common.SourceGeneration." + resourceName;
        return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)
            ?? throw new System.Exception($"Failed to load resource: '{resourceName}'.");
    }

    private static string GetTemplate(string resourceName)
    {
        using Stream stream = GetResourceStream(resourceName);
        using StreamReader sr = new(stream);
        return sr.ReadToEnd();
    }

    private static SourceText RenderTemplate(string resourceName, object model)
    {
        var template = Template.Parse(GetTemplate(resourceName));
        if (template.HasErrors)
        {
            foreach (var msg in template.Messages)
                Console.WriteLine(msg.Message);
            throw new Exception($"Error in {resourceName}: {template.Messages}");
        }
        string renderedTemplate = template.Render(model);
        return SourceText.From(renderedTemplate, Encoding.UTF8);
    }

    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached)
        {
            // For debugging the source generator
            // Debugger.Launch();
        }
#endif
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var model = new { MaxParams };

        context.AddSource("PacketExtensions.g.cs", RenderTemplate("PacketExtensions.sbncs", model));
        // context.AddSource("ConnectionBase.g.cs", RenderTemplate("ConnectionBase.sbncs", model));
        context.AddSource("ConnectionExtensions.g.cs", RenderTemplate("ConnectionExtensions.sbncs", model));

        //
        // // Generate header properties
        // foreach (string direction in new[] { "Incoming", "Outgoing" }) {
        //     Dictionary<string, MessageInfo> messages = new(StringComparer.OrdinalIgnoreCase);
        //
        //     // Load flash messages
        //     using (Stream s = GetResourceStream($"{direction}.Flash.txt"))
        //     using (StreamReader sr = new(s))
        //     {
        //         string line;
        //         while ((line = sr.ReadLine()) is not null)
        //         {
        //             if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
        //                 continue;
        //             messages[line] = new MessageInfo
        //             {
        //                 IsFlash = true,
        //                 FlashName = line
        //             };
        //         }
        //     }
        //
        //     // Load unity messages
        //     using (Stream s = GetResourceStream($"{direction}.Unity.txt"))
        //     using (StreamReader sr = new(s))
        //     {
        //         string line;
        //         while ((line = sr.ReadLine()) is not null)
        //         {
        //             if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
        //                 continue;
        //             string[] split = line.Split(new char[] { ';' }, 2);
        //             for (int i = 0; i < split.Length; i++)
        //                 split[i] = split[i].Trim();
        //             string messageName = split[0];
        //
        //             bool isMerging = messages.TryGetValue(messageName, out MessageInfo messageInfo);
        //             if (!isMerging)
        //                 messageInfo = messages[messageName] = new MessageInfo();
        //             messageInfo.IsUnity = true;
        //             messageInfo.UnityName = messageName;
        //
        //             if (split.Length == 2)
        //             {
        //                 string flashName = split[1];
        //                 switch (flashName)
        //                 {
        //                     case "!": // Does not exist in the Flash client.
        //                         if (isMerging)
        //                             throw new Exception($"Flash message should not exist: '{messageName}'.");
        //                         messageInfo.Description = "This message is not used in the Flash client.";
        //                         break;
        //                     case "*": // Message name is the same between both clients.
        //                         if (!isMerging)
        //                             throw new Exception($"Flash message does not exist to merge: '{messageName}'.");
        //                         break;
        //                     default:
        //                         if (!messages.TryGetValue(flashName, out MessageInfo flashMessageInfo))
        //                             throw new Exception($"Flash message does not exist: '{flashName}'.");
        //                         messageInfo.FlashName = flashName;
        //                         flashMessageInfo.UnityName = messageName;
        //                         break;
        //                 }
        //             }
        //         }
        //     }
        //
        //     SourceText source = RenderTemplate("Headers.sbncs", new { direction, messages });
        //     context.AddSource($"{direction}.g.cs", source);
        // }
    }
}
