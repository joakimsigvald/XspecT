using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableULong : IsNullableNumerical<ulong, IsNullableULong>
{
    internal IsNullableULong(ulong? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<ulong> Should() => _actual.Should();
}