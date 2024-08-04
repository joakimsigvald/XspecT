using FluentAssertions;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Assert;
using XspecT.Assert.Numerical;
using XspecT.Assert.Time;

namespace XspecT;

/// <summary>
/// Fluent assertions with verbs Is, Has and Does
/// </summary>
public static class AssertionExtensionsNumerical
{
    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsByte Is(this byte actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsSByte Is(this sbyte actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsShort Is(this short actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsUShort Is(this ushort actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="actualExpr"></param>
    /// <param name="ignore1">This argument is ignored (used to control method overload precedence)</param>
    /// <param name="ignore2">This argument is ignored (used to control method overload precedence)</param>
    /// <returns></returns>
    public static IsInt Is(
        this int actual,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        string ignore1 = null, string ignore2 = null) 
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsUInt Is(this uint actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="actualExpr"></param>
    /// <param name="ignore1">This argument is ignored (used to control method overload precedence)</param>
    /// <param name="ignore2">This argument is ignored (used to control method overload precedence)</param>
    /// <returns></returns>
    public static Assert.Numerical.IsLong Is(
        this long actual,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        string ignore1 = null, string ignore2 = null) 
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsULong Is(this ulong actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsFloat Is(this float actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsDouble Is(this double actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsDecimal Is(this decimal actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableByte Is(this byte? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableSByte Is(this sbyte? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableShort Is(this short? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableUShort Is(this ushort? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableInt Is(this int? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableUInt Is(this uint? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableLong Is(this long? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableULong Is(this ulong? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableFloat Is(this float? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableDouble Is(this double? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableDecimal Is(this decimal? actual) => new(actual);

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableByte> Is(this byte? actual, byte? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableSByte> Is(this sbyte? actual, sbyte? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableUShort> Is(this ushort? actual, ushort? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableShort> Is(this short? actual, short? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableUInt> Is(this uint? actual, uint? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableInt> Is(this int? actual, int? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableULong> Is(this ulong? actual, ulong? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableLong> Is(this long? actual, long? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableFloat> Is(this float? actual, float? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableDouble> Is(this double? actual, double? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableDecimal> Is(this decimal? actual, decimal? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }
}