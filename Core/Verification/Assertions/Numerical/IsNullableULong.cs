using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableULong : IsNullableNumerical<ulong, IsNullableULong>
{
    public IsNullableULong(ulong? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<ulong> Should() => Actual.Should();
}