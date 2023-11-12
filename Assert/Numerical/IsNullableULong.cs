using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableULong : IsNullableNumerical<ulong, IsNullableULong>
{
    internal IsNullableULong(ulong? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<ulong> Should() => _actual.Should();
}