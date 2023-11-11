using FluentAssertions;

namespace XspecT.Verification.Assertions.Numerical;

public class IsNullableShort : IsNullableNumerical<short, IsNullableShort>
{
    internal IsNullableShort(short? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<short> Should() => _actual.Should();
}