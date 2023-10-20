using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableDouble : IsNullableNumerical<double, IsNullableDouble>
{
    public IsNullableDouble(double? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<double> Should() => Actual.Should();
}