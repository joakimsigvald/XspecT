using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable long
/// </summary>
public class IsNullableLong : IsNullableNumerical<long, IsNullableLong>
{
    internal IsNullableLong(long? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<long> Should() => _actual.Should();
}