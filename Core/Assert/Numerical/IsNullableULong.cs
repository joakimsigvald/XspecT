using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable ulong
/// </summary>
public record IsNullableULong : IsNullableNumerical<ulong, IsNullableULong>
{
    internal IsNullableULong(ulong? actual, string actualExpr = null) : base(actual, actualExpr) { }
    [CustomAssertion] internal override FluentAssertions.Numeric.NullableNumericAssertions<ulong> Should() => Actual.Should();
}