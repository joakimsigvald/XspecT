using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided float
/// </summary>
public class IsFloat : IsNumerical<IsFloat, float>
{
    internal IsFloat(float actual) : base(actual) { }

    /// <summary>
    /// Asserts that the float is close to the given value, within the provided precision
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    public ContinueWith<IsFloat> Around(float expected, float precision)
    {
        _actual.Should().BeApproximately(expected, precision);
        return And();
    }
}