using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable DateTime
/// </summary>
public class IsNullableDateTime : Constraint<IsNullableDateTime, DateTime?>
{
    internal IsNullableDateTime(DateTime? actual) : base(actual) { }

    /// <summary>
    /// Asserts that the dateTime is null or not equal to the given value
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    public ContinueWith<IsNullableDateTime> Not(DateTime unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is not null
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotNull()
    {
        _actual.Should().NotBeNull();
        return new(new(_actual.Value));
    }

    /// <summary>
    /// Asserts that the dateTime is null
    /// </summary>
    public void Null() => _actual.Should().BeNull();
}