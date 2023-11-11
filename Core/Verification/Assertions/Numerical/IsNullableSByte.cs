using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableSByte : IsNullableNumerical<sbyte, IsNullableSByte>
{
    internal IsNullableSByte(sbyte? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<sbyte> Should() => _actual.Should();
}