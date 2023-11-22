using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// TODO
/// </summary>
public class DoesString : Constraint<DoesString, string>
{
    internal DoesString(string actual) : base(actual) { }

    /// <summary>
    /// TODO
    /// </summary>
    public ContinueWith<DoesString> Contain(string expected)
    {
        _actual.Should().Contain(expected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    public ContinueWith<DoesString> NotContain(string other)
    {
        _actual.Should().NotContain(other);
        return And();
    }
}