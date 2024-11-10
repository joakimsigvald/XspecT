using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided float
/// </summary>
public record IsFloat : IsNumerical<float, IsFloat>
{
    internal IsFloat(float actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the float is close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsFloat> Around(
        float expected, float precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().BeApproximately(expected, precision), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the float is not close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsFloat> NotAround(
        float expected, float precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().NotBeApproximately(expected, precision), expectedExpr);
        return And();
    }
}