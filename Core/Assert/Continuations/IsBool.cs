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
        => Assert(Ignore.Me, Xunit.Assert.True).And();

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    public ContinueWith<IsBool> False()
        => Assert(Ignore.Me, Xunit.Assert.False).And();

    private protected override string Describe(bool value) => $"{value}".ToLower();
}