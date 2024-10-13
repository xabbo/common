using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Tests;

public class ModifyTests
{
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, BooleanData>))]
    public void Modify(Constant constant, bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Modify{(at ? "At" : "")}<{constant.Type}>({(at ? "0, " : "")}x => x);
            ((IPacket)null).Modify{(at ? "At" : "")}({(at ? "0, " : "")}({constant.Type} x) => x);
        ",
        severity: constant.Type == "object" ? DiagnosticSeverity.Error : null,
        args: [constant.Type, at]
    );

    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, BooleanData>))]
    public void ModifyFuncArg(Constant constant, bool at) => V.Diagnostic(
        @$"
            Func<{constant.Type}, {constant.Type}> modifier = s => s;
            ((IPacket)null).Modify{(at ? "At" : "")}({(at ? "0, " : "")}modifier);
        ",
        severity: constant.Type == "object" ? DiagnosticSeverity.Error : null,
        args: [constant.Type, at]
    );

    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, BooleanData>))]
    public void ModifyStaticMethod(Constant constant, bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Modify{(at ? "At" : "")}<{constant.Type}>({(at ? "0, " : "")}Modifier);

            static {constant.Type} Modifier({constant.Type} x) => x;
        ",
        severity: constant.Type == "object" ? DiagnosticSeverity.Error : null,
        args: [constant.Type, at]
    );

    [Theory]
    [ClassData(typeof(BooleanData))]
    public void ModifyVariadic(bool at) => V.Script(
        string.Join(
            "\n",
            Enumerable.Range(2, 9)
                .Select(n => $"((IPacket)null).Modify{
                    (at ? "At" : "")
                }({
                    (at ? "0, " : "")
                }{
                    string.Join(", ",
                        Enumerable.Range(0, n)
                            .Select(_ => "(int x) => x")
                    )
                });")
        ),
        testType: TestType.VariadicModify,
        args: [at]
    );

    [Fact]
    public void ModifyArray() => V.Diagnostic(
        @$"
            ((IPacket)null).Modify<int[]>(x => x);
        ",
        severity: DiagnosticSeverity.Error
    );

    [Fact]
    public void ModifyEnumerable() => V.Diagnostic(
        @"
            ((IPacket)null).Modify<IEnumerable<int>>(x => x);
        ",
        severity: DiagnosticSeverity.Error
    );

    /// <summary>
    /// This test ensures that a case for ParserComposer is emitted in ReadImpl.g.cs and ReplaceImpl.g.cs.
    /// </summary>
    [Fact]
    public void ModifyParserComposer() => V.Script(
        @$"
            ((IPacket)null).Modify<ParserComposer>(x => x);
            ((IPacket)null).ModifyAt<ParserComposer>(0, x => x);

            {TestConstants.ParserComposerClass}
        ",
        testType: TestType.ModifyImpl
    );
}