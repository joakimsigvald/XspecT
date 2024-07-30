using FluentAssertions;
using System.Linq.Expressions;
using XspecT.Assert;
using XspecT.Assert.Numerical;
using XspecT.Assert.Time;

namespace XspecT;

/// <summary>
/// Fluent assertions with verbs Is, Has and Does
/// </summary>
public static class AssertionExtensions
{
    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsByte Is(this byte actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsSByte Is(this sbyte actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsShort Is(this short actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsUShort Is(this ushort actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsInt Is(this int actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsUInt Is(this uint actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsLong Is(this long actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsULong Is(this ulong actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsFloat Is(this float actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsDouble Is(this double actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsDecimal Is(this decimal actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableByte Is(this byte? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableSByte Is(this sbyte? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableShort Is(this short? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableUShort Is(this ushort? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableInt Is(this int? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableUInt Is(this uint? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableLong Is(this long? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableULong Is(this ulong? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableFloat Is(this float? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableDouble Is(this double? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsNullableDecimal Is(this decimal? actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsBool Is(this bool actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsDateTime Is(this DateTime actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsTimeSpan Is(this TimeSpan actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given string
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsString Is(this string actual) => new(actual);

    /// <summary>
    /// Get available assertions for the characteristics of the given string
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static DoesString Does(this string actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given object
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="callerExpr"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsObject Is(this object actual, [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string callerExpr = null)
        => new(actual, callerExpr);

    /// <summary>
    /// Get available assertions for the given enumerable
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static IsEnumerable<TItem> Is<TItem>(this IEnumerable<TItem> actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given comparable
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static IsComparable<TValue> Is<TValue>(this TValue actual)
        where TValue : IComparable<TValue>
        => new(actual);

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableByte> Is(this byte? actual, byte? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableSByte> Is(this sbyte? actual, sbyte? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableUShort> Is(this ushort? actual, ushort? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableShort> Is(this short? actual, short? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableUInt> Is(this uint? actual, uint? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableInt> Is(this int? actual, int? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableULong> Is(this ulong? actual, ulong? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableLong> Is(this long? actual, long? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableFloat> Is(this float? actual, float? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableDouble> Is(this double? actual, double? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected and return continuation for further assertions of the value
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsNullableDecimal> Is(this decimal? actual, decimal? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected timeSpan and return continuation for further assertions of the value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static ContinueWith<IsNullableTimeSpan> Is(this TimeSpan? actual, TimeSpan? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected timeSpan and return continuation for further assertions of the value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static ContinueWith<IsTimeSpan> Is(this TimeSpan? actual, TimeSpan expected)
    {
        actual.Should().Be(expected);
        return new(new(actual.Value));
    }

    /// <summary>
    /// Verify that actual is expected dateTime and return continuation for further assertions of the value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static ContinueWith<IsNullableDateTime> Is(this DateTime? actual, DateTime? expected)
    {
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected dateTime and return continuation for further assertions of the value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static ContinueWith<IsDateTime> Is(this DateTime? actual, DateTime expected)
    {
        actual.Should().Be(expected);
        return new(new(actual.Value));
    }

    /// <summary>
    /// Verify that actual object is same reference as expected and return continuation for further assertions of the object
    /// </summary>
    public static ContinueWith<IsObject> Is(
        this object actual, 
        object expected,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Specification.AddAssert(
            [CustomAssertion] () => actual.Should().BeSameAs(expected),
            actualExpr,
            expectedExpr);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual object satisfy a given predicate
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsObject> Match<TValue>(this TValue actual, Expression<Func<TValue, bool>> predicate)
    {
        actual.Should().Match(predicate);
        return new(new(actual));
    }

    /// <summary>
    /// Get available assertions for enumerable
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion] public static HasEnumerable<TItem> Has<TItem>(this IEnumerable<TItem> actual) => new(actual);

    /// <summary>
    /// Verify that actual struct is same as expected and return continuation for further assertions of the struct
    /// </summary>
    [CustomAssertion]
    public static ContinueWith<IsObject> Is<TValue>(
        this TValue actual, 
        TValue expected,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        where TValue : struct
    {
        Specification.AddAssert([CustomAssertion] () => actual.Should().Be(expected), actualExpr, expectedExpr);
        actual.Should().Be(expected);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual string is same as expected and return continuation for further assertions of the string
    /// </summary>
    public static ContinueWith<IsString> Is(
        this string actual, 
        string expected,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [System.Runtime.CompilerServices.CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Specification.AddAssert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(new(actual));
    }

    /// <summary>
    /// Provide actual of any type to continue the chain of assertions on the new value
    /// </summary>
    /// <typeparam name="TActual"></typeparam>
    /// <typeparam name="TContinuation"></typeparam>
    /// <param name="_"></param>
    /// <param name="actual"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static TActual And<TActual, TContinuation>(this ContinueWith<TContinuation> _, TActual actual)
    {
        Specification.AddAnd();
        return actual;
    }
}