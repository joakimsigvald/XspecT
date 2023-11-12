using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableDouble : IsNullableNumerical<double, IsNullableDouble>
{
    internal IsNullableDouble(double? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<double> Should() => _actual.Should();
}