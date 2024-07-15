using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the nullable int
/// </summary>
public class IsNullableInt : IsNullableNumerical<int, IsNullableInt>
{
    internal IsNullableInt(int? actual) : base(actual) { }
    [CustomAssertion] internal override FluentAssertions.Numeric.NullableNumericAssertions<int> Should() => _actual.Should();
}