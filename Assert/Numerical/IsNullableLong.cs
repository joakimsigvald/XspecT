using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableLong : IsNullableNumerical<long, IsNullableLong>
{
    internal IsNullableLong(long? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<long> Should() => _actual.Should();
}