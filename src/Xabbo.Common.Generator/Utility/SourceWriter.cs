using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis.Text;

namespace Xabbo.Common.Generator.Utility;

internal sealed class SourceWriter() : IndentedTextWriter(new StringWriter(), new string(' ', 4))
{
    readonly struct IntentationBlock : IDisposable
    {
        private readonly SourceWriter w;
        private readonly string? end;
        public IntentationBlock(SourceWriter w, string? begin = null, string? end = null)
        {
            this.w = w;
            this.end = end;
            if (begin is not null)
                w.WriteLine(begin);
            w.Indent++;
        }
        public void Dispose()
        {
            w.Indent--;
            if (end is not null)
                w.WriteLine(end);
        }
    }

    public void WriteLines(ReadOnlySpan<string> lines)
    {
        foreach (string line in lines)
            WriteLine(line);
    }

    public IDisposable IndentScope() => new IntentationBlock(this);
    public IDisposable BraceScope(string? openingLine = null)
    {
        if (openingLine is not null)
            WriteLine(openingLine);
        return new IntentationBlock(this, "{", "}");
    }
    public IDisposable? NamespaceScope(string? name)
        => (string.IsNullOrWhiteSpace(name) || name == "<global namespace>") ? null : BraceScope($"namespace {name}");

    public void WriteTypeParams(int n, string? prefix = null, string? suffix = null, int group = 10, bool includeAngleBrackets = true)
    {
        if (n == 0) return;

        if (includeAngleBrackets)
            Write('<');

        if (n > group)
        {
            WriteLine();
            Indent++;
        }

        for (int i = 0; i < n; i++)
        {
            if (i > 0)
            {
                Write(", ");
                if (i % group == 0)
                    WriteLine();
            }
            if (prefix is not null)
                Write(prefix);
            WriteTypeParam(i, n);
            if (suffix is not null)
                Write(suffix);
        }

        if (n > group)
        {
            Indent--;
            WriteLine();
        }

        if (includeAngleBrackets)
            Write('>');
    }

    public void WriteTypeParam(int i, int arity) => Write(arity > 1 ? $"T{i + 1}" : "T");
    public void WriteTypeArg(int i, int arity) => Write(arity > 1 ? $"T{i + 1} arg{i + 1}" : "T arg");
    public void WriteTypeArgName(int i, int arity) => Write(arity > 1 ? $"arg{i + 1}" : "arg");

    /// <summary>
    /// Writes type arguments for use in a method's argument list.
    /// </summary>
    public void WriteTypeArgs(int n, string? prefix = null, string? suffix = null, int group = 10)
    {
        if (n > group)
        {
            WriteLine();
            Indent++;
        }

        for (int i = 0; i < n; i++)
        {
            if (i > 0)
            {
                Write(", ");
                if (i % group == 0)
                    WriteLine();
            }
            if (prefix is not null)
                Write(prefix);
            WriteTypeArg(i, n);
            if (suffix is not null)
                Write(suffix);
        }

        if (n > group)
        {
            Indent--;
            WriteLine();
        }
    }

    public override string ToString()
    {
        Flush();
        return ((StringWriter)InnerWriter).ToString();
    }

    public SourceText ToSourceText() => SourceText.From(ToString(), System.Text.Encoding.UTF8);
}