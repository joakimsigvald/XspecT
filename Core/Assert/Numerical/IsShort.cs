namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided short
/// </summary>
public record IsShort : IsNumerical<IsShort, short>
{
    internal IsShort(short actual, string actualExpr = null) : base(actual, actualExpr) { }
}