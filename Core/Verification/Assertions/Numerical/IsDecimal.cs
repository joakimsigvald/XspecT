using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsDecimal : IsNumerical<IsDecimal, decimal>
{
    public IsDecimal(decimal actual) : base(actual) { }

    public ContinueWith<IsDecimal> Around(decimal expected, decimal precision)
    {
        Actual.Should().BeApproximately(expected, precision);
        return And();
    }
}