﻿using System.Runtime.CompilerServices;
using static Xunit.Assert;

namespace XspecT.Assert.Continuations.String;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public record IsString : StringConstraint<IsStringContinuation>
{
    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> Like(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(
            Describe(expected),
            () => Equal(Actual?.Trim().ToLower(), 
                expected?.Trim().ToLower()), 
            expectedExpr!).And();

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotLike(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(
            Describe(expected),
            () => NotEqual(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), 
            expectedExpr!).And();

    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> EquivalentTo(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(
            Describe(expected),
            () => Equal(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), 
            expectedExpr!).And();

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEquivalentTo(
        string expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(
            Describe(expected),
            () => NotEqual(Actual?.Trim().ToLower(), expected?.Trim().ToLower()), 
            expectedExpr!).And();

    /// <summary>
    /// Asserts that the string is null
    /// </summary>
    public ContinueWith<IsStringContinuation> Null()
        => Assert(Ignore.Me, () => Xunit.Assert.Null(Actual), string.Empty).And();

    /// <summary>
    /// Asserts that the string is empty
    /// </summary>
    public ContinueWith<IsStringContinuation> Empty()
        => Assert(Ignore.Me, NotNullAnd(Xunit.Assert.Empty), string.Empty).And();

    /// <summary>
    /// Asserts that the string is not empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEmpty()
        => Assert(Ignore.Me, NotNullAnd(Xunit.Assert.NotEmpty), string.Empty).And();

    /// <summary>
    /// Asserts that the string is not null
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNull()
        => Assert(Ignore.Me, () => Xunit.Assert.NotNull(Actual), string.Empty).And();

    /// <summary>
    /// Asserts that the string is null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrEmpty()
        => Assert(Ignore.Me, () => Xunit.Assert.Empty(Actual ?? string.Empty), string.Empty).And();

    /// <summary>
    /// Asserts that the string is not null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrEmpty()
        => Assert(Ignore.Me, () => Xunit.Assert.NotEmpty(Actual ?? string.Empty), string.Empty).And();

    /// <summary>
    /// Asserts that the string does not contain non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrWhitespace()
        => Assert(Ignore.Me, () => Xunit.Assert.Empty((Actual ?? string.Empty).Trim()), string.Empty).And();

    /// <summary>
    /// Asserts that the string contains non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrWhitespace()
        => Assert(Ignore.Me, () => Xunit.Assert.NotEmpty((Actual ?? string.Empty).Trim()), string.Empty).And();

    /// <summary>
    /// Asserts that the string is not the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsStringContinuation> Not(
        string expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(
            Describe(expected),
            () => NotEqual(Actual, expected), 
            expectedExpr!).And();

    internal override ContinueWith<IsStringContinuation> Value(
        string expected, string expectedExpr)
        => Assert(() => Equal(expected, Actual), new() { Expected = expectedExpr, MethodName = string.Empty }).And();

    internal override IsStringContinuation Continue() => Create(Actual, ActualExpr);
}