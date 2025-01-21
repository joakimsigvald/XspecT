using System.Runtime.CompilerServices;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided TimeSpan
/// </summary>
public record IsTimeSpan : Constraint<TimeSpan, IsTimeSpan>
{
    internal IsTimeSpan(TimeSpan actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the timeSpan is not equal to the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> Not(
        TimeSpan expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().NotBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is shorter than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> LessThan(
        TimeSpan expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().BeLessThan(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is longer than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> GreaterThan(
        TimeSpan expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().BeGreaterThan(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is equal to or longer than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotLessThan(
        TimeSpan expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().BeGreaterThanOrEqualTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is equal to or shorter than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotGreaterThan(
        TimeSpan expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().BeLessThanOrEqualTo(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> CloseTo(
        TimeSpan expected, TimeSpan precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().BeCloseTo(expected, precision), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is not within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotCloseTo(
        TimeSpan expected, TimeSpan precision, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().NotBeCloseTo(expected, precision), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is less than zero
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> Negative()
    {
        Assert(() => Actual.Should().BeNegative());
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is greater than zero
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> Positive()
    {
        Assert(() => Actual.Should().BePositive());
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is zero or greater than zero
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotNegative()
    {
        Assert(() => Actual.Should().BeGreaterThanOrEqualTo(TimeSpan.Zero));
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is zero or less than zero
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotPositive()
    {
        Assert(() => Actual.Should().BeLessThanOrEqualTo(TimeSpan.Zero));
        return And();
    }
}