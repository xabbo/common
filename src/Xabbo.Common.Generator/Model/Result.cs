namespace Xabbo.Common.Generator.Model;

internal readonly record struct Result<TValue>(TValue? Value, EquatableArray<DiagnosticInfo> Errors)
    where TValue : IEquatable<TValue>?
{
    public static Result<TValue> FromErrors(EquatableArray<DiagnosticInfo> errors) => new(default, errors);
    public static Result<TValue> FromErrors(IEnumerable<DiagnosticInfo> errors) => new(default, errors.ToArray());
    public static implicit operator Result<TValue>(TValue value) => new(value, []);
}
