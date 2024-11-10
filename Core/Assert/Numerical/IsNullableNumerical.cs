using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// base class that allows an assertions to be made on the provided nullable numerical
/// </summary>
/// <typeparam name="TActual"></typeparam>
/// <typeparam name="TContinuation"></typeparam>
public abstract record IsNullableNumerical<TActual, TContinuation> : Constraint<TActual?, TContinuation>
    where TActual : struct, IComparable<TActual>
    where TContinuation : IsNullableNumerical<TActual, TContinuation>
{
    internal IsNullableNumerical(TActual? actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TContinuation> Null()
    {
        Assert([CustomAssertion] () => Should().BeNull());
        return And();
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TContinuation> NotNull()
    {
        Assert([CustomAssertion] () => Should().NotBeNull());
        return And();
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TContinuation> Not(
        TActual? expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Should().NotBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TContinuation> GreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Should().BeGreaterThan(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TContinuation> LessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Should().BeLessThan(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TContinuation> NotGreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Should().BeLessThanOrEqualTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TContinuation> NotLessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Should().BeGreaterThanOrEqualTo(expected), expectedExpr);
        return And();
    }

    [CustomAssertion] internal abstract FluentAssertions.Numeric.NullableNumericAssertions<TActual> Should();
}