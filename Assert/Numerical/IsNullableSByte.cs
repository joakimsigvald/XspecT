using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable sbyte
/// </summary>
public class IsNullableSByte : IsNullableNumerical<sbyte, IsNullableSByte>
{
    internal IsNullableSByte(sbyte? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<sbyte> Should() => _actual.Should();
}