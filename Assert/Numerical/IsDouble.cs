using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsDouble : IsNumerical<IsDouble, double>
{
    internal IsDouble(double actual) : base(actual) { }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    public ContinueWith<IsDouble> Around(double expected, double precision)
    {
        _actual.Should().BeApproximately(expected, precision);
        return And();
    }
}