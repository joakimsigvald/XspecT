using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsNumerical<TActual> where TActual : struct, IComparable<TActual>
{
    private readonly TActual _actual;

    public IsNumerical(TActual actual) => _actual = actual;

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TActual>> Not(TActual expected)
        => _actual.Should().NotBe(expected);

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TActual>> GreaterThan(TActual expected)
        => _actual.Should().BeGreaterThan(expected);

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TActual>> LessThan(TActual expected)
        => _actual.Should().BeLessThan(expected);

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TActual>> NotGreaterThan(TActual expected)
        => _actual.Should().BeLessThanOrEqualTo(expected);

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.ComparableTypeAssertions<TActual>> NotLessThan(TActual expected)
        => _actual.Should().BeGreaterThanOrEqualTo(expected);
}