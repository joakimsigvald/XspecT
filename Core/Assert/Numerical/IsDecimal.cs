using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided decimal
/// </summary>
public record IsDecimal : IsNumerical<IsDecimal, decimal>
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
        AddAssert([CustomAssertion] () => Actual.Should().BeApproximately(expected, precision), expectedExpr);
        return And();
    }
}