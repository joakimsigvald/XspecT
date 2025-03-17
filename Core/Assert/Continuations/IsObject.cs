using System.Runtime.CompilerServices;
using static Xunit.Assert;

namespace XspecT.Assert.Continuations;

/// <summary>
/// Object that allows an assertions to be made on the provided object
/// </summary>
public record IsObject : Constraint<object, IsObject>
{
    /// <summary>
    /// Assert that the object is not same as the given object
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> Not(
        object expected, [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), actual => NotSame(expected, actual), expectedExpr!).And();

    /// <summary>
    /// Assert that the object is null
    /// </summary>
    public ContinueWith<IsObject> Null() => Assert(Ignore.Me, Xunit.Assert.Null, string.Empty).And();

    /// <summary>
    /// Assert that the object is not null
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    [Obsolete("Use Not().Null instead")]
    public ContinueWith<IsObject> NotNull()
        => Assert(Ignore.Me, Xunit.Assert.NotNull, string.Empty).And();

    /// <summary>
    /// Assert that the object is equal to the given object
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> EqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), actual => Equal(expected, actual), expectedExpr!).And();

    /// <summary>
    /// Assert that the object is not equal to the given object
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    [Obsolete("Use Not().EqualTo instead")]
    public ContinueWith<IsObject> NotEqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), actual => NotEqual(expected, actual), expectedExpr!).And();

    /// <summary>
    /// Assert that the object is equivalent to the given object with respect to public fields and properties, but ignoring type
    /// (if it walks like a duck...)
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> Like(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), actual => Equivalent(expected, actual), expectedExpr!).And();

    /// <summary>
    /// Assert that the object is not equivalent to the given object with respect to public fields and properties, but ignoring type
    /// (if it doesn't walk like a duck...)
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    [Obsolete("Use Not().Like instead")]
    public ContinueWith<IsObject> NotLike(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), AssertNot(_ => Like(expected)), expectedExpr!).And();

    /// <summary>
    /// Assert that the object is equivalent to the given object with respect to public fields and properties, but ignoring type
    /// (if it walks like a duck...)
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> EquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), actual => Equivalent(expected, actual), expectedExpr!).And();

    /// <summary>
    /// Assert that the object is not equivalent to the given object with respect to public fields and properties, but ignoring type
    /// (if it doesn't walk like a duck...)
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    [Obsolete("Use Not().EquivalentTo instead")]
    public ContinueWith<IsObject> NotEquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), AssertNot(_ => EquivalentTo(expected)), expectedExpr!).And();
}