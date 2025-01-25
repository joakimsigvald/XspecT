namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable ulong
/// </summary>
public record IsNullableULong : IsNullableNumerical<ulong, IsNullableULong, IsULong>
{
    internal IsNullableULong(ulong? actual, string actualExpr = null) : base(actual, actualExpr) { }
    private protected override IsULong ValueContinuation => new(Actual.Value, ActualExpr);
}