using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable double
/// </summary>
public class IsNullableDouble : IsNullableNumerical<double, IsNullableDouble>
{
    internal IsNullableDouble(double? actual) : base(actual) { }
    [CustomAssertion] internal override FluentAssertions.Numeric.NullableNumericAssertions<double> Should() => _actual.Should();
}