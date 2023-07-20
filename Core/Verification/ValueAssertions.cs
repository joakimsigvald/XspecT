using FluentAssertions;

namespace XspecT.Verification;

public static class ValueAssertions
{
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
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TValue>> IsGreaterThan<TValue>(
        this TValue actual, TValue expected)
        where TValue : struct, IComparable<TValue>
        => actual.Should().BeGreaterThan(expected);

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TValue>> IsLessThan<TValue>(
        this TValue actual, TValue expected)
        where TValue : struct, IComparable<TValue>
        => actual.Should().BeLessThan(expected);

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TValue>> IsNotGreaterThan<TValue>(
        this TValue actual, TValue expected)
        where TValue : struct, IComparable<TValue>
        => actual.Should().BeLessThanOrEqualTo(expected);

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TValue>> IsNotLessThan<TValue>(
        this TValue actual, TValue expected)
        where TValue : struct, IComparable<TValue>
        => actual.Should().BeGreaterThanOrEqualTo(expected);

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

    /// <summary>
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.StringAssertions> IsIgnoringCase(
        this string actual, string expected)
        => actual.Should().BeEquivalentTo(expected);

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsNot<TValue>(
        this TValue actual, TValue expected) where TValue : struct
        => actual.Should().NotBe(expected);

    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.BooleanAssertions> IsTrue(this bool actual) 
        => actual.Should().BeTrue();

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    public static AndConstraint<FluentAssertions.Primitives.BooleanAssertions> IsFalse(this bool actual) 
        => actual.Should().BeFalse();
}