using System.Runtime.CompilerServices;
using XspecT.Assert.Continuations.Numerical.Nullable;

namespace XspecT.Assert.Continuations.Numerical.Fractional;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TActual"></typeparam>
/// <typeparam name="TIsFractional"></typeparam>
public abstract record IsFractional<TActual, TIsFractional> : IsNumerical<TActual, TIsFractional>
    where TActual : struct, IComparable<TActual>
    where TIsFractional : IsFractional<TActual, TIsFractional>, new()
{
    /// <summary>
    /// Asserts that the actual value is approximately equal to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<TIsFractional> Around(
        TActual expected, TActual precision, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(expected, actual => AssertEqual(expected, actual, precision), expectedExpr!).And();

    /// <summary>
    /// Asserts that the actual value is not approximately equal to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<TIsFractional> NotAround(
        TActual expected, TActual precision, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(expected, actual => AssertNotEqual(expected, actual, precision), expectedExpr!).And();

    private protected abstract void AssertEqual(TActual expected, TActual actual, TActual precision);
    private protected abstract void AssertNotEqual(TActual expected, TActual actual, TActual precision);
}