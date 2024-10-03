using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Tests;

public class WriteTests
{
    /// <summary>
    /// This test ensures that packet primitives do not emit an error,
    /// while a non-primitive and non-composer emits an error at the
    /// generic type argument if available, otherwise the parameter itself.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="at">Whether to use the positional method.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, BooleanData>))]
    public void Write(Constant constant, bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Write{(at ? "At" : "")}<{constant.Type}>({(at ? "0, " : "")}{constant.Value});
            ((IPacket)null).Write{(at ? "At" : "")}({(at ? "0, " : "")}({constant.Type}){constant.Value});
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, at]
    );

    /// <summary>
    /// This test ensures that the array element type is extracted correctly,
    /// and that packet primitives do not emit an error,
    /// while a non-primitive and non-composer emits an error at the
    /// generic type argument if available, otherwise the parameter itself.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="at">Whether to use the positional method.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, BooleanData>))]
    public void WriteArray(Constant constant, bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Write{(at ? "At" : "")}<{constant.Type}[]>({(at ? "0, " : "")}[{constant.Value}]);
            ((IPacket)null).Write{(at ? "At" : "")}({(at ? "0, " : "")}({constant.Type}[])[{constant.Value}]);
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, at]
    );

    /// <summary>
    /// This test ensures that a type is extracted from an IEnumerable<T>,
    /// and that packet primitives do not emit an error,
    /// while a non-primitive and non-composer emits an error at the
    /// generic type argument if available, otherwise the parameter itself.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="at">Whether to use the positional method.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, BooleanData>))]
    public void WriteEnumerable(Constant constant, bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Write{(at ? "At" : "")}<IEnumerable<{constant.Type}>>({(at ? "0, " : "")}null);
            ((IPacket)null).Write{(at ? "At" : "")}({(at ? "0, " : "")}(IEnumerable<{constant.Type}>)null);
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, at]
    );

    /// <summary>
    /// This test ensures that a type is extracted from a List<T>,
    /// and that packet primitives do not emit an error,
    /// while a non-primitive and non-composer emits an error at the
    /// generic type argument if available, otherwise the parameter itself.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="at">Whether to use the positional method.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, BooleanData>))]
    public void WriteList(Constant constant, bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Write{(at ? "At" : "")}({(at ? "0, " : "")}(List<{constant.Type}>)null);
            ((IPacket)null).Write{(at ? "At" : "")}<List<{constant.Type}>>({(at ? "0, " : "")}null);
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, at]
    );

    /// <summary>
    /// This test ensures that Write(At) arities are emitted to Write.g.cs.
    /// </summary>
    /// <param name="at">Whether to use the positional method.</param>
    [Theory]
    [ClassData(typeof(BooleanData))]
    public void WriteVariadic(bool at) => V.Script(
        string.Join(
            "\n",
            Enumerable.Range(1, 10)
                .Select(n => $"((IPacket)null).Write{(at ? "At" : "")}({
                    (at ? "0, " : "")
                }{
                    string.Join(", ", Enumerable.Range(1, n)
                        .Select(i => $"{i}"))
                });")
        ),
        testType: TestType.VariadicWrite,
        args: [at]
    );

    /// <summary>
    /// This test ensures that an IComposer implementation does not emit a diagnostic.
    /// </summary>
    [Theory]
    [ClassData(typeof(BooleanData))]
    public void WriteComposer(bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Write{(at ? "At" : "")}({(at ? "0, " : "")}new Composer());

            {TestConstants.ComposerClass}
        ",
        args: [at]
    );

    /// <summary>
    /// This test ensures that an IComposer does not emit a diagnostic.
    /// </summary>
    [Theory]
    [ClassData(typeof(BooleanData))]
    public void WriteIComposer(bool at) => V.Diagnostic(
        @$"
            IComposer composer = null;
            ((IPacket)null).Write{(at ? "At" : "")}({(at ? "0, " : "")}composer);
        ",
        args: [at]
    );

    /// <summary>
    /// This test ensures that IComposer is considered before IEnumerable{T} by the PacketTypeAnalyzer.
    /// </summary>
    [Theory]
    [ClassData(typeof(BooleanData))]
    public void WriteEnumerableComposer(bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Write{(at ? "At" : "")}({(at ? "0, " : "")}new EnumerableComposer());

            class EnumerableComposer : IComposer, IEnumerable<object>
            {{
                public void Compose(in PacketWriter p) {{ }}
                public IEnumerator<object> GetEnumerator() => throw new NotImplementedException();
                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }}
        "
    );

    /// <summary>
    /// This test ensures that the type of a parameter is analyzed correctly.
    /// </summary>
    /// <param name="constant">The packet primitive constant.</param>
    [Theory]
    [ClassData(typeof(ConcatData<PacketPrimitives, ObjectConstantData>))]
    public void WriteMethodParameter(Constant constant) => V.Diagnostic(
        @$"
            static void Write({constant.Type} value)
            {{
                ((IPacket)null).Write(value);
            }}
        ",
        severity: constant.Type == "object" ? DiagnosticSeverity.Error : null,
        args: [constant.Type]
    );

    /// <summary>
    /// This test ensures that deconstructed types are analyzed correctly,
    /// and that a diagnostic is emitted appropriately.
    /// </summary>
    [Theory]
    [InlineData("int", null)]
    [InlineData("object", DiagnosticSeverity.Error)]
    public void WriteDeconstructedVar(string type, DiagnosticSeverity? expectedSeverity) => V.Diagnostic(
        @$"
            var (x, y) = ((IPacket)null).Read<int, {type}>();
            ((IPacket)null).Write(x, y);
        ",
        severity: expectedSeverity,
        args: [type, expectedSeverity]
    );
}