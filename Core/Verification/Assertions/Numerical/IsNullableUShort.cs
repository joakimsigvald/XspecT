using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableUShort : IsNullableNumerical<ushort, IsNullableUShort>
{
    internal IsNullableUShort(ushort? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<ushort> Should() => _actual.Should();
}