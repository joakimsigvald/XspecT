using FluentAssertions;

namespace XspecT.Verification;

public static class ValueAssertionsObsolete
{
    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    [Obsolete("Replaced by Is().GreaterThan")]
    public static AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TValue>> IsGreaterThan<TValue>(
        this TValue actual, TValue expected)
        where TValue : struct, IComparable<TValue>
        => actual.Should().BeGreaterThan(expected);

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    [Obsolete("Replaced by Is().LessThan")]
    public static AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TValue>> IsLessThan<TValue>(
        this TValue actual, TValue expected)
        where TValue : struct, IComparable<TValue>
        => actual.Should().BeLessThan(expected);

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    [Obsolete("Replaced by Is().NotGreaterThan")]
    public static AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TValue>> IsNotGreaterThan<TValue>(
        this TValue actual, TValue expected)
        where TValue : struct, IComparable<TValue>
        => actual.Should().BeLessThanOrEqualTo(expected);

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    [Obsolete("Replaced by Is().NotLessThan")]
    public static AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TValue>> IsNotLessThan<TValue>(
        this TValue actual, TValue expected)
        where TValue : struct, IComparable<TValue>
        => actual.Should().BeGreaterThanOrEqualTo(expected);

    /// <summary>
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    [Obsolete("Replaced by Is().Like()")]
    public static AndConstraint<FluentAssertions.Primitives.StringAssertions> IsIgnoringCase(
        this string actual, string expected)
        => actual.Should().BeEquivalentTo(expected);

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    [Obsolete("Replaced by Is().Not()")]
    public static AndConstraint<FluentAssertions.Primitives.ObjectAssertions> IsNot<TValue>(
        this TValue actual, TValue expected) where TValue : struct
        => actual.Should().NotBe(expected);

    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    [Obsolete("Replaced by Is().True()")]
    public static AndConstraint<FluentAssertions.Primitives.BooleanAssertions> IsTrue(this bool actual)
        => actual.Should().BeTrue();

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    [Obsolete("Replaced by Is().False()")]
    public static AndConstraint<FluentAssertions.Primitives.BooleanAssertions> IsFalse(this bool actual)
        => actual.Should().BeFalse();
}