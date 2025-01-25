namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable decimal
/// </summary>
public record IsNullableDecimal : IsNullableNumerical<decimal, IsNullableDecimal>
{
    internal IsNullableDecimal(decimal? actual, string actualExpr = null) : base(actual, actualExpr) { }
}