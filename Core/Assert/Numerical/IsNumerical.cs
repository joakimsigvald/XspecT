using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Base class that allows an assertions to be made on the provided numerical
/// </summary>
/// <typeparam name="TConstraint"></typeparam>
/// <typeparam name="TActual"></typeparam>
public abstract class IsNumerical<TConstraint, TActual> : Constraint<TConstraint, TActual>
    where TConstraint : IsNumerical<TConstraint, TActual>
    where TActual : struct, IComparable<TActual>
{
    internal IsNumerical(TActual actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TConstraint> Not(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => _actual.Should().NotBe(expected), "is not", expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstraint> GreaterThan(TActual expected)
    {
        _actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstraint> LessThan(TActual expected)
    {
        _actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstraint> NotGreaterThan(TActual expected)
    {
        _actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstraint> NotLessThan(TActual expected)
    {
        _actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }
}