using Shouldly;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable byte
/// </summary>
public record IsNullableByte : IsNullableNumerical<byte, IsNullableByte>
{
    internal IsNullableByte(byte? actual, string actualExpr = null) : base(actual, actualExpr) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<byte> Should() => Actual.Should();
}