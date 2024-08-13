using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable uint
/// </summary>
public record IsNullableUInt : IsNullableNumerical<uint, IsNullableUInt>
{
    internal IsNullableUInt(uint? actual) : base(actual) { }
    [CustomAssertion] internal override FluentAssertions.Numeric.NullableNumericAssertions<uint> Should() => Actual.Should();
}