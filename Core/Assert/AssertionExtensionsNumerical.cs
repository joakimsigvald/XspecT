using System.Runtime.CompilerServices;
using XspecT.Assert.Numerical;
using XspecT.Internal.Specification;

namespace XspecT.Assert;

/// <summary>
/// Fluent assertions with verbs Is, Has and Does
/// </summary>
public static class AssertionExtensionsNumerical
{
    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableByte> Is(
        this byte? actual,
        byte? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableByte.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableSByte> Is(
        this sbyte? actual,
        sbyte? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual),actualExpr, expectedExpr);
        return new(IsNullableSByte.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableUShort> Is(
        this ushort? actual,
        ushort? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableUShort.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableShort> Is(
        this short? actual,
        short? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableShort.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableUInt> Is(
        this uint? actual,
        uint? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableUInt.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableInt> Is(
        this int? actual,
        int? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableInt.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsInt> Is(
        this int actual,
        int expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null) 
        => actual.Is(actualExpr: actualExpr).Value(expected, expectedExpr);

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableULong> Is(
        this ulong? actual,
        ulong? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableULong.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableLong> Is(
        this long? actual,
        long? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableLong.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableFloat> Is(
        this float? actual,
        float? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableFloat.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableDouble> Is(
        this double? actual,
        double? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableDouble.Create(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    public static ContinueWith<IsNullableDecimal> Is(
        this decimal? actual,
        decimal? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(IsNullableDecimal.Create(actual));
    }

    private static void Assert(
        Action assert, string actual = null, string expected = null, [CallerMemberName] string verb = "")
        => SpecificationGenerator.Assert(assert, actual.ParseActual(), expected, verb);

    private static void Assert(
        Action<string> assert,
        string actual = null,
        string expected = null,
        [CallerMemberName] string verb = "")
    {
        var actualExpr = actual.ParseActual();
        SpecificationGenerator.Assert(() => assert(actualExpr), actualExpr, expected, verb);
    }
}