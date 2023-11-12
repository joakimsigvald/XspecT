using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsDecimal : IsNumerical<IsDecimal, decimal>
{
    internal IsDecimal(decimal actual) : base(actual) { }

    /// <summary>
    /// TODO
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