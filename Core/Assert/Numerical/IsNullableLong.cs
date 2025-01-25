namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable long
/// </summary>
public record IsNullableLong : IsNullableNumerical<long, IsNullableLong>
{
    internal IsNullableLong(long? actual, string actualExpr = null) : base(actual, actualExpr) { }
}