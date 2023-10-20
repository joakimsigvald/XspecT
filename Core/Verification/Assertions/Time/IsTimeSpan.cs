using FluentAssertions;

namespace XspecT.Verification.Assertions.Time;

public class IsTimeSpan : Constraint<IsTimeSpan, TimeSpan>
{
    public IsTimeSpan(TimeSpan actual) : base(actual) { }

    public ContinueWith<IsTimeSpan> Not(TimeSpan unexpected)
    {
        Actual.Should().NotBe(unexpected);
        return And();
    }

    public ContinueWith<IsTimeSpan> LessThan(TimeSpan expected)
    {
        Actual.Should().BeLessThan(expected);
        return And();
    }

    public ContinueWith<IsTimeSpan> GreaterThan(TimeSpan expected)
    {
        Actual.Should().BeGreaterThan(expected);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotLessThan(TimeSpan expected)
    {
        Actual.Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotGreaterThan(TimeSpan expected)
    {
        Actual.Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    public ContinueWith<IsTimeSpan> CloseTo(TimeSpan expected, TimeSpan precision)
    {
        Actual.Should().BeCloseTo(expected, precision);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotCloseTo(TimeSpan expected, TimeSpan precision)
    {
        Actual.Should().NotBeCloseTo(expected, precision);
        return And();
    }

    public ContinueWith<IsTimeSpan> Negative()
    {
        Actual.Should().BeNegative();
        return And();
    }

    public ContinueWith<IsTimeSpan> Positive()
    {
        Actual.Should().BePositive();
        return And();
    }

    public ContinueWith<IsTimeSpan> NotNegative()
    {
        Actual.Should().BeGreaterThanOrEqualTo(TimeSpan.Zero);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotPositive()
    {
        Actual.Should().BeLessThanOrEqualTo(TimeSpan.Zero);
        return And();
    }
}