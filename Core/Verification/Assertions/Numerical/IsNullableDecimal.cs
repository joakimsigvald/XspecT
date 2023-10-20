using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableDecimal : IsNullableNumerical<decimal, IsNullableDecimal>
{
    public IsNullableDecimal(decimal? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<decimal> Should() => Actual.Should();
}