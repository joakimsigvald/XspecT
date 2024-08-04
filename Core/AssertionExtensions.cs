using FluentAssertions;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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
    public static IsBool Is(this bool actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="actualExpr"></param>
    /// <param name="ignore1">This argument is ignored (used to control method overload precedence)</param>
    /// <param name="ignore2">This argument is ignored (used to control method overload precedence)</param>
    /// <returns></returns>
    public static IsDateTime Is(
        this DateTime actual,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        string ignore1 = null, string ignore2 = null) 
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given value
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsTimeSpan Is(this TimeSpan actual) => new(actual);

    /// <summary>
    /// Get available assertions for the given string
    /// </summary>
    /// <param name="actual"></param>
    /// <returns></returns>
    public static IsString Is(this string actual) => new(actual);

    /// <summary>
    /// Get available assertions for the characteristics of the given string
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static DoesString Does(
        this string actual,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null) 
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given object
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsObject Is(
        this object actual, 
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given enumerable
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="actual"></param>
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    public static IsEnumerable<TItem> Is<TItem>(
        this IEnumerable<TItem> actual, 
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null)
        => new(actual, actualExpr);

    /// <summary>
    /// Get available assertions for the given comparable
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="actual"></param>
    /// <param name="actualExpr"></param>
    /// <param name="ignore1">This argument is ignored (used to control method overload precedence)</param>
    /// <param name="ignore2">This argument is ignored (used to control method overload precedence)</param>
    /// <returns></returns>
    public static IsComparable<TValue> Is<TValue>(
        this TValue actual,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        string ignore1 = null, string ignore2 = null)
        where TValue : IComparable<TValue>
        => new(actual);

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
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
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
    /// <param name="actualExpr"></param>
    /// <returns></returns>
    [CustomAssertion]
    public static HasEnumerable<TItem> Has<TItem>(
        this IEnumerable<TItem> actual,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null) 
        => new(actual, actualExpr);

    /// <summary>
    /// Verify that actual struct is same as expected and return continuation for further assertions of the struct
    /// </summary>
    public static ContinueWith<IsObject> Is<TValue>(
        this TValue actual, 
        TValue expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
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
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
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