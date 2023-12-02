using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided decimal
/// </summary>
public class IsDecimal : IsNumerical<IsDecimal, decimal>
{
    internal IsDecimal(decimal actual) : base(actual) { }

    /// <summary>
    /// Asserts that the decimal is close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    public ContinueWith<IsDecimal> Around(decimal expected, decimal precision)
    {
        _actual.Should().BeApproximately(expected, precision);
        return And();
    }
}