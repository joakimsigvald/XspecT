namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided byte
/// </summary>
public record IsByte : IsNumerical<byte, IsByte>
{
    internal IsByte(byte actual, string actualExpr = null) : base(actual, actualExpr) { }
}