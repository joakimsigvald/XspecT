using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// base class that allows an assertions to be made on the provided nullable numerical
/// </summary>
/// <typeparam name="TActual"></typeparam>
/// <typeparam name="TConstrain"></typeparam>
public abstract record IsNullableNumerical<TActual, TConstrain> : Constraint<TConstrain, TActual?>
    where TActual : struct, IComparable<TActual>
    where TConstrain : IsNullableNumerical<TActual, TConstrain>
{
    internal IsNullableNumerical(TActual? actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstrain> Null()
    {
        Should().BeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstrain> NotNull()
    {
        Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstrain> Not(
        TActual? expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstrain> GreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstrain> LessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstrain> NotGreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TConstrain> NotLessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }

    [CustomAssertion] internal abstract FluentAssertions.Numeric.NullableNumericAssertions<TActual> Should();
}