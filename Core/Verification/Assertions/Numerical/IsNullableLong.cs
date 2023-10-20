using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableLong : IsNullableNumerical<long, IsNullableLong>
{
    public IsNullableLong(long? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<long> Should() => Actual.Should();
}