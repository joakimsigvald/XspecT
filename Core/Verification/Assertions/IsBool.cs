using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsBool : Constraint<IsBool>
{
    private readonly bool _actual;

    public IsBool(bool actual) => _actual = actual;
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