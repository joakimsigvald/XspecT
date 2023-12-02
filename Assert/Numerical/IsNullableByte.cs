using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable byte
/// </summary>
public class IsNullableByte : IsNullableNumerical<byte, IsNullableByte>
{
    internal IsNullableByte(byte? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<byte> Should() => _actual.Should();
}