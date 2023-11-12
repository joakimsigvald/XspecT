using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// TODO
/// </summary>
public class IsTimeSpan : Constraint<IsTimeSpan, TimeSpan>
{
    internal IsTimeSpan(TimeSpan actual) : base(actual) { }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> Not(TimeSpan unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> LessThan(TimeSpan expected)
    {
        _actual.Should().BeLessThan(expected);
        return And();
    }


    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> GreaterThan(TimeSpan expected)
    {
        _actual.Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotLessThan(TimeSpan expected)
    {
        _actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotGreaterThan(TimeSpan expected)
    {
        _actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// TODO
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
    /// TODO
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
    /// TODO
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> Negative()
    {
        _actual.Should().BeNegative();
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> Positive()
    {
        _actual.Should().BePositive();
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotNegative()
    {
        _actual.Should().BeGreaterThanOrEqualTo(TimeSpan.Zero);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotPositive()
    {
        _actual.Should().BeLessThanOrEqualTo(TimeSpan.Zero);
        return And();
    }
}