using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableUShort : IsNullableNumerical<ushort, IsNullableUShort>
{
    internal IsNullableUShort(ushort? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<ushort> Should() => _actual.Should();
}