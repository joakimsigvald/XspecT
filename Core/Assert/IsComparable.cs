using System.Runtime.CompilerServices;
using XspecT.Internal.Specification;

namespace XspecT.Assert;

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
    /// Asserts that actual value is not equal to the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<TContinuation> Not(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => NotValue(expected, expectedExpr);

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<TContinuation> GreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x > 0, expectedExpr);

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<TContinuation> LessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x < 0, expectedExpr);

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TContinuation> NotGreaterThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x <= 0, expectedExpr);

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TContinuation> NotLessThan(
        TActual expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x >= 0, expectedExpr);

    private protected ContinueWith<TContinuation> CompareTo(
        TActual expected,
        Func<int, bool> comparer,
        string expectedExpr,
        [CallerMemberName] string methodName = null)
        => AssertAnd(() =>
        {
            var methodExpr = methodName.AsWords();
            try
            {
                Xunit.Assert.True(comparer(Actual.CompareTo(expected)));
            }
            catch
            {
                Xunit.Assert.Fail($"Expected {ActualExpr} to be {methodExpr} {expected} but found {Actual}");
            }
        }, expectedExpr, methodName: methodName);
}

/// <summary>
/// Object that allows an assertions to be made on the provided comparable
/// </summary>
/// <typeparam name="TActual"></typeparam>
public record IsComparable<TActual> : IsComparable<TActual, IsComparable<TActual>>
    where TActual : IComparable<TActual>
{
}