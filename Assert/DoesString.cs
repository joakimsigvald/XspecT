using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public class DoesString : Constraint<DoesString, string>
{
    internal DoesString(string actual) : base(actual) { }

    /// <summary>
    /// Asserts that the string contains the expected string
    /// </summary>
    public ContinueWith<DoesString> Contain(string expected)
    {
        _actual.Should().Contain(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the string does not contain the expected string
    /// </summary>
    public ContinueWith<DoesString> NotContain(string other)
    {
        _actual.Should().NotContain(other);
        return And();
    }

    /// <summary>
    /// Asserts that the string starts with a prefix
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    public ContinueWith<DoesString> StartWith(string prefix)
    {
        _actual.Should().StartWith(prefix);
        return And();
    }

    /// <summary>
    /// Asserts that the string ends with a suffix
    /// </summary>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public ContinueWith<DoesString> EndWith(string suffix)
    {
        _actual.Should().EndWith(suffix);
        return And();
    }
}