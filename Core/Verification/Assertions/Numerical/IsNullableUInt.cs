using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableUInt : IsNullableNumerical<uint, IsNullableUInt>
{
    internal IsNullableUInt(uint? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<uint> Should() => _actual.Should();
}