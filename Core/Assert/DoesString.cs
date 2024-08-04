using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public class DoesString : Constraint<DoesString, string>
{
    internal DoesString(string actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the string contains the expected string
    /// </summary>
    [CustomAssertion]
    public ContinueWith<DoesString> Contain(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => _actual.Should().Contain(expected), "does contain", expectedExpr);
        _actual.Should().Contain(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the string does not contain the expected string
    /// </summary>
    [CustomAssertion]
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
    [CustomAssertion]
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
    [CustomAssertion]
    public ContinueWith<DoesString> EndWith(string suffix)
    {
        _actual.Should().EndWith(suffix);
        return And();
    }
}