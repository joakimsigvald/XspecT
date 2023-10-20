using FluentAssertions;
using XspecT.Verification.Assertions;

namespace XspecT.Verification;

public static class AssertionExtensions
{
    public static IsNumerical<byte> Is(this byte actual) => new(actual);
    public static IsNumerical<sbyte> Is(this sbyte actual) => new(actual);
    public static IsNumerical<short> Is(this short actual) => new(actual);
    public static IsNumerical<ushort> Is(this ushort actual) => new(actual);
    public static IsNumerical<int> Is(this int actual) => new(actual);
    public static IsNumerical<uint> Is(this uint actual) => new(actual);
    public static IsNumerical<long> Is(this long actual) => new(actual);
    public static IsNumerical<ulong> Is(this ulong actual) => new(actual);
    public static IsNumerical<float> Is(this float actual) => new(actual);
    public static IsNumerical<double> Is(this double actual) => new(actual);
    public static IsNullableInt Is(this int? actual) => new(actual);
    public static IsNullableDecimal Is(this decimal? actual) => new(actual);
    public static IsNullableFloat Is(this float? actual) => new(actual);
    public static IsBool Is(this bool actual) => new(actual);
    public static IsDateTime Is(this DateTime actual) => new(actual);
    public static IsTimeSpan Is(this TimeSpan actual) => new(actual);
    public static IsString Is(this string actual) => new(actual);

    public static IsObject Is(this object actual) => new(actual);

    public static IsEnumerable<TItem> Is<TItem>(this IEnumerable<TItem> actual) => new(actual);

    public static IsComparable<TValue> Is<TValue>(this TValue actual)
        where TValue : IComparable<TValue>
        => new(actual);

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableByte> Is(this byte? actual, byte? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableSByte> Is(this sbyte? actual, sbyte? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableUShort> Is(this ushort? actual, ushort? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableShort> Is(this short? actual, short? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableUInt> Is(this uint? actual, uint? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableInt> Is(this int? actual, int? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableULong> Is(this ulong? actual, ulong? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableLong> Is(this long? actual, long? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableFloat> Is(this float? actual, float? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableDouble> Is(this double? actual, double? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsNullableDecimal> Is(this decimal? actual, decimal? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().BeSameAs(expected)
    /// </summary>
    public static ContinueWith<IsObject> Is(this object actual, object expected)
    {
        actual.Should().BeSameAs(expected);
        return new(new(actual));
    }

    public static HasEnumerable<TItem> Has<TItem>(this IEnumerable<TItem> actual) => new(actual);

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsObject> Is<TValue>(this TValue actual, TValue expected) where TValue : struct
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().BeApproximately(expected, precision)
    /// </summary>
    public static ContinueWith<IsNumerical<decimal>> Is(this decimal actual, decimal expected, decimal precision)
    {
        actual.Should().BeApproximately(expected, precision);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().BeCloseTo(expected, precision)
    /// </summary>
    public static void Is(this DateTime actual, DateTime expected, TimeSpan precision)
        => actual.Should().BeCloseTo(expected, precision);

    /// <summary>
    /// actual.Should().BeApproximately(expected, precision)
    /// </summary>
    public static ContinueWith<IsNumerical<double>> Is(this double actual, double expected, double precision)
    {
        actual.Should().BeApproximately(expected, precision);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().BeApproximately(expected, precision)
    /// </summary>
    public static ContinueWith<IsNumerical<float>> Is(this float actual, float expected, float precision)
    {
        actual.Should().BeApproximately(expected, precision);
        return new(new(actual));
    }

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsString> Is(this string actual, string expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    public static TService And<TService, TContinuation>(this ContinueWith<TContinuation> _, TService service) => service;
}