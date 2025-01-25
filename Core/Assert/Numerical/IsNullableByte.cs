namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable byte
/// </summary>
public record IsNullableByte : IsNullableNumerical<byte, IsNullableByte, IsByte>
{
    internal IsNullableByte(byte? actual, string actualExpr = null) : base(actual, actualExpr) { }

    private protected override IsByte ValueContinuation => new(Actual.Value, ActualExpr);
}