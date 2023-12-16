using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable TimeSpan
/// </summary>
public class IsNullableTimeSpan : Constraint<IsNullableTimeSpan, TimeSpan?>
{
    internal IsNullableTimeSpan(TimeSpan? actual) : base(actual) { }

    /// <summary>
    /// Asserts that the timeSpan is null or not equal to the given value
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    public ContinueWith<IsNullableTimeSpan> Not(TimeSpan unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is not null
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotNull()
    {
        _actual.Should().NotBeNull();
        return new(new(_actual.Value));
    }

    /// <summary>
    /// Asserts that the timeSpan is null
    /// </summary>
    public void Null() => _actual.Should().BeNull();
}