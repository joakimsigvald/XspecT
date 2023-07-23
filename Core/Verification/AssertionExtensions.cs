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

    public static IsObject Is(this object actual) => new(actual);

    public static IsEnumerable<TItem> Is<TItem>(this IEnumerable<TItem> actual) => new(actual);

    public static IsComparable<TValue> Is<TValue>(this TValue actual)
        where TValue : IComparable<TValue>
        => new(actual);

    /// <summary>
    /// actual.Should().BeSameAs(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> Is(
        this object actual, object expected)
        => actual.Should().BeSameAs(expected);

    public static HasEnumerable<TItem> Has<TItem>(this IEnumerable<TItem> actual) => new(actual);
    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> Is<TValue>(
        this TValue actual, TValue expected) where TValue : struct
        => actual.Should().Be(expected);

    /// <summary>
    /// actual.Should().BeApproximately(expected, precision)
    /// </summary>
    public static AndConstraint<FluentAssertions.Numeric.NumericAssertions<decimal>> Is(
        this decimal actual, decimal expected, decimal precision)
        => actual.Should().BeApproximately(expected, precision);

    /// <summary>
    /// actual.Should().BeCloseTo(expected, precision)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.DateTimeAssertions> Is(
        this DateTime actual, DateTime expected, TimeSpan precision)
        => actual.Should().BeCloseTo(expected, precision);

    /// <summary>
    /// actual.Should().BeApproximately(expected, precision)
    /// </summary>
    public static AndConstraint<FluentAssertions.Numeric.NumericAssertions<double>> Is(
        this double actual, double expected, double precision)
        => actual.Should().BeApproximately(expected, precision);

    /// <summary>
    /// actual.Should().BeApproximately(expected, precision)
    /// </summary>
    public static AndConstraint<FluentAssertions.Numeric.NumericAssertions<float>> Is(
        this float actual, float expected, float precision)
        => actual.Should().BeApproximately(expected, precision);

    /// <summary>
    /// actual.Should().Be(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.StringAssertions> Is(
        this string actual, string expected)
        => actual.Should().Be(expected);
}