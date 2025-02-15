namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided decimal
/// </summary>
public record IsDecimal : IsFractional<decimal, IsDecimal>
{
    private protected override void AssertEqual(decimal expected, decimal precision) 
        => Xunit.Assert.True(Math.Abs(Actual - expected) <= precision);

    private protected override void AssertNotEqual(decimal expected, decimal precision) 
        => Xunit.Assert.False(Math.Abs(Actual - expected) <= precision);
}