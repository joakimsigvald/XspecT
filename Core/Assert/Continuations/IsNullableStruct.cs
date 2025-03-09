using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable Enum
/// </summary>
public record IsNullableStruct<TValue> : Constraint<TValue?, IsNullableStruct<TValue>>
    where TValue : struct
{
    /// <summary>
    /// Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsNullableStruct<TValue>> Not(
        TValue expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), () => Xunit.Assert.NotEqual(expected, Actual), expectedExpr!).And();

    /// <summary>
    /// Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsNullableStruct<TValue>> Not(
        TValue? expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), () => Xunit.Assert.NotEqual(expected, Actual), expectedExpr!).And();

    /// <summary>
    /// Should().BeNull()
    /// </summary>
    public ContinueWith<IsNullableStruct<TValue>> Null()
        => Assert(Ignore.Me, () => Xunit.Assert.Null(Actual), string.Empty).And();

    /// <summary>
    /// Should().NotBeNull()
    /// </summary>
    public ContinueWith<IsNullableStruct<TValue>> NotNull()
        => Assert(Ignore.Me, () => Xunit.Assert.NotNull(Actual), string.Empty).And();
}