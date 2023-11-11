using FluentAssertions;

namespace XspecT.Verification.Assertions.Time;

public class IsDateTime : Constraint<IsDateTime, DateTime>
{
    internal IsDateTime(DateTime actual) : base(actual) { }

    public ContinueWith<IsDateTime> Not(DateTime unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    public ContinueWith<IsDateTime> Before(DateTime expected)
    {
        _actual.Should().BeBefore(expected);
        return And();
    }

    public ContinueWith<IsDateTime> After(DateTime expected)
    {
        _actual.Should().BeAfter(expected);
        return And();
    }

    public ContinueWith<IsDateTime> NotBefore(DateTime expected)
    {
        _actual.Should().NotBeBefore(expected);
        return And();
    }

    public ContinueWith<IsDateTime> NotAfter(DateTime expected)
    {
        _actual.Should().NotBeAfter(expected);
        return And();
    }

    public ContinueWith<IsDateTime> CloseTo(DateTime expected, TimeSpan precision)
    {
        _actual.Should().BeCloseTo(expected, precision);
        return And();
    }

    public ContinueWith<IsDateTime> NotCloseTo(DateTime expected, TimeSpan precision)
    {
        _actual.Should().NotBeCloseTo(expected, precision);
        return And();
    }
}