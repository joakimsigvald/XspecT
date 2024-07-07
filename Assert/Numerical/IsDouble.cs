using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided double
/// </summary>
public class IsDouble : IsNumerical<IsDouble, double>
{
    internal IsDouble(double actual) : base(actual) { }

    /// <summary>
    /// Asserts that the double is close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsDouble> Around(double expected, double precision)
    {
        _actual.Should().BeApproximately(expected, precision);
        return And();
    }
}