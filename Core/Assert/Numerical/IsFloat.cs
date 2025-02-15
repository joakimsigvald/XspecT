namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided float
/// </summary>
public record IsFloat : IsFractional<float, IsFloat>
{
    private protected override void AssertEqual(float expected, float precision)
        => Xunit.Assert.Equal(expected, Actual, precision);

    private protected override void AssertNotEqual(float expected, float precision)
        => Xunit.Assert.NotEqual(expected, Actual, precision);
}