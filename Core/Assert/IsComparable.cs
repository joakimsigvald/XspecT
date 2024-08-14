using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided comparable
/// </summary>
/// <typeparam name="TActual"></typeparam>
public record IsComparable<TActual> : Constraint<IsComparable<TActual>, TActual>
    where TActual : IComparable<TActual>
{
    internal IsComparable(TActual actual, string actualExpr) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsComparable<TActual>> GreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsComparable<TActual>> LessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsComparable<TActual>> NotGreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsComparable<TActual>> NotLessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }
}
