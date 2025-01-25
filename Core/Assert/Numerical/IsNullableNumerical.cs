using Shouldly;
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
    /// actual.ShouldNotBe(expected)
    /// </summary>
    public ContinueWith<TContinuation> Null()
    {
        Assert(() => ShouldBeNull());
        return And();
    }

    /// <summary>
    /// actual.ShouldNotBe(expected)
    /// </summary>
    public ContinueWith<TContinuation> NotNull()
    {
        Assert(() => ShouldNotBeNull());
        return And();
    }

    /// <summary>
    /// actual.ShouldNotBe(expected)
    /// </summary>
    public ContinueWith<TContinuation> Not(
        TActual? expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => ShouldNotBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.ShouldBeGreaterThan(expected)
    /// </summary>
    public ContinueWith<TContinuation> GreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => ShouldBeGreaterThan(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.ShouldBeLessThan(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TContinuation> LessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => ShouldBeLessThan(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.ShouldBeLessThanOrEqualTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<TContinuation> NotGreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => ShouldBeLessThanOrEqualTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// actual.ShouldBeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TContinuation> NotLessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => ShouldBeGreaterThanOrEqualTo(expected), expectedExpr);
        return And();
    }

    internal abstract FluentAssertions.Numeric.NullableNumericAssertions<TActual> Should();
}