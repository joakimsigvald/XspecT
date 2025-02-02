using XspecT.Assert.Numerical;
using CallerArgument = System.Runtime.CompilerServices.CallerArgumentExpressionAttribute;

namespace XspecT.Assert;

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
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsByte.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsSByte Is(
        this sbyte actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsSByte.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsShort Is(
        this short actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsShort.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsUShort Is(
        this ushort actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsUShort.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsInt Is(
        this int actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsInt.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsUInt Is(
        this uint actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsUInt.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsLong Is(
        this long actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsLong.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsULong Is(
        this ulong actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsULong.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsFloat Is(
        this float actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsFloat.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsDouble Is(
        this double actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsDouble.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsDecimal Is(
        this decimal actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsDecimal.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableByte Is(
        this byte? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableByte.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableSByte Is(
        this sbyte? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableSByte.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableShort Is(
        this short? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableShort.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableUShort Is(
        this ushort? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableUShort.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableInt Is(
        this int? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableInt.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableUInt Is(
        this uint? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableUInt.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableLong Is(
        this long? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableLong.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableULong Is(
        this ulong? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableULong.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableFloat Is(
        this float? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableFloat.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableDouble Is(
        this double? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableDouble.Create(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="_"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsNullableDecimal Is(
        this decimal? actual,
        Ignore _ = default,
        [CallerArgument(nameof(actual))] string actualExpr = null)
        => IsNullableDecimal.Create(actual, actualExpr);
}