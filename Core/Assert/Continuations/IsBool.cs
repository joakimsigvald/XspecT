namespace XspecT.Assert.Continuations;

/// <summary>
/// Object that allows an assertions to be made on the provided bool
/// </summary>
public record IsBool : Constraint<bool, IsBool>
{
    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    public ContinueWith<IsBool> True()
        => Assert(null, () => Xunit.Assert.True(Actual), null).And();

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    public ContinueWith<IsBool> False()
        => Assert(null, () => Xunit.Assert.False(Actual), null).And();

    private protected override string Describe(bool value) => $"{value}".ToLower();
}