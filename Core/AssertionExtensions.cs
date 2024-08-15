using FluentAssertions;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Assert;
using XspecT.Assert.Time;

namespace XspecT;

/// <summary>
/// Fluent assertions with verbs Is, Has and Does
/// </summary>
public static class AssertionExtensions
{
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
    public static ContinueWith<IsTimeSpan> Is(
        this TimeSpan? actual, TimeSpan expected)
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
    public static ContinueWith<IsDateTime> Is(
        this DateTime? actual, DateTime expected)
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
        SpecificationGenerator.AddAssert(
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
    /// Verify that actual struct is same as expected and return continuation for further assertions of the struct
    /// </summary>
    public static ContinueWith<IsObject> Is<TValue>(
        this TValue actual, 
        TValue expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        where TValue : struct
    {
        SpecificationGenerator.AddAssert([CustomAssertion] () => actual.Should().Be(expected), actualExpr, expectedExpr);
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
        SpecificationGenerator.AddAssert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(new(actual));
    }
}