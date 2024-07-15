using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable decimal
/// </summary>
public class IsNullableDecimal : IsNullableNumerical<decimal, IsNullableDecimal>
{
    internal IsNullableDecimal(decimal? actual) : base(actual) { }
    [CustomAssertion] internal override FluentAssertions.Numeric.NullableNumericAssertions<decimal> Should() => _actual.Should();
}