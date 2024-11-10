using FluentAssertions;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Assert.Time;
using XspecT.Internal.Specification;

namespace XspecT.Assert;

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
    /// <param name="actualExpr"></param>
    /// <param name="expectedExpr"></param>
    /// <returns>Continuation for further assertions of the TimeSpan?</returns>
    public static ContinueWith<IsNullableTimeSpan> Is(
        this TimeSpan? actual, TimeSpan? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        SpecificationGenerator.Assert(
            [CustomAssertion] () => actual.Should().Be(expected),
            actualExpr,
            expectedExpr);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected timeSpan and return continuation for further assertions of the value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <param name="actualExpr"></param>
    /// <param name="expectedExpr"></param>
    /// <returns>Continuation for further assertions of the TimeSpan?</returns>
    public static ContinueWith<IsTimeSpan> Is(
        this TimeSpan? actual, TimeSpan expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        SpecificationGenerator.Assert(
            [CustomAssertion] () => actual.Should().Be(expected),
            actualExpr,
            expectedExpr);
        return new(new(actual.Value));
    }

    /// <summary>
    /// Verify that actual is expected dateTime and return continuation for further assertions of the value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <param name="actualExpr"></param>
    /// <param name="expectedExpr"></param>
    /// <returns>Continuation for further assertions of the DateTime?</returns>
    public static ContinueWith<IsNullableDateTime> Is(
        this DateTime? actual, DateTime? expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        SpecificationGenerator.Assert(
            [CustomAssertion] () => actual.Should().Be(expected),
            actualExpr,
            expectedExpr);
        return new(new(actual));
    }

    /// <summary>
    /// Verify that actual is expected dateTime and return continuation for further assertions of the value
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <param name="actualExpr"></param>
    /// <param name="expectedExpr"></param>
    /// <returns>Continuation for further assertions of the DateTime?</returns>
    public static ContinueWith<IsDateTime> Is(
        this DateTime? actual, DateTime expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        SpecificationGenerator.Assert(
            [CustomAssertion] () => actual.Should().Be(expected),
            actualExpr,
            expectedExpr);
        return new(new(actual.Value));
    }

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
    {
        SpecificationGenerator.Assert(
            [CustomAssertion] () => actual.Should().BeSameAs(expected),
            actualExpr,
            expectedExpr);
        return new(new(actual));
    }

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
        this TValue actual, Expression<Func<TValue, bool>> predicate,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(predicate))] string predicateExpr = null)
    {
        SpecificationGenerator.Assert(
            [CustomAssertion] () => actual.Should().Match(predicate),
            actualExpr,
            predicateExpr);
        return new(new(actual));
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
    {
        SpecificationGenerator.Assert([CustomAssertion] () => actual.Should().Be(expected), actualExpr, expectedExpr);
        return new(new(actual));
    }

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
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        where TValue : struct
    {
        if (actual is null || expected is null)
            SpecificationGenerator.Assert(
                [CustomAssertion] () => actual.Should().BeSameAs(expected),
                actualExpr,
                expectedExpr);
        else
            SpecificationGenerator.Assert(
                [CustomAssertion] () => actual.Value.Should().Be(expected.Value),
                actualExpr,
                expectedExpr);
        return new(new(actual));
    }
    /// <summary>
    /// Verify that actual string is same as expected
    /// </summary>
    /// <param name="actual"></param>
    /// <param name="expected"></param>
    /// <param name="actualExpr"></param>
    /// <param name="expectedExpr"></param>
    /// <returns>Continuation for further assertions of the string</returns>
    public static ContinueWith<IsString> Is(
        this string actual,
        string expected,
        [CallerArgumentExpression(nameof(actual))] string actualExpr = null,
        [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        SpecificationGenerator.Assert(() => Xunit.Assert.Equal(expected, actual), actualExpr, expectedExpr);
        return new(new(actual));
    }
}