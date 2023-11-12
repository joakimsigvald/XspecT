using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableDecimal : IsNullableNumerical<decimal, IsNullableDecimal>
{
    internal IsNullableDecimal(decimal? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<decimal> Should() => _actual.Should();
}