using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// TODO
/// </summary>
public class IsBool : Constraint<IsBool, bool>
{
    internal IsBool(bool actual) : base(actual) { }

    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    public ContinueWith<IsBool> True()
    {
        _actual.Should().BeTrue();
        return And();
    }

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    public ContinueWith<IsBool> False()
    {
        _actual.Should().BeFalse();
        return And();
    }
}