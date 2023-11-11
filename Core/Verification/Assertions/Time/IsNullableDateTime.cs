using FluentAssertions;

namespace XspecT.Verification.Assertions.Time;

public class IsNullableDateTime : Constraint<IsNullableDateTime, DateTime?>
{
    internal IsNullableDateTime(DateTime? actual) : base(actual) { }

    public ContinueWith<IsNullableDateTime> Not(DateTime unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    public ContinueWith<IsDateTime> NotNull()
    {
        _actual.Should().NotBeNull();
        return new(new(_actual.Value));
    }

    public void Null() => _actual.Should().BeNull();
}