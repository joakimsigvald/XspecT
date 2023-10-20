using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsFloat : IsNumerical<IsFloat, float>
{
    public IsFloat(float actual) : base(actual) { }

    public ContinueWith<IsFloat> Around(float expected, float precision)
    {
        Actual.Should().BeApproximately(expected, precision);
        return And();
    }
}