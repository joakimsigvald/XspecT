using FluentAssertions;

namespace XspecT.Verification.Assertions.Time;

public class IsDateTime : Constraint<IsDateTime, DateTime>
{
    public IsDateTime(DateTime actual) : base(actual) { }

    public ContinueWith<IsDateTime> Not(DateTime unexpected)
    {
        Actual.Should().NotBe(unexpected);
        return And();
    }

    public ContinueWith<IsDateTime> Before(DateTime expected)
    {
        Actual.Should().BeBefore(expected);
        return And();
    }

    public ContinueWith<IsDateTime> After(DateTime expected)
    {
        Actual.Should().BeAfter(expected);
        return And();
    }

    public ContinueWith<IsDateTime> NotBefore(DateTime expected)
    {
        Actual.Should().NotBeBefore(expected);
        return And();
    }

    public ContinueWith<IsDateTime> NotAfter(DateTime expected)
    {
        Actual.Should().NotBeAfter(expected);
        return And();
    }

    public ContinueWith<IsDateTime> CloseTo(DateTime expected, TimeSpan precision)
    {
        Actual.Should().BeCloseTo(expected, precision);
        return And();
    }

    public ContinueWith<IsDateTime> NotCloseTo(DateTime expected, TimeSpan precision)
    {
        Actual.Should().NotBeCloseTo(expected, precision);
        return And();
    }
}