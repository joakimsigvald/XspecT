using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public abstract class IsNumerical<TConstraint, TActual> : Constraint<TConstraint, TActual>
    where TConstraint : IsNumerical<TConstraint, TActual>
    where TActual : struct, IComparable<TActual>
{
    public IsNumerical(TActual actual) : base(actual) { }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TConstraint> Not(TActual expected)
    {
        Actual.Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<TConstraint> GreaterThan(TActual expected)
    {
        Actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<TConstraint> LessThan(TActual expected)
    {
        Actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TConstraint> NotGreaterThan(TActual expected)
    {
        Actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TConstraint> NotLessThan(TActual expected)
    {
        Actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }
}