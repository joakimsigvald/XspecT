using FluentAssertions;
using XspecT.Verification.Assertions;
using XspecT.Verification.Assertions.Numerical;
using XspecT.Verification.Assertions.Time;

namespace XspecT.Verification;

public static class AssertionExtensions
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsByte Is(this byte actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsSByte Is(this sbyte actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsShort Is(this short actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsUShort Is(this ushort actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsInt Is(this int actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsUInt Is(this uint actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsLong Is(this long actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsULong Is(this ulong actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsFloat Is(this float actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsDouble Is(this double actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsDecimal Is(this decimal actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableByte Is(this byte? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableSByte Is(this sbyte? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableShort Is(this short? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableUShort Is(this ushort? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableInt Is(this int? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableUInt Is(this uint? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableLong Is(this long? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableULong Is(this ulong? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableFloat Is(this float? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableDouble Is(this double? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsNullableDecimal Is(this decimal? actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsBool Is(this bool actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsDateTime Is(this DateTime actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsTimeSpan Is(this TimeSpan actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsString Is(this string actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsObject Is(this object actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsEnumerable<TItem> Is<TItem>(this IEnumerable<TItem> actual) => new(actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="actual"></param>
    /// <returns></returns>
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
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static ContinueWith<IsNullableTimeSpan> Is(this TimeSpan? actual, TimeSpan? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static ContinueWith<IsTimeSpan> Is(this TimeSpan? actual, TimeSpan expected)
    {
        actual.Should().Be(expected);
        return new(new(actual.Value));
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static ContinueWith<IsNullableDateTime> Is(this DateTime? actual, DateTime? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    public static ContinueWith<IsDateTime> Is(this DateTime? actual, DateTime expected)
    {
        actual.Should().Be(expected);
        return new(new(actual.Value));
    }

    /// <summary>
    /// actual.Should().BeSameAs(expected)
    /// </summary>
    public static ContinueWith<IsObject> Is(this object actual, object expected)
    {
        actual.Should().BeSameAs(expected);
        return new(new(actual));
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="actual"></param>
    /// <returns></returns>
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
    /// actual.Should().Be(expected)
    /// </summary>
    public static ContinueWith<IsString> Is(this string actual, string expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TContinuation"></typeparam>
    /// <param name="_"></param>
    /// <param name="service"></param>
    /// <returns></returns>
    public static TService And<TService, TContinuation>(this ContinueWith<TContinuation> _, TService service) => service;
}