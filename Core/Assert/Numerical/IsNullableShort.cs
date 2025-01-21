namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable short
/// </summary>
public record IsNullableShort : IsNullableNumerical<short, IsNullableShort>
{
    internal IsNullableShort(short? actual, string actualExpr = null) : base(actual, actualExpr) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<short> Should() => Actual.Should();
}