namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable double
/// </summary>
public record IsNullableDouble : IsNullableNumerical<double, IsNullableDouble>
{
    internal IsNullableDouble(double? actual, string actualExpr = null) : base(actual, actualExpr) { }
}