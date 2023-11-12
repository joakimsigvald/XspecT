using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableInt : IsNullableNumerical<int, IsNullableInt>
{
    internal IsNullableInt(int? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<int> Should() => _actual.Should();
}