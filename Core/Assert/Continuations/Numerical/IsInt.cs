

namespace XspecT.Assert.Continuations.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided int
/// </summary>
public record IsInt : IsNumerical<int, IsInt>
{
    /// <summary>
    /// Assert that the value is greater than expected
    /// </summary>
    public ContinueWith<IsInt> Even()
        => Assert(null, NotNullAnd(actual => Xunit.Assert.True(actual % 2 == 0)))
        .And();
}