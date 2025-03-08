using System.Runtime.CompilerServices;
using XspecT.Assert.Continuations;
using XspecT.Assert.Continuations.Enumerable;
using XspecT.Internal.Specification;

namespace XspecT.Assert;

/// <summary>
/// Fluent assertions with verbs Is, Has and Does
/// </summary>
public static class AssertionExtensions
{
    /// <summary>
    /// Verify that actual object is same reference as expected and return continuation for further assertions of the object
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <param name="actualExpr"></param>
    /// <param name="expectedExpr"></param>
    /// <returns>Continuation for further assertions of the object</returns>
    public static ContinueWith<IsNullableStruct<TValue>> Is<TValue>(
        this TValue? actual,
        TValue? expected,
        [CallerArgumentExpression(nameof(actual))] string? actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string? expectedExpr = null)
        where TValue : struct
        => actual.Is(actualExpr: actualExpr).Value(expected, expectedExpr!);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsBool Is(
        this bool actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string? actualExpr = null)
        => IsBool.Create(actual, actualExpr!);

    /// <summary>
    /// Get available assertions for the given object
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableStruct<TValue> Is<TValue>
        (this TValue? actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string? actualExpr = null)
        where TValue : struct
        => IsNullableStruct<TValue>.Create(actual, actualExpr!);

    /// <summary>
    /// Get available assertions for the given enumerable
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsEnumerable<TItem> Is<TItem>(
        this IEnumerable<TItem> actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string? actualExpr = null)
        => IsEnumerable<TItem>.Create(actual, actualExpr!);

    /// <summary>
    /// Get available assertions for the given enumerable
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="actual"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static DoesEnumerable<TItem> Does<TItem>(
        this IEnumerable<TItem> actual,
        [CallerArgumentExpression(nameof(actual))] string? actualExpr = null)
        => DoesEnumerable<TItem>.Create(actual, actualExpr!);

    /// <summary>
    /// Get available assertions for the given comparable
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsComparable<TValue> Is<TValue>(
        this TValue actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string? actualExpr = null)
        where TValue : IComparable<TValue>
        => IsComparable<TValue>.Create(actual, actualExpr!);

    /// <summary>
    /// Get available assertions for enumerable
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static HasEnumerable<TItem> Has<TItem>(
        this IEnumerable<TItem> actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string? actualExpr = null)
        => HasEnumerable<TItem>.Create(actual, actualExpr!);

    /// <summary>
    /// Verify that the value satisfies a given condition
    /// </summary>
    /// <typeparam name="TActual"></typeparam>
    /// <param name="actual"></param>
    /// <param name="condition"></param>
    /// <param name="actualExpr"></param>
    /// <param name="conditionExpr"></param>
    /// <returns></returns>
    public static ContinueWithActual<TActual> Satisfies<TActual>(
        this TActual actual, Func<TActual, bool> condition,
        [CallerArgumentExpression(nameof(actual))] string? actualExpr = null,
        [CallerArgumentExpression(nameof(condition))] string? conditionExpr = null)
    {
        DoesValue<TActual>.Create(actual, actualExpr!).Satisfy(condition, conditionExpr!);
        return new(actual);
    }

    /// <summary>
    /// Provide actual of any type to continue the chain of assertions on the new value
    /// </summary>
    /// <typeparam name="TActual"></typeparam>
    /// <typeparam name="TContinuation"></typeparam>
    /// <param name="_"></param>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static TActual And<TActual, TContinuation>(
        this ContinueWith<TContinuation> _,
        TActual actual)
        where TContinuation : Constraint
    {
        SpecificationGenerator.AddThen();
        return actual;
    }
}