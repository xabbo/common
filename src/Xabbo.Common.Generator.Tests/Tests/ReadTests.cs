using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Tests;

public class ReadTests
{
    /// <summary>
    /// This test ensures that packet primitives do not emit diagnostics,
    /// while a non-primitive and non-parser emits a diagnostic at the generic type parameter.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="at">Whether to use the positional method.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, BooleanData>))]
    public void Read(Constant constant, bool at) => V.Diagnostic(
        @$"
            ((IPacket)null).Read{(at ? "At" : "")}<{constant.Type}>({(at ? "0" : "")});
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, at]
    );

    /// <summary>
    /// This test ensures that the array element type is extracted properly,
    /// that a case for the array is emitted to ReadImpl.g.cs,
    /// and that a non-primitive and non-parser emits an error diagnostic.
    /// </summary>
    /// <param name="constant">The constant.</param>
    [Theory]
    [ClassData(typeof(PacketPrimitivesAndObject))]
    public void ReadArray(Constant constant) => V.Script(
        @$"
            ((IPacket)null).Read<{constant.Type}[]>();
            ((IPacket)null).ReadAt<{constant.Type}[]>(0);
        ",
        testType: TestType.ReadImpl,
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type]
    );

    /// <summary>
    /// This test ensures that an IParser<T> implementation is recognized,
    /// and that a case for the parser is emitted to ReadImpl.g.cs.
    /// If it is an array, its array should also be emitted to ReadImpl.g.cs.
    /// </summary>
    [Theory]
    [ClassData(typeof(BooleanData))]
    public void ReadParser(bool isArray) => V.Script(
        @$"
            ((IPacket)null).Read<Parser{(isArray ? "[]" : "")}>();
            ((IPacket)null).ReadAt<Parser{(isArray ? "[]" : "")}>(0);

            {TestConstants.ParserClass}
        ",
        testType: TestType.ReadImpl,
        args: [isArray]
    );

    /// <summary>
    /// This test ensures that Read(At) arities are emitted to Read.g.cs.
    /// </summary>
    /// <param name="at">Whether to use the positional WriteAt method.</param>
    [Theory]
    [ClassData(typeof(BooleanData))]
    public void ReadVariadic(bool at) => V.Script(
        string.Join(
            "\n",
            Enumerable.Range(2, 9)
                .Select(n => $"((IPacket)null).Read{(at ? "At" : "")}<{
                    string.Join(", ", Enumerable.Range(0, n).Select(_ => "int"))
                }>({(at ? "0" : "")});")
        ),
        testType: TestType.VariadicRead,
        args: [at]
    );

    [Fact]
    public void ReadEnumerable() => V.Diagnostic(
        @"
            ((IPacket)null).Read<IEnumerable<int>>();
        ",
        severity: DiagnosticSeverity.Error
    );
}
