namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable sbyte
/// </summary>
public record IsNullableSByte : IsNullableNumerical<sbyte, IsNullableSByte>
{
    internal IsNullableSByte(sbyte? actual, string actualExpr = null) : base(actual, actualExpr) { }
}