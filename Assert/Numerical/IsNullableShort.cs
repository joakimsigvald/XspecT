using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable short
/// </summary>
public class IsNullableShort : IsNullableNumerical<short, IsNullableShort>
{
    internal IsNullableShort(short? actual) : base(actual) { }
    [CustomAssertion] internal override FluentAssertions.Numeric.NullableNumericAssertions<short> Should() => _actual.Should();
}