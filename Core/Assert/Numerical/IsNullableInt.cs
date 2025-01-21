namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the nullable int
/// </summary>
public record IsNullableInt : IsNullableNumerical<int, IsNullableInt>
{
    internal IsNullableInt(int? actual, string actualExpr = null) : base(actual, actualExpr) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<int> Should() => Actual.Should();
}