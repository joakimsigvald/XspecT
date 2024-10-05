namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided uint
/// </summary>
public record IsUInt : IsNumerical<uint, IsUInt>
{
    internal IsUInt(uint actual, string actualExpr = null) : base(actual, actualExpr) { }
}