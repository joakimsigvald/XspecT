using Shouldly;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided decimal
/// </summary>
public record IsDecimal : IsNumerical<decimal, IsDecimal>
{
    internal IsDecimal(decimal actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the decimal is close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDecimal> Around(
        decimal expected, decimal precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.ShouldBeApproximately(expected, precision), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the decimal is not close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDecimal> NotAround(
        decimal expected, decimal precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.ShouldNotBeApproximately(expected, precision), expectedExpr);
        return And();
    }
}