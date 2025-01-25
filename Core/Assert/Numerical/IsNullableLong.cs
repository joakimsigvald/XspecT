namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable long
/// </summary>
public record IsNullableLong : IsNullableNumerical<long, IsNullableLong, IsLong>
{
    internal IsNullableLong(long? actual, string actualExpr = null) : base(actual, actualExpr) { }
    private protected override IsLong ValueContinuation => new(Actual.Value, ActualExpr);
}