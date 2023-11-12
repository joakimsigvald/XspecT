using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsFloat : IsNumerical<IsFloat, float>
{
    internal IsFloat(float actual) : base(actual) { }

    /// <summary>
    /// TODO
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