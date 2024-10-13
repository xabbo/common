using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Tests;

public class ReplaceTests
{
    /// <summary>
    /// This test ensures that packet primitives do not emit diagnostics,
    /// while a non-primitive and non-parser emits a diagnostic at the
    /// generic type argument if available, otherwise the parameter itself.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="at">Whether to use the positional method.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, BooleanData>))]
    public void Replace(Constant constant, bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Replace{(at ? "At" : "")}<{constant.Type}>({(at ? "0, " : "")}{constant.Value});
            ((IPacket)null).Replace{(at ? "At" : "")}({(at ? "0, " : "")}({constant.Type}){constant.Value});
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, at]
    );

    /// <summary>
    /// This test ensures that attempting to replace an array emits an error.
    /// </summary>
    [Fact]
    public void ReplaceArray() => V.Diagnostic(
        @$"
            ((IPacket)null).Replace<int[]>(null);
            ((IPacket)null).Replace((int[])null);
            int[] array = [];
            ((IPacket)null).Replace(array);
        ",
        severity: DiagnosticSeverity.Error
    );

    /// <summary>
    /// This test ensures that a case for ParserComposer is emitted in
    /// Read.Impl.g.cs and Replace.Impl.g.cs.
    /// </summary>
    [Fact]
    public void ReplaceParserComposer() => V.Script(
        @$"
            ((IPacket)null).Replace(new ParserComposer());

            {TestConstants.ParserComposerClass}
        ",
        testType: TestType.ReadImpl | TestType.ReplaceImpl
    );

    /// <summary>
    /// This test ensures that Replace(At) arities are emitted to Replace(At).g.cs.
    /// </summary>
    /// <param name="at">Whether to use the positional WriteAt method.</param>
    [Theory]
    [ClassData(typeof(BooleanData))]
    public void ReplaceVariadic(bool at) => V.Script(
        string.Join(
            "\n",
            Enumerable.Range(1, 10)
                .Select(n => $"((IPacket)null).Replace{(at ? "At" : "")}({
                    (at ? "0, " : "")
                }{
                    string.Join(", ", Enumerable.Range(1, n).Select(i => $"{i}"))
                });")
        ),
        testType: TestType.VariadicReplace,
        args: [at]
    );

    [Fact]
    public void ReplaceEnumerable() => V.Diagnostic(
        @"
            ((IPacket)null).Replace<IEnumerable<int>>(null);
        ",
        severity: DiagnosticSeverity.Error
    );
}