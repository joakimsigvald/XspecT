using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations;

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
    private TValueContinuation ValueContinuation => Constraint<TActual, TValueContinuation>.Create(Actual!.Value, ActualExpr);

    /// <summary>
    /// Assert that Actual is null
    /// </summary>
    public void Null() => Assert(Ignore.Me, () => Xunit.Assert.Null(Actual), string.Empty);

    /// <summary>
    /// Assert that Actual is not null
    /// </summary>
    public ContinueWith<TValueContinuation> NotNull()
    {
        Assert(Ignore.Me, () => Xunit.Assert.NotNull(Actual), string.Empty);
        return new(ValueContinuation);
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TContinuation> Not(
        TActual? expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(expected, () => Xunit.Assert.NotEqual(expected, Actual), expectedExpr!).And();

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> Not(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => CompareTo(expected, x => x != 0, expectedExpr!);

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> GreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => CompareTo(expected, x => x > 0, expectedExpr!);

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> LessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => CompareTo(expected, x => x < 0, expectedExpr!);

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> NotGreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => CompareTo(expected, x => x <= 0, expectedExpr!);

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TValueContinuation> NotLessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => CompareTo(expected, x => x >= 0, expectedExpr!);

    internal ContinueWith<TValueContinuation> Value(
        TActual expected, string expectedExpr)
    {
        Assert(() =>
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
            }, new() { Expected = expectedExpr, MethodName = string.Empty });
        return new(ValueContinuation);
    }

    private protected ContinueWith<TValueContinuation> CompareTo(
        TActual expected,
        Func<int, bool> comparer,
        string expectedExpr,
        string auxVerb = "be",
        [CallerMemberName] string? methodName = null)
    {
        Assert(expected, () =>
            {
                Xunit.Assert.NotNull(Actual);
                Xunit.Assert.True(comparer(Actual.Value.CompareTo(expected)));
            }, expectedExpr, auxVerb, methodName: methodName);
        return new(ValueContinuation);
    }
}