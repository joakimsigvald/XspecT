using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public record IsString : Constraint<IsStringContinuation, string>
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
        AddAssert([CustomAssertion] () => Actual.Should().BeEquivalentTo(expected), expectedExpr);
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
        AddAssert([CustomAssertion] () => Actual.Should().NotBeEquivalentTo(expected), expectedExpr);
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
        AddAssert([CustomAssertion] () => Actual.Should().BeEquivalentTo(expected), expectedExpr);
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
        AddAssert([CustomAssertion] () => Actual.Should().NotBeEquivalentTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the string is null
    /// </summary>
    public ContinueWith<IsStringContinuation> Null()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeNull());
        return And();
    }

    /// <summary>
    /// Asserts that the string is empty
    /// </summary>
    public ContinueWith<IsStringContinuation> Empty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeEmpty());
        return And();
    }

    /// <summary>
    /// Asserts that the string is not empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEmpty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeEmpty());
        return And();
    }

    /// <summary>
    /// Asserts that the string is not null
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNull()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeNull());
        return And();
    }

    /// <summary>
    /// Asserts that the string is null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrEmpty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeNullOrEmpty());
        return And();
    }

    /// <summary>
    /// Asserts that the string is not null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrEmpty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeNullOrEmpty());
        return And();
    }

    /// <summary>
    /// Asserts that the string does not contain non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrWhitespace()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeNullOrWhiteSpace());
        return And();
    }

    /// <summary>
    /// Asserts that the string contains non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrWhitespace()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeNullOrWhiteSpace());
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
        AddAssert([CustomAssertion] () => Actual.Should().NotBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the string starts with a prefix
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    [Obsolete("Use Does.StartWith instead")]
    public ContinueWith<IsStringContinuation> StartingWith(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().StartWith(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the string ends with a suffix
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    [Obsolete("Use Does.EndWith instead")]
    public ContinueWith<IsStringContinuation> EndingWith(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().EndWith(expected), expectedExpr);
        return And();
    }

    internal override IsStringContinuation Continue() => new(Actual);
}