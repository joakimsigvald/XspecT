namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided ulong
/// </summary>
public record IsULong : IsNumerical<IsULong, ulong>
{
    internal IsULong(ulong actual, string actualExpr = null) : base(actual, actualExpr) { }
}