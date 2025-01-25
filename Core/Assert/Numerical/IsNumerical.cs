using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Base class that allows an assertions to be made on the provided numerical
/// </summary>
/// <typeparam name="TContinuation"></typeparam>
/// <typeparam name="TActual"></typeparam>
public abstract record IsNumerical<TActual, TContinuation> : Constraint<TActual, TContinuation>
    where TContinuation : IsNumerical<TActual, TContinuation>
    where TActual : struct, IComparable<TActual>
{
    internal IsNumerical(TActual actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TContinuation> Not(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.NotEqual(expected, Actual), expectedExpr);

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<TContinuation> GreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Actual.Should().BeGreaterThan(expected), expectedExpr);

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<TContinuation> LessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Actual.Should().BeLessThan(expected), expectedExpr);

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TContinuation> NotGreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Actual.Should().BeLessThanOrEqualTo(expected), expectedExpr);

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TContinuation> NotLessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Actual.Should().BeGreaterThanOrEqualTo(expected), expectedExpr);
}