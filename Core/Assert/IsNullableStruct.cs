using System.Runtime.CompilerServices;

namespace XspecT.Assert;

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
        TValue expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.NotEqual(expected, Actual), expectedExpr);

    /// <summary>
    /// Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsNullableStruct<TValue>> Not(
        TValue? expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.NotEqual(expected, Actual), expectedExpr);

    /// <summary>
    /// Should().BeNull()
    /// </summary>
    public ContinueWith<IsNullableStruct<TValue>> Null()
        => AssertAnd(() => Xunit.Assert.Null(Actual));

    /// <summary>
    /// Should().NotBeNull()
    /// </summary>
    public ContinueWith<IsNullableStruct<TValue>> NotNull()
        => AssertAnd(() => Xunit.Assert.NotNull(Actual));
}