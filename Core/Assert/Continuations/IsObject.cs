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
        => Assert(Describe(expected), () => NotSame(expected, Actual), expectedExpr!).And();

    /// <summary>
    /// Assert that the object is null
    /// </summary>
    public void Null() => Assert(Ignore.Me, () => Xunit.Assert.Null(Actual), string.Empty);

    /// <summary>
    /// Assert that the object is not null
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> NotNull()
        => Assert(Ignore.Me, () => Xunit.Assert.NotNull(Actual), string.Empty).And();

    /// <summary>
    /// Assert that the object is equal to the given object
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> EqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), () => Equal(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Assert that the object is not equal to the given object
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> NotEqualTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), () => NotEqual(expected, Actual), expectedExpr).And();

    /// <summary>
    /// Assert that the object is equivalent to the given object with respect to public fields and properties, but ignoring type
    /// (if it walks like a duck...)
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> Like(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), () => Equivalent(expected, Actual), expectedExpr!).And();

    /// <summary>
    /// Assert that the object is not equivalent to the given object with respect to public fields and properties, but ignoring type
    /// (if it doesn't walk like a duck...)
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> NotLike(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), () =>
        {
            try
            {
                Like(expected);
            }
            catch (Xunit.Sdk.XunitException)
            {
                return;
            }
            Fail();
        }, expectedExpr!).And();

    /// <summary>
    /// Assert that the object is equivalent to the given object with respect to public fields and properties, but ignoring type
    /// (if it walks like a duck...)
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> EquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), () => Equivalent(expected, Actual), expectedExpr!).And();

    /// <summary>
    /// Assert that the object is not equivalent to the given object with respect to public fields and properties, but ignoring type
    /// (if it doesn't walk like a duck...)
    /// </summary>
    /// <returns>A continuation for making additional asserts on the object</returns>
    public ContinueWith<IsObject> NotEquivalentTo(
        object expected,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        => Assert(Describe(expected), () =>
        {
            try
            {
                EquivalentTo(expected);
            }
            catch (Xunit.Sdk.XunitException)
            {
                return;
            }
            Fail();
        }, expectedExpr!).And();
}