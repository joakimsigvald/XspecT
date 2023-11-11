using FluentAssertions;

namespace XspecT.Verification.Assertions.Time;

public class IsNullableTimeSpan : Constraint<IsNullableTimeSpan, TimeSpan?>
{
    internal IsNullableTimeSpan(TimeSpan? actual) : base(actual) { }

    public ContinueWith<IsNullableTimeSpan> Not(TimeSpan unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotNull()
    {
        _actual.Should().NotBeNull();
        return new(new(_actual.Value));
    }

    public void Null() => _actual.Should().BeNull();
}