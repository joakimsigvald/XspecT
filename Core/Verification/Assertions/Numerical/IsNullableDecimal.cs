using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableDecimal : IsNullableNumerical<decimal, IsNullableDecimal>
{
    internal IsNullableDecimal(decimal? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<decimal> Should() => _actual.Should();
}