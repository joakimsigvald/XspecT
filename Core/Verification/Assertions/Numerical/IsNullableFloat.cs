using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableFloat : IsNullableNumerical<float, IsNullableFloat>
{
    internal IsNullableFloat(float? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<float> Should() => _actual.Should();
}