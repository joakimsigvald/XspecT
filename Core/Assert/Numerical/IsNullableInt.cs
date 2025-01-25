namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the nullable int
/// </summary>
public record IsNullableInt : IsNullableNumerical<int, IsNullableInt, IsInt>
{
    internal IsNullableInt(int? actual, string actualExpr = null) : base(actual, actualExpr) { }
    private protected override IsInt ValueContinuation => new(Actual.Value, ActualExpr);
}