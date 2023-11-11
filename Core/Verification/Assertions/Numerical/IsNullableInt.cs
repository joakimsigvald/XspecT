using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableInt : IsNullableNumerical<int, IsNullableInt>
{
    internal IsNullableInt(int? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<int> Should() => _actual.Should();
}