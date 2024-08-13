using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable ushort
/// </summary>
public record IsNullableUShort : IsNullableNumerical<ushort, IsNullableUShort>
{
    internal IsNullableUShort(ushort? actual) : base(actual) { }
    [CustomAssertion] internal override FluentAssertions.Numeric.NullableNumericAssertions<ushort> Should() => Actual.Should();
}