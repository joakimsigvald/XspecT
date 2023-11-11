using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableLong : IsNullableNumerical<long, IsNullableLong>
{
    internal IsNullableLong(long? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<long> Should() => _actual.Should();
}