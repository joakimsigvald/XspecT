using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable TimeSpan
/// </summary>
public record IsNullableTimeSpan : Constraint<IsNullableTimeSpan, TimeSpan?>
{
    internal IsNullableTimeSpan(TimeSpan? actual) : base(actual, "") { }

    /// <summary>
    /// Asserts that the timeSpan is null or not equal to the given value
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsNullableTimeSpan> Not(TimeSpan unexpected)
    {
        Actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is not null
    /// </summary>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsTimeSpan> NotNull()
    {
        Actual.Should().NotBeNull();
        return new(new(Actual.Value));
    }

    /// <summary>
    /// Asserts that the timeSpan is null
    /// </summary>
    [CustomAssertion] public void Null() => Actual.Should().BeNull();
}