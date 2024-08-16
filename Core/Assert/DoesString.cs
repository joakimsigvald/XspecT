using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public record DoesString : Constraint<DoesStringContinuation, string>
{
    internal DoesString(string actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the string contains the expected string
    /// </summary>
    public ContinueWith<DoesStringContinuation> Contain(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().Contain(expected), expectedExpr, "contains");
        return And();
    }

    /// <summary>
    /// Asserts that the string does not contain the expected string
    /// </summary>
    public ContinueWith<DoesStringContinuation> NotContain(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotContain(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the string starts with a prefix
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<DoesStringContinuation> StartWith(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().StartWith(expected), expectedExpr, "starts with");
        return And();
    }

    /// <summary>
    /// Asserts that the string ends with a suffix
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<DoesStringContinuation> EndWith(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().EndWith(expected), expectedExpr, "ends with");
        return And();
    }

    internal override DoesStringContinuation Continue() => new(Actual);
}