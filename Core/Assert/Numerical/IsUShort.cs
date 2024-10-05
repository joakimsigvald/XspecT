namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided ushort
/// </summary>
public record IsUShort : IsNumerical<ushort, IsUShort>
{
    internal IsUShort(ushort actual, string actualExpr = null) : base(actual, actualExpr) { }
}