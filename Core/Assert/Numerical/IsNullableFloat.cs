namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable float
/// </summary>
public record IsNullableFloat : IsNullableNumerical<float, IsNullableFloat, IsFloat>
{
    internal IsNullableFloat(float? actual, string actualExpr = null) : base(actual, actualExpr) { }
    private protected override IsFloat ValueContinuation => new(Actual.Value, ActualExpr);
}