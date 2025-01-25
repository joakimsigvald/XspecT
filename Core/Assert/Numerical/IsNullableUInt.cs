namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable uint
/// </summary>
public record IsNullableUInt : IsNullableNumerical<uint, IsNullableUInt, IsUInt>
{
    internal IsNullableUInt(uint? actual, string actualExpr = null) : base(actual, actualExpr) { }
    private protected override IsUInt ValueContinuation => new(Actual.Value, ActualExpr);
}