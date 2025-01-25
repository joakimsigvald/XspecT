namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable double
/// </summary>
public record IsNullableDouble : IsNullableNumerical<double, IsNullableDouble, IsDouble>
{
    internal IsNullableDouble(double? actual, string actualExpr = null) : base(actual, actualExpr) { }
    private protected override IsDouble ValueContinuation => new(Actual.Value, ActualExpr);
}