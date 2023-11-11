using FluentAssertions;

namespace XspecT.Verification.Assertions.Time;

public class IsTimeSpan : Constraint<IsTimeSpan, TimeSpan>
{
    internal IsTimeSpan(TimeSpan actual) : base(actual) { }

    public ContinueWith<IsTimeSpan> Not(TimeSpan unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    public ContinueWith<IsTimeSpan> LessThan(TimeSpan expected)
    {
        _actual.Should().BeLessThan(expected);
        return And();
    }

    public ContinueWith<IsTimeSpan> GreaterThan(TimeSpan expected)
    {
        _actual.Should().BeGreaterThan(expected);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotLessThan(TimeSpan expected)
    {
        _actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotGreaterThan(TimeSpan expected)
    {
        _actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    public ContinueWith<IsTimeSpan> CloseTo(TimeSpan expected, TimeSpan precision)
    {
        _actual.Should().BeCloseTo(expected, precision);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotCloseTo(TimeSpan expected, TimeSpan precision)
    {
        _actual.Should().NotBeCloseTo(expected, precision);
        return And();
    }

    public ContinueWith<IsTimeSpan> Negative()
    {
        _actual.Should().BeNegative();
        return And();
    }

    public ContinueWith<IsTimeSpan> Positive()
    {
        _actual.Should().BePositive();
        return And();
    }

    public ContinueWith<IsTimeSpan> NotNegative()
    {
        _actual.Should().BeGreaterThanOrEqualTo(TimeSpan.Zero);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotPositive()
    {
        _actual.Should().BeLessThanOrEqualTo(TimeSpan.Zero);
        return And();
    }
}