using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided TimeSpan
/// </summary>
public record IsTimeSpan : Constraint<IsTimeSpan, TimeSpan>
{
    internal IsTimeSpan(TimeSpan actual) : base(actual, "") { }

    /// <summary>
    /// Asserts that the timeSpan is not equal to the given value
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> Not(TimeSpan unexpected)
    {
        Actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is shorter than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> LessThan(TimeSpan expected)
    {
        Actual.Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is longer than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> GreaterThan(TimeSpan expected)
    {
        Actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is equal to or longer than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> NotLessThan(TimeSpan expected)
    {
        Actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is equal to or shorter than the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> NotGreaterThan(TimeSpan expected)
    {
        Actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> CloseTo(TimeSpan expected, TimeSpan precision)
    {
        Actual.Should().BeCloseTo(expected, precision);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is not within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> NotCloseTo(TimeSpan expected, TimeSpan precision)
    {
        Actual.Should().NotBeCloseTo(expected, precision);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is less than zero
    /// </summary>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> Negative()
    {
        Actual.Should().BeNegative();
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is greater than zero
    /// </summary>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> Positive()
    {
        Actual.Should().BePositive();
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is zero or greater than zero
    /// </summary>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> NotNegative()
    {
        Actual.Should().BeGreaterThanOrEqualTo(TimeSpan.Zero);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is zero or less than zero
    /// </summary>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> NotPositive()
    {
        Actual.Should().BeLessThanOrEqualTo(TimeSpan.Zero);
        return And();
    }
}