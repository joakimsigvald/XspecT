using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableInt : IsNullableNumerical<int, IsNullableInt>
{
    public IsNullableInt(int? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<int> Should() => Actual.Should();
}