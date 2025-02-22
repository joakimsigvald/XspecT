using System.Runtime.CompilerServices;
using XspecT.Assert.Continuations;
using XspecT.Internal.Specification;

namespace XspecT.Assert;

/// <summary>
/// Fluent assertions with verbs Is, Has and Does
/// </summary>
public static class AssertionExtensionsObject
{
    /// <summary>
    /// Verify that actual object is same reference as expected and return continuation for further assertions of the object
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <param name="actualExpr"></param>
    /// <param name="expectedExpr"></param>
    /// <returns>Continuation for further assertions of the object</returns>
    public static ContinueWith<IsObject> Is(
        this object actual,
        object expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => actual.Is(actualExpr: actualExpr).Value(expected, expectedExpr);

    /// <summary>
    /// Verify that actual object satisfy a given predicate
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="actual"></param>
    /// <param name="predicate"></param>
    /// <param name="actualExpr"></param>
    /// <param name="predicateExpr"></param>
    /// <returns>Continuation for further assertions of the object</returns>
    public static ContinueWith<IsObject> Match<TValue>(
        this TValue actual, Func<TValue, bool> predicate,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
    {
        Assert(() => Xunit.Assert.True(predicate(actual)), actualExpr, predicateExpr);
        return new(IsObject.Create(actual));
    }

    /// <summary>
    /// Verify that actual struct is same as expected
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <param name="actualExpr"></param>
    /// <param name="expectedExpr"></param>
    /// <returns>Continuation for further assertions of the struct</returns>
    public static ContinueWith<IsObject> Is<TValue>(
        this TValue actual,
        TValue expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        where TValue : struct
        => actual.Is(actualExpr: actualExpr).Value(expected, expectedExpr);

    private static void Assert(
        Action assert, string actual = null, string expected = null, [CallerMemberName] string verb = "")
        => SpecificationGenerator.Assert(assert, actual.ParseActual(), expected, verb);

    /// <summary>
    /// Get available assertions for the given object
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsObject Is(
        this object actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => IsObject.Create(actual, actualExpr);
}