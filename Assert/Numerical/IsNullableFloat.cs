using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable float
/// </summary>
public class IsNullableFloat : IsNullableNumerical<float, IsNullableFloat>
{
    internal IsNullableFloat(float? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<float> Should() => _actual.Should();
}