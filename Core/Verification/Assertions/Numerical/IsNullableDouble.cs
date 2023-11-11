using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableDouble : IsNullableNumerical<double, IsNullableDouble>
{
    internal IsNullableDouble(double? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<double> Should() => _actual.Should();
}