using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableShort : IsNullableNumerical<short, IsNullableShort>
{
    internal IsNullableShort(short? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<short> Should() => _actual.Should();
}