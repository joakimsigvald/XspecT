using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public record DoesString : Constraint<DoesString, string>
{
    internal DoesString(string actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the string contains the expected string
    /// </summary>
    [CustomAssertion]
    public ContinueWith<DoesString> Contain(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().Contain(expected), "does contain", expectedExpr);
        Actual.Should().Contain(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the string does not contain the expected string
    /// </summary>
    [CustomAssertion]
    public ContinueWith<DoesString> NotContain(string other)
    {
        Actual.Should().NotContain(other);
        return And();
    }

    /// <summary>
    /// Asserts that the string starts with a prefix
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<DoesString> StartWith(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().StartWith(expected), "starts with", expectedExpr);
        Actual.Should().StartWith(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the string ends with a suffix
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<DoesString> EndWith(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().EndWith(expected), "ends with", expectedExpr);
        return And();
    }
}