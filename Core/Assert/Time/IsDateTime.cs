using System.Runtime.CompilerServices;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided DateTime
/// </summary>
public record IsDateTime : IsComparable<DateTime, IsDateTime>
{
    internal IsDateTime(DateTime actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the dateTime is before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> Before(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x < 0, expectedExpr);

    /// <summary>
    /// Asserts that the dateTime is after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> After(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x > 0, expectedExpr);

    /// <summary>
    /// Asserts that the dateTime is at or after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotBefore(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x >= 0, expectedExpr);

    /// <summary>
    /// Asserts that the dateTime is at or before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotAfter(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => CompareTo(expected, x => x <= 0, expectedExpr);

    /// <summary>
    /// Asserts that the dateTime is within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> CloseTo(
        DateTime expected, TimeSpan precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.Equal(expected, Actual, precision), expectedExpr);

    /// <summary>
    /// Asserts that the dateTime differ more than the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotCloseTo(
        DateTime expected, TimeSpan precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.True((Actual - expected).Duration() <= precision), expectedExpr);
}