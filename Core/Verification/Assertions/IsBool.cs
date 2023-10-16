using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsBool : Constraint<IsBool, bool>
{
    public IsBool(bool actual) : base(actual) { }

    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    public ContinueWith<IsBool> True()
    {
        Actual.Should().BeTrue();
        return And();
    }

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    public ContinueWith<IsBool> False()
    {
        Actual.Should().BeFalse();
        return And();
    }
}