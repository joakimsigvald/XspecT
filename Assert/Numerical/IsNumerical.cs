using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TConstraint"></typeparam>
/// <typeparam name="TActual"></typeparam>
public abstract class IsNumerical<TConstraint, TActual> : Constraint<TConstraint, TActual>
    where TConstraint : IsNumerical<TConstraint, TActual>
    where TActual : struct, IComparable<TActual>
{
    internal IsNumerical(TActual actual) : base(actual) { }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TConstraint> Not(TActual expected)
    {
        _actual.Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<TConstraint> GreaterThan(TActual expected)
    {
        _actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<TConstraint> LessThan(TActual expected)
    {
        _actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TConstraint> NotGreaterThan(TActual expected)
    {
        _actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TConstraint> NotLessThan(TActual expected)
    {
        _actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }
}