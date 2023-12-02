using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable ulong
/// </summary>
public class IsNullableULong : IsNullableNumerical<ulong, IsNullableULong>
{
    internal IsNullableULong(ulong? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<ulong> Should() => _actual.Should();
}