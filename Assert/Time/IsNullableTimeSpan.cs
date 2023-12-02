using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable TimeSpan
/// </summary>
public class IsNullableTimeSpan : Constraint<IsNullableTimeSpan, TimeSpan?>
{
    internal IsNullableTimeSpan(TimeSpan? actual) : base(actual) { }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    public ContinueWith<IsNullableTimeSpan> Not(TimeSpan unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotNull()
    {
        _actual.Should().NotBeNull();
        return new(new(_actual.Value));
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void Null() => _actual.Should().BeNull();
}