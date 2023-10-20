using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableFloat : IsNullableNumerical<float, IsNullableFloat>
{
    public IsNullableFloat(float? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<float> Should() => Actual.Should();
}