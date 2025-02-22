using System.Runtime.CompilerServices;
using XspecT.Assert.Continuations;
using static Xunit.Assert;

namespace XspecT.Assert.Continuations.String;

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
        => Assert(() => Equal(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), expectedExpr).And();

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotLike(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => NotEqual(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), expectedExpr).And();

    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> EquivalentTo(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => Equal(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), expectedExpr).And();

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEquivalentTo(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => NotEqual(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), expectedExpr).And();

    /// <summary>
    /// Asserts that the string is null
    /// </summary>
    public ContinueWith<IsStringContinuation> Null()
        => Assert(() => Xunit.Assert.Null(Actual)).And();

    /// <summary>
    /// Asserts that the string is empty
    /// </summary>
    public ContinueWith<IsStringContinuation> Empty()
        => Assert(() => Xunit.Assert.Empty(Actual)).And();

    /// <summary>
    /// Asserts that the string is not empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEmpty()
        => Assert(() => Xunit.Assert.NotEmpty(Actual)).And();

    /// <summary>
    /// Asserts that the string is not null
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNull()
        => Assert(() => Xunit.Assert.NotNull(Actual)).And();

    /// <summary>
    /// Asserts that the string is null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrEmpty()
        => Assert(() => Xunit.Assert.Empty(Actual ?? string.Empty)).And();

    /// <summary>
    /// Asserts that the string is not null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrEmpty()
        => Assert(() => Xunit.Assert.NotEmpty(Actual ?? string.Empty)).And();

    /// <summary>
    /// Asserts that the string does not contain non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrWhitespace()
        => Assert(() => Xunit.Assert.Empty((Actual ?? string.Empty).Trim())).And();

    /// <summary>
    /// Asserts that the string contains non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrWhitespace()
        => Assert(() => Xunit.Assert.NotEmpty((Actual ?? string.Empty).Trim())).And();

    /// <summary>
    /// Asserts that the string is not the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsStringContinuation> Not(
        string expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert(() => NotEqual(Actual, expected), expectedExpr).And();

    internal override ContinueWith<IsStringContinuation> Value(
        string expected, string expectedExpr)
        => Assert(() => Equal(expected, Actual), expectedExpr, methodName: "").And();

    internal override IsStringContinuation Continue() => Create(Actual);
}