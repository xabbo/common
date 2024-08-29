using System.Reflection;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using Xabbo.Common.Generator.Diagnostics;
using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;

using TypeInfo = Microsoft.CodeAnalysis.TypeInfo;

namespace Xabbo.Common.Generator;


[Generator(LanguageNames.CSharp)]
public class VariadicGenerator : IIncrementalGenerator
{
    static readonly InvocationKind[] InvocationKinds = [
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

    static InvocationKind GetInvocationKind(string methodName) => methodName switch
    {
        "Read" => InvocationKind.Read,
        "ReadAt" => InvocationKind.ReadAt,
        "Write" => InvocationKind.Write,
        "WriteAt" => InvocationKind.WriteAt,
        "Replace" => InvocationKind.Replace,
        "ReplaceAt" => InvocationKind.ReplaceAt,
        "Modify" => InvocationKind.Modify,
        "ModifyAt" => InvocationKind.ModifyAt,
        "Send" => InvocationKind.Send,
        _ => (InvocationKind)(-1)
    };
    
    static bool ImplementsParser(ITypeSymbol? symbol) =>
        symbol is { } type && type.AllInterfaces.Any(IsIParserInterface);

    static bool ImplementsComposer(ITypeSymbol? symbol) =>
        symbol is { } type && type.AllInterfaces.Any(IsIComposerInterface);
    
    static bool IsPrimitivePacketType(ITypeSymbol? symbol)
    {
        if (symbol is null) return false;
        
        if (symbol is {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "System",
            },
            Name: "Boolean" or "Byte" or "Int16" or "Int32" or "Single" or "Int64" or "String",
        }) return true;
        
        if (symbol is {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo",
            },
            Name: "Id" or "Length"
        }) return true;

        return false;
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => {
            using var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Xabbo.Common.Generator.Templates.XabboExtensions.Base.cs");
            using var sr = new StreamReader(s);
            context.AddSource("XabboExtensions.Base.g.cs", SourceText.From(sr.ReadToEnd(), Encoding.UTF8));
        });

        // Collect invocations
        IncrementalValuesProvider<Result<VariadicInvocation?>> allInvocationResults = context.SyntaxProvider.CreateSyntaxProvider(
            static (node, _) => node is InvocationExpressionSyntax {
                Expression: MemberAccessExpressionSyntax {
                    Name.Identifier.Value:
                        "Read" or "ReadAt" or
                        "Write" or "WriteAt" or
                        "Replace" or "ReplaceAt" or
                        "Modify" or "ModifyAt" or
                        "Send"
                }
            },
            static (ctx, _) => {
                var invocationExpression = (InvocationExpressionSyntax)ctx.Node;
                var memberAccess = (MemberAccessExpressionSyntax)invocationExpression.Expression;
                var simpleName = memberAccess.Name;
                
                InvocationKind invocationKind = GetInvocationKind(simpleName.Identifier.ValueText);
                if (invocationKind == (InvocationKind)(-1))
                    return null;
                
                bool hasArguments = (invocationKind & InvocationKind.HasArguments) > 0; 
                bool requireParser = (invocationKind & InvocationKind.RequiresParser) > 0;
                bool requireComposer = (invocationKind & InvocationKind.RequiresComposer) > 0; 
                bool isPositional = (invocationKind & InvocationKind.At) > 0;
                bool isSend = (invocationKind & InvocationKind.Send) > 0;
                
                TypeInfo memberTypeInfo = ctx.SemanticModel.GetTypeInfo(memberAccess.Expression);
                if (memberTypeInfo.Type is not INamedTypeSymbol memberType) return null;
                
                Func<INamedTypeSymbol, bool> checkInterface = isSend ? IsIConnectionInterface : IsIPacketInterface;
                
                if (!checkInterface(memberType) &&
                    !memberType.AllInterfaces.Any(checkInterface))
                {
                    return null;
                }

                var diagnostics = new List<DiagnosticInfo>();
                
                VariadicType[] types;
                
                List<SyntaxNode> args;
                if (hasArguments)
                {
                    args = [.. invocationExpression.ArgumentList.Arguments];
                    if (isSend)
                    {
                        TypeInfo typeInfo = ctx.SemanticModel.GetTypeInfo(((ArgumentSyntax)args[0]).Expression);
                        if (typeInfo.Type is not INamedTypeSymbol typeSymbol) return null;

                        if (IsHeaderType(typeSymbol)) invocationKind |= InvocationKind.Header;    
                        else if (IsIdentifierType(typeSymbol)) invocationKind |= InvocationKind.Identifier;
                        else return null;
                    }
                    
                    if (isSend || isPositional)
                        args.RemoveAt(0);
                }
                else
                {
                    if (simpleName is not GenericNameSyntax genericName)
                        return null;
                    args = [.. genericName.TypeArgumentList.Arguments]; 
                }

                types = new VariadicType[args.Count];
                for (int i = 0; i < args.Count; i++)
                {
                    bool isTypeKnown = false;
                    string namespaceName = "";
                    string typeName = "";
                    bool isArray = false;
                    bool isParser = false;
                    bool isComposer = false;

                    TypeInfo typeInfo = ctx.SemanticModel.GetTypeInfo(hasArguments ? ((ArgumentSyntax)args[i]).Expression : args[i]);
                    ITypeSymbol? type = typeInfo.Type;
                    
                    if (type is IArrayTypeSymbol arrayType)
                    {
                        isArray = true;
                        type = arrayType.ElementType;
                    }

                    if (type is INamedTypeSymbol namedType)
                    {
                        isTypeKnown = true;

                        bool isValidType = IsPrimitivePacketType(namedType) || (
                            !(requireComposer && !ImplementsComposer(namedType)) &&
                            !(requireParser && !ImplementsParser(namedType))
                        );

                        if (!isValidType)
                        {
                            DiagnosticDescriptor descriptor = requireComposer switch {
                                true when requireParser => DiagnosticDescriptors.NotPrimitiveOrParserComposerType,
                                true => DiagnosticDescriptors.NotPrimitiveOrComposerType,
                                false when requireParser => DiagnosticDescriptors.NotPrimitiveOrParserType,
                                false => DiagnosticDescriptors.NotPrimitiveType
                            };

                            diagnostics.Add(new DiagnosticInfo(
                                descriptor,
                                args[i].GetLocation(),
                                hasArguments ? type.ToDisplayString() : args[i].GetText().ToString()
                            ));
                        }
                        
                        namespaceName = type.ContainingNamespace?.ToDisplayString() ?? "";
                        typeName = type.Name;
                        isParser = ImplementsParser(type); // type.Interfaces.Any(IsIParserInterface);
                        isComposer = ImplementsComposer(type);
                    }
                    else
                    {
                        // For Modify<T, ...>(Func<T, T> modifier, ...)
                        // The compiler doesn't know about the Func<T, T> argument type, since the source hasn't been generated yet.
                        // However, the type information will be available if a Func<T, T> is explicitly suppplied.
                        // 
                        // It's possible to get the type of the first parameter,
                        // from the IMethodSymbol of the argument expression
                        //
                        // var op = ctx.SemanticModel.GetSymbolInfo(((ArgumentSyntax)args[i]).Expression);
                        // if (op.Symbol is IMethodSymbol methodSymbol)
                        // {
                        //     var type = methodSymbol.Parameters[0].Type;
                        // }
                    }
                    
                    types[i] = new VariadicType(
                        IsTypeKnown: isTypeKnown,
                        Namespace: namespaceName,
                        Name: typeName,
                        IsArray: isArray,
                        IsParser: isParser,
                        IsComposer: isComposer
                    );
                }
                
                return new Result<VariadicInvocation?>(
                    new VariadicInvocation(
                        invocationKind,
                        new EquatableArray<VariadicType>(types)
                    ),
                    diagnostics.ToEquatableArray()
                );
            }
        );
        
        // Report diagnostics
        context.RegisterSourceOutput(
            allInvocationResults.SelectMany((x, _) => x.Errors),
            Executor.ReportDiagnostic
        );

        // Get all invocations
        var allInvocations = allInvocationResults
            .Where(static (x) => x.Value is not null)
            .Select(static (x, _) => x.Value!);

        var distinctReadTypes = allInvocations
            .Where(x => (x.Kind & InvocationKind.RequiresParser) > 0)
            .SelectMany((x, _) => x.Types)
            .Collect()
            .Select((types, _) => {
                HashSet<VariadicType> distinctTypes = [];
                foreach (var type in types.Where(type => type.IsParser || type.IsArray))
                {
                    distinctTypes.Add(type);
                    if (type.IsParser && type.IsArray)
                        distinctTypes.Add(type with { IsArray = false });
                }
                return distinctTypes.ToEquatableArray();
            });

        // Output source for each invocation kind / arity
        foreach (var invocationKind in InvocationKinds)
        {
            int minimumArity = (invocationKind & InvocationKind.Send) > 0 ? 1 : 2;

            var arities = allInvocations
                .Where(invocation => invocation.Kind == invocationKind)
                .Collect()
                .Select((invocations, _) => invocations
                    .Select(invocation => invocation.Types.Length)
                    .Where(x => x >= minimumArity)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToEquatableArray()
                );

            context.RegisterSourceOutput(arities, (spc, arities) => {
                if (arities.Length > 0)
                {
                    string sourceText = GenerateInvocationCountSource(invocationKind, arities);
                    spc.AddSource($"XabboExtensions.{invocationKind}.g.cs", SourceText.From(sourceText, Encoding.UTF8));
                }
            });
        }

        // Generate Read<T> implementation
        context.RegisterSourceOutput(distinctReadTypes, (spc, readTypes) => {
            using var w = new SourceWriter();

            using (w.BraceScope("internal static partial class XabboExtensions"))
            {
                w.WriteLine("private static T Read<T>(in global::Xabbo.Messages.PacketReader r)");
                using (w.BraceScope())
                {
                    // emit primitive types
                    EmitPrimitiveReaders(w);

                    w.WriteLine($"/* Generated {readTypes.Length} case{(readTypes.Length == 1 ? "" : "s")} */");
                    foreach (var type in readTypes)
                    {
                        w.Write($"if (typeof(T) == typeof(global::");
                        if (!string.IsNullOrWhiteSpace(type.Namespace))
                        {
                            w.Write(type.Namespace);
                            w.Write('.');
                        }
                        w.Write(type.Name);
                        if (type.IsArray)
                            w.Write("[]");
                        w.WriteLine("))");
                        using (w.IndentScope())
                        {
                            w.Write("return (T)(object)");
                            w.Write(type.IsArray ? "ReadArray" : "r.Parse");
                            w.Write("<global::");
                            if (!string.IsNullOrWhiteSpace(type.Namespace))
                            {
                                w.Write(type.Namespace);
                                w.Write('.');
                            }
                            w.Write(type.Name);
                            w.Write(">(");
                            if (type.IsArray)
                                w.Write("in r");
                            w.WriteLine(");");
                        }
                    }
                    w.WriteLine("throw new global::System.NotSupportedException($\"Cannot read value of type '{typeof(T)}'.\");");
                }
            }
            spc.AddSource("XabboExtensions.Read.Impl.g.cs", SourceText.From(w.ToString(), Encoding.UTF8));
        });
    }

    static void EmitPrimitiveReaders(SourceWriter w)
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
    }

    static bool IsIComposerInterface(INamedTypeSymbol symbol) => symbol is {
        TypeKind: TypeKind.Interface,
        IsGenericType: false,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages",
        },
        Name: "IComposer"
    };

    static bool IsIParserInterface(INamedTypeSymbol symbol) => symbol is {
        TypeKind: TypeKind.Interface,
        IsGenericType: true,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages",
        },
        Name: "IParser"
    };

    static bool IsIParserComposerInterface(INamedTypeSymbol symbol) => symbol is {
        TypeKind: TypeKind.Interface,
        IsGenericType: true,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages",
        },
        Name: "IParserComposer"
    };


    static bool IsIPacketInterface(INamedTypeSymbol symbol) => symbol is {
        TypeKind: TypeKind.Interface,
        IsGenericType: false,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages",
        },
        Name: "IPacket"
    };

    
    static bool IsIConnectionInterface(INamedTypeSymbol symbol) => symbol is {
        TypeKind: TypeKind.Interface,
        IsGenericType: false,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Connection",
        },
        Name: "IConnection"
    };
    
    static bool IsHeaderType(INamedTypeSymbol symbol) => symbol is {
        TypeKind: TypeKind.Struct,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages",
        },
        Name: "Header"
    };

    static bool IsIdentifierType(INamedTypeSymbol symbol) => symbol is {
        TypeKind: TypeKind.Struct,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages",
        },
        Name: "Identifier"
    };

    static string GenerateInvocationCountSource(InvocationKind kind, EquatableArray<int> arities) => kind switch
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

    static string GenerateReadInvocationArities(EquatableArray<int> arities, bool positional)
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
                w.WriteTypeParams(arity);
                if (arity > 1) w.Write(')');
                w.Write(" Read");
                if (positional)
                    w.Write("At");
                w.Write('<');
                w.WriteTypeParams(arity);
                w.Write(">(this global::Xabbo.Messages.IPacket p");
                if (positional)
                    w.Write(", int pos");
                w.WriteLine(")");
                using (w.BraceScope())
                {
                    w.Write("global::Xabbo.Messages.PacketReader r = new global::Xabbo.Messages.PacketReader(p");
                    if (positional)
                        w.Write(", ref pos");
                    w.WriteLine(");");
                    w.Write("return ");
                    if (arity > 1) w.Write('(');
                    w.WriteTypeParams(arity, "Read<", ">(in r)", 5);
                    if (arity > 1) w.Write(')');
                    w.WriteLine(';');
                }
            }
        }
        return w.ToString();
    }

    static string GenerateWriterInvocationArities(bool replace, EquatableArray<int> arities, bool positional)
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
                w.Write('<');
                w.WriteTypeParams(arity);
                w.Write(">(this global::Xabbo.Messages.IPacket p, ");
                if (positional)
                    w.Write("int pos, ");
                w.WriteTypeArgs(arity);
                w.WriteLine(')');
                using (w.BraceScope())
                {
                    w.Write("global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p");
                    if (positional)
                        w.Write(", ref pos");
                    w.WriteLine(");");
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
        return w.ToString();
    }

    static string GenerateModifyInvocationArities(EquatableArray<int> arities, bool positional)
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
                w.Write('<');
                w.WriteTypeParams(arity);
                w.Write(">(this global::Xabbo.Messages.IPacket p, ");
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
                    w.Write("global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p");
                    if (positional)
                        w.Write(", ref pos");
                    w.WriteLine(");");
                    for (int arg = 0; arg < arity; arg++)
                    {
                        w.Write("Modify(in w, ");
                        w.WriteTypeArgName(arg, arity);
                        w.WriteLine(");");
                    }
                }
            }
        }
        return w.ToString();
    }

    static string GenerateSendInvocationArities(InvocationKind kind, EquatableArray<int> arities)
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
                w.Write('<');
                w.WriteTypeParams(arity);
                w.Write(">(this global::Xabbo.Connection.IConnection c, ");
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
                    w.WriteLine("using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);");
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
        return w.ToString();
    }

}