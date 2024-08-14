using System.Runtime.CompilerServices;
using XspecT.Assert.Numerical;

namespace XspecT;

/// <summary>
/// Fluent assertions with verbs Is, Has and Does
/// </summary>
public static class AssertionContinuationExtensionsNumerical
{
    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsByte Is(
        this byte actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsSByte Is(
        this sbyte actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsShort Is(
        this short actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsUShort Is(
        this ushort actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsInt Is(
        this int actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null) 
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsUInt Is(
        this uint actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static Assert.Numerical.IsLong Is(
        this long actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null) 
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsULong Is(
        this ulong actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsFloat Is(
        this float actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsDouble Is(
        this double actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsDecimal Is(
        this decimal actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableByte Is(
        this byte? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableSByte Is(
        this sbyte? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableShort Is(
        this short? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableUShort Is(
        this ushort? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableInt Is(
        this int? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableUInt Is(
        this uint? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableLong Is(
        this long? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableULong Is(
        this ulong? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableFloat Is(
        this float? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableDouble Is(
        this double? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableDecimal Is(
        this decimal? actual,
        Ignore _ = null,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);
}