namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable short
/// </summary>
public record IsNullableShort : IsNullableNumerical<short, IsNullableShort, IsShort>
{
    internal IsNullableShort(short? actual, string actualExpr = null) : base(actual, actualExpr) { }
    private protected override IsShort ValueContinuation => new(Actual.Value, ActualExpr);
}