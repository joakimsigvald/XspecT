using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableSByte : IsNullableNumerical<sbyte, IsNullableSByte>
{
    internal IsNullableSByte(sbyte? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<sbyte> Should() => _actual.Should();
}