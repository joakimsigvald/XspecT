namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided int
/// </summary>
public record IsInt : IsNumerical<int, IsInt>
{
    internal IsInt(int actual, string actualExpr = null) : base(actual, actualExpr) { }
}