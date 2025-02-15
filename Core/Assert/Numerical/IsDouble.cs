using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided double
/// </summary>
public record IsDouble : IsNumerical<double, IsDouble>
{
    /// <summary>
    /// Asserts that the double is close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDouble> Around(
        double expected, double precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(expected, () => Xunit.Assert.Equal(expected, Actual, precision), expectedExpr);

    /// <summary>
    /// Asserts that the double is not close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDouble> NotAround(
        double expected, double precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(expected, () => Xunit.Assert.NotEqual(expected, Actual, precision), expectedExpr);
}