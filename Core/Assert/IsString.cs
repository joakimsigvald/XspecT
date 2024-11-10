using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public record IsString : Constraint<string, IsStringContinuation>
{
    internal IsString(string actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> Like(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().BeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotLike(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().NotBeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> EquivalentTo(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().BeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEquivalentTo(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().NotBeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the string is null
    /// </summary>
    public ContinueWith<IsStringContinuation> Null()
    {
        Assert([CustomAssertion] () => Actual.Should().BeNull());
        return And();
    }

    /// <summary>
    /// Asserts that the string is empty
    /// </summary>
    public ContinueWith<IsStringContinuation> Empty()
    {
        Assert([CustomAssertion] () => Actual.Should().BeEmpty());
        return And();
    }

    /// <summary>
    /// Asserts that the string is not empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEmpty()
    {
        Assert([CustomAssertion] () => Actual.Should().NotBeEmpty());
        return And();
    }

    /// <summary>
    /// Asserts that the string is not null
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNull()
    {
        Assert([CustomAssertion] () => Actual.Should().NotBeNull());
        return And();
    }

    /// <summary>
    /// Asserts that the string is null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrEmpty()
    {
        Assert([CustomAssertion] () => Actual.Should().BeNullOrEmpty());
        return And();
    }

    /// <summary>
    /// Asserts that the string is not null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrEmpty()
    {
        Assert([CustomAssertion] () => Actual.Should().NotBeNullOrEmpty());
        return And();
    }

    /// <summary>
    /// Asserts that the string does not contain non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrWhitespace()
    {
        Assert([CustomAssertion] () => Actual.Should().BeNullOrWhiteSpace());
        return And();
    }

    /// <summary>
    /// Asserts that the string contains non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrWhitespace()
    {
        Assert([CustomAssertion] () => Actual.Should().NotBeNullOrWhiteSpace());
        return And();
    }

    /// <summary>
    /// Asserts that the string is not the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsStringContinuation> Not(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert([CustomAssertion] () => Actual.Should().NotBe(expected), expectedExpr);
        return And();
    }

    internal override IsStringContinuation Continue() => new(Actual);
}