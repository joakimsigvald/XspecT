using System.Runtime.CompilerServices;
using XspecT.Assert.Time;
using XspecT.Internal.Specification;

namespace XspecT.Assert;

/// <summary>
/// Fluent assertions with verbs Is, Has and Does
/// </summary>
public static class AssertionContinuationExtensions
{
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
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => IsBool.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsDateTime Is(
        this DateTime actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => IsDateTime.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableDateTime Is(
        this DateTime? actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => IsNullableDateTime.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableTimeSpan Is(
        this TimeSpan? actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => IsNullableTimeSpan.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsTimeSpan Is(
        this TimeSpan actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => IsTimeSpan.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given string
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsString Is(
        this string actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => IsString.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the characteristics of the given string
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static DoesString Does(
        this string actual,
        Ignore _ = default,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => DoesString.Create(actual, actualExpr);

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
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        where TValue : struct
        => IsNullableStruct<TValue>.Create(actual, actualExpr);

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
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => IsEnumerable<TItem>.Create(actual, actualExpr);

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
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        where TValue : IComparable<TValue>
        => IsComparable<TValue>.Create(actual, actualExpr);

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
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => HasEnumerable<TItem>.Create(actual, actualExpr);

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