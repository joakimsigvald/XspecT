namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided bool
/// </summary>
public record IsBool : Constraint<bool, IsBool>
{
    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    public ContinueWith<IsBool> True()
        => Assert(() => Xunit.Assert.True(Actual)).And();

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    public ContinueWith<IsBool> False()
        => Assert(() => Xunit.Assert.False(Actual)).And();
}