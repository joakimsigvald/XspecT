using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableByte : IsNullableNumerical<byte, IsNullableByte>
{
    internal IsNullableByte(byte? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<byte> Should() => _actual.Should();
}