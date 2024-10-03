using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Tests;

public class ConnectionTests
{
    /// <summary>
    /// This test ensures that packet primitives do not emit an error,
    /// while a non-primitive and non-composer emits an error at the
    /// generic type argument if available, otherwise the parameter itself.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="specifier">The message specifier, either a Header or Identifier.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, HeaderIdentifierData>))]
    public void Send(Constant constant, Constant specifier) => V.Diagnostic(
        @$"
            ((IConnection)null).Send<{constant.Type}>({specifier.Value}, {constant.Value});
            ((IConnection)null).Send({specifier.Value}, ({constant.Type}){constant.Value});
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, specifier.Type]
    );

    /// <summary>
    /// This test ensures that the array element type is extracted correctly,
    /// and that packet primitives do not emit an error,
    /// while a non-primitive and non-composer emits an error at the
    /// generic type argument if available, otherwise the parameter itself.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="specifier">The message specifier, either a Header or Identifier.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, HeaderIdentifierData>))]
    public void SendArray(Constant constant, Constant specifier) => V.Diagnostic(
        @$"
            ((IConnection)null).Send<{constant.Type}[]>({specifier.Value}, [{constant.Value}]);
            ((IConnection)null).Send({specifier.Value}, ({constant.Type}[])[{constant.Value}]);
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, specifier.Type]
    );

    /// <summary>
    /// This test ensures that a type is extracted from an IEnumerable<T>,
    /// and that packet primitives do not emit an error,
    /// while a non-primitive and non-composer emits an error at the
    /// generic type argument if available, otherwise the parameter itself.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="specifier">The message specifier, either a Header or Identifier.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, HeaderIdentifierData>))]
    public void SendEnumerable(Constant constant, Constant specifier) => V.Diagnostic(
        @$"
            ((IConnection)null).Send<IEnumerable<{constant.Type}>>({specifier.Value}, null);
            ((IConnection)null).Send({specifier.Value}, (IEnumerable<{constant.Type}>)null);
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, specifier.Type]
    );

    /// <summary>
    /// This test ensures that a type is extracted from a List<T>,
    /// and that packet primitives do not emit an error,
    /// while a non-primitive and non-composer emits an error at the
    /// generic type argument if available, otherwise the parameter itself.
    /// </summary>
    /// <param name="constant">The constant.</param>
    /// <param name="specifier">The message specifier, either a Header or Identifier.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, HeaderIdentifierData>))]
    public void SendList(Constant constant, Constant specifier) => V.Diagnostic(
        @$"
            ((IConnection)null).Send({specifier.Value}, (List<{constant.Type}>)null);
            ((IConnection)null).Send<List<{constant.Type}>>({specifier.Value}, null);
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, specifier.Type]
    );

    /// <summary>
    /// This test ensures that Write(At) arities are emitted to Write.g.cs.
    /// </summary>
    /// <param name="specifier">The message specifier, either a Header or Identifier.</param>
    [Theory]
    [ClassData(typeof(HeaderIdentifierData))]
    public void SendVariadic(Constant specifier) => V.Script(
        string.Join(
            "\n",
            Enumerable.Range(1, 10)
                .Select(n => $"((IConnection)null).Send({
                    specifier.Value
                }, {
                    string.Join(", ", Enumerable.Range(1, n)
                        .Select(i => $"{i}"))
                });")
        ),
        testType: TestType.VariadicSend,
        args: [specifier.Type]
    );

    /// <summary>
    /// This test ensures that an IComposer implementation does not emit a diagnostic.
    /// </summary>
    /// <param name="specifier">The message specifier, either a Header or Identifier.</param>
    [Theory]
    [ClassData(typeof(HeaderIdentifierData))]
    public void SendComposer(Constant specifier) => V.Diagnostic(
        @$"
            ((IConnection)null).Send({specifier.Value}, new Composer());

            {TestConstants.ComposerClass}
        ",
        args: [specifier.Type]
    );

    /// <summary>
    /// This test ensures that an IComposer does not emit a diagnostic.
    /// </summary>
    /// <param name="specifier">The message specifier, either a Header or Identifier.</param>
    [Theory]
    [ClassData(typeof(HeaderIdentifierData))]
    public void SendIComposer(Constant specifier) => V.Diagnostic(
        @$"
            IComposer composer = null;
            ((IConnection)null).Send({specifier.Value}, composer);
        ",
        args: [specifier.Type]
    );

    /// <summary>
    /// This test ensures that IComposer is considered before IEnumerable{T} by the PacketTypeAnalyzer.
    /// </summary>
    /// <param name="specifier">The message specifier, either a Header or Identifier.</param>
    [Theory]
    [ClassData(typeof(HeaderIdentifierData))]
    public void SendEnumerableComposer(Constant specifier) => V.Diagnostic(
        @$"
            ((IConnection)null).Send({specifier.Value}, new EnumerableComposer());

            class EnumerableComposer : IComposer, IEnumerable<object>
            {{
                public void Compose(in PacketWriter p) {{ }}
                public IEnumerator<object> GetEnumerator() => throw new NotImplementedException();
                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            }}
        ",
        args: [specifier.Type]
    );

    /// <summary>
    /// This test ensures that the type of a parameter is analyzed correctly.
    /// </summary>
    /// <param name="constant">The packet primitive constant.</param>
    /// <param name="specifier">The message specifier, either a Header or Identifier.</param>
    [Theory]
    [ClassData(typeof(Matrix<PacketPrimitivesAndObject, HeaderIdentifierData>))]
    public void SendMethodParameter(Constant constant, Constant specifier) => V.Diagnostic(
        @$"
            static void Send({constant.Type} value)
            {{
                ((IConnection)null).Send({specifier.Value}, value);
            }}
        ",
        severity: constant.IsPacketPrimitive ? null : DiagnosticSeverity.Error,
        args: [constant.Type, specifier.Type]
    );

    /// <summary>
    /// This test ensures that deconstructed types are analyzed correctly,
    /// and that a diagnostic is emitted appropriately.
    /// </summary>
    [Theory]
    [InlineData("int", null)]
    [InlineData("object", DiagnosticSeverity.Error)]
    public void SendDeconstructedVar(string type, DiagnosticSeverity? expectedSeverity) => V.Diagnostic(
        @$"
            var (x, y) = ((IPacket)null).Read<int, {type}>();
            ((IConnection)null).Send((Identifier)default, x, y);
        ",
        severity: expectedSeverity,
        args: [type]
    );
}