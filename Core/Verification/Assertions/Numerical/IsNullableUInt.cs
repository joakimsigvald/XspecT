using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableUInt : IsNullableNumerical<uint, IsNullableUInt>
{
    public IsNullableUInt(uint? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<uint> Should() => Actual.Should();
}