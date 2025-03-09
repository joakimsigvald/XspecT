namespace XspecT.Assert.Continuations.Numerical.Fractional;

/// <summary>
/// Object that allows an assertions to be made on the provided double
/// </summary>
public record IsDouble : IsFractional<double, IsDouble>
{
    private protected override void AssertEqual(double expected, double actual, double precision)
        => Xunit.Assert.Equal(expected, actual, precision);

    private protected override void AssertNotEqual(double expected, double actual, double precision)
        => Xunit.Assert.NotEqual(expected, actual, precision);
}