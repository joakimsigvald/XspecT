using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsDouble : IsNumerical<IsDouble, double>
{
    public IsDouble(double actual) : base(actual) { }

    public ContinueWith<IsDouble> Around(double expected, double precision)
    {
        Actual.Should().BeApproximately(expected, precision);
        return And();
    }
}