using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable sbyte
/// </summary>
public record IsNullableSByte : IsNullableNumerical<sbyte, IsNullableSByte>
{
    internal IsNullableSByte(sbyte? actual) : base(actual) { }
    [CustomAssertion] internal override FluentAssertions.Numeric.NullableNumericAssertions<sbyte> Should() => Actual.Should();
}