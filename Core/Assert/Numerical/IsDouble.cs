using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided double
/// </summary>
public record IsDouble : IsNumerical<IsDouble, double>
{
    internal IsDouble(double actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the double is close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsDouble> Around(
        double expected, double precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Actual.Should().BeApproximately(expected, precision);
        return And();
    }
}