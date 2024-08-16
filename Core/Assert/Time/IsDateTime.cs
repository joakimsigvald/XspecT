using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided DateTime
/// </summary>
public record IsDateTime : Constraint<IsDateTime, DateTime>
{
    internal IsDateTime(DateTime actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the dateTime is not equal to the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> Not(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> Before(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeBefore(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> After(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeAfter(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is at or after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotBefore(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeBefore(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is at or before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotAfter(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeAfter(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> CloseTo(
        DateTime expected, TimeSpan precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeCloseTo(expected, precision), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime differ more than the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotCloseTo(
        DateTime expected, TimeSpan precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeCloseTo(expected, precision), expectedExpr);
        return And();
    }
}