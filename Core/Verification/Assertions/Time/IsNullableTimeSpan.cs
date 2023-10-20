using FluentAssertions;

namespace XspecT.Verification.Assertions.Time;

public class IsNullableTimeSpan : Constraint<IsNullableTimeSpan, TimeSpan?>
{
    public IsNullableTimeSpan(TimeSpan? actual) : base(actual) { }

    public ContinueWith<IsNullableTimeSpan> Not(TimeSpan unexpected)
    {
        Actual.Should().NotBe(unexpected);
        return And();
    }

    public ContinueWith<IsTimeSpan> NotNull()
    {
        Actual.Should().NotBeNull();
        return new(new(Actual.Value));
    }

    public void Null() => Actual.Should().BeNull();
}