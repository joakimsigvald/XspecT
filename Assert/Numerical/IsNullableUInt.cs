using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable uint
/// </summary>
public class IsNullableUInt : IsNullableNumerical<uint, IsNullableUInt>
{
    internal IsNullableUInt(uint? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<uint> Should() => _actual.Should();
}