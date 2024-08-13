using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided bool
/// </summary>
public record IsBool : Constraint<IsBool, bool>
{
    internal IsBool(bool actual, string actualExpr = null) : base(actual, actualExpr, "is") { }

    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsBool> True()
    {
        Actual.Should().BeTrue();
        return And();
    }

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsBool> False()
    {
        Actual.Should().BeFalse();
        return And();
    }
}