namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable ushort
/// </summary>
public record IsNullableUShort : IsNullableNumerical<ushort, IsNullableUShort, IsUShort>
{
    internal IsNullableUShort(ushort? actual, string actualExpr = null) : base(actual, actualExpr) { }
    private protected override IsUShort ValueContinuation => new(Actual.Value, ActualExpr);
}