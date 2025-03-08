using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations;

/// <summary>
/// Object that allows an assertions to be made on the provided comparable
/// </summary>
/// <typeparam name="TActual"></typeparam>
/// <typeparam name="TContinuation"></typeparam>
public abstract record IsComparable<TActual, TContinuation> : Constraint<TActual, TContinuation>
    where TContinuation : IsComparable<TActual, TContinuation>, new()
    where TActual : IComparable<TActual>
{
    /// <summary>
    /// Assert that the value is not same as the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns>A continuation for making additional asserts on the value</returns>
    public ContinueWith<TContinuation> Not(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(expected, () => Xunit.Assert.NotEqual(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Assert that the value is greater than expected
    /// </summary>
    public ContinueWith<TContinuation> GreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x > 0, expectedExpr);

    /// <summary>
    /// Assert that the value is less than expected
    /// </summary>
    public ContinueWith<TContinuation> LessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x < 0, expectedExpr);

    /// <summary>
    /// Assert that the value is not greater than expected (i.e. less than or equal)
    /// </summary>
    public ContinueWith<TContinuation> NotGreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x <= 0, expectedExpr);

    /// <summary>
    /// Assert that the value is not less than expected (i.e. greater than or equal)
    /// </summary>
    public ContinueWith<TContinuation> NotLessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x >= 0, expectedExpr);

    private protected ContinueWith<TContinuation> CompareTo(
        TActual expected,
        Func<int, bool> comparer,
        string expectedExpr,
        string auxVerb = "be",
        [CallerMemberName] string methodName = null)
        => Assert(expected, () => Xunit.Assert.True(comparer(Actual.CompareTo(expected))), expectedExpr, auxVerb, methodName: methodName)
        .And();
}

/// <summary>
/// Object that allows an assertions to be made on the provided comparable
/// </summary>
/// <typeparam name="TActual"></typeparam>
public record IsComparable<TActual> : IsComparable<TActual, IsComparable<TActual>>
    where TActual : IComparable<TActual>
{
}