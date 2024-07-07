using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided bool
/// </summary>
public class IsBool : Constraint<IsBool, bool>
{
    internal IsBool(bool actual) : base(actual) { }

    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsBool> True()
    {
        _actual.Should().BeTrue();
        return And();
    }

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsBool> False()
    {
        _actual.Should().BeFalse();
        return And();
    }
}