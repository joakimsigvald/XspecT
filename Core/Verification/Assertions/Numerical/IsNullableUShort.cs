using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableUShort : IsNullableNumerical<ushort, IsNullableUShort>
{
    public IsNullableUShort(ushort? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<ushort> Should() => Actual.Should();
}