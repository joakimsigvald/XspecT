using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableFloat : IsNullableNumerical<float, IsNullableFloat>
{
    internal IsNullableFloat(float? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<float> Should() => _actual.Should();
}