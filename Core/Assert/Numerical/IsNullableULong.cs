using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable ulong
/// </summary>
public record IsNullableULong : IsNullableNumerical<ulong, IsNullableULong>
{
    internal IsNullableULong(ulong? actual) : base(actual) { }
    [CustomAssertion] internal override FluentAssertions.Numeric.NullableNumericAssertions<ulong> Should() => Actual.Should();
}