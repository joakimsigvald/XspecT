using FluentAssertions;
using XspecT.Assert;
using XspecT.Assert.Numerical;

namespace XspecT;

/// <summary>
/// Fluent assertions with verbs Is, Has and Does
/// </summary>
public static class AssertionExtensionsNumerical
{
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