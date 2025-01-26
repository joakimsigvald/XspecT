using System.Runtime.CompilerServices;

using static Xunit.Assert;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public record IsString : Constraint<string, IsStringContinuation>
{
    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> Like(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Equal(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), expectedExpr);

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotLike(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => NotEqual(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), expectedExpr);

    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> EquivalentTo(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Equal(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), expectedExpr);

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEquivalentTo(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => NotEqual(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), expectedExpr);

    /// <summary>
    /// Asserts that the string is null
    /// </summary>
    public ContinueWith<IsStringContinuation> Null()
        => AssertAnd(() => Xunit.Assert.Null(Actual));

    /// <summary>
    /// Asserts that the string is empty
    /// </summary>
    public ContinueWith<IsStringContinuation> Empty()
        => AssertAnd(() => Xunit.Assert.Empty(Actual));

    /// <summary>
    /// Asserts that the string is not empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEmpty()
        => AssertAnd(() => Xunit.Assert.NotEmpty(Actual));

    /// <summary>
    /// Asserts that the string is not null
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNull()
        => AssertAnd(() => Xunit.Assert.NotNull(Actual));

    /// <summary>
    /// Asserts that the string is null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrEmpty()
        => AssertAnd(() => Xunit.Assert.Empty(Actual ?? string.Empty));

    /// <summary>
    /// Asserts that the string is not null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrEmpty()
        => AssertAnd(() => Xunit.Assert.NotEmpty(Actual ?? string.Empty));

    /// <summary>
    /// Asserts that the string does not contain non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrWhitespace()
        => AssertAnd(() => Xunit.Assert.Empty((Actual ?? string.Empty).Trim()));

    /// <summary>
    /// Asserts that the string contains non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrWhitespace()
        => AssertAnd(() => Xunit.Assert.NotEmpty((Actual ?? string.Empty).Trim()));

    /// <summary>
    /// Asserts that the string is not the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsStringContinuation> Not(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => NotEqual(Actual, expected), expectedExpr);

    internal override IsStringContinuation Continue() => Create(Actual);
}