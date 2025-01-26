using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided decimal
/// </summary>
public record IsDecimal : IsNumerical<decimal, IsDecimal>
{
    /// <summary>
    /// Asserts that the decimal is close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDecimal> Around(
        decimal expected, decimal precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.True(Math.Abs(Actual - expected) <= precision), expectedExpr);

    /// <summary>
    /// Asserts that the decimal is not close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDecimal> NotAround(
        decimal expected, decimal precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Xunit.Assert.False(Math.Abs(Actual - expected) <= precision), expectedExpr);
}