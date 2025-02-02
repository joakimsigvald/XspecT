using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// base class that allows an assertions to be made on the provided nullable numerical
/// </summary>
/// <typeparam name="TActual"></typeparam>
/// <typeparam name="TContinuation"></typeparam>
/// <typeparam name="TValueContinuation"></typeparam>
public abstract record IsNullableComparableStruct<TActual, TContinuation, TValueContinuation>
    : Constraint<TActual?, TContinuation>
    where TActual : struct, IComparable<TActual>
    where TContinuation : IsNullableComparableStruct<TActual, TContinuation, TValueContinuation>, new()
    where TValueContinuation : IsComparable<TActual, TValueContinuation>, new()
{
    private TValueContinuation ValueContinuation => Constraint<TActual, TValueContinuation>.Create(Actual.Value);

    /// <summary>
    /// Assert that Actual is null
    /// </summary>
    public void Null() => Assert(() => Xunit.Assert.Null(Actual));

    /// <summary>
    /// Assert that Actual is not null
    /// </summary>
    public ContinueWith<TValueContinuation> NotNull()
    {
        Assert(() => Xunit.Assert.NotNull(Actual));
        return new(ValueContinuation);
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TContinuation> Not(
        TActual? expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.NotEqual(expected, Actual), expectedExpr);

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> Not(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x != 0, expectedExpr);

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> GreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x > 0, expectedExpr);

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> LessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x < 0, expectedExpr);

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> NotGreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x <= 0, expectedExpr);

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> NotLessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x >= 0, expectedExpr);

    internal ContinueWith<TValueContinuation> Value(
        TActual expected, string expectedExpr)
        => AssertAndValue(() =>
        {
            try
            {
                Xunit.Assert.True(Actual.HasValue);
                Xunit.Assert.Equal(expected, Actual.Value);
            }
            catch
            {
                Xunit.Assert.Fail($"Expected {ActualExpr} to be {expected} but found {Actual}");
            }
        }, expectedExpr, methodName: "");

    private protected ContinueWith<TValueContinuation> CompareTo(
        TActual expected,
        Func<int, bool> comparer,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null,
        [CallerMemberName] string methodName = null)
        => AssertAndValue(() =>
        {
            Xunit.Assert.NotNull(Actual);
            Xunit.Assert.True(comparer(Actual.Value.CompareTo(expected)));
        }, expectedExpr, methodName);

    private protected ContinueWith<TValueContinuation> AssertAndValue(
        Action assert,
        string expectedExpr,
        [CallerMemberName] string methodName = null)
    {
        Assert(assert, expectedExpr, methodName: methodName);
        return new(ValueContinuation);
    }
}