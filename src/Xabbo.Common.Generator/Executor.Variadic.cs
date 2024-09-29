using System.Reflection;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;

namespace Xabbo.Common.Generator;

internal static partial class Executor
{
    internal static partial class Variadic
    {
        public static readonly InvocationKind[] InvocationGenerationKinds = [
            InvocationKind.Read,
            InvocationKind.ReadAt,
            InvocationKind.Write,
            InvocationKind.WriteAt,
            InvocationKind.Replace,
            InvocationKind.ReplaceAt,
            InvocationKind.Modify,
            InvocationKind.ModifyAt,
            InvocationKind.SendHeader,
            InvocationKind.SendIdentifier,
        ];

        internal static void RegisterPostInitializationOutput(IncrementalGeneratorPostInitializationContext context)
        {
            using var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Xabbo.Common.Generator.Templates.XabboExtensions.Base.cs");
            using var sr = new StreamReader(s);
            context.AddSource("XabboExtensions.Base.g.cs", SourceText.From(sr.ReadToEnd(), Encoding.UTF8));
        }

        public static void GenerateInvocationArities(SourceProductionContext context, InvocationKind kind, EquatableArray<int> arities)
        {
            if (arities.Length > 0)
                context.AddSource($"XabboExtensions.{kind}.g.cs", GenerateInvocationCountSource(kind, arities));
        }

        public static SourceText GenerateInvocationCountSource(InvocationKind kind, EquatableArray<int> arities) => kind switch
        {
            InvocationKind.Read => GenerateReadInvocationArities(arities, false),
            InvocationKind.ReadAt => GenerateReadInvocationArities(arities, true),
            InvocationKind.Write => GenerateWriterInvocationArities(false, arities, false),
            InvocationKind.WriteAt => GenerateWriterInvocationArities(false, arities, true),
            InvocationKind.Replace => GenerateWriterInvocationArities(true, arities, false),
            InvocationKind.ReplaceAt => GenerateWriterInvocationArities(true, arities, true),
            InvocationKind.Modify => GenerateModifyInvocationArities(arities, false),
            InvocationKind.ModifyAt => GenerateModifyInvocationArities(arities, true),
            InvocationKind.SendHeader or InvocationKind.SendIdentifier => GenerateSendInvocationArities(kind, arities),
            _ => throw new NotImplementedException(kind.ToString()),
        };

        static SourceText GenerateReadInvocationArities(EquatableArray<int> arities, bool positional)
        {
            using var w = new SourceWriter();

            using (w.BraceScope("internal static partial class XabboExtensions"))
            {
                for (int i = 0; i < arities.Length; i++)
                {
                    int arity = arities[i];

                    if (i > 0)
                        w.WriteLine();
                    w.WriteLines([
                        "/// <summary>",
                        $"/// Reads {arity} values of the specified types from the {(positional ? "specified" : "current")} position in the packet.",
                        "/// </summary>"
                    ]);
                    w.Write($"public static ");
                    if (arity > 1) w.Write('(');
                    w.WriteTypeParams(arity, includeAngleBrackets: false);
                    if (arity > 1) w.Write(')');
                    w.Write(" Read");
                    if (positional)
                        w.Write("At");
                    w.WriteTypeParams(arity);
                    w.Write("(this global::Xabbo.Messages.IPacket p");
                    if (positional)
                        w.Write(", int pos");
                    w.WriteLine(")");
                    using (w.BraceScope())
                    {
                        w.Write("global::Xabbo.Messages.PacketReader r = p.Reader");
                        w.WriteLine(positional ? "At(ref pos);" : "();");
                        w.Write("return ");
                        if (arity > 1) w.Write('(');
                        w.WriteTypeParams(arity, "Read<", ">(in r)", group: 5, includeAngleBrackets: false);
                        if (arity > 1) w.Write(')');
                        w.WriteLine(';');
                    }
                }
            }

            return w.ToSourceText();
        }

        static SourceText GenerateWriterInvocationArities(bool replace, EquatableArray<int> arities, bool positional)
        {
            string methodName = replace ? "Replace" : "Write";

            using var w = new SourceWriter();
            using (w.BraceScope("internal static partial class XabboExtensions"))
            {
                for (int i = 0; i < arities.Length; i++)
                {
                    int arity = arities[i];

                    if (i > 0)
                        w.WriteLine();
                    w.WriteLines([
                        "/// <summary>",
                        $"/// {methodName}s {arity} values of the specified types {(replace ? "at" : "to")} the {(positional ? "specified" : "current")} position in the packet.",
                        "/// </summary>"
                    ]);
                    w.Write("public static void ");
                    w.Write(methodName);
                    if (positional)
                        w.Write("At");
                    w.WriteTypeParams(arity);
                    w.Write("(this global::Xabbo.Messages.IPacket p, ");
                    if (positional)
                        w.Write("int pos, ");
                    w.WriteTypeArgs(arity);
                    w.WriteLine(')');
                    using (w.BraceScope())
                    {
                        w.Write("global::Xabbo.Messages.PacketWriter w = p.Writer");
                        w.WriteLine(positional ? "At(ref pos);" : "();");
                        for (int arg = 0; arg < arity; arg++)
                        {
                            w.Write(methodName);
                            w.Write("(in w, ");
                            w.WriteTypeArgName(arg, arity);
                            w.WriteLine(");");
                        }
                    }
                }
            }

            return w.ToSourceText();
        }

        static SourceText GenerateModifyInvocationArities(EquatableArray<int> arities, bool positional)
        {
            using var w = new SourceWriter();
            using (w.BraceScope("internal static partial class XabboExtensions"))
            {
                for (int i = 0; i < arities.Length; i++)
                {
                    int arity = arities[i];

                    if (i > 0)
                        w.WriteLine();
                    w.WriteLines([
                        "/// <summary>",
                        $"/// Modifies {arity} values of the specified types at the {(positional ? "specified" : "current")} position in the packet.",
                        "/// </summary>"
                    ]);
                    w.Write("public static void Modify");
                    if (positional)
                        w.Write("At");
                    w.WriteTypeParams(arity);
                    w.Write("(this global::Xabbo.Messages.IPacket p, ");
                    if (positional)
                        w.Write("int pos, ");

                    if (arity > 4)
                    {
                        w.WriteLine();
                        w.Indent++;
                    }

                    for (int j = 0; j < arity; j++)
                    {
                        if (j > 0)
                        {
                            w.Write(", ");
                            if (j % 4 == 0)
                                w.WriteLine();
                        }
                        w.Write("global::System.Func<");
                        w.WriteTypeParam(j, arity);
                        w.Write(", ");
                        w.WriteTypeParam(j, arity);
                        w.Write("> ");
                        w.WriteTypeArgName(j, arity);
                    }

                    if (arity > 4)
                    {
                        w.Indent--;
                        w.WriteLine();
                    }

                    w.WriteLine(')');
                    using (w.BraceScope())
                    {
                        w.Write("global::Xabbo.Messages.PacketWriter w = p.Writer");
                        w.WriteLine(positional ? "At(ref pos);" : "();");
                        for (int arg = 0; arg < arity; arg++)
                        {
                            w.Write("Modify(in w, ");
                            w.WriteTypeArgName(arg, arity);
                            w.WriteLine(");");
                        }
                    }
                }
            }

            return w.ToSourceText();
        }

        static SourceText GenerateSendInvocationArities(InvocationKind kind, EquatableArray<int> arities)
        {
            bool isHeader = (kind & InvocationKind.Header) > 0;

            using var w = new SourceWriter();
            using (w.BraceScope("internal static partial class XabboExtensions"))
            {
                for (int i = 0; i < arities.Length; i++)
                {
                    int arity = arities[i];

                    if (i > 0)
                        w.WriteLine();
                    w.WriteLines([
                        "/// <summary>",
                        $"/// Sends {(arity == 1 ? "a" : arity)} value{(arity == 1 ? "" : "s")} of the specified type{(arity == 1 ? "" : "s")} with the specified message {(isHeader ? "header" : "identifier")}.",
                        "/// </summary>"
                    ]);
                    w.Write("public static void Send");
                    w.WriteTypeParams(arity);
                    w.Write("(this global::Xabbo.Connection.IConnection c, ");
                    if (isHeader)
                        w.Write("global::Xabbo.Messages.Header header, ");
                    else
                        w.Write("global::Xabbo.Messages.Identifier identifier, ");
                    w.WriteTypeArgs(arity);
                    w.WriteLine(')');
                    using (w.BraceScope())
                    {
                        if (!isHeader)
                            w.WriteLine("global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);");
                        w.WriteLine("using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };");
                        w.WriteLine("global::Xabbo.Messages.PacketWriter w = p.Writer();");
                        for (int arg = 0; arg < arity; arg++)
                        {
                            w.Write("Write(in w, ");
                            w.WriteTypeArgName(arg, arity);
                            w.WriteLine(");");
                        }
                        w.WriteLine("c.Send(p);");
                    }
                }
            }

            return w.ToSourceText();
        }

        public static void GenerateReadImplementation(SourceProductionContext context, EquatableArray<VariadicType> types)
        {
            using var w = new SourceWriter();

            using (w.BraceScope("internal static partial class XabboExtensions"))
            {
                w.WriteLine("private static partial T Read<T>(in global::Xabbo.Messages.PacketReader r)");
                using (w.BraceScope())
                {
                    w.WriteLines([
                        "if (typeof(T) == typeof(bool)) return (T)(object)r.ReadBool();",
                        "if (typeof(T) == typeof(byte)) return (T)(object)r.ReadByte();",
                        "if (typeof(T) == typeof(short)) return (T)(object)r.ReadShort();",
                        "if (typeof(T) == typeof(int)) return (T)(object)r.ReadInt();",
                        "if (typeof(T) == typeof(float)) return (T)(object)r.ReadFloat();",
                        "if (typeof(T) == typeof(long)) return (T)(object)r.ReadLong();",
                        "if (typeof(T) == typeof(string)) return (T)(object)r.ReadString();",
                        "if (typeof(T) == typeof(global::Xabbo.Id)) return (T)(object)r.ReadId();",
                        "if (typeof(T) == typeof(global::Xabbo.Length)) return (T)(object)r.ReadLength();",
                    ]);

                    w.WriteLine($"/* Generated {types.Length} case{(types.Length == 1 ? "" : "s")} */");
                    foreach (var type in types)
                    {
                        w.Write($"if (typeof(T) == typeof({type.FullyQualifiedName}");
                        if (type.IsArray)
                            w.Write("[]");
                        w.WriteLine("))");
                        using (w.IndentScope())
                        {
                            w.Write("return (T)(object)");
                            w.Write(type.IsArray ? "ReadArray" : "r.Parse");
                            w.Write($"<{type.FullyQualifiedName}>(");
                            if (type.IsArray)
                                w.Write("in r");
                            w.WriteLine(");");
                        }
                    }
                    w.WriteLine("throw new global::System.NotSupportedException($\"Cannot read value of type '{typeof(T)}'.\");");
                }
            }

            context.AddSource("XabboExtensions.Read.Impl.g.cs", SourceText.From(w.ToString(), Encoding.UTF8));
        }

        public static void GenerateReplaceImplementation(SourceProductionContext context, EquatableArray<VariadicType> types)
        {
            using var w = new SourceWriter();

            using (w.BraceScope("internal static partial class XabboExtensions"))
            {
                using (w.BraceScope("private static partial void Replace<T>(in global::Xabbo.Messages.PacketWriter w, T value)"))
                {
                    w.WriteLine("switch (value)");
                    using (w.BraceScope())
                    {
                        w.WriteLines([
                            "case bool v: w.ReplaceBool(v); break;",
                            "case byte v: w.ReplaceByte(v); break;",
                            "case short v: w.ReplaceShort(v); break;",
                            "case int v: w.ReplaceInt(v); break;",
                            "case float v: w.ReplaceFloat(v); break;",
                            "case long v: w.ReplaceLong(v); break;",
                            "case string v: w.ReplaceString(v); break;",
                            "case global::Xabbo.Length v: w.ReplaceLength(v); break;",
                            "case global::Xabbo.Id v: w.ReplaceId(v); break;",
                            "case global::Xabbo.Messages.B64 v: w.ReplaceB64(v); break;",
                            "case global::Xabbo.Messages.VL64 v: w.ReplaceVL64(v); break;",
                        ]);
                        // Write cases
                        w.WriteLine($"/* Generated {types.Length} case{(types.Length == 1 ? "" : "s")} */");
                        foreach (var type in types)
                            w.WriteLine($"case {type.FullyQualifiedName} v: w.ReplaceStruct(v); break;");
                        w.WriteLine("default: throw new global::System.NotSupportedException($\"Cannot replace value of type '{typeof(T)}'.\");");
                    }
                }
            }

            context.AddSource("XabboExtensions.Replace.Impl.g.cs", w.ToSourceText());
        }
    }
}