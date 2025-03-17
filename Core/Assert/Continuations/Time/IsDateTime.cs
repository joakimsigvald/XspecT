using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided DateTime
/// </summary>
public record IsDateTime : IsComparable<DateTime, IsDateTime>
{
    /// <summary>
    /// Asserts that the dateTime is before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> Before(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => CompareTo(expected, x => x < 0, expectedExpr!, "occur");

    /// <summary>
    /// Asserts that the dateTime is after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> After(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => CompareTo(expected, x => x > 0, expectedExpr!, "occur");

    /// <summary>
    /// Asserts that the dateTime is at or after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    [Obsolete("Use Not().Before instead")]
    public ContinueWith<IsDateTime> NotBefore(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => CompareTo(expected, x => x >= 0, expectedExpr!, "occur");

    /// <summary>
    /// Asserts that the dateTime is at or before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    [Obsolete("Use Not().After instead")]
    public ContinueWith<IsDateTime> NotAfter(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => CompareTo(expected, x => x <= 0, expectedExpr!, "occur");

    /// <summary>
    /// Asserts that the dateTime is within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="tolerance"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> CloseTo(
        DateTime expected, TimeSpan tolerance, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(expected, actual => Xunit.Assert.Equal(expected, actual, tolerance), expectedExpr!).And();

    /// <summary>
    /// Asserts that the dateTime differ more than the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="tolerance"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    [Obsolete("Use Not().CloseTo instead")]
    public ContinueWith<IsDateTime> NotCloseTo(
        DateTime expected, TimeSpan tolerance, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(expected, actual => Xunit.Assert.False((actual - expected).Duration() <= tolerance), expectedExpr!).And();
}