using System.CodeDom.Compiler;

namespace Xabbo.Common.Generator.Utility;

internal static class TextWriterExtensions
{
    readonly struct IntentationBlock : IDisposable
    {
        private readonly IndentedTextWriter w;
        private readonly string? end;
        public IntentationBlock(IndentedTextWriter w, string? begin = null, string? end = null)
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

    public static IDisposable IndentBlock(this IndentedTextWriter w) => new IntentationBlock(w);
    public static IDisposable BraceBlock(this IndentedTextWriter w) => new IntentationBlock(w, "{", "}");

    public static void WriteLines(this IndentedTextWriter w, ReadOnlySpan<string> lines)
    {
        foreach (string line in lines)
            w.WriteLine(line);
    }
}