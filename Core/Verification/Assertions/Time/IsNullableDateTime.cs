using FluentAssertions;

namespace XspecT.Verification.Assertions.Time;

public class IsNullableDateTime : Constraint<IsNullableDateTime, DateTime?>
{
    public IsNullableDateTime(DateTime? actual) : base(actual) { }

    public ContinueWith<IsNullableDateTime> Not(DateTime unexpected)
    {
        Actual.Should().NotBe(unexpected);
        return And();
    }

    public ContinueWith<IsDateTime> NotNull()
    {
        Actual.Should().NotBeNull();
        return new(new(Actual.Value));
    }

    public void Null() => Actual.Should().BeNull();
}