using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided TimeSpan
/// </summary>
public class IsTimeSpan : Constraint<IsTimeSpan, TimeSpan>
{
    internal IsTimeSpan(TimeSpan actual) : base(actual) { }

    /// <summary>
    /// Asserts that the timeSpan is not equal to the given value
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> Not(TimeSpan unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is shorter than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> LessThan(TimeSpan expected)
    {
        _actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is longer than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> GreaterThan(TimeSpan expected)
    {
        _actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is equal to or longer than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotLessThan(TimeSpan expected)
    {
        _actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is equal to or shorter than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotGreaterThan(TimeSpan expected)
    {
        _actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> CloseTo(TimeSpan expected, TimeSpan precision)
    {
        _actual.Should().BeCloseTo(expected, precision);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is not within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotCloseTo(TimeSpan expected, TimeSpan precision)
    {
        _actual.Should().NotBeCloseTo(expected, precision);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is less than zero
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> Negative()
    {
        _actual.Should().BeNegative();
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is greater than zero
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> Positive()
    {
        _actual.Should().BePositive();
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is zero or greater than zero
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotNegative()
    {
        _actual.Should().BeGreaterThanOrEqualTo(TimeSpan.Zero);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is zero or less than zero
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotPositive()
    {
        _actual.Should().BeLessThanOrEqualTo(TimeSpan.Zero);
        return And();
    }
}