using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableUInt : IsNullableNumerical<uint, IsNullableUInt>
{
    internal IsNullableUInt(uint? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<uint> Should() => _actual.Should();
}